using System.Collections.Generic;
using USMarcLibrary;
using USMarcLibrary.Bib1Attributes;
using USMarcLibrary.Z3950;
using ZClient.Models;

namespace ZClient.Search
{
    public class SearchQuery
    {
        private readonly IEnumerable<Server> _servers;
        private readonly Bib1Attr _bib1Attr;
        private readonly string _query;

        public SearchQuery(IEnumerable<Server> servers, Bib1Attr attr, string query)
        {
            _servers = servers;
            _bib1Attr = attr;
            _query = $"\"{query}\"";
        }

        public SearchQuery(Server server, Bib1Attr attr, string query)
            : this(new[] {server}, attr, query)
        {
        }

        public IDictionary<Server, IEnumerable<MarcRecord>> Search()
        {
            var result = new Dictionary<Server, IEnumerable<MarcRecord>>();
            var manager = new MarcRecordZ3950Manager();

            foreach (var server in _servers)
            {
                var endpoint = new Z3950Endpoint(server.Name, server.Host, server.Port, server.Database);
                var records = manager.GetRecords(endpoint, _bib1Attr, _query);
                result.Add(server, records);
            }

            return result;
        }
    }
}
