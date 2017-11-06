using System.Collections.Generic;

namespace CifFile.Lib
{
    public class BatchArgs
    {
        public IEnumerable<ScheduleCriteria> ScheduleCriteria { get; }
        
        public BatchArgs(IEnumerable<ScheduleCriteria> scheduleCriteria)
        {
            ScheduleCriteria = scheduleCriteria;
        }
    }
}