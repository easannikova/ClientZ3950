using System;
using System.IO;
using ZClient.Abstract;

namespace ZClient.Loader
{
    public class CsvLoader : ILoader
    {
        public ILoaderResult LoaderResult { get; }

        public CsvLoader(ILoaderResult loaderResult)
        {
            LoaderResult = loaderResult;
        }

        public void Save(string filename)
        {
            throw new NotImplementedException();
        }

        public void Load(string filename)
        {
            using (var fs = File.OpenRead(filename))
            using (var reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    LoaderResult.Parse(line);
                }
            }
        }
    }
}
