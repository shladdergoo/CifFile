using System.Collections.Generic;
using System.Linq;

namespace CifFile.Lib
{
    public class ScheduleMatcher : IScheduleMatcher
    {
        public bool Match(IList<ScheduleCriteria> scheduleCriteria, string trainUid)
        {
            if (trainUid == null) { throw new System.ArgumentNullException(nameof(trainUid)); }

            return DoMatch(scheduleCriteria, trainUid, null, null, null);
        }

        public bool Match(IList<ScheduleCriteria> scheduleCriteria, string trainUid,
            string stpIndicator)
        {
            if (trainUid == null) { throw new System.ArgumentNullException(nameof(trainUid)); }
            if (stpIndicator == null) { throw new System.ArgumentNullException(nameof(stpIndicator)); }

            return DoMatch(scheduleCriteria, trainUid, stpIndicator, null, null);
        }

        public bool Match(IList<ScheduleCriteria> scheduleCriteria, string trainUid, string stpIndicator,
            string locationOrigin, string locationTerminates)
        {
            if (trainUid == null) { throw new System.ArgumentNullException(nameof(trainUid)); }
            if (stpIndicator == null) { throw new System.ArgumentNullException(nameof(stpIndicator)); }
            if (locationOrigin == null) { throw new System.ArgumentNullException(nameof(locationOrigin)); }
            if (locationTerminates == null) { throw new System.ArgumentNullException(nameof(locationTerminates)); }

            return DoMatch(scheduleCriteria, trainUid, stpIndicator, locationOrigin, locationTerminates);
        }

        bool DoMatch(IList<ScheduleCriteria> scheduleCriteria, string trainUid, string stpIndicator,
            string locationOrigin, string locationTerminates)
        {
            if (scheduleCriteria == null || !scheduleCriteria.Any()) { return true; }

            return scheduleCriteria.Any(c => c.TrainUID == trainUid &&
                (stpIndicator == null || c.STPIndicator == stpIndicator) &&
                (locationOrigin == null || c.LocationOrigin == locationOrigin) &&
                (locationTerminates == null || c.LocationTerminates == locationTerminates));
        }
    }
}