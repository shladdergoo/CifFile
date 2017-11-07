using System.Collections.Generic;

namespace CifFile.Lib
{
    public class CifRecordDefFactory : ICifRecordDefFactory
    {
        public IEnumerable<CifRecordBase> GetRecordDefs(ScheduleType scheduleType)
        {
            if (scheduleType == ScheduleType.Association)
            {
                return new List<CifRecordBase> {
                    new AaRecord()
                };
            }

            else if (scheduleType == ScheduleType.Journey)
            {
                return new List<CifRecordBase> {
                    new HdRecord(),
                    new BsRecord(),
                    new BxRecord(),
                    new LoRecord(),
                    new LiRecord(),
                    new CrRecord(),
                    new LtRecord(),
                    new ZzRecord()
                };
            }

            return new List<CifRecordBase> {
                    new AaRecord(),
                    new HdRecord(),
                    new BsRecord(),
                    new BxRecord(),
                    new LoRecord(),
                    new LiRecord(),
                    new CrRecord(),
                    new LtRecord(),
                    new ZzRecord()
                };
        }
    }
}