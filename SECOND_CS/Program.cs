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
        //тейлор

        static void Main(string[] args)
        {
            var comp1 = new Complex(new Fraction(2, 1), new Fraction(1, 1));
            var comp2 = new Complex(new Fraction(3, 1), new Fraction(1, 1));
            Console.WriteLine((comp1*comp2).ToString());

            Console.WriteLine((comp1).ToString());
            Console.WriteLine((comp1.SqrtN(2)[0]* comp1.SqrtN(2)[0]).ToString());
            foreach (var el in comp1.SqrtN(3))
            {
                Console.WriteLine(el.ToString());
            }
            //Console.WriteLine((comp1.SqrtN(2)[0]* comp1.SqrtN(2)[0]).ToString());

            Console.ReadLine();
        }
    }
}
