using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using MeetingFinder.Api.Queries.Employees;
using Xunit;

namespace MeetingFinder.Tests.Queries.Employees
{
    public class EmployeeFileLineTests
    {
        [Theory]
        [InlineData("57646786307395936680161735716561753784", "57646786307395936680161735716561753784;Al Pacino")]
        [InlineData("161735756561753784", "161735756561753784;Leo Dicaprio")]
        [InlineData("161", "161;Joaquin Phoenix")]
        public void Should_BeTrue_When_ContainsNameForEmployeeWith(string id, string line)
        {
            var employeeFileLine = new EmployeeFileLine(line);

            employeeFileLine.ContainsNameForEmployeeWith(id).Should().BeTrue();
        }

        [Theory]
        [InlineData("57646786307395936680161735716561753784", "80161735716561753784;Al Pacino")]
        [InlineData("57646786307395936680161735716561753784", "57646786307395936680161735716561753784 Al Pacino")]
        [InlineData("57646786307395936680161735716561753784", "57646786307395936680161735716561753784 Al Pacino;")]
        [InlineData("57646786307395936680161735716561753784",
            "57646786307395936680161735716561753784;Al Paci23123234234no")]
        [InlineData("57646786307395936680161735716561753784",
            "57646786307395936680161735716561753784;Al Pacino;576467863073959366801")]
        public void Should_BeFalse_When_ContainsNameForEmployeeWith(string id, string line)
        {
            var employeeFileLine = new EmployeeFileLine(line);

            employeeFileLine.ContainsNameForEmployeeWith(id).Should().BeFalse();
        }

        [Theory]
        [ClassData(typeof(TryExtractBusySlotForEmployeeWithDataGenerator))]
        public void Should_ReturnFalseAndDefaultBusySlot_When_TryExtractBusySlotForEmployeeWith(string line)
        {
            var employeeFileLine = new EmployeeFileLine(line);
            var employeeId = "424";
            employeeFileLine.TryExtractBusySlotForEmployeeWith(employeeId, out var busySlot).Should().BeFalse();
            busySlot.Should().BeNull();
            busySlot.Should().Be(default);
        }
        
        [Fact]
        public void Should_ReturnTrueAndBusySlot_When_TryExtractBusySlotForEmployeeWith()
        {
            const string line = "42451679698671819029426836087187605499;" +
                                "3/27/2015 7:00:00 AM;3/27/2015 10:30:00 AM;" +
                                "630D43370D3E3B743142F0448BB3B12A648A3B0A262F138" +
                                "C0597A660B982C9273FC015A2E45BE95A4AD902AD743C0FF" +
                                "1C138771361CA101E469FA002EF9DA6102B90125B6B33ACF2B" +
                                "F6AA63F1589DEC205BC64045602F567A93D1A55201880BCC02E" +
                                "42E6DCE9E07BFC0D600A07CA21EEBB054A7F8DF4E5531938332AFBD4655A";
            var employeeFileLine = new EmployeeFileLine(line);
            const string employeeId = "42451679698671819029426836087187605499";
            employeeFileLine.TryExtractBusySlotForEmployeeWith(employeeId, out var busySlot).Should().BeTrue();
            busySlot.Should().NotBeNull();
        }

        private class TryExtractBusySlotForEmployeeWithDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {"424;3/27/2015 7:00:00 AM;630"},
                new object[] {"424;3/27/2015 7:00:00 AM;3/27/2015 10:30:00 ;"},
                new object[] {"3/27/2015 7:00:00 ;3/27/2015 10:30:00 AM;630"},
                new object[] {"424;3/27/2015 7:00:00 ;3/27/2015 10:30:00 AM;63"},
                new object[] {"424;3/27/2015 7:00:00 AM;3/27/2015 10:30:00 ;63"}
            };
            
            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}