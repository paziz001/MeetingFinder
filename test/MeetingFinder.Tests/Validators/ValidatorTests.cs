using System;
using System.Linq;
using FluentValidation.TestHelper;
using MeetingFinder.Api.Queries.Meetings;
using MeetingFinder.Api.Validators;
using Xunit;

namespace MeetingFinder.Tests.Validators
{
    public class ValidatorTests : IClassFixture<SuitableMeetingsRequestValidator>
    {
        private readonly SuitableMeetingsRequestValidator _validator;

        public ValidatorTests(SuitableMeetingsRequestValidator validator)
        {
            _validator = validator;
        }

        [Fact]
        public void Should_ReturnError_When_EmployeeIdsExceed40()
        {
            var suitableMeetingsRequest = new SuitableMeetingsRequest
            {
                EmployeeIds = Enumerable.Repeat("1", 41)
            };

            _validator.ShouldHaveValidationErrorFor(x => x.EmployeeIds.Count(), suitableMeetingsRequest);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_ReturnError_When_DesiredMeetingLengthLoweOrEqualTo0(int desiredMeetingLength)
        {
            var suitableMeetingsRequest = new SuitableMeetingsRequest
            {
                EmployeeIds = Enumerable.Repeat("1", 40),
                DesiredMeetingLength = desiredMeetingLength
            };

            _validator.ShouldHaveValidationErrorFor(x => x.DesiredMeetingLength, suitableMeetingsRequest);
        }
        
        [Theory]
        [InlineData("11/23/2019 11:00:00", "11/23/2019 11:00:00")]
        [InlineData("11/23/2019 13:00:00", "11/23/2019 11:00:00")]
        public void Should_ReturnError_When_RequestedEarliestDateTimeGreaterOrEqualToLatestTime(
            string requestedEarliestMeetingTime, string requestedLatestMeetingTime)
        {
            var suitableMeetingsRequest = new SuitableMeetingsRequest
            {
                EmployeeIds = Enumerable.Repeat("1", 40),
                DesiredMeetingLength = 1,
                requestedEarliestMeetingTime = DateTime.Parse(requestedEarliestMeetingTime),
                requestedLatestMeetingTime = DateTime.Parse(requestedLatestMeetingTime)
            };

            _validator.ShouldHaveValidationErrorFor(x => x.requestedEarliestMeetingTime, suitableMeetingsRequest);
        }
        
        [Theory]
        [InlineData("11:00:00", "11:00:00")]
        [InlineData("13:00:00", "11:00:00")]
        public void Should_ReturnError_When_OfficeHoursStartTimeGreaterOrEqualToEndTime(
            string officeHoursStartTime, string officeHoursEndTime)
        {
            var suitableMeetingsRequest = new SuitableMeetingsRequest
            {
                EmployeeIds = Enumerable.Repeat("1", 40),
                DesiredMeetingLength = 1,
                requestedEarliestMeetingTime = DateTime.Parse("11/23/2019 11:00:00"),
                requestedLatestMeetingTime = DateTime.Parse("11/23/2019 11:30:00"),
                officeHoursStartTime = TimeSpan.Parse(officeHoursStartTime),
                officeHoursEndTime = TimeSpan.Parse(officeHoursEndTime),
                
            };

            _validator.ShouldHaveValidationErrorFor(x => x.officeHoursStartTime, suitableMeetingsRequest);
        }
    }
}