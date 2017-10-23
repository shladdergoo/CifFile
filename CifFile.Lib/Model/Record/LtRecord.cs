using System.Collections.Generic;

namespace CifFile.Lib
{
    public class LtRecord: CifRecordBase
    {
        
        private readonly List<FieldInfo> _fields;

        public LtRecord()
        {
            _fields = new List<FieldInfo>{
                new FieldInfo("Record_Identity", 2),
                new FieldInfo("Location", 8),
                new FieldInfo("Scheduled_Arrival", 5),
                new FieldInfo("Public_Arrival", 4),
                new FieldInfo("Platform", 3),
                new FieldInfo("Path", 3),
                new FieldInfo("Activity", 12),
                new FieldInfo("Reserved", 2),
                new FieldInfo("Spare", 40)
            };
        }

        public override bool IsParent { get { return false; } }

        public override string RecordIdentifier { get { return "LT"; } }

        public override IEnumerable<FieldInfo> Fields { get { return _fields; } }
    }
}