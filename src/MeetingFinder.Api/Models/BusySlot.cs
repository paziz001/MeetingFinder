using System;

namespace MeetingFinder.Api.Models
{
    public class BusySlot
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public BusySlot(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}