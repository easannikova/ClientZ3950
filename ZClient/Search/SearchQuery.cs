using System.Collections.Generic;
using System.Threading.Tasks;
using ZClient.Library.USMarc;
using ZClient.Library.USMarc.Bib1Attributes;
using ZClient.Library.USMarc.Models;
using ZClient.Library.USMarc.Z3950;

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

        public async Task<IDictionary<Server, IEnumerable<MarcRecord>>> Search()
        {
            var result = new Dictionary<Server, IEnumerable<MarcRecord>>();
            var manager = new Z3950Manager();

            foreach (var server in _servers)
            {
                var records = await Task.Factory.StartNew(() => manager.GetRecords(server, _bib1Attr, _query));
                if (records.Count > 0)
                    result.Add(server, records);
            }

            return result;
        }
    }
}
