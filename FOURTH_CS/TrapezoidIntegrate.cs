using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using FIRST_CS;

namespace FOURTH_CS
{
    class TrapezoidIntegrate
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

            var sum = new Fraction(0, 1);
            for (var i = 0; i <= n; i++)
            {
                var x1 = a + new Fraction(i, 1) * h;
                var x2 = a + new Fraction(i+1, 1) * h;
                sum += ((function(x1) + function(x2))/2)*h;
            }

            stopWatch.Stop();
            timeElapsed = stopWatch.Elapsed;
            return sum;
        }

    }
}
