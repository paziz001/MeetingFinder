using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using MeetingFinder.Api.Models;
using MeetingFinder.Api.Queries.Meetings;
using Xunit;
using Xunit.Abstractions;

namespace MeetingFinder.Tests.Queries.Meetings
{
    public class MeetingQueryTests
    {
        private readonly ITestOutputHelper _output;

        public MeetingQueryTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Theory]
        [MemberData(nameof(MeetingQueryTestData.GetDataForGetSuitableMeetingsTest), MemberType = typeof(MeetingQueryTestData))]
        public void Should_ReturnCorrectMeetings_When_GetSuitableMeetings(
            IEnumerable<BusySlot> busySlots, int desiredMeetingLength,
            (DateTime EarliestDateTime, DateTime desiredLatestDateTime) desiredDateTimeRange,
            (TimeSpan StartTime, TimeSpan EndTime) officeHours)
        {
            var meetingQuery = new MeetingQuery();
            var suitableMeetings = meetingQuery.GetSuitableMeetings(busySlots, desiredMeetingLength, desiredDateTimeRange, officeHours);

            foreach (var meeting in suitableMeetings)
            {
                _output.WriteLine($"Start: {meeting.Start}, Start: {meeting.End}");
            }
        }
        
        [Theory]
        [MemberData(nameof(MeetingQueryTestData.GetDataThatThrowsArgumentExceptionForGetSuitableMeetings), MemberType = typeof(MeetingQueryTestData))]
        public void Should_ThrowArgumentException_When_GetSuitableMeetings(IEnumerable<BusySlot> busySlots, int desiredMeetingLength,
            (DateTime desiredEarliestTime, DateTime desiredLatestTime) desiredTimeRange,
            (TimeSpan officeHoursStartTime, TimeSpan officeHoursEndTime) officeHours)
        {
            var meetingQuery = new MeetingQuery();
            var getSuitableMeetings = new Func<IEnumerable<Meeting>>(() =>  meetingQuery.GetSuitableMeetings(busySlots, desiredMeetingLength,
                                                                                                     desiredTimeRange, officeHours));
            getSuitableMeetings.Should().Throw<ArgumentException>();
        }
        
        private class MeetingQueryTestData
        {
            public static IEnumerable<object[]> GetDataForGetSuitableMeetingsTest()
            {
                yield return new object[]
                {
                    new List<BusySlot>
                    {
                        new BusySlot(DateTime.Parse("11/27/2019 07:30:00"), DateTime.Parse("11/27/2019 09:00:00")),
                        new BusySlot(DateTime.Parse("11/27/2019 08:30:00"), DateTime.Parse("11/27/2019 09:30:00")),
                        new BusySlot(DateTime.Parse("11/27/2019 10:00:00"), DateTime.Parse("11/27/2019 11:30:00")),
                    },
                    1, (DateTime.Parse("11/27/2019 07:30:00"), DateTime.Parse("11/27/2019 12:10:00")),
                    (TimeSpan.Parse("08:40:00"), TimeSpan.Parse("17:00:00"))
                };
            }

            public static IEnumerable<object[]> GetDataThatThrowsArgumentExceptionForGetSuitableMeetings()
            {
                yield return new object[]
                {
                    null, 0, (new DateTime(), new DateTime()), (new TimeSpan(), new TimeSpan())
                };
                yield return new object[]
                {
                    new List<BusySlot>(), 0, (new DateTime(), new DateTime()), (new TimeSpan(), new TimeSpan())
                };
                yield return new object[]
                {
                    new List<BusySlot>(), 10,
                    (DateTime.Parse("11/23/2019 08:00:00"), DateTime.Parse("11/23/2019 07:00:00")),
                    (new TimeSpan(), new TimeSpan())
                };
                yield return new object[]
                {
                    new List<BusySlot>(), 10,
                    (DateTime.Parse("11/23/2019 08:00:00"), DateTime.Parse("11/23/2019 09:00:00")),
                    (new TimeSpan(-1, 3, -4), new TimeSpan())
                };
                yield return new object[]
                {
                    new List<BusySlot>(), 10,
                    (DateTime.Parse("11/23/2019 08:00:00"), DateTime.Parse("11/23/2019 09:00:00")),
                    (new TimeSpan(1, 3, 4), new TimeSpan(-1, 3, 4))
                };
                yield return new object[]
                {
                    new List<BusySlot>(), 10,
                    (DateTime.Parse("11/23/2019 08:00:00"), DateTime.Parse("11/23/2019 09:00:00")),
                    (new TimeSpan(8, 10, 0), new TimeSpan(7, 0, 0))
                };
            }
        }
    }
}