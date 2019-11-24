using System.Collections.Generic;

namespace MeetingFinder.Api.Models
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
}