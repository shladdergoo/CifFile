using System.Collections.Generic;

namespace CifFile.Lib
{
    public class CrRecord: CifRecordBase
    {   
        private readonly List<FieldInfo> _fields;

        public CrRecord()
        {
            _fields = new List<FieldInfo>{
                new FieldInfo("Record_Identity", 2),
                new FieldInfo("Location", 8),
                new FieldInfo("Train_Category", 2),
                new FieldInfo("Train_Identity", 4),
                new FieldInfo("Headcode", 4),
                new FieldInfo("Course_Indicator", 1),
                new FieldInfo("Train_Service_Code", 8),
                new FieldInfo("Portion_Id", 1),
                new FieldInfo("Power_Type", 3),
                new FieldInfo("Timing_Load", 4),
                new FieldInfo("Speed", 3),
                new FieldInfo("Operating_Characteristics", 6),
                new FieldInfo("Seating_Class", 1),
                new FieldInfo("Sleepers", 1),
                new FieldInfo("Reservations", 1),
                new FieldInfo("Connection_Indicator", 1),
                new FieldInfo("Catering_Code", 4),
                new FieldInfo("Service_Branding", 4),
                new FieldInfo("Traction_Class", 4),
                new FieldInfo("UIC_Code", 5),
                new FieldInfo("Reserved", 8),
                new FieldInfo("Spare", 5)
            };
        }

        public override bool IsParent { get { return false; } }

        public override string RecordIdentifier { get { return "CR"; } }

        public override IEnumerable<FieldInfo> Fields { get { return _fields; } }
    }
}