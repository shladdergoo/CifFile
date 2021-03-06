using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CifFile.Lib
{
    public class CifEditor : ICifProcessor
    {
        private readonly IInputStreamFactory _inputStreamFactory;
        private readonly ICifRecordDefFactory _recordDefFactory;
        private readonly IScheduleMatcher _scheduleMatcher;

        private StreamReader _reader;
        private List<string> _buffer;
        private IList<ScheduleCriteria> _scheduleCriteria;

        public CifEditor(IInputStreamFactory inputStreamFactory, ICifRecordDefFactory recordDefFactory,
            IScheduleMatcher scheduleMatcher)
        {
            _inputStreamFactory = inputStreamFactory ?? throw new ArgumentNullException(nameof(inputStreamFactory));
            _recordDefFactory = recordDefFactory ?? throw new ArgumentNullException(nameof(recordDefFactory));
            _scheduleMatcher = scheduleMatcher ?? throw new ArgumentNullException(nameof(scheduleMatcher));
        }

        public void Initialize(Stream inputStream)
        {
            _reader = new StreamReader(inputStream) ?? throw new ArgumentNullException(nameof(inputStream));
        }

        public int ProcessBatch(IEnumerable<IEnumerable<string>> buffer, int batchSize, ScheduleType scheduleType,
            BatchArgs batchArgs)
        {
            if (_reader == null) throw new InvalidOperationException("CifEditor not initialized");
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (batchArgs == null) throw new ArgumentNullException(nameof(batchArgs));

            _scheduleCriteria = GetScheduleCriteria(batchArgs);

            IEnumerable<CifRecordBase> recordDefs = _recordDefFactory.GetRecordDefs(scheduleType);

            _buffer = new List<string>();

            return DoProcessBatch(buffer, batchSize, recordDefs);
        }

        private int DoProcessBatch(IEnumerable<IEnumerable<string>> buffer, int batchSize,
            IEnumerable<CifRecordBase> recordDefs)
        {
            string line = null;
            int recordCount = 0;

            do
            {
                line = _reader.ReadLine();
                if (line == null) continue;

                ProcessLine(line, _reader, recordDefs);
                recordCount++;

            } while (line != null && recordCount < batchSize);

            ((List<List<string>>)buffer).Clear();
            if (_buffer.Any())
            {
                ((List<List<string>>)buffer).AddRange(new List<List<string>>() { _buffer.ToList() });
            }

            return recordCount;
        }

        private void ProcessLine(string line, StreamReader reader, IEnumerable<CifRecordBase> recordDefs)
        {
            List<string> lineValues = ParseLine(line, recordDefs);

            string recordId = lineValues.FirstOrDefault();

            if (recordId == null) return;

            switch (recordId)
            {
                case "HD":
                    _buffer.Add(line);
                    break;
                case "AA":
                    ProcessAssociation(line, lineValues);
                    break;
                case "BS":
                    ProcessSchedule(line, lineValues, reader, recordDefs);
                    break;
                case "ZZ":
                    _buffer.Add(line);
                    break;
            }
        }

        private void ProcessSchedule(string line, List<string> lineValues, StreamReader reader,
            IEnumerable<CifRecordBase> recordDefs)
        {
            string trainUid = lineValues[2];
            string stpIndicator = lineValues[25];
            string lo = null;
            string lt = null;

            if (!_scheduleMatcher.Match(_scheduleCriteria, trainUid, stpIndicator)) { return; }

            List<string> scheduleBuffer = new List<string> { line };

            string recordId;
            string scheduleLine;
            do
            {
                scheduleLine = _reader.ReadLine();
                if (scheduleLine == null) { break; }

                IList<string> scheduleLineValues = ParseLine(scheduleLine, recordDefs);
                recordId = scheduleLineValues[0];

                if (recordId != "BX") { scheduleBuffer.Add(scheduleLine); }
                if (recordId == "LO") { lo = recordId = scheduleLineValues[1]; }
                if (recordId == "LT") { lt = recordId = scheduleLineValues[1]; }

            } while (scheduleLine != null && recordId != "LT");

            if (_scheduleMatcher.Match(_scheduleCriteria, trainUid, stpIndicator, lo, lt))
            {
                _buffer.AddRange(scheduleBuffer);
            }
        }

        private void ProcessAssociation(string line, IList<string> lineValues)
        {
            string mainTrainUid = lineValues[2];
            string associatedTrainUid = lineValues[3];

            if (_scheduleMatcher.Match(_scheduleCriteria, mainTrainUid) ||
                _scheduleMatcher.Match(_scheduleCriteria, associatedTrainUid))
            {
                _buffer.Add(line);
            }
        }

        private List<string> ParseLine(string line, IEnumerable<CifRecordBase> recordDefs)
        {
            List<string> lineValues = new List<string>();

            CifRecordBase lineRecord = recordDefs
                .FirstOrDefault(r => line.StartsWith(r.RecordIdentifier));

            if (lineRecord == null) { return lineValues; }

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

        private IList<ScheduleCriteria> GetScheduleCriteria(BatchArgs batchArgs)
        {
            return batchArgs?.ScheduleCriteria?.ToList() ?? new List<ScheduleCriteria>();
        }
    }
}