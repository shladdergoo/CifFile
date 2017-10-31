using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            IConfiguration loggingConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("logging.json", optional: false, reloadOnChange: true)
                .Build();

            _serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                    {
                        builder.AddConfiguration(loggingConfiguration.GetSection("Logging"));
                    })
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

        public static ILogger GetLogger<T>()
        {
            if (_serviceProvider == null)
            {
                Build();
            }

            return _serviceProvider.GetRequiredService<ILogger<T>>();
        }
    }
}