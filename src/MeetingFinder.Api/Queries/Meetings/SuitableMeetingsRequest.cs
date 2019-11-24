using System;
using System.Collections.Generic;

namespace MeetingFinder.Api.Queries.Meetings
{
    public class SuitableMeetingsRequest
    {
        public IList<string>? EmployeeIds { get; set; }
        public int DesiredMeetingLength { get; set; }
        public DateTime requestedEarliestMeetingTime { get; set; }
        public DateTime requestedLatestMeetingTime { get; set; }
        public TimeSpan officeHoursStartTime { get; set; }
        public TimeSpan officeHoursEndTime { get; set; }
    }
}