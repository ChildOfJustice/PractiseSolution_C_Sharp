using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FIRST_CS;

namespace FIFTH_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var fraction = new Fraction(-100, 200);

            Console.WriteLine(fraction.Denominator == (BigInteger)(-3));

            try
            {
                var validator = new Validator<Fraction>.ValidatorBuilder()
                    .AddRule((obj) => (obj.Denominator % obj.Numerator) == 0)
                    .AddRule((obj) => obj.Denominator > (BigInteger)(100))
                    .GetValidator();
                
                validator.Validate(fraction);

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


            Console.ReadLine();
        }
    }
}
