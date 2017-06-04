using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZClient.Abstract
{
    public interface ILoaderResult
    {
        void Parse(string lineToParse);
    }
}
