using System;
using System.Collections.Generic;

namespace LINQOperations.Extensions
{
    public static class EnumerableExtension
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var item in source)
            {
                if (!predicate.Invoke(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Count() > 0;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Any())
            {
                foreach (var item in source)
                {
                    if (predicate.Invoke(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var result = new TSource[source.Count() + 1];
            Array.Copy(source.ToArray(), result, source.Count());
            result[result.Length - 1] = element;

            return result;
        }

        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            var index = 0;
            var concat = new TSource[first.Count() + second.Count()];

            foreach (var item in first)
            {
                concat[index++] = item;
            }

            foreach (var item in second)
            {
                concat[index++] = item;
            }

            return concat;
        }

        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var count = 0;

            foreach (var item in source)
            {
                count++;
            }

            return count;
        }

        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, Index index)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            var count = 0;
            foreach (var item in source)
            {
                if (index.Value == count++)
                {
                    return item;
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            var count = 0;
            foreach (var item in source)
            {
                if (count == index)
                {
                    return item;
                }
            }

            return default(TSource);
        }

        public static IEnumerable<TSource> Empty<TSource>()
        {
            return new TSource[0];
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            return source.ElementAtOrDefault(0);
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null || predicate is null)
            {
                throw new ArgumentException(source is null ? nameof(source) : nameof(predicate));
            }

            foreach (var item in source)
            {
                if (predicate.Invoke(item))
                {
                    return item;
                }
            }

            return default(TSource);
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            if (source.Any())
            {
                return source.ElementAt(0);
            }

            throw new InvalidOperationException($"Sequence contains no matching element ${nameof(source)}");
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null || predicate is null)
            {
                throw new ArgumentException(source is null ? nameof(source) : nameof(predicate));
            }

            foreach (var item in source)
            {
                if (predicate.Invoke(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException($"Sequence contains no matching element ${nameof(source)} {nameof(predicate)}");
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if ( first is null || second is null)
            {
                throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
            }

            var matchList = new List<TSource>();
            foreach(var item1 in first)
            {
                foreach (var item2 in second)
                {
                    if(Object.Equals(item1, item2))
                    {
                        matchList.Add(item1);
                    }
                }
            }

            return matchList;
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            var matchList = new List<TSource>();
            foreach (var item1 in first)
            {
                foreach (var item2 in second)
                {
                    if (comparer.Equals(item1, item2))
                    {
                        matchList.Add(item1);
                    }
                }
            }

            return matchList;
        }

        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            return source.ElementAtOrDefault(source.Count() - 1);
        }

        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null || predicate is null)
            {
                throw new ArgumentException(source is null ? nameof(source) : nameof(predicate));
            }

            TSource lastMatch = default(TSource);
            foreach (var item in source)
            {
                if (predicate.Invoke(item))
                {
                    lastMatch = item;
                }
            }

            return lastMatch;
        }

        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            if (!source.Any())
            {
                throw new InvalidOperationException("Source is empty");
            }

            return source.ElementAt(source.Count() - 1);
        }

        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null || predicate is null)
            {
                throw new ArgumentException(source is null ? nameof(source) : nameof(predicate));
            }

            if (!source.Any())
            {
                throw new InvalidOperationException("Source is empty");
            }

            TSource lastMatch = default(TSource);
            bool isFound = false;
            foreach (var item in source)
            {
                if (predicate.Invoke(item))
                {
                    lastMatch = item;
                    isFound = true;
                }
            }

            if (isFound)
            {
                throw new InvalidOperationException($"Sequence contains no matching element ${nameof(source)} {nameof(predicate)}");
            }

            return lastMatch;
        }

        public static IEnumerable<TSource> Range<TSource>(int start, int end)
        {
            if (end - start < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new TSource[end - start];
        }

        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            if(source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var count = source.Count();
            var reverse = new TSource[count];

            foreach (var item in source)
            {
                reverse[--count] = item;
            }

            return reverse;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            var resultArr = new List<TResult>();
            foreach (var item in source)
            {
                var result = selector.Invoke(item);
                resultArr.Add(result);
            }

            return resultArr;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            var resultArr = new List<TResult>();
            var index = 0;
            foreach (var item in source)
            {
                var result = selector.Invoke(item, index);
                resultArr.Add(result);
            }

            return resultArr;
        }

        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            var array = new TSource[source.Count()];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = source.ElementAt(i);
            }

            return array;
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentException(nameof(source));
            }

            return new List<TSource>(source);
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null || predicate is null)
            {
                throw new ArgumentException(source is null ? nameof(source) : nameof(predicate));
            }

            var matches = new List<TSource>();

            foreach (var item in source)
            {
                var hasMatch = predicate.Invoke(item);

                if (hasMatch)
                {
                    matches.Add(item);
                }
            }

            return matches;
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source is null || predicate is null)
            {
                throw new ArgumentException(source is null ? nameof(source) : nameof(predicate));
            }

            var matches = new List<TSource>();
            var index = 0;

            foreach (var item in source)
            {
                var hasMatch = predicate.Invoke(item, index++);

                if (hasMatch)
                {
                    matches.Add(item);
                }
            }

            return matches;
        }

    }
}
