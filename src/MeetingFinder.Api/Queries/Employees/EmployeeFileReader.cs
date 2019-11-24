using System.IO;
using System.Threading.Tasks;

namespace MeetingFinder.Api.Queries.Employees
{
    public class EmployeeFileReader : IEmployeeFileReader
    {
        private readonly StreamReader _streamReader;

        public EmployeeFileReader(StreamReader streamReader)
        {
            _streamReader = streamReader;
        }

        public async Task<EmployeeFileLine> ReadLineAsync()
        {
            var line = await _streamReader.ReadLineAsync();
            
            return line != null ? new EmployeeFileLine(line) : default;
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }
    }
}