#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string input)
        {
            return String.IsNullOrEmpty(input);
        }

        public static bool IsNullOrEmptyOrWhitespace(this string input)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);
        }

        public static bool EqualsIgnoreCase(this string input, string target)
        {
            return (input.IsNullOrEmptyOrWhitespace() && target.IsNullOrEmptyOrWhitespace()) ||
                   (!input.IsNullOrEmptyOrWhitespace() && input.Equals(target, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsNullOrEmptyOrError(this string input)
        {
            return String.IsNullOrEmpty(input) || input.ToUpper().StartsWith("#");
        }

        public static string DefaultValue(this string input, string defaultValue)
        {
            return String.IsNullOrEmpty(input) ? defaultValue : input;
        }

        public static bool ContainsIgnoreCase(this string input, string other)
        {
            return input.ToLower().Contains(other.ToLower());
        }

        public static IEnumerable<string> Slice(this string input, string delim)
        {
            return input.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        public static string Repeat(this string input, int count)
        {
            return String.Concat(Enumerable.Repeat(input, count).ToArray());
        }

        public static long? AsLong(this string input)
        {
            long lVal;
            if (!long.TryParse(input, out lVal))
                return null;
            return lVal;
        }

        public static double? AsDouble(this string input)
        {
            double lVal;
            if (!double.TryParse(input, out lVal))
                return null;
            return lVal;
        }

        public static bool AsBoolean(this string input)
        {
            bool lVal;
            return bool.TryParse(input, out lVal) && lVal;
        }

        public static int? AsInteger(this string input)
        {
            int lVal;
            if (!int.TryParse(input, out lVal))
                return null;
            return lVal;
        }

        public static DateTime? AsDateTime(this string input)
        {
            DateTime lVal;
            if (!DateTime.TryParse(input, out lVal))
                return null;
            return lVal;
        }

        public static string Format(this string input, params object[] args)
        {
            return String.Format(input, args);
        }
    }
}

#pragma warning restore 1591
