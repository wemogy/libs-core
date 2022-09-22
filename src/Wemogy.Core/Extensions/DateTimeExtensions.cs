using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Wemogy.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToUtcString(this DateTime dateTime)
        {
            return dateTime.ToUnixEpochDate().ToString();
        }

        /// <returns>Date converted to milliseconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        public static long ToUnixEpochDate(this DateTime date)
            => date.ToDateTimeOffset().ToUnixTimeMilliseconds();

        public static DateTime FromUnixEpochDate(this long timestamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
        }

        /// <returns>The seconds since epoch</returns>
        public static long ToUnixTimeSeconds(this DateTime date)
        {
            return date.ToDateTimeOffset().ToUnixTimeSeconds();
        }

        public static DateTime FromUnixTimeSeconds(this long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
        }

        public static DateTime GetFirstDayOfWeek(this DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Tuesday:
                    dateTime = dateTime.Subtract(TimeSpan.FromDays(1));
                    break;
                case DayOfWeek.Wednesday:
                    dateTime = dateTime.Subtract(TimeSpan.FromDays(2));
                    break;
                case DayOfWeek.Thursday:
                    dateTime = dateTime.Subtract(TimeSpan.FromDays(3));
                    break;
                case DayOfWeek.Friday:
                    dateTime = dateTime.Subtract(TimeSpan.FromDays(4));
                    break;
                case DayOfWeek.Saturday:
                    dateTime = dateTime.Subtract(TimeSpan.FromDays(5));
                    break;
                case DayOfWeek.Sunday:
                    dateTime = dateTime.Subtract(TimeSpan.FromDays(6));
                    break;
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            var lastDayOfMonth = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            return lastDayOfMonth;
        }

        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            var numberOfDaysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            var lastDayOfMonth = new DateTime(dateTime.Year, dateTime.Month, numberOfDaysInMonth, 0, 0, 0, DateTimeKind.Utc);
            return lastDayOfMonth;
        }

        public static bool IsLastDayOfMonth(this DateTime dateTime)
        {
            return dateTime.GetLastDayOfMonth().Date == dateTime.Date;
        }

        public static bool IsDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            return dateTime.DayOfWeek == dayOfWeek;
        }

        public static bool IsFriday(this DateTime dateTime)
        {
            return dateTime.IsDayOfWeek(DayOfWeek.Friday);
        }

        public static int GetWeekOfYear(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.Calendar
                .GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static List<DateTime> GetLastDays(this DateTime dateTime, int numberOfDays)
        {
            return Enumerable.Range(0, numberOfDays)
                    .Select(x => dateTime.Subtract(TimeSpan.FromDays(x)))
                    .ToList();
        }

        public static List<DateTime> GetLastWeeks(this DateTime dateTime, int numberOfWeeks)
        {
            return Enumerable.Range(0, numberOfWeeks)
                .Select(x => dateTime.Subtract(TimeSpan.FromDays(x * 7))
                    .GetFirstDayOfWeek())
                .ToList();
        }

        public static List<DateTime> GetLastMonths(this DateTime dateTime, int numberOfMonths)
        {
            return Enumerable.Range(0, numberOfMonths)
                .Select(x => dateTime
                    .AddMonths(x * -1)
                    .GetFirstDayOfMonth())
                .ToList();
        }

        public static bool IsSameDay(this DateTime dateTime, DateTime otherDateTime)
        {
            return dateTime.Date == otherDateTime.Date;
        }

        public static bool IsSameWeek(this DateTime dateTime, DateTime otherDateTime)
        {
            return dateTime.GetFirstDayOfWeek() == otherDateTime.GetFirstDayOfWeek();
        }

        public static bool IsSameMonth(this DateTime dateTime, DateTime otherDateTime)
        {
            return dateTime.GetFirstDayOfMonth() == otherDateTime.GetFirstDayOfMonth();
        }

        public static bool IsSameUnixDateTime(this DateTime dateTime, DateTime otherDateTime)
        {
            return dateTime.ToUnixEpochDate() == otherDateTime.ToUnixEpochDate();
        }

        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime() <= DateTimeOffset.MinValue.UtcDateTime
                ? DateTimeOffset.MinValue
                : new DateTimeOffset(dateTime);
        }
    }
}
