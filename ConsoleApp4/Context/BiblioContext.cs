using ConsoleApp4.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Context
{
    public class BiblioContext : DbContext
    {

        public BiblioContext() : base()
        {

        }

        public BiblioContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}
