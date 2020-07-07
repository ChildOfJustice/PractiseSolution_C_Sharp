using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIRST_CS;



namespace FOURTH_CS
{
    interface IIntegratable
    {
        Fraction Integrate(Func<Fraction, Fraction> function, Fraction a, Fraction b, Fraction eps, string methodName, out TimeSpan timeElapsed, out int iterations);
        
    }
}
