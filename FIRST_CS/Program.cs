using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FIRST_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var fr1 = new Fraction(-3, 2);
            var fr2 = new Fraction(-1, 4);
            //Console.WriteLine((fr1 / fr2).ToString());
            //Console.WriteLine((fr1 / 2).ToString());

            //Console.WriteLine(((new Fraction(9, 4) - new Fraction(2, 1))*(new Fraction(3, 1) - new Fraction(2, 1))).ToString());

            //Console.WriteLine((fr2.getSquareRootNewTon(new Fraction(1, 10))).ToString());

            Console.WriteLine((fr1 - fr2).ToString());

            Console.ReadLine();
        }
    }
}
