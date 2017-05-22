using System;
using SobekCM.Bib_Package.MARC;
using SobekCM.Bib_Package.MARC.Parsers;
using SobekCM_Marc_Library;
using SobekCM_Marc_Library.Writers;
using SobekCM_Marc_Library.Z3950;

namespace Marc_Demo_App
{
    class Program
    {
        static void Main()
        {
            //// DEMO 1: Read a MARC DAT file completely using the IEnumerator interface. and adding it 
            ////                 to  a MarcXML output file
            //Demo1();

            //// DEMO 2: Read the second record from a MARC21 DAT file, w/o using the IEnumerator 
            ////                  interface and write that single record to both a XML file and a DAT file, w/o calling
            ////                  the writer classes explicitly
            //demo2();

            //// DEMO 3: Read the resulting demo2.dat file, change the title, publisher, and add a subject field
            ////                 and then save it again
            //demo3();

            //// DEMO 4: Read a record from Z39.50 and save it as MarcXML
            demo4();

            Console.WriteLine("COMPLETE!");
            Console.ReadLine();

        }

        /// <summary> DEMO 1 : Read a MARC DAT file completely using the IEnumerator interface. and adding it to  a MarcXML output file </summary>
        private static void Demo1()
        {
            Console.WriteLine("Performing demo1");

            // Create the marc21 exchange reader
            var parser1 = new Marc21ExchangeFormatParser("AgricNewCat02.dat");

            // Create the marc xml writer
            var writer1 = new MarcxmlWriter("AgricNewCat02.xml");

            try
            {
                // Step through each record from the Marc21 dat file and output to the XML file
                foreach (MarcRecord thisRecord in parser1)
                {
                    writer1.AppendRecord(thisRecord);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.StackTrace);
            }
            finally
            {
                // Close all the streams
                parser1.Close();
                writer1.Close();
            }
        }

        /// <summary> DEMO 2: Read the second record from a MARC21 DAT file, w/o using the IEnumerator 
        /// interface and write that single record to both a XML file and a DAT file, w/o calling the writer classes 
        /// explicitly </summary>
        private static void demo2()
        {
            Console.WriteLine("Performing demo2");

            // Create the marc21 exchange reader
            Marc21ExchangeFormatParser parser1 = new Marc21ExchangeFormatParser();

            // This parses and pulls out the first record (discarded)
            parser1.Parse("CIMMYT01.dat");

            // We'll pull again to get the second
            MarcRecord record = parser1.Next();

            // If this is null, say so
            if (record == null)
            {
                Console.WriteLine("Unable to read the second record from test file.");
                return;
            }

            // Save as a MarcXML file
            if (!record.Save_MARC_XML("demo2.xml"))
            {
                Console.WriteLine("Error encountered while writing demo2.xml");
                return;
            }

            // Save as a single Marc21 file
            if (!record.Save_MARC21("demo2.dat"))
            {
                Console.WriteLine("Error encountered while writing demo2.dat");
            }
        }

        /// <summary> DEMO 3: Read the resulting demo2.dat file, change the title, publisher, and add 
        /// a subject field and then save it again</summary>
        private static void demo3()
        {
            Console.WriteLine("Performing demo3");

            // Create the marc21 exchange reader
            Marc21ExchangeFormatParser parser1 = new Marc21ExchangeFormatParser();

            // Read the record
            MarcRecord record = parser1.Parse("demo2.dat");

            // If this is null, say so
            if (record == null)
            {
                Console.WriteLine("Unable to read the demo2.dat in the 3rd demo portion");
                return;
            }

            // Change the title field ( 245 )
            record[245][0].Add_NonRepeatable_Subfield('a', "New Title");

            // Also change the creator field (110 in this case)
            record[110][0].Add_NonRepeatable_Subfield('a', "Corn Maze Production, Incorporated");

            // Add a new field to record
            MarcField newSubject = record.Add_Field(650, ' ', '0');
            newSubject.Add_Subfield('a', "Soils");
            newSubject.Add_Subfield('x', "Phosphorous content");
            newSubject.Add_Subfield('z', "Indonesia");

            // Save this as XML and also as Marc21
            record.Save_MARC_XML("demo3.xml");
            record.Save_MARC21("demo3.dat");
        }

        private static void demo4()
        {
            Console.WriteLine("Performing demo4 ( z39.50 )");

            try
            {
                // Create the Z39.50 endpoint
                //var endpoint = new Z3950_Endpoint("vsu", "z3950.lib.vsu.ru", 210, "automat");
                var endpoint = new Z3950Endpoint("Library of Congress", "z3950.ssu.samara.ru", 210, "books");
                //var endpoint = new Z3950Endpoint("Library of Congress", "z3950.loc.gov", 7090, "VOYAGER");
                //Z3950_Endpoint endpoint = new Z3950_Endpoint("Canadian National Catalogue", "142.78.200.109", 210, "NL");

                // Retrieve the record by primary identifier
                string outMessage;
                var recordFromZ3950 = MarcRecordZ3950Retriever.GetRecordByPrimaryIdentifier("4543338",
                    endpoint, out outMessage);

                //var rec = MarcRecordZ3950Retriever.Get_Record(4, "sql", endpoint, out outMessage);

                // Display any error message encountered
                if (recordFromZ3950 == null)
                {
                    Console.WriteLine(outMessage.Length > 0
                        ? outMessage
                        : "Unknown error occurred during Z39.50 request");
                    return;
                }

                // Write as MARCXML
                recordFromZ3950.Save_MARC_XML("demo4.xml");
            }
            catch (Exception ee)
            {
                Console.WriteLine("EXCEPTION CAUGHT while performing Z39.50 demo. ( " + ee.Message + " )");
            }

        }

    }
}
