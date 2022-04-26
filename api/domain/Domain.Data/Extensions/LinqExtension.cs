using System;
using System.Collections.Generic;

namespace Domain.Data.Extensions  
{
    public static class LinqExtension
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}
