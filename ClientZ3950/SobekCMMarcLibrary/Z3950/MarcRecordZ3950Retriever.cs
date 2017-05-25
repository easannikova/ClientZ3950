#region Using directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using USMarcLibrary.Parsers;
using Zoom.Net;
using Zoom.Net.YazSharp;

#endregion

namespace USMarcLibrary.Z3950
{
    public class MarcRecordZ3950Retriever
    {
        public static ICollection<MarcRecord> GetRecordByPrimaryIdentifier(string primaryIdentifier, Z3950Endpoint z3950Server,
            out string message)
        {
            // Initially set the message to empty
            message = string.Empty;
            var result = new Collection<MarcRecord>();

            // http://jai-on-asp.blogspot.com/2010/01/z3950-client-in-cnet-using-zoomnet-and.html
            // http://www.indexdata.com/yaz/doc/tools.html#PQF
            // http://www.loc.gov/z3950/agency/defns/bib1.html
            // http://www.assembla.com/code/wasolic/subversion/nodes/ZOOM.NET 
            // http://lists.indexdata.dk/pipermail/yazlist/2007-June/002080.html
            // http://fclaweb.fcla.edu/content/z3950-access-aleph

            const string prefix = "@attrset Bib-1 @attr 1=4 ";

            try
            {
                //	allocate MARC tools
                var parser = new Marc21ExchangeFormatParser();

                //	establish connection
                var connection = new Connection(z3950Server.Uri, Convert.ToInt32(z3950Server.Port))
                {
                    DatabaseName = z3950Server.DatabaseName
                };

                // Any authentication here?
                if (z3950Server.Username.Length > 0)
                    connection.Username = z3950Server.Username;
                if (z3950Server.Password.Length > 0)
                    connection.Password = z3950Server.Password;

                // Set to USMARC
                connection.Syntax = RecordSyntax.USMARC;

                //	call the Z39.50 server
                var query = new PrefixQuery(prefix + "\"sql\"");
                //var query = new CQLQuery("Title = \"sql\"");

                //string query = "(TITLE = \"SQL\")";
                //var q = new CQLQuery(query);

                var records = connection.Search(query);

                // If the record count is not one, return a message
                //if (records.Count != 1)
                if (records.Count == 0)
                {
                    message = records.Count == 0
                        ? "ERROR: No matching record found in Z39.50 endpoint"
                        : "ERROR: More than one matching record found in Z39.50 endpoint by primary identifier";
                    return null;
                }

                foreach (IRecord rec in records)
                {
                    var ms = new MemoryStream(rec.Content);

                    try
                    {
                        //	feed the record to the parser and add the 955
                        var marcrec = parser.Parse(ms);
                        result.Add(marcrec);
                        parser.Close();
                    }
                    catch (Exception error)
                    {
                        message = "ERROR: Unable to parse resulting record into the MARC Record structure!\n\n" +
                                  error.Message;
                        return null;
                    }
                }

                return result;

            }
            catch (Exception error)
            {
                if (error.Message.IndexOf("The type initializer for 'Zoom.Net.Yaz") >= 0)
                {
                    message =
                        "ERROR: The Z39.50 libraries did not correctly initialize.\n\nThese libraries do not currently work in 64-bit environments.";
                }
                else
                {
                    message = "ERROR: Unable to connect and search provided Z39.50 endpoint.\n\n" + error.Message;
                }

                return null;
                //MessageBox.Show("Could not convert item " + Primary_Identifier + " to MARCXML.");
                //Trace.WriteLine("Could not convert item " + Primary_Identifier   + " to MARCXML.");
                //Trace.WriteLine("Error details: " + error.Message);
            }

            return null;
        }


        public static MarcRecord Get_Record(int attributeNumber, string searchTerm, Z3950Endpoint z3950Server,
            out string message)
        {
            // Initially set the message to empty
            message = string.Empty;

            // http://jai-on-asp.blogspot.com/2010/01/z3950-client-in-cnet-using-zoomnet-and.html
            // http://www.indexdata.com/yaz/doc/tools.html#PQF
            // http://www.loc.gov/z3950/agency/defns/bib1.html
            // http://www.assembla.com/code/wasolic/subversion/nodes/ZOOM.NET 
            // http://lists.indexdata.dk/pipermail/yazlist/2007-June/002080.html
            // http://fclaweb.fcla.edu/content/z3950-access-aleph

            string prefix = "@attrset Bib-1 @attr 1=" + attributeNumber + " ";

            try
            {
                IConnection connection; //	zoom db connector
                IPrefixQuery query; //	zoom query
                IRecord record; //	one zoom record
                IResultSet records; //	collection of records	

                //	allocate MARC tools
                Marc21ExchangeFormatParser parser = new Marc21ExchangeFormatParser();

                //	establish connection
                connection = new Connection(z3950Server.Uri, Convert.ToInt32(z3950Server.Port));
                connection.DatabaseName = z3950Server.DatabaseName;

                // Any authentication here?
                if (z3950Server.Username.Length > 0)
                    connection.Username = z3950Server.Username;
                if (z3950Server.Password.Length > 0)
                    connection.Password = z3950Server.Password;

                // Set to USMARC
                connection.Syntax = RecordSyntax.USMARC;

                //	call the Z39.50 server
                query = new PrefixQuery(prefix + searchTerm);
                records = connection.Search(query);

                // If the record count is not one, return a message
                if (records.Count != 1)
                {
                    if (records.Count == 0)
                        message = "ERROR: No matching record found in Z39.50 endpoint";
                    else
                        message = "ERROR: More than one matching record found in Z39.50 endpoint by ISBN";
                    return null;
                }

                //	capture the byte stream
                record = records[0];
                MemoryStream ms = new MemoryStream(record.Content);

                //	display while debugging
                //MessageBox.Show(Encoding.UTF8.GetString(record.Content));

                try
                {
                    //	feed the record to the parser and add the 955
                    MarcRecord marcrec = parser.Parse(ms);
                    parser.Close();
                    return marcrec;
                }
                catch (Exception error)
                {
                    message = "ERROR: Unable to parse resulting record into the MARC Record structure!\n\n" +
                              error.Message;
                    return null;
                    //MessageBox.Show("Could not convert item " + Primary_Identifier + " to MARCXML.");
                    //Trace.WriteLine("Could not convert item " + Primary_Identifier   + " to MARCXML.");
                    //Trace.WriteLine("Error details: " + error.Message);
                }
            }
            catch (Exception error)
            {
                if (error.Message.IndexOf("The type initializer for 'Zoom.Net.Yaz") >= 0)
                {
                    message =
                        "ERROR: The Z39.50 libraries did not correctly initialize.\n\nThese libraries do not currently work in 64-bit environments.";
                }
                else
                {
                    message = "ERROR: Unable to connect and search provided Z39.50 endpoint.\n\n" + error.Message;
                }

                return null;
                //MessageBox.Show("Could not convert item " + Primary_Identifier + " to MARCXML.");
                //Trace.WriteLine("Could not convert item " + Primary_Identifier   + " to MARCXML.");
                //Trace.WriteLine("Error details: " + error.Message);
            }

            return null;
        }
    }
}