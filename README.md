# MeetingFinder API

MeetingFinder API helps you find the best suitable time slot for a meeting based on different parameters

## Prerequisites
* .NET Core SDK 3.0

## Setup
1. Building and serving the API  
  run `dotnet run` inside a console from ./src/MeetingFinder.Api  
  This will start a server at http://localhost5000 and at https://localhost:5001  
2. Building and running the tests  
  run `dotnet test` inside a console from ./test/MeetingFinder.Tests  

## Trying the API
You can access the api documentation  page at https://localhost:5001/swagger/index.html  
Expand the /meetings endpoint panel by clicking on it and then by clicking "Try it out" which is situated on the right  
You can now use the textboxes to specify the request parameters and fetch suitable meetings based on those parameters

### Example
```
https://localhost:5001/meetings?EmployeeIds=23&EmployeeIds=43&EmployeeIds=345&DesiredMeetingLength=30&requestedEarliestMeetingTime=11/23/2019 08:00:00&requestedLatestMeetingTime=11/23/2019 09:00:00&officeHoursStartTime=08:00:00&officeHoursEndTime=10:00:00
```
