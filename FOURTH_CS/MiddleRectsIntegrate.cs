﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using FIRST_CS;

namespace FOURTH_CS
{
    class MiddleRectsIntegrate : IIntegratable
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

            var x = a += h / 2;

            for (var i = 0; i < n; i++)
            {
                sum += function(x);
                x += h;
            }

            var result = h * sum;
            stopWatch.Stop();
            timeElapsed = stopWatch.Elapsed;
            return result;
        }

       
    }
}
