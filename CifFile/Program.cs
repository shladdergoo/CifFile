using Microsoft.Extensions.CommandLineUtils;

using System;

namespace CifFile
{
    class Program
    {
        const string HelpOptionTemplate = "-? | -h | --h | --help";

        static void Main(string[] args)
        {
            CommandLineApplication commandLineApplication =
               new CommandLineApplication(false);

            commandLineApplication.Command("parse", ParseCommand.Configure);
            commandLineApplication.Command("edit", EditCommand.Configure);
            commandLineApplication.HelpOption(HelpOptionTemplate);
            commandLineApplication.Execute(args);
        }
    }
}
