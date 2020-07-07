using FIRST_CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURTH_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan timeElapsed;
            int iterations;

            var lInt = new LeftRectsIntegrate();
            var rInt = new RightRectIntegrate();
            var mInt = new MiddleRectsIntegrate();
            var sympsInt = new SympsonIntegrate();
            var trapInt = new TrapezoidIntegrate();

            Console.WriteLine("Function is: x + 2");

            try
            {
                Console.WriteLine($"Left rects integral: {lInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10012), "LeftRects", out timeElapsed, out iterations).ToDecimalFractionString(7)} timeElapsed: {timeElapsed}  iterations:  {iterations}");
                Console.WriteLine("Right rects integral: " + rInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10033), "RightRects", out timeElapsed, out iterations).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed + " iterations: " + iterations);
                Console.WriteLine("Middle rects integral: " + mInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10050), "MiddleRects", out timeElapsed, out iterations).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed + " iterations: " + iterations);
                Console.WriteLine("Symps method integral: " + sympsInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10008), "MiddleRects", out timeElapsed, out iterations).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed + " iterations: " + iterations);
                Console.WriteLine("Trap integral: " + trapInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10100), "MiddleRects", out timeElapsed, out iterations).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed + " iterations: " + iterations);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
