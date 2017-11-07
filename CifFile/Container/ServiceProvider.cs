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
            Build(CifProcessorType.Parse);
        }

        public static void Build(CifProcessorType processorType)
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddTransient<IScheduleMatcher, ScheduleMatcher>()
                .AddTransient<IInputStreamFactory, InputStreamFactory>()
                .AddTransient<IFileSystem, FileSystem>()
                .AddTransient<IOutputWriter, CsvFileOutputWriter>()
                .AddTransient<ICifRecordDefFactory, CifRecordDefFactory>();

            if (processorType == CifProcessorType.Edit)
            {
                serviceCollection.AddTransient<ICifProcessor, CifEditor>();
            }
            else
            {
                serviceCollection.AddTransient<ICifProcessor, CifEditor>();
            }

            serviceCollection.AddTransient<IProcessingService, ProcessingService>()
                .AddTransient<ICifProcessor, CifParser>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
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