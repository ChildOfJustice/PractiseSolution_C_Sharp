using FIRST_CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;


namespace FOURTH_CS
{
    class LeftRectsIntegrate : IIntegratable
    {

        public Fraction Integrate(Func<Fraction, Fraction> function, Fraction a, Fraction b, Fraction eps, string methodName, out TimeSpan timeElapsed, out int iterations)
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
            iterations = n;


            var sum = new Fraction(0, 1);
            for (var i = 0; i <= n - 1; i++)
            {
                var x = a + new Fraction(i,1) * h;
                sum += function(x);
            }

            var result = h * sum;
            stopWatch.Stop();
            timeElapsed = stopWatch.Elapsed;
            return result;
        }
    }
}
