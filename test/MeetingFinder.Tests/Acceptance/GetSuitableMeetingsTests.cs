using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using MeetingFinder.Api;
using MeetingFinder.Api.Models;
using MeetingFinder.Api.Queries.Employees;
using MeetingFinder.Api.Queries.Meetings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace MeetingFinder.Tests.Acceptance
{
    public class GetMeetingsTests : IDisposable
    {
        private readonly HttpClient _client;
        private readonly IHost _host;
        private IEmployeeQuery _employeeQuery;
        private IMeetingQuery _meetingQuery;

        public GetMeetingsTests()
        {
            _meetingQuery = A.Fake<IMeetingQuery>();
            _employeeQuery = A.Fake<IEmployeeQuery>();
            _host = new HostBuilder() 
                .ConfigureWebHost(webBuilder => 
                { 
                    webBuilder 
                        .UseTestServer() 
                        .Configure(app => { })
                        .UseStartup<Startup>()
                        .ConfigureTestServices(services =>
                        {
                            services.AddTransient(provider => _employeeQuery)
                                    .AddTransient(provider => _meetingQuery);
                        });
                })
                .Start();
            _client = _host.GetTestClient();
        }

        [Fact]
        public async Task Should_Return200OK_When_ValidParametersFor_GetSuitableMeetings()
        {
            var apiEndpoint = "/meetings?EmployeeIds=23&EmployeeIds=43&EmployeeIds=345&DesiredMeetingLength=30&" +
                              "requestedEarliestMeetingTime=11/23/2019 08:00:00&requestedLatestMeetingTime=11/23/2019 09:00:00&" +
                              "officeHoursStartTime=08:00:00&officeHoursEndTime=10:00:00";

            A.CallTo(() => _employeeQuery.GetEmployees(A<IEnumerable<string>>._))
                .Returns(Task.FromResult(A.Dummy<IEnumerable<Employee>>()));

            var response = await  _client.GetAsync(apiEndpoint);

            A.CallTo(() => _employeeQuery.GetEmployees(A<IEnumerable<string>>.That.IsSameSequenceAs("23", "43", "345")))
                .MustHaveHappenedOnceExactly();
            
            var requestedEarliestTime = (DateTime.Parse("11/23/2019 08:00:00"), DateTime.Parse("11/23/2019 09:00:00"));
            var officeHours = (TimeSpan.Parse("08:00:00"), TimeSpan.Parse("10:00:00"));
            A.CallTo(() => _meetingQuery.GetSuitableMeetings(A<IEnumerable<BusySlot>>._, 30,
                    requestedEarliestTime, officeHours))
                .MustHaveHappened();
            
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);

        }
        
        [Fact]
        public async Task Should_Return400BadRequest_When_InvalidParametersFor_GetSuitableMeetings()
        {
            var apiEndpoint = "/meetings?EmployeeIds=23&EmployeeIds=43&EmployeeIds=345&DesiredMeetingLength=30&" +
                              "requestedEarliestMeetingTime=11/23/2019 08:00:00&requestedLatestMeetingTime=11/23/2019 09:00:00&" +
                              "officeHoursStartTime=10:00:00&officeHoursEndTime=09:00:00";
            
            var response = await  _client.GetAsync(apiEndpoint);

            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        public void Dispose()
        {
            _client?.Dispose();
            _host?.Dispose();
        }
    }
}