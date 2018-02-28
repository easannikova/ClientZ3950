using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Entity
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        public int OfferId { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public string Year { get; set; }

        public string ISBN { get; set; }

        public string Description { get; set; }

        public string Pages { get; set; }

        public Category Category { get; set; }

        //public string ToFormateString()
        //{
        //    return $"Автор: {Author}\nНазвание: {Title} \nИздатель: {Publisher}" +
        //        $"\nГод: {Year} \nОписание: {Description} \nISBN: {ISBN}";
        //}
    }
}
