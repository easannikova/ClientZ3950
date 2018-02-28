using ConsoleApp4.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Старт поиска...\n");
            using (var reader = new Reader("1132527.xml"))
            {
                var filter = new Filter("В. А. Жуковский. Стихотворения и баллады", Field.Name);
                reader.FillGroup();

            }

            using (var reader = new Reader("1132527.xml"))
            {
                var filter = new Filter("В. А. Жуковский. Стихотворения и баллады", Field.Name);
                reader.FillOffer();

            }
            Console.WriteLine("\nПоиск окончен...");

            Console.ReadLine();
        }
    }
}
