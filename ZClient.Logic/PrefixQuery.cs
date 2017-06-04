using ZClient.Abstract;

namespace ZClient.Logic
{
    public class PrefixQuery : IPrefixQuery
    {
        public PrefixQuery(string query)
        {
            QueryString = query;
        }

        public string QueryString { get; set; }
    }
}