using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PractiseSolution
{
    class Program
    {
        static void Main(string[] args){
            var obj = new object();//var like auto
            Console.Write(obj.GetType());

            var bigInt = new BigInteger();

            Figure f1;
            Figure f2;


            var var1 = "123s";
            var var2 = "123s";
            Console.Write(ReferenceEquals(var1, var2));//TRUE!!!
            Console.Write(ReferenceEquals(new object(), new object()));//FALSE!!!


            Figure[] figures = new Figure[5];



            Console.Read();
        }
    }
}
