using System.Collections.Generic;

namespace CifFile.Lib
{
    public interface IOutputWriter
    {
        void Open(string locator, ScheduleType scheduleType);
        
        void Write(IEnumerable<IEnumerable<string>> buffer);

        void Close();
    }
}