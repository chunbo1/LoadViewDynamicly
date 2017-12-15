#pragma warning disable 1591

using System;

namespace Core.Extensions
{
    public static class IntegerExtensions
    {
        public static DateTime January(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 1))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 1, day);
        }

        public static DateTime February(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 2))
                throw new ArgumentException("day cannot exceed number of days in month"); 
            return new DateTime(year, 2, day);
        }

        public static DateTime March(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 3))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 3, day);
        }

        public static DateTime April(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 4))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 4, day);
        }

        public static DateTime May(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 5))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 5, day);
        }

        public static DateTime June(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 6))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 6, day);
        }

        public static DateTime July(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 7))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 7, day);
        }

        public static DateTime August(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 8))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 8, day);
        }

        public static DateTime September(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 9))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 9, day);
        }

        public static DateTime October(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 10))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 10, day);
        }

        public static DateTime November(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 11))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 11, day);
        }

        public static DateTime December(this int day, int year)
        {
            if (day > DateTime.DaysInMonth(year, 12))
                throw new ArgumentException("day cannot exceed number of days in month");
            return new DateTime(year, 12, day);
        }

        public static T As<T>(this int input)
        {
            var lType = typeof(T);
            return (T)Convert.ChangeType(input, lType);
        }

        public static bool IsInRange(this int input, int start, int end)
        {
            return input >= start && input <= end;
        }
    }
}

#pragma warning restore 1591
