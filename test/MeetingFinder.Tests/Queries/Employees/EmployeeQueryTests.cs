using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MeetingFinder.Api.Models;
using MeetingFinder.Api.Queries.Employees;
using MeetingFinder.Tests.Seed;
using Xunit;

namespace MeetingFinder.Tests.Queries.Employees
{
    public class EmployeeQueryTests
    {
        [Theory]
        [ClassData(typeof(EmployeeQueryTestData))]
        public async Task Should_ReturnEmployees_When_GetEmployees(string employeeFileData, IEnumerable<Employee> expectedEmployees)
        {
            var streamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(employeeFileData)));
            var employeeFileReader = new EmployeeFileReader(() => streamReader);
            var employeeQuery = new EmployeeQuery(() => employeeFileReader);
            
            var employees = await employeeQuery.GetEmployees(new[]
                {"192667735736991251148855951122872467118", "92667735736991251148855951122872467118"});
            
            employees.Should().BeEquivalentTo(expectedEmployees);
        }

        private class EmployeeQueryTestData : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    EmployeeFileData.GetWithEmployeeNamesBeforeTheirMeetings(),
                    GetEmployeesWithMathingMeetings(),
                },
                new object[]
                {
                    EmployeeFileData.GetWithEmployeeNamesAfterTheirMeetings(),
                    GetEmployeesWithMathingMeetings(),
                },
                new object[]
                {
                    EmployeeFileData.GetWithValidEmployessButWithNoMatchingMeetings(),
                    new List<Employee>
                    {
                        new Employee("192667735736991251148855951122872467118", new List<BusySlot>()),
                        new Employee("92667735736991251148855951122872467118", new List<BusySlot>()),
                    },
                },
                new object[]
                {
                    EmployeeFileData.GetWithValidEmployessButInvalidMeetings(),
                    new List<Employee>
                    {
                        new Employee("192667735736991251148855951122872467118", new List<BusySlot>()),
                        new Employee("92667735736991251148855951122872467118", new List<BusySlot>()),
                    },
                },
                new object[]
                {
                    EmployeeFileData.GetWithValidMeetingsButInvalidEmployees(),
                    new List<Employee>(),
                },
                new object[]
                {
                    EmployeeFileData.GetWithValidMeetingsButWithNoMatchingEmployees(),
                    new List<Employee>(),
                }
            };

            private static IList<Employee> GetEmployeesWithMathingMeetings()
            {
                return new List<Employee>
                {
                    new Employee("192667735736991251148855951122872467118",
                        new List<BusySlot>
                        {
                            new BusySlot(DateTime.Parse("2/12/2015 9:00:00 AM"),
                                DateTime.Parse("2/12/2015 10:30:00 AM")),
                            new BusySlot(DateTime.Parse("2/12/2015 11:00:00 AM"),
                                DateTime.Parse("2/12/2015 11:30:00 AM"))
                        }),
                    new Employee("92667735736991251148855951122872467118",
                        new List<BusySlot>
                        {
                            new BusySlot(DateTime.Parse("2/12/2015 9:00:00 AM"),
                                DateTime.Parse("2/12/2015 10:30:00 AM")),
                        }),
                };
            }

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}