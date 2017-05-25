#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using USMarcLibrary.ErrorHandling;

#endregion

namespace USMarcLibrary.Parsers
{
    /// <summary> Enumeration indicates the type of CHARACTER encoding within the MARC record </summary>
    public enum RecordCharacterEncoding : byte
    {
        /// <summary> Marc Character encoding </summary>
        Marc = 1,

        /// <summary> Unicode character encoding </summary>
        Unicode,

        /// <summary> Unrecognized character encoding value found  (treated as Unicode) </summary>
        Unrecognized
    }

    /// <summary> Enumeration indicates what action should be taken when errors are encountered during
    /// parsing a file </summary>
    public enum ActionOnErrorEncounteredEnum : byte
    {
        /// <summary> Throws an exception and stops processing a file immediately on the first error </summary>
        ThrowException = 1,

        /// <summary> [DEFAULT] Store the error in the record and return the record </summary>
        StoreInRecord
    }

    /// <summary> Parser steps through the records in a MARC21 Electronic Format file or stream. </summary>
    /// <remarks> Written by Mark Sullivan (2005) <br /><br />
    /// You can either pass in the stream or file to read into the constructor and immediately begin using Next() to step
    /// through them, or you can use the empty constructor and call the Parse methods for the first record. <br /><br />
    /// To  use the IEnumerable interface, you must pass in the Stream or filename in the constructor.</remarks>
    public class Marc21ExchangeFormatParser : IEnumerable<MarcRecord>, IEnumerator<MarcRecord>
    {
        // Stream used to read the Marc21 records
        private BinaryReader _reader;

        // Constants used when parsing the Marc21 stream
        private const char EndOfRecord = (char) 29;
        private const char RecordSeperator = (char) 30;
        private const char UnitSeperator = (char) 31;
        private const int AlternateCharacterSetIndicator = 27;

        /// <summary> Variable indicates what action should be taken when an error is encountered
        /// while parsing a MARC21 file </summary>
        public static ActionOnErrorEncounteredEnum ActionOnError =
            ActionOnErrorEncounteredEnum.StoreInRecord;

        #region Constructors 

        /// <summary> Constructor for a new instance of this class </summary>
        public Marc21ExchangeFormatParser()
        {
            // Constructor does nothing
        }

        /// <summary> Constructor for a new instance of this class </summary>
        /// <param name="marc21Stream"> Open stream from which to read Marc21 records </param>
        public Marc21ExchangeFormatParser(Stream marc21Stream)
        {
            // Create the new reader object
            _reader = new BinaryReader(marc21Stream);
        }

        /// <summary> Constructor for a new instance of this class </summary>
        /// <param name="marc21File"> Name of the file to parse </param>
        public Marc21ExchangeFormatParser(string marc21File)
        {
            // Create the new reader object
            _reader = new BinaryReader(File.Open(marc21File, FileMode.Open));
        }

        #endregion

        /// <summary> Begins parsing from a stream containing MARC21 Electronic format records. </summary>
        /// <param name="marc21Stream"> Open stream from which to read Marc21 records </param>
        /// <returns> A built record, or NULL if no records are contained within the file </returns>
        public MarcRecord Parse(Stream marc21Stream)
        {
            // Create the new reader object
            _reader = new BinaryReader(marc21Stream);

            // Return the first record
            return parse_next_record();
        }

        /// <summary> Begins parsing a new MARC21 Electronic format file. </summary>
        /// <param name="marc21File"> Name of the file to parse </param>
        /// <returns> A built record, or NULL if no records are contained within the file </returns>
        public MarcRecord Parse(string marc21File)
        {
            // Create the new reader object
            _reader = new BinaryReader(File.Open(marc21File, FileMode.Open));

            // Return the first record
            return parse_next_record();
        }


        /// <summary> Returns the next record in the MARC21 Electronic format file or stream </summary>
        /// <returns> Next object, or NULL </returns>
        public MarcRecord Next()
        {
            if (_reader != null)
                return parse_next_record();
            return null;
        }

        /// <summary> Close the stream reader used for this parsing </summary>
        public void Close()
        {
            try
            {
                if (_reader != null)
                {
                    _reader.Close();
                    _reader = null;
                }
            }
            catch
            {
                // ignored
            }
        }

        #region Method which actually parses the stream for the next record 

        /// <summary> Flag indicates if an end of file has been reached </summary>
        /// <remarks> Whenever the EOF is reached, the stream is closed automatically </remarks>
        public bool EofFlag { get; set; }

        private MarcRecord parse_next_record()
        {
            // Create the MARC record to return and subfield collection
            var thisRecord = new MarcRecord();

            var fieldDatas = new Dictionary<short, Marc21ParserVariableFieldData>();
            try
            {
                // Some values to check the end of the file
                long fileLength = _reader.BaseStream.Length;

                // Create the StringBuilder object for this record
                var leaderBuilder = new StringBuilder(30);

                // Read to first character
                int result = _reader.Read();
                bool eof = false;

                // Read the leader and directory directly into a string, since this will not have specially
                // coded characters ( leader and directory end with a RECORD_SEPERATOR )
                int count = 0;
                while ((!eof) && (result != EndOfRecord) && (result != RecordSeperator) && (count < 24))
                {
                    // Want to skip any special characters at the beginning (like encoding characters)
                    if (result < 127)
                    {
                        // Save this character directly
                        leaderBuilder.Append((char) result);
                        count++;
                    }

                    // Read the next character and increment the count
                    if (_reader.BaseStream.Position < fileLength)
                    {
                        result = _reader.ReadByte();
                    }
                    else
                    {
                        eof = true;
                    }
                }

                // If this is the empty string, then just return null (DONE!)
                if (eof)
                {
                    //set flag to indicate that the EOF has been reached
                    EofFlag = true;

                    // Close the reader
                    Close();

                    // return a null value to end file processing of the MARC file
                    return null;
                }

                // Ensure the leader was correctly retrieved
                if (leaderBuilder.Length < 24)
                {
                    throw new ApplicationException(
                        "Error reading leader.  Either end of file, group seperator, or record seperator found prematurely.");
                }

                // Save the leader into the record 
                thisRecord.Leader = leaderBuilder.ToString();

                // Verify the type of character encoding used here
                RecordCharacterEncoding encoding = RecordCharacterEncoding.Unrecognized;
                switch (thisRecord.Leader[9])
                {
                    case ' ':
                        encoding = RecordCharacterEncoding.Marc;
                        break;

                    case 'a':
                        encoding = RecordCharacterEncoding.Unicode;
                        break;
                }

                // Now, read in all the directory information
                var directoryEntries = new List<Marc21ParserDirectoryEntry>();
                count = 0;
                int tag = 0;
                int fieldLength = 0;
                int startingPosition = 0;
                while ((result != EndOfRecord) && (result != RecordSeperator))
                {
                    // Set the temp value to zero here
                    short temp = 0;
                    if (!short.TryParse(((char) result).ToString(), out temp))
                    {
                        if (ActionOnError == ActionOnErrorEncounteredEnum.StoreInRecord)
                            thisRecord.Add_Error(MarcRecordParsingErrorTypeEnum.InvalidDirectoryEncountered,
                                "Found invalid (non-numeric) character in a directory entry.");
                        else
                            throw new ApplicationException("Found invalid (non-numeric) character in a directory entry.");
                    }

                    // Increment different values, depending on how far into this directory
                    // the reader has gotten.
                    switch (count)
                    {
                        case 0:
                        case 1:
                        case 2:
                            tag = (tag * 10) + temp;
                            break;

                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            fieldLength = (fieldLength * 10) + temp;
                            break;

                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            startingPosition = (startingPosition * 10) + temp;
                            break;
                    }

                    // Read the next character
                    result = _reader.Read();
                    count++;

                    // If this directory entry has been completely read, save it
                    // and reset the values for the next directory
                    if (count == 12)
                    {
                        directoryEntries.Add(new Marc21ParserDirectoryEntry((short) tag, (short) fieldLength,
                            (short) startingPosition));
                        tag = 0;
                        fieldLength = 0;
                        startingPosition = 0;
                        count = 0;
                    }
                }

                // Use a memory stream to accumulate bytes (we don't yet know the character
                // encoding for this record, so needs to remain bytes )
                var byteFieldBuilder = new MemoryStream();

                // Read all the data from the variable fields
                count = 0;
                var startIndex = 0;
                short lastFieldStartIndex = 0;
                result = _reader.Read();
                while (result != EndOfRecord)
                {
                    // Was this the end of the field (or tag)?
                    if (result == RecordSeperator)
                    {
                        // Get the value for this field
                        byte[] fieldAsByteArray = byteFieldBuilder.ToArray();

                        // Get the field as string, depending on the encoding
                        string fieldAsString;
                        switch (encoding)
                        {
                            case RecordCharacterEncoding.Marc:
                                fieldAsString = ConvertMarcBytesToUnicodeString(fieldAsByteArray);
                                break;

                            default:
                                fieldAsString = Encoding.UTF8.GetString(fieldAsByteArray);
                                break;
                        }

                        // Clear the byte field builder (create new memory stream)
                        byteFieldBuilder = new MemoryStream();

                        // Add the field to the list of variable data
                        fieldDatas.Add((short) startIndex,
                            new Marc21ParserVariableFieldData((short) startIndex, fieldAsString));

                        // This may be the last field, so save this index
                        lastFieldStartIndex = (short) startIndex;

                        // Save the count as the next start index
                        startIndex = count + 1;
                    }
                    else
                    {
                        // Save this byte
                        byteFieldBuilder.WriteByte((byte) result);
                    }

                    // Read the next character
                    result = _reader.ReadByte();
                    count++;
                }

                // Now, step through the directory, retrieve each pre-converted field data,
                // and finish parsing
                int directoryErrorCorrection = 0;
                foreach (Marc21ParserDirectoryEntry directoryEntry in directoryEntries)
                {
                    // Get the field
                    if (!fieldDatas.ContainsKey((short) (directoryEntry.StartingPosition + directoryErrorCorrection)))
                    {
                        while (
                            (!fieldDatas.ContainsKey(
                                (short) (directoryEntry.StartingPosition + directoryErrorCorrection))) &&
                            (lastFieldStartIndex > directoryEntry.StartingPosition + directoryErrorCorrection))
                        {
                            directoryErrorCorrection += 1;
                        }

                        // If this still didn't work, throw the exception
                        if (
                            !fieldDatas.ContainsKey(
                                (short) (directoryEntry.StartingPosition + directoryErrorCorrection)))
                        {
                            if (ActionOnError == ActionOnErrorEncounteredEnum.StoreInRecord)
                                thisRecord.Add_Error(
                                    MarcRecordParsingErrorTypeEnum.DirectoryFieldMismatchUnhandled);
                            else
                                throw new ApplicationException(
                                    "Field indexes and directory information cannot be resolved with one another.");
                        }
                        else
                        {
                            // This worked, but add a warning none-the-less
                            thisRecord.Add_Warning(
                                MarcRecordParsingWarningTypeEnum.DirectoryFieldMismatchHandled);
                        }
                    }
                    Marc21ParserVariableFieldData fieldData =
                        fieldDatas[(short) (directoryEntry.StartingPosition + directoryErrorCorrection)];
                    string variable_field_data = fieldData.FieldData;

                    // See if this row has an indicator
                    string indicator = "";
                    if ((variable_field_data.Length > 3) && (variable_field_data[2] == (UnitSeperator)))
                    {
                        indicator = variable_field_data.Substring(0, 2);
                        variable_field_data = variable_field_data.Substring(2);
                    }
                    else
                        variable_field_data = variable_field_data.Substring(0);

                    // Is this split into seperate subfields?
                    if ((variable_field_data.Length > 1) && (variable_field_data[0] == (UnitSeperator)))
                    {
                        // Split this into subfields
                        string[] subfields = variable_field_data.Substring(1).Split(new[] {UnitSeperator});

                        // Create the new field
                        MarcField newField = new MarcField
                        {
                            Tag = Convert.ToInt32(directoryEntry.Tag),
                            Indicators = indicator
                        };

                        // Step through each subfield
                        foreach (string thisSubfield in subfields)
                        {
                            // Add this subfield
                            newField.Add_Subfield(thisSubfield[0], thisSubfield.Substring(1));
                        }

                        // Add this entry to the current record
                        thisRecord.Add_Field(newField);
                    }
                    else
                    {
                        // Must be just one subfield
                        thisRecord.Add_Field(Convert.ToInt32(directoryEntry.Tag), variable_field_data);
                    }
                }

                // if this was MARC8 encoding originally, change the encoding specified in the 
                // leader, since this was converted to Unicode
                if (encoding == RecordCharacterEncoding.Marc)
                {
                    thisRecord.Leader = thisRecord.Leader.Substring(0, 9) + "a" + thisRecord.Leader.Substring(10);
                }
            }
            catch (EndOfStreamException)
            {
                if (ActionOnError == ActionOnErrorEncounteredEnum.StoreInRecord)
                    thisRecord.Add_Error(MarcRecordParsingErrorTypeEnum.UnexpectedEndOfStreamEncountered);
                else
                    throw new ApplicationException(
                        "Unexpected end of stream encountered!  Input stream may be invalid format or truncated.");
            }

            return thisRecord;
        }

        #endregion

        #region Methods used for converting MARC character encoded fields to Unicode 

        private static string ConvertMarcBytesToUnicodeString(IReadOnlyCollection<byte> input)
        {
            // Create the string builder to build the array
            var builder = new StringBuilder(input.Count);

            // Step through all the bytes in the array
            foreach (var t in input)
            {
                // If any previous bytes, save them

                // Get this byte frmo the array
                var marcByte1 = (int) t;

                builder.Append((char)marcByte1);
            }

            // Return the string
            return builder.ToString();
        }

        #endregion

        #region Methods implementing IDisposable

        /// <summary> Close any open streams which may remain </summary>
        /// <remarks> Required to implement IDisposable </remarks>
        void IDisposable.Dispose()
        {
            Close();
        }

        /// <summary> Close any open streams which may remain </summary>
        /// <remarks> Required to implement IDisposable </remarks>
        public void Dispose()
        {
            Close();
        }

        #endregion

        #region Methods implementing IEnumerator

        /// <summary> Gets the IEnumerator for this (itself) </summary>
        /// <returns></returns>
        /// <remarks> Required to implement IEnumerator </remarks>
        public IEnumerator<MarcRecord> GetEnumerator()
        {
            return this;
        }

        /// <summary> Gets the IEnumerator for this (itself) </summary>
        /// <returns></returns>
        /// <remarks> Required to implement IEnumerator </remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion

        #region Methods implementing IEnumerable

        /// <summary> Returns the current record </summary>
        /// <remarks> Required to implement IEnumerable </remarks>
        public MarcRecord Current { get; private set; }

        /// <summary> Returns the current record </summary>
        /// <remarks> Required to implement IEnumerable </remarks>
        object IEnumerator.Current => Current;

        /// <summary> Moves to the next record, and returns TRUE if one existed </summary>
        /// <returns> TRUE if another record was found, otherwise FALSE </returns>
        /// <remarks> Required to implement IEnumerable </remarks>
        public bool MoveNext()
        {
            Current = parse_next_record();
            if ((Current == null) || (EofFlag))
                return false;
            return true;
        }

        /// <summary> Resets the base stream, if that is possible </summary>
        /// <remarks> Required to implement IEnumerable </remarks>
        public void Reset()
        {
            if (_reader.BaseStream.CanSeek)
                _reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        #endregion
    }
}