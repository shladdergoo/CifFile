using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CifFile.Lib
{
    public class CifParser : ICifProcessor
    {
        private readonly IInputStreamFactory _inputStreamFactory;
        private readonly ICifRecordDefFactory _recordDefFactory;

        private StreamReader _reader;

        private long _parentId;

        public CifParser(IInputStreamFactory inputStreamFactory, ICifRecordDefFactory recordDefFactory)
        {
            _inputStreamFactory = inputStreamFactory ?? throw new ArgumentNullException(nameof(inputStreamFactory));
            _recordDefFactory = recordDefFactory ?? throw new ArgumentNullException(nameof(recordDefFactory));
        }

        public void Initialize(Stream inputStream)
        {
            if (inputStream == null) { throw new ArgumentNullException(nameof(inputStream)); }

            _reader = new StreamReader(inputStream);
        }

        public int ProcessBatch(IEnumerable<IEnumerable<string>> buffer, int batchSize, ScheduleType scheduleType,
            BatchArgs batchArgs)
        {
            if (_reader == null) throw new InvalidOperationException("CifParser not initialized");
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (batchSize < 1) throw new ArgumentException("batchSize must be greater than zero", nameof(batchSize));

            IEnumerable<CifRecordBase> recordDefs = _recordDefFactory.GetRecordDefs(scheduleType);
            List<List<string>> internalBuffer = new List<List<string>>();

            string line = null;
            List<string> lineValues = new List<string>();
            do
            {
                line = _reader.ReadLine();
                if (line == null) { continue; }

                lineValues = ParseLine(line, recordDefs);
                if (lineValues.Count > 0) { internalBuffer.Add(lineValues); }

            } while (line != null && internalBuffer.Count < batchSize);

            ((List<List<string>>)buffer).Clear();
            ((List<List<string>>)buffer).AddRange(internalBuffer);

            return internalBuffer.Count;
        }

        private List<string> ParseLine(string line, IEnumerable<CifRecordBase> recordDefs)
        {
            List<string> lineValues = new List<string>();

            CifRecordBase lineRecord = recordDefs
                .FirstOrDefault(r => line.StartsWith(r.RecordIdentifier));

            if (lineRecord == null) { return lineValues; }

            if (lineRecord.IsParent) { _parentId++; }

            lineValues.Add(_parentId.ToString());

            lineValues.AddRange(GetFieldValues(line, lineRecord));

            return lineValues;
        }

        private static IEnumerable<string> GetFieldValues(string line, CifRecordBase lineRecord)
        {
            List<string> fieldValues = new List<String>();

            int pos = 0;
            foreach (FieldInfo fieldInfo in lineRecord.Fields)
            {
                fieldValues.Add(line.Substring(pos, fieldInfo.Length));
                pos = pos + fieldInfo.Length;
            }

            return fieldValues;
        }
    }
}