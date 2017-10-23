using System;
using System.Collections.Generic;

using NSubstitute;
using Xunit;

using CifFile.Lib;

namespace CifFile.Test
{
    public class CsvOutputWriterTest
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICifRecordDefFactory _recordDefFactory;

        public CsvOutputWriterTest()
        {
            _fileSystem = Substitute.For<IFileSystem>();
            _recordDefFactory = Substitute.For<ICifRecordDefFactory>();
        }

        [Fact]
        public void Write_NotOpen_ThrowsException()
        {
            IOutputWriter sut = new CsvFileOutputWriter(_fileSystem, _recordDefFactory);

            Assert.Throws<InvalidOperationException>(() => {
                sut.Write(default(IEnumerable<IEnumerable<string>>));
            });
        }
    }
}