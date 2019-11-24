using System;

namespace MeetingFinder.Api.Extensions
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
            if (roundedDateTime.Minute > 0 && roundedDateTime.Minute <= 15)
            {
                roundedDateTime = roundedDateTime.Subtract(TimeSpan.FromMinutes(roundedDateTime.Minute));
            }
            if (roundedDateTime.Minute > 15 && roundedDateTime.Minute < 30)
            {
                roundedDateTime = roundedDateTime.AddMinutes(30 - roundedDateTime.Minute);
            }
            if (roundedDateTime.Minute > 30 && roundedDateTime.Minute <= 45)
            {
                roundedDateTime = roundedDateTime.Subtract(TimeSpan.FromMinutes(roundedDateTime.Minute - 30));
            }
            else if (roundedDateTime.Minute > 45 && roundedDateTime.Minute <= 59)
            {
                roundedDateTime = roundedDateTime.AddMinutes(60 - roundedDateTime.Minute);
            }

            return roundedDateTime;
        }

        public static DateTime GetLatestTimeComparedWith(this DateTime dateTime, TimeSpan time)
        {
            return dateTime.TimeOfDay > time
                ? dateTime
                : dateTime.WithTime(time);
        }
        
        public static DateTime GetEarliestTimeComparedWith(this DateTime dateTime, TimeSpan timeSpan)
        {
            return dateTime.TimeOfDay < timeSpan
                ? dateTime
                : dateTime.WithTime(timeSpan);
        }
    }
}