using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIXTH_SC
{
    class HeapSortStrategy : ISortableArray
    {
        public bool SortArray<T>(out List<T> result, ref List<T> arrayToSort, SortOrder order, IComparer<T> comparer, List<object> args)
        {
            if (order == SortOrder.BYINCREASING)
            {
                // Построение кучи (перегруппируем массив)
                for (int i = arrayToSort.Count / 2 - 1; i >= 0; i--)
                    MakeHeapMax(arrayToSort, arrayToSort.Count, i, comparer);

                // Один за другим извлекаем элементы из кучи
                for (int i = arrayToSort.Count - 1; i >= 0; i--)
                {
                    // Перемещаем текущий корень в конец
                    Swap(arrayToSort, 0, i);

                    // вызываем процедуру heapify на уменьшенной куче
                    MakeHeapMax(arrayToSort, i, 0, comparer);
                }

                result = arrayToSort;
                return true;
            } else
            {
                // Построение кучи (перегруппируем массив)
                for (int i = arrayToSort.Count / 2 - 1; i >= 0; i--)
                    MakeHeapMin(arrayToSort, arrayToSort.Count, i, comparer);

                // Один за другим извлекаем элементы из кучи
                for (int i = arrayToSort.Count - 1; i >= 0; i--)
                {
                    // Перемещаем текущий корень в конец
                    Swap(arrayToSort, 0, i);

                    // вызываем процедуру heapify на уменьшенной куче
                    MakeHeapMin(arrayToSort, i, 0, comparer);
                }

                result = arrayToSort;
                return true;
            }

        }

        // Процедура для преобразования в двоичную кучу поддерева с корневым узлом i, что является
        // индексом в arr[]. n - размер кучи
        private static void MakeHeapMax<T>(List<T> arrayToSort, int n, int i, IComparer<T> comparer)//, new() WTF???!!!
        {
            int largest = i;
            // Инициализируем наибольший элемент как корень
            int l = 2 * i + 1; // левый = 2*i + 1
            int r = 2 * i + 2; // правый = 2*i + 2

            // Если левый дочерний элемент больше корня
            if (l < n && (comparer.Compare(arrayToSort[l], arrayToSort[largest]) > 0))
                largest = l;

            // Если правый дочерний элемент больше, чем самый большой элемент на данный момент
            if (r < n && (comparer.Compare(arrayToSort[r], arrayToSort[largest]) > 0))
                largest = r;

            // Если самый большой элемент не корень
            if (largest != i)
            {
                Swap(arrayToSort, i, largest);

                // Рекурсивно преобразуем в двоичную кучу затронутое поддерево
                MakeHeapMax(arrayToSort, n, largest, comparer);
            }
            
        }
        private static void MakeHeapMin<T>(List<T> arrayToSort, int n, int i, IComparer<T> comparer)//, new() WTF???!!!
        {
            int min = i;
            // Инициализируем наибольший элемент как корень
            int l = 2 * i + 1; // левый = 2*i + 1
            int r = 2 * i + 2; // правый = 2*i + 2

            
            // Если левый дочерний элемент больше корня
            if (l < n && (comparer.Compare(arrayToSort[l], arrayToSort[min]) < 0))
                min = l;

            // Если правый дочерний элемент больше, чем самый большой элемент на данный момент
            if (r < n && (comparer.Compare(arrayToSort[r], arrayToSort[min]) < 0))
                min = r;

            // Если самый большой элемент не корень
            if (min != i)
            {
                Swap(arrayToSort, i, min);

                // Рекурсивно преобразуем в двоичную кучу затронутое поддерево
                MakeHeapMin(arrayToSort, n, min, comparer);
            }

        }
        private static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}
