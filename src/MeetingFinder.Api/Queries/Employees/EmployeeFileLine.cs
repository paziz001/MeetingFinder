using System;
using System.Text.RegularExpressions;
using MeetingFinder.Api.Models;

namespace MeetingFinder.Api.Queries.Employees
{
    public class EmployeeFileLine
    {
        private readonly string _line;
        
        public EmployeeFileLine(string line)
        {
            _line = line;
        }

        public bool ContainsNameForEmployeeWith(string id)
        {
            var employeeIdAndNameRegex = @"^" + id + @";[a-zA-Z]+ [a-zA-Z]+$";

            return Regex.IsMatch(_line, employeeIdAndNameRegex);
        }

        public bool TryExtractBusySlotForEmployeeWith(string id, out BusySlot? busySlot)
        {
            busySlot = null;
            const string dateTimeRegexGroup = @"(\d{1,2}\/\d{1,2}\/\d{4} \d{1,2}:\d{1,2}:\d{1,2} (PM|AM))";
            var lineWithEmployeeMeeting = @"^" + id + ";" + dateTimeRegexGroup + ";" + dateTimeRegexGroup + @";\w+$";
            
            var match = Regex.Match(_line, lineWithEmployeeMeeting);
            if (match.Groups.Count != 5)
            {
                return false;
            }
            if (!DateTime.TryParse(match.Groups[1].Value, out var busySlotStartTime))
            {
                return false;
            }
            if (!DateTime.TryParse(match.Groups[3].Value, out var busySlotEndTime))
            {
                return false;
            }
            
            busySlot = new BusySlot(busySlotStartTime, busySlotEndTime);
            return true;
        }
    }
}