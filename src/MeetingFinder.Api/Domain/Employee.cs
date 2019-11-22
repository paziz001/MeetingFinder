using System;
using System.Collections.Generic;

namespace MeetingFinder.Api.Domain
{
    public class Employee
    {
        public string Id { get; }
        public IList<BusySlot> BusySlots { get; }
        
        public Employee(string id, IList<BusySlot> busySlots)
        {
            Id = id;
            BusySlots = busySlots;
        }
    }

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