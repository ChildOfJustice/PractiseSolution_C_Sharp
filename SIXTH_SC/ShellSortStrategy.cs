using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIXTH_SC
{
    class ShellSortStrategy : ISortableArray
    {
        public bool SortArray<T>(out List<T> result, ref List<T> arrayToSort, SortOrder order, IComparer<T> comparer, List<object> args)
        {
            if(order == SortOrder.BYINCREASING)
            {
                int j;
                int step = arrayToSort.Count / 2;
                while (step > 0)
                {
                    for (int i = 0; i < (arrayToSort.Count - step); i++)
                    {
                        j = i;
                        while ((j >= 0) && (comparer.Compare(arrayToSort[j], arrayToSort[j + step]) > 0))
                        {
                            var tmp = arrayToSort[j];
                            arrayToSort[j] = arrayToSort[j + step];
                            arrayToSort[j + step] = tmp;
                            j -= step;
                        }
                    }
                    step = step / 2;
                }
                result = arrayToSort;
                return true;
            } else
            {
                int j;
                int step = arrayToSort.Count / 2;
                while (step > 0)
                {
                    for (int i = 0; i < (arrayToSort.Count - step); i++)
                    {
                        j = i;
                        while ((j >= 0) && (comparer.Compare(arrayToSort[j], arrayToSort[j + step]) < 0))
                        {
                            var tmp = arrayToSort[j];
                            arrayToSort[j] = arrayToSort[j + step];
                            arrayToSort[j + step] = tmp;
                            j -= step;
                        }
                    }
                    step = step / 2;
                }
                result = arrayToSort;
                return true;
            }
        }


        void shellSort(int[] arr)
        {
            int j;
            int step = arr.Length / 2;
            while (step > 0)
            {
                for (int i = 0; i < (arr.Length - step); i++)
                {
                    j = i;
                    while ((j >= 0) && (arr[j] > arr[j + step]))
                    {
                        int tmp = arr[j];
                        arr[j] = arr[j + step];
                        arr[j + step] = tmp;
                        j -= step;
                    }
                }
                step = step / 2;
            }
        }
    }
}
