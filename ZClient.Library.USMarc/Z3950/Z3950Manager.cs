using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using NLog;
using ZClient.Abstract;
using ZClient.Library.USMarc.Bib1Attributes;
using ZClient.Library.USMarc.Models;
using ZClient.Library.USMarc.Parsers;
using ZClient.Logic;

namespace ZClient.Library.USMarc.Z3950
{
    public class Z3950Manager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private const string Prefix = "@attrset Bib-1 ";

        public ICollection<MarcRecord> GetRecords(Server endpoint, Bib1Attr attr, string key)
        {
            var marcRecords = new Collection<MarcRecord>();
            try
            {
                var parser = new ExchangeFormatParser();

                var connection = new Connection(endpoint.Uri, Convert.ToInt32(endpoint.Port))
                {
                    DatabaseName = endpoint.DatabaseName
                };

                if (!string.IsNullOrEmpty(endpoint.Username))
                    connection.Username = endpoint.Username;

                if (!string.IsNullOrEmpty(endpoint.Password))
                    connection.Password = endpoint.Password;

                connection.Syntax = RecordSyntax.UsMarc;
                var query = new PrefixQuery(Prefix + attr.GetAttributes() + key);

                var records = connection.Search(query);

                if (records.Count == 0)
                    return marcRecords;

                foreach (IRecord rec in records)
                {
                    var ms = new MemoryStream(rec.Content);

                    try
                    {
                        var marcrec = parser.Parse(ms);
                        marcRecords.Add(marcrec);
                        parser.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                Logger.Error(ex);
            }

            return marcRecords;
        }
    }
}