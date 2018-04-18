using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProject
{
    public class BestVariant
    {
        public readonly List<string> authors;

        public BestVariant(List<string> authors)
        {
            this.authors = authors;
        }
        private bool AllNull()
        {
            bool oneNotNull = false;
            foreach(string str in authors)
            {
                if (!String.IsNullOrEmpty(str))
                    oneNotNull = true;
            }

            return !oneNotNull;
        }

        private string OneNull()
        {
            bool hasNull = false;
            bool hasNotNull = false;
            string res = null;
            foreach (string str in authors)
            {
                if (String.IsNullOrEmpty(str))
                    hasNull = true;
                if (!String.IsNullOrEmpty(str))
                {
                    hasNotNull = true;
                    res = str;
                }
                if (hasNull && hasNotNull)
                    return res;
            }

            return null;
        }

        private string TwoDots()
        {
            foreach(string str in authors)
            {
                if (str.IndexOf(".") < str.LastIndexOf("."))
                    return str;
            }

            return null;
        }

        private string AllEqual()
        {
            string equ = authors.ElementAt(0);
            foreach(string str in authors)
            {
                if (!str.Equals(equ))
                    return null;
            }

            return equ;
        }

        private string OneDot()
        {
            foreach (string str in authors)
            {
                if (str.IndexOf(".") > 0)
                    return str;
            }

            return null;
        }
        public string Choose()
        {
            if (AllNull())
                return null;
            string res = OneNull();
            if (!String.IsNullOrEmpty(res))
                return res;
            res = TwoDots();
            if (!String.IsNullOrEmpty(res))
                return res;
            res = OneDot();
            if (!String.IsNullOrEmpty(res))
                return res;
            res = AllEqual();
            if (!String.IsNullOrEmpty(res))
                return res;
            return authors.ElementAt(0);
        }
    }
}
