using System.Collections.Generic;

namespace CifFile.Lib
{
    public interface ICifRecordDefFactory
    {
         IEnumerable<CifRecordBase> GetRecordDefs(ScheduleType scheduleType); 
    }
}