#region License and Copyright

//          SobekCM MARC Library ( Version 1.2 )
//          
//          Copyright (2005-2012) Mark Sullivan. ( Mark.V.Sullivan@gmail.com )
//          
//          This file is part of SobekCM MARC Library.
//          
//          SobekCM MARC Library is free software: you can redistribute it and/or modify
//          it under the terms of the GNU Lesser Public License as published by
//          the Free Software Foundation, either version 3 of the License, or
//          (at your option) any later version.
//            
//          SobekCM MARC Library is distributed in the hope that it will be useful,
//          but WITHOUT ANY WARRANTY; without even the implied warranty of
//          MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//          GNU Lesser Public License for more details.
//            
//          You should have received a copy of the GNU Lesser Public License
//          along with SobekCM MARC Library.  If not, see <http://www.gnu.org/licenses/>.


#endregion

#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SobekCM.Bib_Package.MARC;
using SobekCMMarcLibrary;

#endregion

namespace SobekCM_Marc_Library.Writers
{
    public class Marc21ExchangeFormatWriter : IDisposable
    {
        // Constants used when writing the Marc21 stream
        private const char GroupSeperator = (char) 29;
        private const char RecordSeperator = (char) 30;
        private const char UnitSeperator = (char) 31;

        private StreamWriter _writer;

        /// <summary> Constructor for a new instance of this class </summary>
        /// <param name="fileName"> Name of the output file </param>
        public Marc21ExchangeFormatWriter(string fileName)
        {
            // Open the stream
            _writer = new StreamWriter(fileName, false, Encoding.UTF8);
        }

        /// <summary> Append a single record to the file </summary>
        /// <param name="record">New record to append </param>
        public void AppendRecord(MarcRecord record)
        {
            _writer.WriteLine(To_Machine_Readable_Record(record));
        }

        /// <summary> Append a list of records to the file </summary>
        /// <param name="records">Collection of records to append </param>
        public void AppendRecords(IEnumerable<MarcRecord> records)
        {
            foreach (MarcRecord record in records)
            {
                _writer.WriteLine(To_Machine_Readable_Record(record));
            }
        }

        /// <summary> Close the stream writer used for this </summary>
        public void Close()
        {
            try
            {
                if (_writer != null)
                {
                    _writer.Flush();
                    _writer.Close();
                    _writer = null;
                }
            }
            catch
            {
                // ignored
            }
        }

        #region Static methods converts a single MARC record to a Marc21-formatted string

        /// <summary> Returns a string which represents a record in machine readable record format. </summary>
        /// <param name="record"> MARC record to convert to MARC21 </param>
        /// <returns> MARC record as MARC21 Exchange format record string</returns>
        public static string To_Machine_Readable_Record(MarcRecord record)
        {
            // Create the stringbuilder for this
            var directory = new StringBuilder(1000);
            var completefields = new StringBuilder(2000);
            var completeLine = new StringBuilder(200);

            // Step through each entry by key from the hashtable
            var overallRecord = new List<string>();
            var runningLength = 0;

            // Step through each field ( control and data ) in the record
            foreach (var thisEntry in record.SortedMarcTagList)
            {
                // Perpare to build this line
                if (completeLine.Length > 0)
                    completeLine.Remove(0, completeLine.Length);

                // Is this a control field (with no subfields) or a data field?
                if (thisEntry.SubfieldCount == 0)
                {
                    if (!String.IsNullOrEmpty(thisEntry.ControlFieldValue))
                    {
                        completeLine.Append(int_to_string(thisEntry.Tag, 3) + RecordSeperator);
                        completeLine.Append(thisEntry.ControlFieldValue);
                        overallRecord.Add(completeLine.ToString());
                    }
                }
                else
                {
                    // Start this tag and add the indicator, if there is one
                    if (thisEntry.Indicators.Length == 0)
                        completeLine.Append(int_to_string(thisEntry.Tag, 3) + RecordSeperator);
                    else
                        completeLine.Append(int_to_string(thisEntry.Tag, 3) + RecordSeperator + thisEntry.Indicators);

                    // Build the complete line
                    foreach (MarcSubfield thisSubfield in thisEntry.Subfields)
                    {
                        if (thisSubfield.SubfieldCode == ' ')
                        {
                            if (thisEntry.Indicators.Length == 0)
                                completeLine.Append(thisSubfield.Data);
                            else
                                completeLine.Append(UnitSeperator.ToString() + thisSubfield.Data);
                        }
                        else
                        {
                            completeLine.Append(UnitSeperator.ToString() + thisSubfield.SubfieldCode +
                                                thisSubfield.Data);
                        }
                    }

                    // Add this to the list
                    overallRecord.Add(completeLine.ToString());
                }
            }

            // Now, add these to the directory and completefields StringBuilders
            foreach (string thisLin in overallRecord)
            {
                // Add this line to the directory and fields
                directory.Append(thisLin.Substring(0, 3) + (int_to_string(adjusted_length(thisLin) - 3, 4)) +
                                 (int_to_string(runningLength, 5)));
                completefields.Append(thisLin.Substring(3));

                // Increment the running length
                runningLength += adjusted_length(thisLin) - 3;
            }

            // Get the length of just the directory, before we start appending more to it
            var directoryLength = directory.Length;

            // Compile the return value
            directory.Append(completefields.ToString() + RecordSeperator + GroupSeperator);

            // Get the leader
            string leader = record.Leader;

            // Insert the total length of this record
            runningLength += leader.Length + directoryLength + 2;

            // Return the combination of these two fields, plus the end of record char
            return int_to_string(runningLength, 5) + leader.Substring(5, 7) +
                   int_to_string(leader.Length + directoryLength + 1, 5) +
                   leader.Substring(17) + directory;
        }

        private static string int_to_string(int number, int lengthRequired)
        {
            // Verify the number fits
            if (number.ToString().Length > lengthRequired)
                throw new ApplicationException("Number too large for field length!  Record may be too large!");

            // Return the value
            return number.ToString().PadLeft(lengthRequired, '0');
        }

        private static int adjusted_length(string line)
        {
            int length = 0;
            foreach (char thisChar in line)
            {
                double ascii = thisChar;
                if (ascii < 128)
                {
                    length++;
                }
                if ((ascii >= 128) && (ascii <= 2047))
                {
                    length += 2;
                }
                if (ascii > 2047)
                {
                    length += 3;
                }
            }
            return length;
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
    }
}