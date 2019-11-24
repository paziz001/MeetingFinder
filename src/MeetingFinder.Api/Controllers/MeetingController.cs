﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingFinder.Api.Models;
using MeetingFinder.Api.Queries.Employees;
using MeetingFinder.Api.Queries.Meetings;
using Microsoft.AspNetCore.Mvc;

namespace MeetingFinder.Api.Controllers
{
    [ApiController]
    [Route("/meetings")]
    public class MeetingController : ControllerBase
    {
        private readonly IEmployeeQuery _employeeQuery;
        private readonly IMeetingQuery _meetingQuery;

        public MeetingController(IEmployeeQuery employeeQuery, IMeetingQuery meetingQuery)
        {
            _employeeQuery = employeeQuery;
            _meetingQuery = meetingQuery;
        }

        [HttpGet]
        public async Task<IEnumerable<Meeting>> GetSuitableMeetings([FromQuery] SuitableMeetingsRequest suitableMeetingsRequest)
        {
            var employees = await _employeeQuery.GetEmployees(suitableMeetingsRequest.EmployeeIds ?? new List<string>());
            var allEmployeeBusySlots =
                employees.Aggregate(new List<BusySlot>() as IEnumerable<BusySlot>, (a, b) => a.Concat(b.BusySlots));
            return _meetingQuery.GetSuitableMeetings(
                allEmployeeBusySlots, suitableMeetingsRequest.DesiredMeetingLength,
                (suitableMeetingsRequest.requestedEarliestMeetingTime, suitableMeetingsRequest.requestedLatestMeetingTime),
                (suitableMeetingsRequest.officeHoursStartTime, suitableMeetingsRequest.officeHoursEndTime));
        }
    }
}