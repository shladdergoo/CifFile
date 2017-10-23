using System;
using System.Collections.Generic;
using System.IO;

namespace CifFile.Lib
{
    public delegate void BatchProcessedEventHandler(object sender, EventArgs e);

    public class ProcessingService : IProcessingService
    {
        private readonly ICifProcessor _cifProcessor;
        private readonly IOutputWriter _outputWriter;
        private readonly IFileSystem _fileSystem;

        public event EventHandler<BatchProcessedEventArgs> BatchProcessed;

        public ProcessingService(ICifProcessor cifProcessor, IOutputWriter outputWriter, IFileSystem fileSystem)
        {
            if (cifProcessor == null) throw new ArgumentNullException(nameof(cifProcessor));
            if (outputWriter == null) throw new ArgumentNullException(nameof(outputWriter));
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));

            _cifProcessor = cifProcessor;
            _outputWriter = outputWriter;
            _fileSystem = fileSystem;
        }

        private void OnBatchProcessed(BatchProcessedEventArgs e)
        {
            if (BatchProcessed != null) BatchProcessed(this, e);
        }

        public long Process(string filename, string outputLocation, int batchSize, string scheduleType)
        {
            List<List<string>> buffer = new List<List<string>>();
            long batchCount = 0;
            long recordCount = 0;

            ScheduleType scheduleTypeEnum = GetScheduleType(scheduleType);
            _outputWriter.Open(outputLocation, scheduleTypeEnum);
            _cifProcessor.Initialize(_fileSystem.FileOpen(filename, FileMode.Open));

            int bufferCount = 0;
            while ((bufferCount = _cifProcessor.ProcessBatch(buffer, batchSize, scheduleTypeEnum)) > 0)
            {
                batchCount++;
                recordCount += bufferCount;
                _outputWriter.Write(buffer);
                OnBatchProcessed(
                    new BatchProcessedEventArgs { BatchNumber = batchCount, BatchSize = bufferCount });
            }

            _outputWriter.Close();
            return recordCount;
        }

        private static ScheduleType GetScheduleType(string scheduleType)
        {
            if (scheduleType == "a") return ScheduleType.Association;

            if (scheduleType == "j") return ScheduleType.Journey;

            return ScheduleType.All;
        }
    }
}