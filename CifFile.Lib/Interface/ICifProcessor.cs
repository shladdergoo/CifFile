using System.Collections.Generic;
using System.IO;

namespace CifFile.Lib
{
    public interface ICifProcessor
    {
        void Initialize(Stream inputStream);
        int ProcessBatch(IEnumerable<IEnumerable<string>> buffer, int batchSize, ScheduleType scheduleType);
    }
}