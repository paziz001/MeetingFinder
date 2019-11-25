using System.IO;

namespace MeetingFinder.Tests.Fakes
{
    public class FakeStreamReader : StreamReader
    {   
        public FakeStreamReader() : base(new MemoryStream())
        {
        }
    }
}