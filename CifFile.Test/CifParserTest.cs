using System;
using System.Collections.Generic;
using System.IO;

using NSubstitute;
using Xunit;

using CifFile.Lib;

namespace CifFile.Test
{
    public class CifParserTest
    {
        private readonly IInputStreamFactory _inputStreamFactory;
        private readonly ICifRecordDefFactory _recordDefFactory;

        public CifParserTest()
        {
            _inputStreamFactory = Substitute.For<IInputStreamFactory>();
            _recordDefFactory = Substitute.For<ICifRecordDefFactory>();
        }

        [Fact]
        public void ProcessBatch_NotInitialised_ThrowsException()
        {
            ICifProcessor sut = new CifParser(_inputStreamFactory, _recordDefFactory);

            Assert.Throws<InvalidOperationException>(() =>
            {
                sut.ProcessBatch(default(List<List<string>>), 0, ScheduleType.All);
            });
        }

        [Fact]
        public void ProcessBatch_EOFReached_ReturnsLastBatch()
        {
            ICifProcessor sut = new CifParser(_inputStreamFactory, _recordDefFactory);

            sut.Initialize(new MemoryStream());
            List<List<string>> buffer = new List<List<string>>();

            sut.ProcessBatch(buffer, 999, ScheduleType.All);

            Assert.Equal(0, buffer.Count);
        }
    }
}