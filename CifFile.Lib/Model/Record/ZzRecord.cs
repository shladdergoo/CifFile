using System.Collections.Generic;

namespace CifFile.Lib
{
    public class ZzRecord : CifRecordBase
    {
        private readonly List<FieldInfo> _fields;

        public ZzRecord()
        {
            _fields = new List<FieldInfo>{
                new FieldInfo("Record_Identity", 2),
                new FieldInfo("Spare", 78)
            };
        }

        public override bool IsParent { get { return false; } }

        public override string RecordIdentifier { get { return "ZZ"; } }

        public override IEnumerable<FieldInfo> Fields { get { return _fields; } }
    }
}