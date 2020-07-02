using FIRST_CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECOND_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var comp1 = new Complex(new Fraction(2, 1), new Fraction(1, 1));
            var comp2 = new Complex(new Fraction(3, 1), new Fraction(1, 1));
            Console.WriteLine((comp1*comp2).ToString());

            Console.WriteLine(comp1.SqrtN(2).ToString());
            Console.WriteLine((comp1.SqrtN(2)* comp1.SqrtN(2)).ToString());

            Console.ReadLine();
        }
    }
}
