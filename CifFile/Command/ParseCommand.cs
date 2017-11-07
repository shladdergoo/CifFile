using Microsoft.Extensions.CommandLineUtils;

using System;

using CifFile.Lib;

namespace CifFile
{
    static class ParseCommand
    {
        const string HelpOptionTemplate = "-? | -h | --h | --help";

        public static void Configure(CommandLineApplication command)
        {
            CommandArgument filename = command.Argument(
                "filename",
                "Enter the name and path of the CIF file");

            CommandArgument outputDirectory = command.Argument(
                "outputDirectory",
                "Enter the path of the output directory");

            CommandOption scheduleType = command.Option(
                "-s | --s | --scheduleType", "Schedule Type", CommandOptionType.SingleValue);

            command.HelpOption(HelpOptionTemplate);

            command.OnExecute(() =>
            {
                ServiceProvider.Build(CifProcessorType.Parse);

                if (filename.Value != null && outputDirectory.Value != null)
                {
                    Execute(filename.Value, outputDirectory.Value, scheduleType);
                }
                else
                {
                    command.ShowHelp();
                }
                return 0;
            });
        }

        private static void Execute(string filename, string outputDirectory,
            CommandOption scheduleType)
        {
            IProcessingService processingService = ServiceProvider.GetService<IProcessingService>();

            processingService.BatchProcessed += new EventHandler<BatchProcessedEventArgs>(BatchProcessed);

            DateTime startTime = DateTime.UtcNow;
            long recordCount = processingService.Process(filename, outputDirectory, 
                1000, GetScheduleTypeValue(scheduleType), null);

            WriteComplete(GetRuntime(startTime), recordCount);
        }

        private static string GetScheduleTypeValue(CommandOption scheduleType)
        {
            if (scheduleType.HasValue())
            {
                return scheduleType.Value();
            }
            else
            {
                return "j";
            }
        }

        private static void BatchProcessed(object sender, BatchProcessedEventArgs e)
        {
            Console.Write($"\rProcessed batch {e.BatchNumber} - ({e.BatchSize} records)...");
        }

        private static void WriteComplete(int elapsedSeconds, long recordCount)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Processing complete in {elapsedSeconds} sec. {recordCount} records.");
            Console.WriteLine();
        }

        private static int GetRuntime(DateTime startTime)
        {
            return (DateTime.UtcNow - startTime).Seconds;
        }
    }
}