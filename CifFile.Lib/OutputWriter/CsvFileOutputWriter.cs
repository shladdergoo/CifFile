using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CifFile.Lib
{
    public class CsvFileOutputWriter : IOutputWriter
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICifRecordDefFactory _recordDefFactory;
        private Dictionary<string, RecordWriter> _writers;

        private const char Separator = ',';

        public CsvFileOutputWriter(IFileSystem fileSystem, ICifRecordDefFactory recordDefFactory)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            _recordDefFactory = recordDefFactory ?? throw new ArgumentNullException(nameof(recordDefFactory));
        }

        public void Open(string outputDir, ScheduleType scheduleType)
        {
            if (outputDir == null) { throw new ArgumentNullException(nameof(outputDir)); }
            if (!_fileSystem.DirectoryExists(outputDir)) { _fileSystem.CreateDirectory(outputDir); }

            IEnumerable<CifRecordBase> recordDefs = _recordDefFactory.GetRecordDefs(scheduleType);
            _writers = new Dictionary<string, RecordWriter>();

            foreach (CifRecordBase cifRecord in recordDefs)
            {
                string recordIdentifier = cifRecord.RecordIdentifier;

                string outputFilepath = Path.Combine(outputDir, recordIdentifier + ".csv");
                _writers.Add(recordIdentifier,
                    new RecordWriter
                    {
                        Writer = new StreamWriter(_fileSystem.FileOpen(outputFilepath, FileMode.Create)),
                        Fields = cifRecord.Fields
                    });
            }
        }

        public void Write(IEnumerable<IEnumerable<string>> buffer)
        {
            if (_writers == null) { throw new InvalidOperationException("Writer not open."); }
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer)); }

            foreach (IEnumerable<string> lineStrings in buffer)
            {
                WriteLine(lineStrings);
            }

            FlushAll();
        }

        public void Close()
        {
            FlushAll();
            DisposeAll();
        }

        private void WriteLine(IEnumerable<string> lineStrings)
        {
            string recordIdentifier = lineStrings.ElementAt(1);
            StreamWriter writer = _writers[recordIdentifier].Writer;

            if (IsFirstRow(recordIdentifier)) { WriteHeaders(writer, recordIdentifier); }

            StringBuilder builder = new StringBuilder();

            foreach (string field in lineStrings)
            {
                builder.Append(field);
                builder.Append(Separator);
            }
            builder.Remove(builder.Length - 1, 1);

            writer.WriteLine(builder.ToString());
            _writers[recordIdentifier].WriteCount++;
        }

        private bool IsFirstRow(string recordIdentifier)
        {
            return _writers[recordIdentifier].WriteCount == 0;
        }

        private void WriteHeaders(StreamWriter writer, string recordIdentifier)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Key");
            builder.Append(Separator);

            foreach (FieldInfo FieldInfo in _writers[recordIdentifier].Fields)
            {
                builder.Append(FieldInfo.Name);
                builder.Append(Separator);
            }

            builder.Remove(builder.Length - 1, 1);

            writer.WriteLine(builder.ToString());
            writer.Flush();
        }

        private void FlushAll()
        {
            foreach (RecordWriter writer in _writers.Values)
            {
                writer.Writer.Flush();
            }
        }

        private void DisposeAll()
        {
            foreach (RecordWriter writer in _writers.Values)
            {
                writer.Writer.Dispose();
            }
        }

        class RecordWriter
        {
            public StreamWriter Writer { get; set; }
            public IEnumerable<FieldInfo> Fields { get; set; }
            public long WriteCount { get; set; }
        }
    }
}