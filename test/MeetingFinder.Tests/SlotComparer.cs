using System;
using System.Collections.Generic;
using FluentAssertions;
using MeetingFinder.Api.Models;
using Xunit;
using Xunit.Abstractions;

namespace MeetingFinder.Tests
{
    public class SlotComparer
    {
        private readonly ITestOutputHelper _output;

        public SlotComparer(ITestOutputHelper output)
        {
            _output = output;
        }

        public bool IsSlotTaken(DateTime requestedSlotStart, DateTime requestedSlotEnd, IList<BusySlot> busySlots)
        {
            var isSlotTaken = false;
            foreach (var busySlot in busySlots)
            {
                if (requestedSlotStart < busySlot.Start && requestedSlotEnd <= busySlot.Start ||
                    requestedSlotStart >= busySlot.End) continue;
                isSlotTaken = true;
                break;
            }

            return isSlotTaken;
        }
        

        [Fact]
        public void ShouldReturnFalseIfSlotIsTaken()
        {
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

            foreach (var busySlot in busySlots)
            {
                var minutesInterval = 30;
                for (var slotStartTime = busySlot.Start.Subtract(TimeSpan.FromMinutes(minutesInterval));
                    slotStartTime <= busySlot.End;
                    slotStartTime = slotStartTime.AddMinutes(minutesInterval))
                {
                    for (var slotEndTime = slotStartTime.AddMinutes(minutesInterval);
                        slotEndTime <= busySlot.End.AddMinutes(minutesInterval);
                        slotEndTime = slotEndTime.AddMinutes(minutesInterval))
                    {
                        _output.WriteLine($"start: {slotStartTime}, end: {slotEndTime}, taken: {IsSlotTaken(slotStartTime, slotEndTime, busySlots)}");
                        IsSlotTaken(slotStartTime, slotEndTime, busySlots);
                    }
                }
            }
            
        }
    }
}