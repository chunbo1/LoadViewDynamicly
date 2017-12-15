#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions.Statistics
{
    public static class StatisticalExtensions
    {
        public static double? Median<TItem>(this IEnumerable<TItem> items, Func<TItem, double?> valueSelector)
        {
            if (!items.Any())
                return null;

            var @count = items.Count();
            var @itemIndex = @count / 2;
            double? @median;
            var sortedItems = items.OrderBy(valueSelector);
            if (@count % 2 == 0)
            {
                @median = (valueSelector(sortedItems.ElementAt(@itemIndex)) + valueSelector(sortedItems.ElementAt(@itemIndex - 1))) / 2;
            }
            else
            {
                @median = valueSelector(sortedItems.ElementAt(@itemIndex));
            }
            return @median;
        }

        public static double? StandardDeviation<TItem>(this IEnumerable<TItem> items, Func<TItem, double> valueSelector, bool annualize)
        {
            if (!items.Any())
                return null;

            var avg = items.Average(valueSelector);
            var stDev = items.Aggregate(0D, (acc, cur) => acc + Math.Pow((valueSelector(cur) - avg), 2),
                                        result => Math.Sqrt(result / (items.Count() - 1)) * Math.Sqrt(12));

            return stDev;
        }

        public static double? CumulativeReturn<TItem>(this IEnumerable<TItem> items, Func<TItem, double> valuSelector, bool annualize)
        {
            if (!items.Any())
                return null;

            var output = items.Aggregate(1D, (acc, cur) => acc*(1D + valuSelector(cur)), result => result - 1D);

            if (annualize)
                output = output.Annualize(12, items.Count());

            return output;
        }
    }
}
#pragma warning restore 1591