namespace CifFile.Lib
{
    public class ScheduleCriteria
    {
        public string TrainUID { get; }
        public string STPIndicator { get; }
        
        public ScheduleCriteria(string TrainUID, string STPIndicator)
        {
            this.STPIndicator = STPIndicator;
            this.TrainUID = TrainUID;

        }
    }
}