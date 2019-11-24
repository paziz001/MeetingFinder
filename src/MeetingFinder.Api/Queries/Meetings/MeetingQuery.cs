using System;
using System.Collections.Generic;
using System.Linq;
using MeetingFinder.Api.Extensions;
using MeetingFinder.Api.Models;

namespace MeetingFinder.Api.Queries.Meeting
{
    public class MeetingQuery : IMeetingQuery
    {
        public IEnumerable<Models.Meeting> GetSuitableMeetings(
            IEnumerable<BusySlot> busySlots,
            int desiredMeetingLength,
            (DateTime EarliestTime, DateTime LatestTime) desiredDateTimeRange,
            (TimeSpan StartTime, TimeSpan EndTime) officeHours)
        {
            var (desiredEarliestTime, desiredLatestTime) = desiredDateTimeRange;
            var (officeHoursStartTime, officeHoursEndTime) = officeHours;

            AssertMethodParameters(desiredEarliestTime, desiredLatestTime, officeHoursStartTime, officeHoursEndTime);

            var startTimeForMeetingSearch = desiredEarliestTime.GetLatestTimeComparedWith(officeHoursStartTime);
            var endTimeForMeetingSearch = desiredLatestTime.GetEarliestTimeComparedWith(officeHoursEndTime);
            
            var suitableMeetings = new List<Models.Meeting>();
            var possibleMeetingStartTime = startTimeForMeetingSearch.RoundTimeToHalfOrWhole();
            var possibleMeetingEndTime = possibleMeetingStartTime.AddMinutes(desiredMeetingLength).RoundTimeToHalfOrWhole();
            var enumeratedBusySlots = busySlots.ToList();
            while (possibleMeetingEndTime <= endTimeForMeetingSearch)
            {
                if (!IsSlotTaken(possibleMeetingStartTime, possibleMeetingEndTime, enumeratedBusySlots))
                {
                    suitableMeetings.Add(new Models.Meeting {Start = possibleMeetingStartTime, End = possibleMeetingEndTime});    
                }
                possibleMeetingStartTime = possibleMeetingEndTime.RoundTimeToHalfOrWhole();
                possibleMeetingEndTime = possibleMeetingStartTime.AddMinutes(desiredMeetingLength).RoundTimeToHalfOrWhole();
            }

            return suitableMeetings;
        }

        private static void AssertMethodParameters(DateTime desiredEarliestTime, DateTime desiredLatestTime,
                                                   TimeSpan officeHoursStartTime, TimeSpan officeHoursEndTime)
        {
            if (desiredEarliestTime >= desiredLatestTime)
            {
                throw new ArgumentException("Meeting's desired earliest time is greater than or equal to desired latest time");
            }

            if (officeHoursStartTime.Hours < 0 || officeHoursStartTime.Minutes < 0 || officeHoursStartTime.Seconds < 0)
            {
                throw new ArgumentException("Office hours start time value has negative values");
            }

            if (officeHoursEndTime.Hours < 0 || officeHoursEndTime.Minutes < 0 || officeHoursEndTime.Seconds < 0)
            {
                throw new ArgumentException("Office hours end time value has negative values");
            }

            if (officeHoursStartTime >= officeHoursEndTime)
            {
                throw new ArgumentException(
                    "Meeting's office hours start time is greater than or equal to office hours end time");
            }
        }

        public bool IsSlotTaken(DateTime requestedSlotStart, DateTime requestedSlotEnd, IEnumerable<BusySlot> busySlots)
        {
            return busySlots.Any(busySlot => 
                (requestedSlotStart >= busySlot.Start || requestedSlotEnd > busySlot.Start) && requestedSlotStart < busySlot.End);
        }
    }
}