using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Extensions
    {
        public static List<T> ShuffledCopy<T>(this IEnumerable<T> list)
        {
            List<T> copy = new List<T>(list);
            int n = copy.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (copy[k], copy[n]) = (copy[n], copy[k]);
            }
            return copy;
        }
    }
}
