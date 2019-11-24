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
            
            EmployeeFileLine line;
            using var reader = _employeeFileReaderProvider();
            var employeeIds = ids.ToList();
            while ((line = await reader.ReadLineAsync()) != null)
            {
                foreach (var id in employeeIds)
                {
                    if (line.ContainsNameForEmployeeWith(id))
                    {
                        if (!employeesFound.TryAdd(id, (true, new List<BusySlot>())))
                        {
                            var employee = employeesFound.GetValueOrDefault(id);
                            employeesFound.Remove(id);
                            employeesFound.Add(id, (true, employee.BusySlots));
                        }
                        continue;
                    }

                    if (!line.TryExtractBusySlotForEmployeeWith(id, out var busySlot))
                    {
                        continue;
                    }

                    if (employeesFound.ContainsKey(id))
                    {
                        var employee = employeesFound.GetValueOrDefault(id);
                        employee.BusySlots.Add(busySlot);
                    }
                    else
                    {
                        employeesFound.Add(id, (false, new List<BusySlot>{ busySlot }));
                    }
                }
            }

            return employeesFound
                .Where(keyValuePair => keyValuePair.Value.EmployeeWasFound)
                .Select(keyValuePair => new Employee(keyValuePair.Key, keyValuePair.Value.BusySlots));
        }
    }
}