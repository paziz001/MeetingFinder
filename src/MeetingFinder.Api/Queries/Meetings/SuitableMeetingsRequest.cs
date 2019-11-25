using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingFinder.Api.Queries.Meetings
{
    public class SuitableMeetingsRequest
    {
        [Required]
        public IEnumerable<string>? EmployeeIds { get; set; }
        
        [Required]
        public int DesiredMeetingLength { get; set; }
        
        [Required]
        public DateTime requestedEarliestMeetingTime { get; set; }
        
        [Required]
        public DateTime requestedLatestMeetingTime { get; set; }
        
        [Required]
        public TimeSpan officeHoursStartTime { get; set; }
        
        [Required]
        public TimeSpan officeHoursEndTime { get; set; }
    }
}