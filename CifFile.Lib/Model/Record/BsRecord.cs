using System.Collections.Generic;

namespace CifFile.Lib
{
    public class BsRecord : CifRecordBase
    {
        private readonly List<FieldInfo> _fields;

        public BsRecord()
        {
            _fields = new List<FieldInfo>{
                new FieldInfo("Record_Identity", 2),
                new FieldInfo("Transaction_Type", 1),
                new FieldInfo("Train_UID", 6),
                new FieldInfo("Date_Runs_From", 6),
                new FieldInfo("Date_Runs_To", 6),
                new FieldInfo("Days_Run", 7),
                new FieldInfo("Bank_Holiday_Running", 1),
                new FieldInfo("Train_Status", 1),
                new FieldInfo("Train_Category", 2),
                new FieldInfo("Train_Identity_Sig_ID", 4),
                new FieldInfo("NRS_Headcode", 4),
                new FieldInfo("Course_Indicator", 1),
                new FieldInfo("Train_Service_Code", 8),
                new FieldInfo("Portion_ID", 1),
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
                new FieldInfo("Spare", 1),
                new FieldInfo("STP_Indicator", 1)
            };
        }

        public override bool IsParent { get { return true; } }

        public override string RecordIdentifier { get { return "BS"; } }

        public override IEnumerable<FieldInfo> Fields { get { return _fields; } }
    }
}