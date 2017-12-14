using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ZClient.Abstract;
using ZClient.CsvResult;
using ZClient.Library.USMarc.Bib1Attributes;
using ZClient.Library.USMarc.Z3950;
using ZClient.Loader;
using ZClient.Search;

namespace ZClient.Manage
{
    public class Manager
    {
        public List<Server> Servers { get; } = new List<Server>();

        private readonly ILoader _loader;

        public Manager()
        {
            _loader = new CsvLoader(new CsvServerResult());
        }

        public void LoadServers(string filename)
        {
            _loader.Load(filename);
            var collection = (_loader.LoaderResult as CsvServerResult)?.Result;
            if (collection != null)
                Servers.AddRange(collection);
        }

        public async Task<IDictionary<Server, IEnumerable<string>>> Search(string query, Bib1Attr attr)
        {
            var search = new SearchQuery(Servers, attr, query);
            var found = await search.Search();
            var result = new Dictionary<Server, IEnumerable<string>>();

            foreach (var pair in found)
            {
                var xmlRecord = new Collection<string>();
                foreach (var marcRecord in pair.Value)
                    xmlRecord.Add(marcRecord.ToMarcXml());
                result.Add(pair.Key, xmlRecord);
            }

            return result;
        }
    }
}
