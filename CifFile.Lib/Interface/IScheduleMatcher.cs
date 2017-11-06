using System.Collections.Generic;

namespace CifFile.Lib
{
    public interface IScheduleMatcher
    {
        bool Match(IList<ScheduleCriteria> scheduleCriteria, string trainUid);
        bool Match(IList<ScheduleCriteria> scheduleCriteria, string trainUid, string stpIndicator);
        bool Match(IList<ScheduleCriteria> scheduleCriteria, string trainUid, string stpIndicator,
            string locationOrigin, string locationTerminates);
    }
}