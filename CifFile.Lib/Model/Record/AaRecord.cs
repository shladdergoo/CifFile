using System.Collections.Generic;

namespace CifFile.Lib
{
    public class AaRecord : CifRecordBase
    {
        private readonly List<FieldInfo> _fields;

        public AaRecord()
        {
            _fields = new List<FieldInfo>{
                new FieldInfo("Record_Identity", 2),
                new FieldInfo("Transaction_Type", 1),
                new FieldInfo("Main_Train_UID", 6),
                new FieldInfo("Associated_Train_UID", 6),
                new FieldInfo("Association_Start_Date", 6),
                new FieldInfo("Association_End_Date", 6),
                new FieldInfo("Association_Days", 7),
                new FieldInfo("Association_Category", 2),
                new FieldInfo("Association_Date_Ind", 1),
                new FieldInfo("Association_Location", 7),
                new FieldInfo("Base_Location_Suffix", 1),
                new FieldInfo("Assoc_Location_Suffix", 1),
                new FieldInfo("Diagram_Type", 1),
                new FieldInfo("Association_Type", 1),
                new FieldInfo("Spare", 31),
                new FieldInfo("STP_Indicator", 1)
            };
        }

        public override bool IsParent { get { return true; } }

        public override string RecordIdentifier { get { return "AA"; } }

        public override IEnumerable<FieldInfo> Fields { get { return _fields; } }
    }
}