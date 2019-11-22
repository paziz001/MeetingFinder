using System;

namespace MeetingFinder.Api
{
    public static class DateTimeExtensions
    {
        public static DateTime WithTime(this DateTime dateTime, TimeSpan time)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, time.Hours, time.Minutes, time.Seconds);
        }
        
        public static DateTime RoundTimeToHalfOrWhole(this DateTime dateTime)
        {
            var roundedDateTime =
                new DateTime(dateTime.Year,dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            if (roundedDateTime.Minute > 0 && roundedDateTime.Minute < 30)
            {
                roundedDateTime = roundedDateTime.AddMinutes(30 - roundedDateTime.Minute);
            }
            else if (roundedDateTime.Minute > 30 && roundedDateTime.Minute <= 59)
            {
                roundedDateTime = roundedDateTime.AddMinutes(60 - roundedDateTime.Minute);
            }

            return roundedDateTime;
        }
    }
}