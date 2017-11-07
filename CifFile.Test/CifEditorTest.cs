using System;
using System.Collections.Generic;
using System.IO;

using NSubstitute;
using Xunit;

using CifFile.Lib;

namespace CifFile.Test
{
    public class CifEditorTest
    {
        private readonly IInputStreamFactory _inputStreamFactory;
        private readonly ICifRecordDefFactory _recordDefFactory;
        private readonly IScheduleMatcher _scheduleMatcher;

        public CifEditorTest()
        {
            _inputStreamFactory = Substitute.For<IInputStreamFactory>();
            _recordDefFactory = Substitute.For<ICifRecordDefFactory>();
            _scheduleMatcher = Substitute.For<IScheduleMatcher>();
        }

        [Fact]
        public void Initialize_StreamNull_ThrowsException()
        {
            ICifProcessor sut = new CifEditor(_inputStreamFactory,
                _recordDefFactory, _scheduleMatcher);

            Assert.Throws<ArgumentNullException>(() => { sut.Initialize(null); });
        }

        [Fact]
        public void ProcessBatch_NotInitialised_ThrowsException()
        {
            ICifProcessor sut = new CifEditor(_inputStreamFactory,
                _recordDefFactory, _scheduleMatcher);

            Assert.Throws<InvalidOperationException>(() =>
            {
                sut.ProcessBatch(default(List<List<string>>), 0, ScheduleType.All, null);
            });
        }

        [Fact]
        public void ProcessBatch_EOFReached_ReturnsLastBatch()
        {
            ICifProcessor sut = new CifEditor(_inputStreamFactory,
                _recordDefFactory, _scheduleMatcher);

            sut.Initialize(new MemoryStream());
            List<List<string>> buffer = new List<List<string>>();

            int result = sut.ProcessBatch(buffer, 999, ScheduleType.All, 
                new BatchArgs(new List<ScheduleCriteria>()));

            Assert.Equal(0, result);
            Assert.Equal(0, buffer.Count);
        }
    }
}