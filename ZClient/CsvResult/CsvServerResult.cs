using System;
using System.Collections.Generic;
using ZClient.Abstract;
using ZClient.Models;

namespace ZClient.CsvResult
{
    public class CsvServerResult : ILoaderResult
    {
        public readonly ICollection<Server> Result = new List<Server>();

        public void Parse(string lineToParse)
        {
            var values = lineToParse.Split(';');
            uint port;

            if (values.Length < 4)
                throw new FormatException("Input string was invalid.");

            if (uint.TryParse(values[3], out port))
                throw  new FormatException("Input string was invalid.");

            Result.Add(new Server
            {
                Name = values[0],
                Host = values[1],
                Database = values[2],
                Port = port
            });
        }
    }
}
