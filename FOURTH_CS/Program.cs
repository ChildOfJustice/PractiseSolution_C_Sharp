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

            var lInt = new LeftRectsIntegrate();
            var rInt = new RightRectIntegrate();
            var mInt = new MiddleRectsIntegrate();
            var sympsInt = new SympsonIntegrate();
            var trapInt = new TrapezoidIntegrate();

            Console.WriteLine(lInt.Integrate((x)=>x+ new Fraction(2, 1), new Fraction(-2,1), new Fraction(0, 1), new Fraction(1, 10000), "LeftRects", out timeElapsed).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed);
            Console.WriteLine(rInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10000), "RightRects", out timeElapsed).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed);
            Console.WriteLine(mInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10000), "MiddleRects", out timeElapsed).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed);
            Console.WriteLine(sympsInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10000), "MiddleRects", out timeElapsed).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed);
            Console.WriteLine(trapInt.Integrate((x) => x + new Fraction(2, 1), new Fraction(-2, 1), new Fraction(0, 1), new Fraction(1, 10000), "MiddleRects", out timeElapsed).ToDecimalFractionString(7) + " timeElapsed: " + timeElapsed);

            Console.ReadLine();
        }
    }
}
