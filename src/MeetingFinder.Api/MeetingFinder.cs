using System;
using System.Collections.Generic;
using System.Linq;
using MeetingFinder.Api.Domain;
using MeetingFinder.Api.Models;

namespace MeetingFinder.Api
{
    public class MeetingFinder
    {
        public IEnumerable<Meeting> GetSuitableMeetings(
            IList<BusySlot> busySlots,
            int desiredMeetingLength,
            (DateTime EarliestTime, DateTime LatestTime) desiredDateTimeRange,
            (TimeSpan StartTime, TimeSpan EndTime) officeHours)
        {
            var (desiredEarliestTime, desiredLatestTime) = desiredDateTimeRange;
            var (officeHoursStartTime, officeHoursEndTime) = officeHours;

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
                throw new ArgumentException("Meeting's office hours start time is greater than or equal to office hours end time");
            }

            var startTimeForMeetingSearch = desiredEarliestTime.TimeOfDay > officeHoursStartTime &&
                                            desiredEarliestTime.TimeOfDay < officeHoursEndTime
                                            ? desiredEarliestTime
                                            : desiredEarliestTime.WithTime(officeHoursStartTime);
            var endTimeForMeetingSearch = desiredLatestTime.TimeOfDay < officeHoursEndTime
                                          ? desiredLatestTime
                                          : desiredLatestTime.WithTime(officeHoursEndTime);
            
            var suitableMeetings = new List<Meeting>();
            var possibleMeetingStartTime = startTimeForMeetingSearch.RoundTimeToHalfOrWhole();
            var possibleMeetingEndTime = possibleMeetingStartTime.AddMinutes(desiredMeetingLength).RoundTimeToHalfOrWhole();
            while (possibleMeetingEndTime <= endTimeForMeetingSearch)
            {
                if (!IsSlotTaken(possibleMeetingStartTime, possibleMeetingEndTime, busySlots))
                {
                    suitableMeetings.Add(new Meeting {Start = possibleMeetingStartTime, End = possibleMeetingEndTime});    
                }
                possibleMeetingStartTime = possibleMeetingEndTime.RoundTimeToHalfOrWhole();
                possibleMeetingEndTime = possibleMeetingStartTime.AddMinutes(desiredMeetingLength).RoundTimeToHalfOrWhole();
            }

            return suitableMeetings;
        }
        
        public bool IsSlotTaken(DateTime requestedSlotStart, DateTime requestedSlotEnd, IEnumerable<BusySlot> busySlots)
        {
            return busySlots.Any(busySlot => 
                (requestedSlotStart >= busySlot.Start || requestedSlotEnd > busySlot.Start) && requestedSlotStart < busySlot.End);
        }
    }
}