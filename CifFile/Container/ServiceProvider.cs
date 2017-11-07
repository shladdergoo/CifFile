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
                .AddTransient<IScheduleMatcher, ScheduleMatcher>()
                .AddTransient<IInputStreamFactory, InputStreamFactory>()
                .AddTransient<IFileSystem, FileSystem>()
                .AddTransient<IOutputWriter, CsvFileOutputWriter>()
                .AddTransient<ICifRecordDefFactory, CifRecordDefFactory>()
                .AddTransient<ICifProcessor, CifParser>()
                .AddTransient<IProcessingService, ProcessingService>()
                .AddTransient<ICifProcessor, CifParser>()
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