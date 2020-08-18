using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class FiatPunto
    {
        public string boja;

        public FiatPunto(string boja)
        {
            this.boja = boja;
        }
    }

    class Program
    {  
        static void Main(string[] args)
        {
            FiatPunto f1 = new FiatPunto("Plavi");
            FiatPunto f2 = new FiatPunto("Plavi");

            string s = "plava";
            string t = "plava";

            Console.WriteLine(f1 == f2);
            Console.WriteLine(s == t);


            Console.ReadLine();
        }
    }
}
