#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Core.Extensions
{
    public class GroupResult<TItem, TKey, TOutput>
    {
        public TKey Key { get; set; }
        public int Count { get; set; }
        public IEnumerable<TItem> Items { get; set; }
        public IEnumerable<GroupResult<TItem, TKey, TOutput>> SubGroups { get; set; }
        public TOutput Result { get; set; } 

        public override string ToString()
        {
            return string.Format("{0} ({1})", Key, Count);
        }
    }

    public static class CollectionExtensions
    {
        //public static ObservableCollection<TEntity> AsObservable<TEntity>(this EntityCollection<TEntity> enumerable) where TEntity : class, IEntityWithRelationships
        //{
        //    return new ObservableCollection<TEntity>(enumerable);
        //}

        public static ObservableCollection<TEntity> AsObservable<TEntity>(this IEnumerable<TEntity> query)
        {
            return new ObservableCollection<TEntity>(query);
        }

        public static BindingList<TEntity> AsBindingList<TEntity>(this IEnumerable<TEntity> entity)
        {
            return new BindingList<TEntity>(entity.ToList());
        }

        public static Lazy<IEnumerable<T>> AsLazy<T>(this IEnumerable<T> inputs)
        {
            return new Lazy<IEnumerable<T>>(() => inputs, true);
        }

        public static void Merge<T, U>(this ICollection<T> items, ICollection<T> newItems, Func<T, U> keySelector, Action<T, T> updateFunc)
        {
            var newRows = from newItem in newItems
                          where !items.Any(oldItem => keySelector(oldItem).Equals(keySelector(newItem)))
                          select newItem;

            foreach (var exp in newRows)
                items.Add(exp);

            var deletedRows = from oldItem in items
                              where !newItems.Any(newItem => keySelector(newItem).Equals(keySelector(oldItem)))
                              select oldItem;

            foreach (var delItem in deletedRows.ToArray())
            {
                items.Remove(delItem);
            }

            var query = from newItem in newItems
                        join currItem in items on keySelector(newItem) equals keySelector(currItem)
                        select new { newItem, currItem };

            foreach (var item in query)
            {
                updateFunc(item.currItem, item.newItem);
            }
        }

        public static IEnumerable<GroupResult<TElement, TKey, TResult>> GroupByMany<TElement, TKey, TResult>(
                    this IEnumerable<TElement> elements, Func<IEnumerable<TElement>, TResult> resultSelector, 
                    params Func<TElement, TKey>[] groupSelectors)
        {
            if (groupSelectors == null || groupSelectors.Length == 0)
                return null;
            var selector = groupSelectors.First();
            var nextSelectors = groupSelectors.Skip(1).ToArray();
            return elements.GroupBy(selector)
                           .Select(g => new GroupResult<TElement, TKey, TResult>
                               {
                                   Key = g.Key,
                                   Count = g.Count(),
                                   Items = g,
                                   SubGroups = g.GroupByMany(resultSelector, nextSelectors),
                                   Result =  resultSelector(g)
                               });

        }
    }
}

#pragma warning restore 1591
