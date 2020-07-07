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
            var eps = new Fraction(1, 10000);
            var fr1 = new Fraction(8, 10);
            var fr2 = new Fraction(-1, 1);
            var n = 1;
            var fr3 = Fraction.PowFraction(fr1, 2 * n + 1) / (2 * n + 1);
            try
            {  
                Console.WriteLine((Fraction.PowFraction(fr1, 5)).ToDecimalFractionString(10));
                Console.WriteLine((fr3.GetSquareRootNewTon(eps)).ToDecimalFractionString(100));
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
