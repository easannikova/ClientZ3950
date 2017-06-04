using ZClient.Abstract;

namespace ZClient.Logic
{
    public class ScanTerm : IScanTerm
    {
        internal ScanTerm(string term, int occurences)
        {
            Term = term;
            Occurences = occurences;
        }

        public string Term { get; }

        public int Occurences { get; }
    }
}