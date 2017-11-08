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
                .AddTransient<ICifRecordDefFactory, CifRecordDefFactory>();

            if (processorType == CifProcessorType.Edit)
            {
                serviceCollection.AddTransient<IOutputWriter, CifFileOutputWriter>()
                    .AddTransient<ICifProcessor, CifEditor>();
            }
            else
            {
                serviceCollection.AddTransient<IOutputWriter, CsvFileOutputWriter>()
                    .AddTransient<ICifProcessor, CifParser>();
            }

            serviceCollection.AddTransient<IProcessingService, ProcessingService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Service Provider not built");
            }

            return _serviceProvider.GetService<T>();
        }

        public static IEnumerable<T> GetServices<T>()
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Service Provider not built");
            }

            return _serviceProvider.GetServices<T>();
        }
    }
}