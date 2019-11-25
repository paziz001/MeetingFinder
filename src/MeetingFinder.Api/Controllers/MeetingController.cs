using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MeetingFinder.Api.Models;
using MeetingFinder.Api.Queries.Employees;
using MeetingFinder.Api.Queries.Meetings;
using MeetingFinder.Api.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingFinder.Api.Controllers
{
    [ApiController]
    [Route("/meetings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MeetingController : ControllerBase
    {
        private readonly IEmployeeQuery _employeeQuery;
        private readonly IMeetingQuery _meetingQuery;
        private readonly IValidator<SuitableMeetingsRequest> _validator;

        public MeetingController(IEmployeeQuery employeeQuery, IMeetingQuery meetingQuery, IValidator<SuitableMeetingsRequest> validator)
        {
            _employeeQuery = employeeQuery;
            _meetingQuery = meetingQuery;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meeting>>> GetSuitableMeetings([FromQuery] SuitableMeetingsRequest suitableMeetingsRequest)
        {
            var validationResult = _validator.Validate(suitableMeetingsRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }
            var employees = await _employeeQuery.GetEmployees(suitableMeetingsRequest.EmployeeIds ?? new List<string>());
            var allEmployeeBusySlots =
                employees.Aggregate(new List<BusySlot>() as IEnumerable<BusySlot>, (a, b) => a.Concat(b.BusySlots));
            var suitableMeetings =  _meetingQuery.GetSuitableMeetings(
                allEmployeeBusySlots, suitableMeetingsRequest.DesiredMeetingLength,
                (suitableMeetingsRequest.requestedEarliestMeetingTime, suitableMeetingsRequest.requestedLatestMeetingTime),
                (suitableMeetingsRequest.officeHoursStartTime, suitableMeetingsRequest.officeHoursEndTime));


            return Ok(suitableMeetings);
        }
    }
}