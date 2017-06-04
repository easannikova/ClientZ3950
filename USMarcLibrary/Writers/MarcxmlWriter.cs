
#region Using directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;

#endregion

namespace USMarcLibrary.Writers
{
    public class MarcXmlWriter : IDisposable
    {
        private StreamWriter _writer;

        /// <summary> Constructor for a new instance of this class </summary>
        /// <param name="fileName"> Name of the output file </param>
        public MarcXmlWriter(string fileName)
        {
            // Open the stream
            _writer = new StreamWriter(fileName, false, Encoding.UTF8);

            // Start the file
            const string indent = "    ";
            _writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            _writer.WriteLine("<collection xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            _writer.WriteLine(indent + "xsi:schemaLocation=\"http://www.loc.gov/MARC21/slim");
            _writer.WriteLine(indent + indent + "http://www.loc.gov/standards/marcxml/schema/MARC21slim.xsd\"");
            _writer.WriteLine(indent + "xmlns=\"http://www.loc.gov/MARC21/slim\">");
        }

        /// <summary> Append a single record to the file </summary>
        /// <param name="Record">New record to append </param>
        public void AppendRecord(MarcRecord Record)
        {
            _writer.WriteLine(To_MarcXML(Record, false));
        }

        /// <summary> Append a list of records to the file </summary>
        /// <param name="records">Collection of records to append </param>
        public void AppendRecords(IEnumerable<MarcRecord> records)
        {
            foreach (MarcRecord record in records)
            {
                _writer.WriteLine(To_MarcXML(record, false));
            }
        }

        /// <summary> Close the stream writer used for this </summary>
        public void Close()
        {
            try
            {
                if (_writer != null)
                {
                    _writer.WriteLine("</collection>");
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

        #region Static methods converts a single MARC record to a MarcXML-formatted string 

        /// <summary> Returns this MARC record as a MarcXML-formatted string </summary>
        /// <param name="record"> MARC record to convert to a MarcXML-formatted string</param>
        /// <returns> This record as MarcXML-formatted string with the XML and collection declarative tags around the record </returns>
        public static string To_MarcXML(MarcRecord record)
        {
            return To_MarcXML(record, true);
        }

        /// <summary> Returns this MARC record as a MarcXML-formatted string </summary>
        /// <param name="record"> MARC record to convert to a MarcXML-formatted string</param>
        /// <param name="includeStartEndTags"> Flag indicates whether to include the XML and collection declarative tags around the record </param>
        /// <returns> This record as MarcXML-formatted string </returns>
        public static string To_MarcXML(MarcRecord record, bool includeStartEndTags)
        {
            var returnValue = new StringBuilder(5000);

            // Add the MARC XML header and start this collection
            if (includeStartEndTags)
            {
                const string indent = "    ";
                returnValue.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                returnValue.AppendLine("<collection xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                returnValue.AppendLine(indent + "xsi:schemaLocation=\"http://www.loc.gov/MARC21/slim");
                returnValue.AppendLine(indent + indent + "http://www.loc.gov/standards/marcxml/schema/MARC21slim.xsd\"");
                returnValue.AppendLine(indent + "xmlns=\"http://www.loc.gov/MARC21/slim\">");
            }

            // Begin this record and add the leader
            var recordRoot = new XElement("record", new XElement("leader", record.Leader)
            );

            // Step through each field in the collection
            foreach (MarcField thisField in record.SortedMarcTagList)
            {
                if (thisField.SubfieldCount == 0)
                {
                    if (thisField.ControlFieldValue.Length > 0)
                    {
                        // Create this new control field and add it to the root element
                        var controlField = new XElement("controlfield",
                            thisField.ControlFieldValue,
                            new XAttribute("tag", thisField.Tag.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0'))
                        );
                        recordRoot.Add(controlField);
                    }
                }
                else
                {
                    // Create the new datafield element and add it to the root element
                    var dataField = new XElement("datafield",
                        new XAttribute("tag", thisField.Tag.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0')),
                        new XAttribute("ind1", thisField.Indicator1),
                        new XAttribute("ind2", thisField.Indicator2)
                    );
                    recordRoot.Add(dataField);

                    // Add each subfield
                    foreach (MarcSubfield thisSubfield in thisField.Subfields)
                    {
                        // Create this subfield element and add it to the datafield
                        var subfield = new XElement("subfield",
                            thisSubfield.Data,
                            new XAttribute("code", thisSubfield.SubfieldCode)
                        );
                        dataField.Add(subfield);
                    }
                }
            }

            // Add the XML text to the string builder
            returnValue.Append(recordRoot.ToString());

            // Close this collection, if requested
            if (includeStartEndTags)
            {
                returnValue.AppendLine();
                returnValue.AppendLine("</collection>");
            }

            return returnValue.ToString();
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