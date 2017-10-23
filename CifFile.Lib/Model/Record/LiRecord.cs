using System.Collections.Generic;

namespace CifFile.Lib
{
    public class LiRecord: CifRecordBase
    {
        
        private readonly List<FieldInfo> _fields;

        public LiRecord()
        {
            _fields = new List<FieldInfo>{
                new FieldInfo("Record_Identity", 2),
                new FieldInfo("Location", 8),
                new FieldInfo("Scheduled_Arrival", 5),
                new FieldInfo("Scheduled_Departure", 5),
                new FieldInfo("Scheduled_Pass", 5),
                new FieldInfo("Public_Arrival", 4),
                new FieldInfo("Public_Departure", 4),
                new FieldInfo("Platform", 3),
                new FieldInfo("Line", 3),
                new FieldInfo("Path", 3),
                new FieldInfo("Activity", 12),
                new FieldInfo("Engineering_Allowance", 2),
                new FieldInfo("Pathing_Allowance", 2),
                new FieldInfo("Performance_Allowance", 2),
                new FieldInfo("Reserved", 5),
                new FieldInfo("Spare", 15)
            };
        }

        public override bool IsParent { get { return false; } }

        public override string RecordIdentifier { get { return "LI"; } }

        public override IEnumerable<FieldInfo> Fields { get { return _fields; } }
    }
}