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
        
        static double myAtan(double x, double eps)
        {
            
            var sum = 0d;
            var currentValue = 0d;
            var n = 1;
            do
            {
                currentValue = Math.Pow(-1.0, n - 1) * (Math.Pow(x, 2 * n - 1) / (2 * n - 1));
                sum += currentValue;
                n++;
            } while (Math.Abs(currentValue) > eps);
            return sum;
        }
        static void Main(string[] args)
        {
            var eps = new Fraction(1, 100);
            var test = true;
            if (test)
            {
                var comp1 = new Complex(new Fraction(2, 1), new Fraction(1, 1));
                var comp2 = new Complex(new Fraction(3, 1), new Fraction(1, 1));
                Console.WriteLine((comp1 * comp2).ToString());

                Console.WriteLine((comp1).ToString());
                Console.WriteLine((comp1.SqrtN(2, eps)[0] * comp1.SqrtN(2, eps)[0]).ToString());
                foreach (var el in comp1.SqrtN(3, eps))
                {
                    Console.WriteLine(el.ToString());
                }
                Console.WriteLine((comp1.SqrtN(2, eps)[0] * comp1.SqrtN(2, eps)[0]).ToString());
            }
            else
            {
                Console.WriteLine(Complex.AtanFraction(new Fraction(8,10),new Fraction(1,1000)).ToDouble());
                Console.WriteLine(Complex.CountPi(new Fraction(1, 100)).ToDouble());
                //Console.WriteLine(myAtan(0.8, 0.001));


            }
            Console.ReadLine();
        }
    }
}
