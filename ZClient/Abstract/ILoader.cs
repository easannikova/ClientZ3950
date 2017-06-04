using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZClient.Abstract
{
    public interface ILoader
    {
        void Save(string filename);
        void Load(string filename);

        ILoaderResult LoaderResult { get; }
    }
}
