using System;

namespace CifFile.Lib
{
    public interface IProcessingService
    {
        event EventHandler<BatchProcessedEventArgs> BatchProcessed;
        long Process(string filename, string outputLocation, int batchSize, string scheduleType);
    }
}