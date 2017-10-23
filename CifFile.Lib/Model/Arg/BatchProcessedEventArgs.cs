using System;

namespace CifFile.Lib
{
    public class BatchProcessedEventArgs : EventArgs
    {
        public long BatchNumber { get; set; }
        public long BatchSize { get; set; }
    }
}