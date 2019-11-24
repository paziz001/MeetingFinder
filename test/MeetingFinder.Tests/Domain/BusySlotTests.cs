using System;
using FluentAssertions;
using MeetingFinder.Api.Models;
using Xunit;

namespace MeetingFinder.Tests.Domain
{
    public class BusySlotTests
    {
        [Theory]
        [InlineData("11/24/2019 08:00:00", "11/24/2019 8:30:00")]
        [InlineData("11/24/2019 08:30:00", "11/24/2019 9:00:00")]
        public void Should_BeTrue_If_IsOverlappedByTimeSlot(string timeSlotStartDateTime, string timeSlotEndDateTime)
        {
            var busySlot = new BusySlot(DateTime.Parse("11/24/2019 08:00:00"), DateTime.Parse("11/24/2019 09:00:00"));

            busySlot.IsOverlappedByTimeSlot(DateTime.Parse(timeSlotStartDateTime), DateTime.Parse(timeSlotEndDateTime))
                .Should().BeTrue();
        }
        
        [Theory]
        [InlineData("11/24/2019 07:00:00", "11/24/2019 7:0:00")]
        [InlineData("11/24/2019 07:30:00", "11/24/2019 8:00:00")]
        [InlineData("11/24/2019 09:00:00", "11/24/2019 9:30:00")]
        [InlineData("11/24/2019 09:30:00", "11/24/2019 10:00:00")]
        public void Should_BeFalse_If_IsNotOverlappedByTimeSlot(string timeSlotStartDateTime, string timeSlotEndDateTime)
        {
            var busySlot = new BusySlot(DateTime.Parse("11/24/2019 08:00:00"), DateTime.Parse("11/24/2019 09:00:00"));

            busySlot.IsOverlappedByTimeSlot(DateTime.Parse(timeSlotStartDateTime), DateTime.Parse(timeSlotEndDateTime))
                .Should().BeFalse();
        }
    }
}