namespace CifFile.Lib
{
    public class ScheduleCriteria
    {
        public string TrainUID { get; }
        public string STPIndicator { get; }
        public string LocationOrigin { get; }
        public string LocationTerminates { get; }

        public ScheduleCriteria(string trainUID, string stpIndicator, string locationOrigin,
            string locationTerminates)
        {
            LocationTerminates = locationTerminates;
            LocationOrigin = locationOrigin;
            STPIndicator = stpIndicator;
            TrainUID = trainUID;
        }
    }
}