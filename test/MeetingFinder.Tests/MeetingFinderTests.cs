using System;
using System.Collections.Generic;
using MeetingFinder.Api.Models;
using MeetingFinder.Api.Queries.Meeting;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace MeetingFinder.Tests
{
    public class MeetingFinderTests
    {
        private readonly ITestOutputHelper _output;

        public MeetingFinderTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ShouldReturnCorrectSuitableMeetings()
        {
            var meetingFinder = new MeetingQuery();
            var busySlots = new List<BusySlot>
            {
                new BusySlot(
                    new DateTime(2019, 11, 17, 7, 30, 0),
                    new DateTime(2019, 11, 17, 9, 00, 0)),
                new BusySlot(
                    new DateTime(2019, 11, 17, 8, 30, 0),
                    new DateTime(2019, 11, 17, 9, 30, 0)),
                new BusySlot(
                    new DateTime(2019, 11, 17, 10, 00, 0),
                    new DateTime(2019, 11, 17, 11, 30, 0)),
            };
            var desiredMeetingLength = 45;
            var desiredDateTimeRange = ( new DateTime(2019, 11, 17, 7, 30, 0),
                 new DateTime(2019, 11, 17, 18, 30, 0));
            var officeHours = ( new TimeSpan(8, 42, 0), new TimeSpan(17, 0, 0));
            var suitableMeetings = meetingFinder.GetSuitableMeetings(busySlots, desiredMeetingLength, desiredDateTimeRange, officeHours);

            foreach (var meeting in suitableMeetings)
            {
                _output.WriteLine($"Start: {meeting.Start}, Start: {meeting.End}");
            }
        }
    }
}