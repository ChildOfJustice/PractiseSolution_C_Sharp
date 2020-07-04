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
            try
            {
                Console.WriteLine(DihSearch(2d, 4d, (x) => (Math.Tan(x / 4) - 1), 1e-10));


            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        public static double DihSearch(double a, double b, Func<double, double> function, double eps)
        {
            
            if (a > b)
            {
                throw new Exception("wrong interval edges");
            }
            //Console.WriteLine((function(a) * function(b)).ToString());
            if ((function(a) * function(b)) > 0)
            {
                throw new Exception("Cannot find the root here");
            }
            var leftEdge = a;
            var rightEdge = b;
            var newEdge = (rightEdge - leftEdge) / 2;

            while (rightEdge - leftEdge > eps)
            {
                
                //Console.WriteLine($"iter_ {leftEdge};{rightEdge}, middle {newEdge} !!! {function(rightEdge)} {function(newEdge)}");


                
                if ((function(rightEdge) * function(leftEdge + newEdge)) > 0)
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
        
    }
}
