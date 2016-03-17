using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Helpers
{
    public class ComparableArray<T> : IComparable where T : IComparable 
    {
        public T[] Array { get; set; }

        public int CompareTo(object obj)
        {
            var arr = (obj as ComparableArray<T>);
            if (arr == null)
                return -2;

            if (arr.Array.Length != Array.Length)
                return -3;

            for (int i = 0; i < arr.Array.Length; i++)
            {
                if (Array[i].CompareTo(arr.Array[i]) != 0)
                    return -1;
            }

            return 0;
        }

    }
}
