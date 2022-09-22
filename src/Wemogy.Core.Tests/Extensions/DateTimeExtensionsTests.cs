using System;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void GetWeekOfYear_ShouldWork()
        {
            var weekNumber1 = new DateTime(2020, 01, 01).GetWeekOfYear();
            var weekNumber2 = new DateTime(2020, 01, 06).GetWeekOfYear();

            Assert.Equal(1, weekNumber1);
            Assert.Equal(2, weekNumber2);
        }

        [Fact]
        public void GetFirstDayOfMonth_ShouldWork()
        {
            var day1 = new DateTime(2020, 01, 01).GetFirstDayOfMonth();
            var day2 = new DateTime(2020, 01, 23).GetFirstDayOfMonth();
            var day3 = new DateTime(2020, 10, 14).GetFirstDayOfMonth();

            Assert.True(day1.Day == 1 && day1.Month == 1 && day1.Year == 2020);
            Assert.True(day2.Day == 1 && day2.Month == 1 && day2.Year == 2020);
            Assert.True(day3.Day == 1 && day3.Month == 10 && day3.Year == 2020);
        }

        [Fact]
        public void GetFirstDayOfWeek_ShouldWork()
        {
            var day1 = new DateTime(2020, 01, 01).GetFirstDayOfWeek();
            var day2 = new DateTime(2020, 01, 12).GetFirstDayOfWeek();
            var day3 = new DateTime(2020, 01, 14).GetFirstDayOfWeek();

            Assert.True(day1.Day == 30 && day1.Month == 12 && day1.Year == 2019);
            Assert.True(day2.Day == 6 && day2.Month == 1 && day2.Year == 2020);
            Assert.True(day3.Day == 13 && day3.Month == 1 && day3.Year == 2020);
        }

        [Fact]
        public void GetLastDays_ShouldWork()
        {
            var day = new DateTime(2020, 01, 01);
            var lastFiveDays = day.GetLastDays(5);
            Assert.Equal(new DateTime(2020, 01, 01), lastFiveDays[0]);
            Assert.Equal(new DateTime(2019, 12, 31), lastFiveDays[1]);
            Assert.Equal(new DateTime(2019, 12, 30), lastFiveDays[2]);
            Assert.Equal(new DateTime(2019, 12, 29), lastFiveDays[3]);
            Assert.Equal(new DateTime(2019, 12, 28), lastFiveDays[4]);
        }

        [Fact]
        public void GetLastWeeks_ShouldWork()
        {
            var day = new DateTime(2020, 01, 05);
            var lastFiveWeeks = day.GetLastWeeks(5);
            Assert.Equal(new DateTime(2019, 12, 30), lastFiveWeeks[0]);
            Assert.Equal(new DateTime(2019, 12, 23), lastFiveWeeks[1]);
            Assert.Equal(new DateTime(2019, 12, 16), lastFiveWeeks[2]);
            Assert.Equal(new DateTime(2019, 12, 09), lastFiveWeeks[3]);
            Assert.Equal(new DateTime(2019, 12, 02), lastFiveWeeks[4]);
        }

        [Fact]
        public void GetLastMonths_ShouldWork()
        {
            var day = new DateTime(2020, 01, 05);
            var lastFiveMonths = day.GetLastMonths(5);
            Assert.Equal(new DateTime(2020, 01, 01), lastFiveMonths[0]);
            Assert.Equal(new DateTime(2019, 12, 01), lastFiveMonths[1]);
            Assert.Equal(new DateTime(2019, 11, 01), lastFiveMonths[2]);
            Assert.Equal(new DateTime(2019, 10, 01), lastFiveMonths[3]);
            Assert.Equal(new DateTime(2019, 09, 01), lastFiveMonths[4]);
        }

        [Fact]
        public void IsSameDay_ShouldWork()
        {
            var date = new DateTime(2020, 01, 05, 3, 21, 0);
            var otherDate1 = new DateTime(2020, 01, 05, 0, 0, 0);
            var otherDate2 = new DateTime(2020, 01, 05, 3, 32, 23);
            var otherDate3 = new DateTime(2020, 01, 2, 0, 0, 0);

            Assert.True(date.IsSameDay(otherDate1));
            Assert.True(date.IsSameDay(otherDate2));
            Assert.False(date.IsSameDay(otherDate3));
        }

        [Fact]
        public void IsSameWeek_ShouldWork()
        {
            var date = new DateTime(2020, 01, 05, 3, 21, 0);
            var otherDate1 = new DateTime(2020, 01, 05, 0, 0, 0);
            var otherDate2 = new DateTime(2019, 12, 30, 3, 32, 23);
            var otherDate3 = new DateTime(2020, 2, 2, 0, 0, 0);

            Assert.True(date.IsSameWeek(otherDate1));
            Assert.True(date.IsSameWeek(otherDate2));
            Assert.False(date.IsSameWeek(otherDate3));
        }

        [Fact]
        public void IsSameMonth_ShouldWork()
        {
            var date = new DateTime(2020, 01, 05, 3, 21, 0);
            var otherDate1 = new DateTime(2020, 01, 05, 0, 0, 0);
            var otherDate2 = new DateTime(2019, 12, 30, 3, 32, 23);
            var otherDate3 = new DateTime(2020, 2, 2, 0, 0, 0);

            Assert.True(date.IsSameMonth(otherDate1));
            Assert.False(date.IsSameMonth(otherDate2));
            Assert.False(date.IsSameMonth(otherDate3));
        }

        [Fact]
        public void GetLastDayOfMonth_ShouldWork()
        {
            // Arrange
            var date = new DateTime(2020, 01, 05, 3, 21, 0);

            // Act
            var lastDayOfMonth = date.GetLastDayOfMonth();

            // Assert
            Assert.Equal(2020, lastDayOfMonth.Year);
            Assert.Equal(01, lastDayOfMonth.Month);
            Assert.Equal(31, lastDayOfMonth.Day);
        }

        [Fact]
        public void IsDayOfWeek_ShouldWork()
        {
            // Arrange
            var date = new DateTime(2020, 01, 05, 3, 21, 0);

            // Act
            var isDayOfWeek = date.IsDayOfWeek(DayOfWeek.Sunday);

            // Assert
            Assert.True(isDayOfWeek);
        }

        [Fact]
        public void IsSameUnixDateTime_ShouldWork()
        {
            var utcNow = DateTime.UtcNow;

            Assert.True(utcNow.IsSameUnixDateTime(utcNow.ToUniversalTime()));
        }

        [Fact]
        public void ToDateTimeOffset_ShouldWork()
        {
            // Arrange
            var dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var defaultDateTime = default(DateTime);

            // Act
            var exception = Record.Exception(() => dateTime1.ToDateTimeOffset());
            var defaultDateTimeException = Record.Exception(() => defaultDateTime.ToDateTimeOffset());

            // Assert
            Assert.Null(exception);
            Assert.Null(defaultDateTimeException);
        }

        [Fact]
        public void ToUnixTimeSeconds_ShouldWork()
        {
            // Arrange
            var dateTime = new DateTime(2022, 04, 02, 20, 15, 23, DateTimeKind.Utc);

            // Act
            var epochSeconds = dateTime.ToUnixTimeSeconds();

            // Assert
            Assert.Equal(1648930523, epochSeconds);
        }

        [Fact]
        public void FromUnixTimeSeconds_ShouldWork()
        {
            // Arrange
            var timestamp = 1648930523L;

            // Act
            var dateTime = timestamp.FromUnixTimeSeconds();

            // Assert
            Assert.Equal(2022, dateTime.Year);
            Assert.Equal(4, dateTime.Month);
            Assert.Equal(2, dateTime.Day);
            Assert.Equal(20, dateTime.Hour);
            Assert.Equal(15, dateTime.Minute);
            Assert.Equal(23, dateTime.Second);
        }
    }
}
