using System;
using System.Collections.Generic;
using MeetingFinder.Api.Models;

namespace MeetingFinder.Api.Queries.Meeting
{
    public interface IMeetingQuery
    {
        IEnumerable<Models.Meeting> GetSuitableMeetings(
            IEnumerable<BusySlot> busySlots,
            int desiredMeetingLength,
            (DateTime EarliestTime, DateTime LatestTime) desiredDateTimeRange,
            (TimeSpan StartTime, TimeSpan EndTime) officeHours);
    }
}