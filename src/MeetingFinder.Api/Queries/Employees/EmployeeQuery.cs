using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingFinder.Api.Models;

namespace MeetingFinder.Api.Queries.Employees
{
    public class EmployeeQuery : IEmployeeQuery
    {
        private readonly Func<IEmployeeFileReader> _employeeFileReaderProvider;

        public EmployeeQuery(Func<IEmployeeFileReader> employeeFileReaderProvider)
        {
            _employeeFileReaderProvider = employeeFileReaderProvider;
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<string> ids)
        {
            var employeesFound = new Dictionary<string, (bool EmployeeWasFound, IList<BusySlot> BusySlots)>();
            
            EmployeeFileLine? line;
            using var reader = _employeeFileReaderProvider();
            var enumeratedEmployeeIds = ids.ToList();
            while ((line = await reader.ReadLineAsync()) != null)
            {
                foreach (var employeeId in enumeratedEmployeeIds)
                {
                    if (line.ContainsNameForEmployeeWith(employeeId))
                    {
                        if (!employeesFound.TryAdd(employeeId, (true, new List<BusySlot>())))
                        {
                            var employee = employeesFound.GetValueOrDefault(employeeId);
                            employeesFound.Remove(employeeId);
                            employeesFound.Add(employeeId, (true, employee.BusySlots));
                        }
                        continue;
                    }

                    if (!line.TryExtractBusySlotForEmployeeWith(employeeId, out var busySlot))
                    {
                        continue;
                    }

                    if (employeesFound.ContainsKey(employeeId))
                    {
                        var employee = employeesFound.GetValueOrDefault(employeeId);
                        employee.BusySlots.Add(busySlot!);
                    }
                    else
                    {
                        employeesFound.Add(employeeId, (false, new List<BusySlot>{ busySlot! }));
                    }
                }
            }

            return employeesFound
                .Where(keyValuePair => keyValuePair.Value.EmployeeWasFound)
                .Select(keyValuePair => new Employee(keyValuePair.Key, keyValuePair.Value.BusySlots));
        }
    }
}