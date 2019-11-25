using System.IO;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using MeetingFinder.Api.Queries.Employees;
using MeetingFinder.Tests.Fakes;
using Xunit;

namespace MeetingFinder.Tests.Queries.Employees
{
    public class EmployeeFileReaderTests
    {
        private readonly StreamReader _streamReader;
        private readonly IEmployeeFileReader _employeeFileReader;
        public EmployeeFileReaderTests()
        {
            _streamReader = A.Fake<FakeStreamReader>();
            _employeeFileReader = new EmployeeFileReader(() => _streamReader);
        }

        [Fact]
        public async Task Should_ReturnEmployeeFileLine_When_ReadLineAsync()
        {
            A.CallTo( () => _streamReader.ReadLineAsync()).Returns(Task.FromResult("test"));
            var line = await _employeeFileReader.ReadLineAsync();
            line.Should().NotBeNull();
            A.CallTo(() => _streamReader.ReadLineAsync()).MustHaveHappenedOnceExactly();

        }
        
        [Fact]
        public async Task Should_ReturnNull_When_ReadLineAsync()
        {
            A.CallTo( () => _streamReader.ReadLineAsync()).Returns(Task.FromResult((string)null));
            var line = await _employeeFileReader.ReadLineAsync();
            line.Should().BeNull();
            A.CallTo(() => _streamReader.ReadLineAsync()).MustHaveHappenedOnceExactly();

        }
    }
}