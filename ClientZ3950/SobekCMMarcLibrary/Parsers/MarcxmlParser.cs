
#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#endregion

namespace USMarcLibrary.Parsers
{
    /// <summary> Parser steps through the records in a MarcXML file or stream. </summary>
    /// <remarks> Written by Mark Sullivan for the University of Florida Digital Library<br /><br />
    /// You can either pass in the stream or file to read into the constructor and immediately begin using Next() to step
    /// through them, or you can use the empty constructor and call the Parse methods for the first record. <br /><br />
    /// To  use the IEnumerable interface, you must pass in the Stream or filename in the constructor.</remarks>
    public class MarcxmlParser : IEnumerable<MarcRecord>, IEnumerator<MarcRecord>
    {
        // Stream used to read the MarcXML records
        private Stream _baseStream;
        private string _filename;
        private XmlTextReader _reader;

        #region Constructors 

        /// <summary> Constructor for a new instance of this class </summary>
        public MarcxmlParser()
        {
            // Constructor does nothing
        }

        /// <summary> Constructor for a new instance of this class </summary>
        /// <param name="marcXmlStream"> Open stream from which to read MarcXML records </param>
        public MarcxmlParser(Stream marcXmlStream)
        {
            // Create the new reader object
            _reader = new XmlTextReader(marcXmlStream);

            // Save the stream for resetting purposes
            _baseStream = marcXmlStream;
        }

        /// <summary> Constructor for a new instance of this class </summary>
        /// <param name="marcXmlFile"> Name of the file to parse </param>
        public MarcxmlParser(string marcXmlFile)
        {
            // Create the new reader object
            _reader = new XmlTextReader(marcXmlFile);

            // Save the filename
            _filename = marcXmlFile;
        }

        #endregion

        /// <summary> Begins parsing from a stream containing MarcXML records. </summary>
        /// <param name="marcXmlStream"> Open stream from which to read MarcXML records </param>
        /// <returns> A built record, or NULL if no records are contained within the file </returns>
		public MarcRecord Parse( Stream marcXmlStream )
		{
			// Create the new reader object
            _reader = new XmlTextReader(marcXmlStream);

            // Save the stream for resetting purposes
            _baseStream = marcXmlStream;
            _filename = null;

            // Return the first record
            return parse_next_record();
		}

        /// <summary> Begins parsing a new MarcXML file. </summary>
        /// <param name="marcXmlFile"> Name of the file to parse </param>
		/// <returns> A built record, or NULL if no records are contained within the file </returns>
        public MarcRecord Parse(string marcXmlFile)
		{
            // Create the new reader object
            _reader = new XmlTextReader(marcXmlFile);

            // Save the filename
            _filename = marcXmlFile;
            _baseStream = null;

			// Return the first record
			return parse_next_record();
		}

        /// <summary> Returns the next record in the MarcXML_File file or stream </summary>
		/// <returns> Next object, or NULL </returns>
		public MarcRecord Next()
        {
            if ( _reader != null )
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
        public bool EofFlag
        {
            get;
            set;
        }

        private MarcRecord parse_next_record()
        {
            // Create the MARC record to return and subfield collection
            MarcRecord thisRecord = new MarcRecord();

            // Try to read this
            Read_MARC_Info(_reader, thisRecord);

            // Return this record
            return thisRecord;
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
        public MarcRecord Current
        {
            get;
            private set;
        }

        /// <summary> Returns the current record </summary>
        /// <remarks> Required to implement IEnumerable </remarks>
        object IEnumerator.Current
        {
            get { return Current; }
        }

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
            if ( _baseStream != null )
            {
                if ( _baseStream.CanSeek)
                    _baseStream.Seek(0, SeekOrigin.Begin);
                _reader = new XmlTextReader(_baseStream);
            }
            else if ( !String.IsNullOrEmpty(_filename))
            {
                if ( _reader != null )
                    Close();
                _reader = new XmlTextReader(_filename );
            }
        }

        #endregion

        #region Static methods read a single MARC record from a MarcXML file or nodereader 
        
        /// <summary> Reads the data from a MARC XML file into this record </summary>
        /// <param name="marcXmlFile">Input MARC XML file</param>
        /// <param name="record"> Record into which to read the contents of the MarcXML file </param>
        /// <returns>TRUE if successful, otherwise FALSE </returns>
        public static bool Read_From_MARC_XML_File(string marcXmlFile, MarcRecord record )
        {
            try
            {
                // Load this MXF File
                var marcXml = new XmlDocument();
                marcXml.Load(marcXmlFile);

                Stream reader = new FileStream(marcXmlFile, FileMode.Open, FileAccess.Read);

                // create the node reader
                var nodeReader = new XmlTextReader(reader);

                return Read_MARC_Info(nodeReader, record );

            }
            catch
            {
                return false;
            }
        }


        /// <summary> Reads the data from a XML Node Reader </summary>
        /// <param name="nodeReader">XML Node Reader </param>
        /// <param name="record"> Record into which to read the contents of the MarcXML file </param>
        /// <returns>TRUE if successful, otherwise FALSE </returns>
        public static bool Read_MARC_Info(XmlTextReader nodeReader, MarcRecord record)
        {
            try
            {
                // Move to the this node
                move_to_node(nodeReader, "record");

                // Get the leader information
                int tag = -1;
                while (nodeReader.Read())
                {
                    if ((nodeReader.NodeType == XmlNodeType.EndElement) && (nodeReader.Name == "record"))
                        return true;

                    if (nodeReader.NodeType == XmlNodeType.Element)
                    {
                        switch (nodeReader.Name.Trim().Replace("marc:", ""))
                        {
                            case "leader":
                                nodeReader.Read();
                                record.Leader = nodeReader.Value;
                                break;

                            case "controlfield":
                                // Get the tag
                                if (nodeReader.MoveToAttribute("tag"))
                                {
                                    // The tag should always be numeric per the schema, but just relaxing this
                                    // for invalid MARC so the rest of the data can be successfully read.
                                    if (Int32.TryParse(nodeReader.Value, out tag))
                                    {
                                        // Move to the value and then add this
                                        nodeReader.Read();
                                        record.Add_Field(tag, nodeReader.Value);
                                    }
                                }
                                break;

                            case "datafield":
                                // Set the default indicators
                                char ind1 = ' ';
                                char ind2 = ' ';

                                // Get the indicators if they exist
                                while (nodeReader.MoveToNextAttribute())
                                {
                                    if (nodeReader.Name.Trim() == "ind1")
                                    {
                                        string temp1 = nodeReader.Value;
                                        if (temp1.Length > 0)
                                            ind1 = temp1[0];
                                    }
                                    if (nodeReader.Name.Trim() == "ind2")
                                    {
                                        string temp2 = nodeReader.Value;
                                        if (temp2.Length > 0)
                                            ind2 = temp2[0];
                                    }
                                    if (nodeReader.Name.Trim() == "tag")
                                        tag = Convert.ToInt32(nodeReader.Value);
                                }

                                // Add this datafield
                                MarcField newField = record.Add_Field(tag, ind1, ind2);

                                // Now, add each subfield
                                while (nodeReader.Read())
                                {
                                    if ((nodeReader.NodeType == XmlNodeType.EndElement) && (nodeReader.Name.Replace("marc:", "") == "datafield"))
                                        break;

                                    if ((nodeReader.NodeType == XmlNodeType.Element) && (nodeReader.Name.Replace("marc:", "") == "subfield"))
                                    {
                                        // Get the code
                                        nodeReader.MoveToFirstAttribute();
                                        char subfield = nodeReader.Value.Length > 0 ? nodeReader.Value[0] : ' ';

                                        // Get the value
                                        nodeReader.Read();
                                        string dataValue = nodeReader.Value;

                                        // Save this subfield
                                        newField.Add_Subfield(subfield, dataValue);

                                        // Do some special stuff if this is the 260
                                        if (tag == 260)
                                        {
                                            newField.ControlFieldValue = newField.ControlFieldValue + "|" + subfield + " " + dataValue + " ";
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void move_to_node(XmlTextReader nodeReader, string nodeName)
        {
            while (nodeReader.Read())
            {
                if (nodeReader.Name.Trim().Replace("marc:", "") == nodeName)
                {
                    return;
                }
            }
        }

        #endregion
    }
}
