using System.Collections.Generic;

namespace CifFile.Lib
{
    public class BxRecord: CifRecordBase
    {
        private readonly List<FieldInfo> _fields;

        public BxRecord()
        {
            _fields = new List<FieldInfo>{
                new FieldInfo("Record_Identity", 2),
                new FieldInfo("Traction_Class", 4),
                new FieldInfo("UIC_Code", 5),
                new FieldInfo("ATOC_Code", 2),
                new FieldInfo("Applicable_Timetable_Code", 1),
                new FieldInfo("Reserved1", 8),
                new FieldInfo("Reserved2", 1),
                new FieldInfo("Spare", 57)
            };
        }

        public override bool IsParent { get { return false; } }

        public override string RecordIdentifier { get { return "BX"; } }

        public override IEnumerable<FieldInfo> Fields { get { return _fields; } }
    }
}