using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Logic
{
    public class Filter
    {
        private string _query;
        private Field _field;

        public Filter(string query, Field field)
        {
            _query = query;
            _field = field;
        }

        public Field Field
        {
            get => _field;
            set => _field = value;
        }
        public string Query
        {
            get => _query;
            set => _query = value;
        }
    }
}
