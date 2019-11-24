using System;
using System.Collections.Generic;
using FluentAssertions;
using MeetingFinder.Api.Extensions;
using Xunit;

namespace MeetingFinder.Tests.Extensions
{
    public class DateTimeExtensionTests
    {
        [Fact]
        public void Should_ReturnDateTime_When_WithTime()
        {
            var time = new TimeSpan(20, 0, 0);
            var dateTime = new DateTime(2019, 11, 1);
            var expectedDateTime = new DateTime(2019, 11, 1, 20, 0, 0);

            var actualDateTime = dateTime.WithTime(time);
            
            actualDateTime.Should().Be(expectedDateTime);
        }
        
        [Theory]
        [MemberData(nameof(DateTimeExtensionsTestData.GetDataForRoundTimeToHalfOrWholeTest), MemberType = typeof(DateTimeExtensionsTestData))]
        public void Should_ReturnDateTimeRoundedToHalf_When_RoundTimeToHalfOrWhole(DateTime dateTime, DateTime roundedDateTime)
        {
            var actualRoundedDateTime = dateTime.RoundTimeToHalfOrWhole();
                
            actualRoundedDateTime.Should().Be(roundedDateTime);
        }

        [Theory]
        [MemberData(nameof(DateTimeExtensionsTestData.GetDataForGetLatestTimeComparedWithTest), MemberType = typeof(DateTimeExtensionsTestData))]
        public void Should_ReturnCorrectDateTime_GetLatestTimeComparedWith(TimeSpan time, TimeSpan expectedLatestTime)
        {
            var dateTime = DateTime.Parse("11/23/2019 11:00:00");

            var actualLatestTime = dateTime.GetLatestTimeComparedWith(time);

            actualLatestTime.TimeOfDay.Should().Be(expectedLatestTime);
        }
        
        [Theory]
        [MemberData(nameof(DateTimeExtensionsTestData.GetDataForGetEarliestTimeComparedWithTest), MemberType = typeof(DateTimeExtensionsTestData))]
        public void Should_ReturnCorrectDateTime_GetEarliestTimeComparedWith(TimeSpan time, TimeSpan expectedLatestTime)
        {
            var dateTime = DateTime.Parse("11/23/2019 11:00:00");

            var actualLatestTime = dateTime.GetEarliestTimeComparedWith(time);

            actualLatestTime.TimeOfDay.Should().Be(expectedLatestTime);
        }

        private class DateTimeExtensionsTestData
        {
            public static IEnumerable<object[]> GetDataForRoundTimeToHalfOrWholeTest()
            {
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 0, 0),
                    new DateTime(2019, 11, 1, 20, 0, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 7, 0),
                    new DateTime(2019, 11, 1, 20, 0, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 14, 0),
                    new DateTime(2019, 11, 1, 20, 0, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 15, 0),
                    new DateTime(2019, 11, 1, 20, 0, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 18, 0),
                    new DateTime(2019, 11, 1, 20, 30, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 29, 0),
                    new DateTime(2019, 11, 1, 20, 30, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 30, 0),
                    new DateTime(2019, 11, 1, 20, 30, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 37, 0),
                    new DateTime(2019, 11, 1, 20, 30, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 45, 0),
                    new DateTime(2019, 11, 1, 20, 30, 0)
                };
                yield return new object[]
                {
                    new DateTime(2019, 11, 1, 20, 55, 0),
                    new DateTime(2019, 11, 1, 21, 0, 0)
                };
            }

            public static IEnumerable<object[]> GetDataForGetLatestTimeComparedWithTest()
            {
                yield return new object[] { new TimeSpan(10, 35, 0), new TimeSpan(11, 0, 0) };
                yield return new object[] { new TimeSpan(11, 0, 0), new TimeSpan(11, 0, 0) };
                yield return new object[] { new TimeSpan(11, 30, 0), new TimeSpan(11, 30, 0) };
            }
            
            public static IEnumerable<object[]> GetDataForGetEarliestTimeComparedWithTest()
            {
                yield return new object[] { new TimeSpan(10, 35, 0), new TimeSpan(10, 35, 0) };
                yield return new object[] { new TimeSpan(11, 0, 0), new TimeSpan(11, 0, 0) };
                yield return new object[] { new TimeSpan(11, 30, 0), new TimeSpan(11, 00, 0) };
            }
        }
    }
}