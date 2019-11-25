using System.Linq;
using FluentValidation;
using MeetingFinder.Api.Queries.Meetings;

namespace MeetingFinder.Api.Validators
{
    public class SuitableMeetingsRequestValidator : AbstractValidator<SuitableMeetingsRequest>
    {
        public SuitableMeetingsRequestValidator()
        {
            RuleFor(x => x.EmployeeIds.Count()).LessThanOrEqualTo(40)
                .WithMessage("Employee ids requested should be less or equal to 40");
            RuleFor(x => x.DesiredMeetingLength).GreaterThan(0)
                .WithMessage("Desired meeting length should be greater than 0");
            RuleFor(x => x.requestedEarliestMeetingTime)
                .Must((x, requestedEarliestMeetingTime) => x.requestedLatestMeetingTime > requestedEarliestMeetingTime)
                .WithMessage("Requested latest time should be greater than requested earliest time");
            RuleFor(x => x.officeHoursStartTime)
                .Must((x, officeHoursStartTime) => x.officeHoursEndTime > officeHoursStartTime)
                .WithMessage("Requested latest time should be greater than requested earliest time");
        }
    }
}