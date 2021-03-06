using Microsoft.Extensions.CommandLineUtils;

using System;
using System.Collections.Generic;
using System.IO;

using CifFile.Lib;

namespace CifFile
{
    static class EditCommand
    {
        const string HelpOptionTemplate = "-? | -h | --h | --help";

        public static void Configure(CommandLineApplication command)
        {
            CommandArgument filename = command.Argument(
                "filename",
                "Enter the name and path of the CIF file");

            CommandArgument outputFile = command.Argument(
                "outputFile",
                "Enter the name and path of the output file");

            CommandOption criteriaFile = command.Option(
                "-c | --c | --criteriaFile", "Criteria File", CommandOptionType.SingleValue);

            command.HelpOption(HelpOptionTemplate);

            command.OnExecute(() =>
            {
                ServiceProvider.Build(CifProcessorType.Edit);

                if (filename.Value != null && outputFile.Value != null)
                {
                    Execute(filename.Value, outputFile.Value, criteriaFile);
                }
                else
                {
                    command.ShowHelp();
                }
                return 0;
            });
        }

        private static void Execute(string filename, string outputDirectory,
            CommandOption criteriaFile)
        {
            IProcessingService processingService = ServiceProvider.GetService<IProcessingService>();

            processingService.BatchProcessed += new EventHandler<BatchProcessedEventArgs>(BatchProcessed);

            DateTime startTime = DateTime.UtcNow;
            long recordCount = processingService.Process(filename, outputDirectory,
                1000, "l", GetBatchArgs(criteriaFile));

            WriteComplete(GetRuntime(startTime), recordCount);
        }

        private static BatchArgs GetBatchArgs(CommandOption criteriaFile)
        {
            if (!criteriaFile.HasValue()) { return new BatchArgs(null); }

            StreamReader reader = new StreamReader(File.OpenRead(criteriaFile.Value()));

            IEnumerable<ScheduleCriteria> criteria = ReadCriteriaFile(reader);

            return new BatchArgs(criteria);
        }

        private static IEnumerable<ScheduleCriteria> ReadCriteriaFile(StreamReader reader)
        {
            List<ScheduleCriteria> criteria = new List<ScheduleCriteria>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineValues = line.Split(',');
                criteria.Add(new ScheduleCriteria(lineValues[0], lineValues[1], lineValues[2], lineValues[3]));
            }

            return criteria;
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