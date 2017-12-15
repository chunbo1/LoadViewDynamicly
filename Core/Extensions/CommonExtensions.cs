using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class CommonExtensions
    {

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Iterate<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            var lInput = enumerable;
            foreach (var item in lInput)
            {
                var lTemp = item;
                action(lTemp);
            }
            return lInput;
        }

        public static IEnumerable<T> Iterate<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            var lInput = enumerable;
            var index = 0;
            foreach (var item in lInput)
            {
                var lTemp = item;
                action(lTemp, index++);
            }
            return lInput;
        }

        //public static IEnumerable<TElement> In<TElement, TValue>(this IEnumerable<TElement> inputs,
        //                                                 Func<TElement, TValue> valueSelector,
        //                                                 IEnumerable<TValue> values)
        //{
        //    var predicate = PredicateBuilder.False<TElement>();

        //    foreach (var lValue in values)
        //    {
        //        var ltemp = lValue;
        //        predicate = predicate.Or(p => valueSelector(p).Equals(ltemp));
        //    }
        //    return inputs.Where(predicate.Compile());
        //}

        /// <summary>
        /// Returns Minimum of the two objects. 
        /// </summary>
        /// <typeparam name="T">must implement IComparable</typeparam>
        /// <param name="object1">Object 1</param>
        /// <param name="object2">Object 2</param>
        /// <returns>Return Value</returns>
        public static T Min<T>(this T object1, T object2) where T : IComparable<T>
        {
            return object1.CompareTo(object2) <= 0 ? object1 : object2;
        }

        /// <summary>
        /// Returns Maximum of the two objects. 
        /// </summary>
        /// <typeparam name="T">must implement IComparable</typeparam>
        /// <param name="object1">Object 1</param>
        /// <param name="object2">Object 2</param>
        /// <returns>Return Value</returns>
        public static T Max<T>(this T object1, T object2) where T : IComparable<T>
        {
            return object1.CompareTo(object2) <= 0 ? object2 : object1;
        }

        public static TResult Coalesce<TInput, TResult>(this TInput obj, Func<TInput, TResult> func)
        {
            return obj.Coalesce(func, default(TResult));
        }

        public static TResult Coalesce<TInput, TResult>(this TInput obj, Func<TInput, TResult> func, TResult defaultValue)
        {
            if (Equals(obj, default(TInput)))
                return defaultValue;
            var @result = func(obj);
            return Equals(@result, default(TInput)) ? defaultValue : @result;
        }

        public static T InitIfNull<T>(this T entity, Action<T> action) where T : new()
        {
            if (!Equals(entity, default(T)))
                return entity;

            var lNew = new T();
            action(lNew);
            return lNew;
        }

        public static IEnumerable<TResult> FullOuterJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
                                                                                IEnumerable<TInner> inner,
                                                                                Func<TOuter, TKey> outerKeySelector,
                                                                                Func<TInner, TKey> innerKeySelector,
                                                                                Func<TOuter, TInner, TResult> resultSelector)
        {
            var lExact = outer.Join(inner, outerKeySelector, innerKeySelector, (o, i) => new Tuple<TOuter, TInner>(o, i));

            var lLeft = from o in outer
                        from i in inner.Where(arg => innerKeySelector(arg).Equals(outerKeySelector(o))).DefaultIfEmpty()
                        where Equals(i, default(TInner))
                        select new Tuple<TOuter, TInner>(o, default(TInner));

            var lRight = from i in inner
                         from o in outer.Where(arg => outerKeySelector(arg).Equals(innerKeySelector(i))).DefaultIfEmpty()
                         where Equals(o, default(TOuter))
                         select new Tuple<TOuter, TInner>(default(TOuter), i);

            return lExact.Concat(lLeft).Concat(lRight).Select(arg => resultSelector(arg.Item1, arg.Item2));
        }


        public static IEnumerable<TU> Rank<T, TKey, TU>(this IEnumerable<T> source, Func<T, TKey> keySelector, bool sortAscending, Func<T, int, TU> selector)
        {
            if (!source.Any())
            {
                yield break;
            }

            var itemCount = 0;
            var ordered = sortAscending ? source.OrderBy(keySelector).ToArray() : source.OrderByDescending(keySelector).ToArray();
            var previous = keySelector(ordered[0]);
            var rank = 1;
            foreach (T t in ordered)
            {
                itemCount += 1;
                var current = keySelector(t);
                if (!current.Equals(previous))
                {
                    rank = itemCount;
                }
                yield return selector(t, rank);
                previous = current;
            }
        }

        //public static IEnumerable<TSource> Distinct<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> comparer)
        //{
        //    return source.Distinct(new DynamicComparer<TSource, TResult>(comparer));
        //}

        public static string ToCsv<T, U>(this IEnumerable<T> source, Func<T, U> func)
        {
            try
            {
                if (source == null)
                    return "<null>";
                if (!source.Any())
                    return "<empty>";
                return string.Join(",", source.Select(func)
                                              .Where(it => !it.Equals(default(U)))
                                              .Select(it => it.ToString()).ToArray());
            }
            catch (Exception ex)
            {
                var message = new StringBuilder("SFMAppServices Exception Report");
                message.AppendLine(ex.Message);
                message.AppendLine(ex.StackTrace);
                //message.ToString().Log();
                return string.Empty;
            }
        }

        public static void SkipLogging(this Exception ex)
        {
            ex.Data.Add("SkipLogging", true);
        }




    }//Class
}
