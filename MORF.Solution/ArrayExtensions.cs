using System;
using System.Linq;

namespace MORF.Solution
{
    public static class ArrayExtensions
    {
        public static int[] GetMostCommonElements(this int[] array)
        {
            if (array == null) throw new ArgumentNullException("array");

            var countedElements = array.GroupBy(a => a).Select(g => new { Count = g.Count(), Element = g.Key });
            if (countedElements.Any())
            {
                return countedElements.Where(g => g.Count == countedElements.Max(c => c.Count))
                    .Select(g => g.Element)
                    .ToArray();
            }
            return new int[0];
        }
    }
}
