using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using FIRST_CS;

namespace FOURTH_CS
{
    class SympsonIntegrate
    {
        public Fraction Integrate(Func<Fraction, Fraction> function, Fraction a, Fraction b, Fraction eps, string methodName, out TimeSpan timeElapsed)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int n = 1;
            var h = (b - a) / n;
            while (h > eps)
            {
                n++;
                h = (b - a) / n;
            }


            var sum1 = new Fraction(0, 1);
            var sum2 = new Fraction(0, 1);
            for (var k = 1; k <= n; k++)
            {
                var xk = a + new Fraction(k, 1) * h;
                if (k <= n - 1)
                {
                    sum1 += function(xk);
                }

                var xk_1 = a + new Fraction(k - 1, 1) * h;
                sum2 += function((xk + xk_1) / 2);
            }

            var result = h / 3 * (new Fraction(1, 2) * function(a) + sum1 + new Fraction(2,1) * sum2 + new Fraction(1, 2) * function(b));
            
            stopWatch.Stop();
            timeElapsed = stopWatch.Elapsed;
            return result;
        }

    }
}
