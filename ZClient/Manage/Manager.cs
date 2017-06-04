using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USMarcLibrary.Bib1Attributes;
using ZClient.Abstract;
using ZClient.CsvResult;
using ZClient.Loader;
using ZClient.Models;
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

        public void Search(string query)
        {
            var search = new SearchQuery(Servers, Bib1Attr.ISBN, query);
            var found = search.Search();
        }

    }
}
