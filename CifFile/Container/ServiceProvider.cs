using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;

using CifFile.Lib;

namespace CifFile
{
    static class ServiceProvider
    {
        private static IServiceProvider _serviceProvider;

        public static void Build()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IInputStreamFactory, InputStreamFactory>()
                .AddSingleton<IFileSystem, FileSystem>()
                .AddSingleton<IOutputWriter, CsvFileOutputWriter>()
                .AddSingleton<ICifRecordDefFactory, CifRecordDefFactory>()
                .AddSingleton<ICifProcessor, CifParser>()
                .AddSingleton<IProcessingService, ProcessingService>()
                .BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            if (_serviceProvider == null)
            {
                Build();
            }

            return _serviceProvider.GetService<T>();
        }
    }
}