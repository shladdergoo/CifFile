using System.Collections.Generic;

namespace CifFile.Lib
{
    public class HdRecord : CifRecordBase
    {
        private readonly List<FieldInfo> _fields;

        public HdRecord()
        {
            _fields = new List<FieldInfo>{
                new FieldInfo("Record_Identity", 2),
                new FieldInfo("File_Mainframe_Identity", 20),
                new FieldInfo("Date_Of_Extract", 6),
                new FieldInfo("Time_Of_Extract", 4),
                new FieldInfo("Current_File_Ref", 7),
                new FieldInfo("Last_File_Ref", 7),
                new FieldInfo("Update_Ind", 1),
                new FieldInfo("Version", 1),
                new FieldInfo("User_Extract_Start_Date", 6),
                new FieldInfo("User_Extract_End_Date", 6),
                new FieldInfo("Spare", 20)
            };
        }

        public override bool IsParent { get { return false; } }

        public override string RecordIdentifier { get { return "HD"; } }

        public override IEnumerable<FieldInfo> Fields { get { return _fields; } }
    }
}