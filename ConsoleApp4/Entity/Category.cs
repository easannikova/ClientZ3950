using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public int CatId { get; set; }

        public string Name { get; set; }

        public Category Parent { get; set; }
    }
}
