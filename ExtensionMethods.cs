using System;
using System.Collections.Generic;

namespace Alphabet
{
    public static class ExtensionMethods
    {
        public static IEnumerable<(int, int)> FindIndices<T>(this T[,] source, Func<T, bool> predicate)
        {
            for (var r = 0; r < source.GetLength(0); r++)
            {
                for (var c = 0; c < source.GetLength(1); c++)
                {
                    if (predicate.Invoke(source[r, c]))
                    {
                        yield return (r, c);
                    }
                }
            }
        }
    }
}