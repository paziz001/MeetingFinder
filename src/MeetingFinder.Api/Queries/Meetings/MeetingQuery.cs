using System;
using System.Collections.Generic;
using System.Linq;
using MeetingFinder.Api.Extensions;
using MeetingFinder.Api.Models;

namespace MeetingFinder.Api.Queries.Meetings
{
    public class MeetingQuery : IMeetingQuery
    {
        public IEnumerable<Meeting> GetSuitableMeetings(
            IEnumerable<BusySlot> busySlots,
            int desiredMeetingLength,
            (DateTime EarliestTime, DateTime LatestTime) desiredDateTimeRange,
            (TimeSpan StartTime, TimeSpan EndTime) officeHours)
        {
            var (desiredEarliestTime, desiredLatestTime) = desiredDateTimeRange;
            var (officeHoursStartTime, officeHoursEndTime) = officeHours;

            AssertMethodParameters(busySlots, desiredMeetingLength,
                desiredEarliestTime, desiredLatestTime, officeHoursStartTime, officeHoursEndTime);

            var startTimeForMeetingSearch = desiredEarliestTime.GetLatestTimeComparedWith(officeHoursStartTime);
            var endTimeForMeetingSearch = desiredLatestTime.GetEarliestTimeComparedWith(officeHoursEndTime);
            
            var suitableMeetings = new List<Meeting>();
            var possibleMeetingStartTime = startTimeForMeetingSearch.RoundTimeToHalfOrWhole();
            var possibleMeetingEndTime = startTimeForMeetingSearch.AddMinutes(desiredMeetingLength).RoundTimeToHalfOrWhole();

            var enumeratedBusySlots = busySlots.ToList();
            while (possibleMeetingEndTime <= endTimeForMeetingSearch)
            {
                if (!IsTimeSlotTaken(possibleMeetingStartTime, possibleMeetingEndTime, enumeratedBusySlots))
                {
                    suitableMeetings.Add(new Meeting {Start = possibleMeetingStartTime, End = possibleMeetingEndTime});    
                }
                possibleMeetingStartTime = possibleMeetingEndTime.RoundTimeToHalfOrWhole();
                possibleMeetingEndTime = possibleMeetingEndTime.AddMinutes(desiredMeetingLength).RoundTimeToHalfOrWhole();
                var incrementalMeetingLength = desiredMeetingLength;
                while (possibleMeetingStartTime == possibleMeetingEndTime)
                {
                    incrementalMeetingLength++;
                    possibleMeetingEndTime = possibleMeetingEndTime.AddMinutes(incrementalMeetingLength).RoundTimeToHalfOrWhole();
                }
            }

            return suitableMeetings;
        }

        private static void AssertMethodParameters(IEnumerable<BusySlot> busySlots, int desiredMeetingLength,
                                                   DateTime desiredEarliestTime, DateTime desiredLatestTime,
                                                   TimeSpan officeHoursStartTime, TimeSpan officeHoursEndTime)
        {
            if (busySlots == null)
            {
                throw new ArgumentException("Busy slots enumerable is null");
            }
            
            if (desiredMeetingLength <= 0)
            {
                throw new ArgumentException("Meeting's desired length is lower or equal to 0");
            }

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

        private static bool IsTimeSlotTaken(DateTime requestedSlotStart, DateTime requestedSlotEnd, IEnumerable<BusySlot> busySlots)
        {
            return busySlots.Any(busySlot => busySlot.IsOverlappedByTimeSlot(requestedSlotStart, requestedSlotEnd));
        }
    }
}