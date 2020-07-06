using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIXTH_SC
{
    public enum SortOrder
    {
        BYINCREASING,
        BYDECREASING
    }

    interface ISortableArray
    {
        
        bool SortArray<T>(out List<T>result, ref List<T> arrayToSort, SortOrder order, IComparer<T> comparer, List<object>args);//params object[] //where T : IComparable<T>

    }
}
