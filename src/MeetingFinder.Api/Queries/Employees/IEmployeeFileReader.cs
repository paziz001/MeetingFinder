using System;
using System.Threading.Tasks;

namespace MeetingFinder.Api.Queries.Employees
{
    public interface IEmployeeFileReader : IDisposable
    {
        Task<EmployeeFileLine?> ReadLineAsync();
    }
}