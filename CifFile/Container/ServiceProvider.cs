using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;

using CifFile.Lib;

namespace CifFile
{
    static class ServiceProvider
    {
        private static IServiceProvider _serviceProvider;

        public static void Build()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IScheduleMatcher, ScheduleMatcher>()
                .AddSingleton<IInputStreamFactory, InputStreamFactory>()
                .AddSingleton<IFileSystem, FileSystem>()
                .AddSingleton<IOutputWriter, CsvFileOutputWriter>()
                .AddSingleton<ICifRecordDefFactory, CifRecordDefFactory>()
                .AddSingleton<ICifProcessor, CifParser>()
                .AddSingleton<ICifProcessor, CifEditor>()
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

        public static IEnumerable<T> GetServices<T>()
        {
            if (_serviceProvider == null)
            {
                Build();
            }

            return _serviceProvider.GetServices<T>();
        }
    }
}