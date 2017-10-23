using System.Collections.Generic;

namespace CifFile.Lib
{
    public abstract class CifRecordBase
    {
        public virtual bool IsParent { get; }
        public virtual string RecordIdentifier { get; }
        public virtual IEnumerable<FieldInfo> Fields { get; }
    }
}