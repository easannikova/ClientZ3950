using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USMarcLibrary.BibEAttributes
{
    public enum Bib1Attr
    {
        Title = 4,
        ISBN = 7,
        LocNumber = 12,
        Date = 30,
        Author = 1003,
    }

    public static class BibAttributes
    {
        private const string Prefix = "@attrset Bib-1 ";
        public static string GetAttributes(this Bib1Attr attr)
        {
            return Prefix + $"@attr 1={(int)attr} ";
        }
    }
}
