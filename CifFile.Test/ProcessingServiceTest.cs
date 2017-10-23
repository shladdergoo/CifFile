using System;
using System.Collections.Generic;

using NSubstitute;
using Xunit;

using CifFile.Lib;

namespace CifFile.Test
{
    public class ParsingServiceTest
    {
        private readonly ICifProcessor _cifProcessor;
        private readonly IFileSystem _fileSystem;
        private readonly IOutputWriter _outputWriter;


        public ParsingServiceTest()
        {
            _cifProcessor = Substitute.For<ICifProcessor>();
            _outputWriter = Substitute.For<IOutputWriter>();
            _fileSystem = Substitute.For<IFileSystem>();
        }

        [Fact]
        public void Parse_GetsParsedRecords_ReturnsRecordCount()
        {
            const int RecordCount = 3;

            Stack<int> returnStack = new Stack<int>();
            returnStack.Push(0);
            returnStack.Push(RecordCount);

            _cifProcessor.ProcessBatch(default(IEnumerable<IEnumerable<string>>), default(int), default(ScheduleType))
                .ReturnsForAnyArgs(x => { return returnStack.Pop(); });

            ProcessingService sut = new ProcessingService(_cifProcessor, _outputWriter, _fileSystem);

            long returnVal = sut.Process("foo", "bar", 10, "bundy");

            Assert.Equal(RecordCount, returnVal);
        }

        [Fact]
        public void Parse_GetsParsedRecords_WritesRecords()
        {
            const int RecordCount = 3;

            Stack<int> returnStack = new Stack<int>();
            returnStack.Push(0);
            returnStack.Push(RecordCount);

            _cifProcessor.ProcessBatch(default(IEnumerable<IEnumerable<string>>), default(int), default(ScheduleType))
                .ReturnsForAnyArgs(x => { return returnStack.Pop(); });

            ProcessingService sut = new ProcessingService(_cifProcessor, _outputWriter, _fileSystem);

            sut.Process("foo", "bar", 10, "bundy");

            _outputWriter.ReceivedWithAnyArgs(1)
                .Write(default(IEnumerable<IEnumerable<string>>));
        }

        [Fact]
        public void Parse_DoesntGetParsedRecords_ReturnsZero()
        {
            List<List<string>> buffer = Arg.Any<List<List<string>>>();

            _cifProcessor.ProcessBatch(default(IEnumerable<IEnumerable<string>>), default(int), default(ScheduleType))
                .ReturnsForAnyArgs(0);

            ProcessingService sut = new ProcessingService(_cifProcessor, _outputWriter, _fileSystem);

            long returnVal = sut.Process("foo", "bar", 10, "bundy");

            Assert.Equal(0, returnVal);
        }

        [Fact]
        public void Parse_DoesntGetParsedRecords_DoesntWriteRecords()
        {
            List<List<string>> buffer = Arg.Any<List<List<string>>>();

            _cifProcessor.ProcessBatch(default(IEnumerable<IEnumerable<string>>), default(int), default(ScheduleType))
                .ReturnsForAnyArgs(0);

            ProcessingService sut = new ProcessingService(_cifProcessor, _outputWriter, _fileSystem);

            sut.Process("foo", "bar", 10, "bundy");

            _outputWriter.DidNotReceiveWithAnyArgs()
                .Write(default(IEnumerable<IEnumerable<string>>));
        }
    }
}
