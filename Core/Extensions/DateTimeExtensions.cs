#pragma warning disable 1591

using System;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date)
        {
            var lDays = date.DayOfWeek - DayOfWeek.Sunday;
            return date.AddDays(-1 * lDays);
        }

        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime StartOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static DateTime EndOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        public static DateTime PriorEndOfMonth(this DateTime date)
        {
            return date.StartOfMonth().AddDays(-1);
        }

        public static DateTime Previous(this DateTime date, DayOfWeek dayOfWeek)
        {
            var lDays = date.DayOfWeek - dayOfWeek;
            if (lDays <= 0)
                lDays = 7 + lDays;
            return date.AddDays(-1 * lDays);
        }

        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            var lDays = dayOfWeek - date.DayOfWeek;
            if (lDays <= 0)
                lDays = 7 + lDays;
            return date.AddDays(lDays);
        }

        public static bool IsWeekday(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        public static bool IsWeekend(this DateTime date)
        {
            return !date.IsWeekday();
        }

        public static bool IsLeapYear(this DateTime date)
        {
            return (date.Year % 400 == 0 || (date.Year % 4 == 0 && date.Year % 100 != 0));
        }

        public static DateTime PriorQuarterEnd(this DateTime date)
        {
            if (date.Month <= 3)
                return new DateTime(date.Year - 1, 12, 31);
            if (date.Month <= 6)
                return new DateTime(date.Year, 3, 31);
            return date.Month <= 9 ? new DateTime(date.Year, 6, 30) : new DateTime(date.Year, 9, 30);
        }

        public static bool IsEndOfMonth(this DateTime date)
        {
            return date.Equals(date.EndOfMonth());
        }

        public static bool IsEndOfQuarter(this DateTime date)
        {
            return (date.Day == 31 && date.Month == 3) ||
                   (date.Day == 30 && date.Month == 6) ||
                   (date.Day == 30 && date.Month == 9) ||
                   (date.Day == 31 && date.Month == 12);
        }

        public static bool IsEndOfYear(this DateTime date)
        {
            return date.Equals(date.EndOfYear());
        }

        public static bool IsStartOfMonth(this DateTime date)
        {
            return date.Day == 1;
        }

        public static int Days360(this DateTime startDate, DateTime endDate, bool preserveExcelCompatibility = true)
        {
            var day_a = startDate.Day;
            var day_b = endDate.Day;

            if ((startDate.Month == 2 && startDate.IsEndOfMonth()) &&
                (endDate.Month == 2 && endDate.IsEndOfMonth()) && !preserveExcelCompatibility)
                day_b = 30;

            if (day_a == 31 || (startDate.Month == 2 && startDate.IsEndOfMonth()))
                day_a = 30;

            if (day_a == 30 && day_b == 31)
                day_b = 31;

            var days = (endDate.Year - startDate.Year) * 360 +
                       (endDate.Month - startDate.Month) * 30 +
                       (day_b - day_a);
            return days;
        }
    }
}
#pragma warning restore 1591
