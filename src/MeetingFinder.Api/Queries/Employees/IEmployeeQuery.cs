using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingFinder.Api.Models;

namespace MeetingFinder.Api.Queries.Employees
{
    public interface IEmployeeQuery
    {
        Task<Employee> GetEmployeeBy(string id);
        
        Task<IEnumerable<Employee>> GetEmployees(IEnumerable<string> ids);
    }
}