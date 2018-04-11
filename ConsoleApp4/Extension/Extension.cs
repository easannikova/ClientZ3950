using ConsoleApp4.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Extension
{
    public static class Extension
    {
        public static string ToQueryString(this Field field)
        {
            switch (field)
            {
                case Field.ISBN:
                    return field.ToString().ToUpper();
                case Field.CategoryId:
                    return "categoryId";
            }

            return field.ToString().ToLower();
        }
    }
}
