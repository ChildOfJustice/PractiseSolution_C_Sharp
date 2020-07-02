using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIRST_CS;


namespace THIRD_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DihSearch(0, 3, (x) => x - new Fraction(2, 1), new Fraction(1, 1000000)).ToDecimalFractionString(5));

            Console.ReadLine();
        }

        public static Fraction DihSearch(Fraction a, Fraction b, Func<Fraction, Fraction> function, Fraction epsilon)
        {
            if ((function(a)).IsZero())
            {
                return a;
            }
            if (function(b).IsZero())
            {
                return b;
            }
            //Console.WriteLine((function(a) * function(b)).ToString());
            if ((function(a) * function(b)).Sign > 0)
            {
                throw new Exception("There is no root here");
            }
            var leftEdge = a;
            var rightEdge = b;
            var newEdge = (rightEdge - leftEdge) / 2;

            while (rightEdge - leftEdge > epsilon)
            {
                
                //Console.WriteLine($"iter_ {leftEdge};{rightEdge}, middle {newEdge} !!! {function(rightEdge)} {function(newEdge)}");


                if ((function(newEdge)).IsZero())
                {
                    return leftEdge + newEdge;
                }
                if ((function(rightEdge) * function(leftEdge + newEdge)).Sign > 0)
                {
                    rightEdge -= newEdge;
                } else
                {
                    leftEdge += newEdge;
                }
                
                newEdge = (rightEdge - leftEdge) / 2;
            }
            return leftEdge + (rightEdge - leftEdge) / 2;
        }
        #region omg
        public static Fraction DihSearch(int a, int b, Func<Fraction, Fraction> function, Fraction epsilon)
        {
            return DihSearch(new Fraction(a, 1), new Fraction(b, 1), function, epsilon);
        }
        public static Fraction DihSearch(int a, Fraction b, Func<Fraction, Fraction> function, Fraction epsilon)
        {
            return DihSearch(new Fraction(a, 1), b, function, epsilon);
        }
        public static Fraction DihSearch(Fraction a, int b, Func<Fraction, Fraction> function, Fraction epsilon)
        {
            return DihSearch(a, new Fraction(b, 1), function, epsilon);
        }
        #endregion
    }
}
