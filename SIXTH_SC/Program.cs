using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIRST_CS;

namespace SIXTH_SC
{
    class Program
    {
        internal class Comparer1 : IComparer<Fraction>
        {
            public int Compare(Fraction x, Fraction y)
            {
                return x.CompareTo(y);
            }
        }
        internal class Comparer2 : IComparer<Fraction>
        {
            public int Compare(Fraction x, Fraction y)
            {
                if (x.Numerator > y.Numerator) return 1;
                if (x.Numerator < y.Numerator) return -1;
                return 0;
            }
        }

        static void Main(string[] args)
        {
            var heapSort = new HeapSortStrategy();
            var shellSort = new ShellSortStrategy();

            var comparer1 = new Comparer1();
            var comparer2 = new Comparer2();


            var fractions = new List<Fraction>();
            fractions.Add(new Fraction(1,2));
            fractions.Add(new Fraction(-1, 2));
            fractions.Add(new Fraction(1, 3));
            fractions.Add(new Fraction(10, 2));
            fractions.Add(new Fraction(11, 3));
            Console.WriteLine("Heap Sort Source:");
            foreach (var item in fractions)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine(); Console.WriteLine();


            var result = new List<Fraction>();

            ////////
            heapSort.SortArray(out result, ref fractions, SortOrder.BYINCREASING, comparer1, null);
            foreach (var item in result)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine();

            heapSort.SortArray(out result, ref fractions, SortOrder.BYINCREASING, comparer2, null);
            foreach (var item in result)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine();
            Console.WriteLine();

            //DECREASING
            heapSort.SortArray(out result, ref fractions, SortOrder.BYDECREASING, comparer1, null);
            foreach (var item in result)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine();

            heapSort.SortArray(out result, ref fractions, SortOrder.BYDECREASING, comparer2, null);
            foreach (var item in result)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine();

            //////
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            ////////
            var fractions2 = new List<Fraction>();
            fractions2.Add(new Fraction(1, 2));
            fractions2.Add(new Fraction(-1, 2));
            fractions2.Add(new Fraction(1, 3));
            fractions2.Add(new Fraction(10, 2));
            fractions2.Add(new Fraction(11, 3));
            Console.WriteLine("Shell Sort Source");
            foreach (var item in fractions2)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine(); Console.WriteLine();

            shellSort.SortArray(out result, ref fractions2, SortOrder.BYINCREASING, comparer1, null);
            foreach (var item in result)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine();

            shellSort.SortArray(out result, ref fractions2, SortOrder.BYINCREASING, comparer2, null);
            foreach (var item in result)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine();
            Console.WriteLine();

            //DECREASING
            shellSort.SortArray(out result, ref fractions2, SortOrder.BYDECREASING, comparer1, null);
            foreach (var item in result)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine();

            shellSort.SortArray(out result, ref fractions2, SortOrder.BYDECREASING, comparer2, null);
            foreach (var item in result)
            {
                Console.Write($"{item.ToString()} ");
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
