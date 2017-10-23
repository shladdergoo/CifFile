﻿using Microsoft.Extensions.CommandLineUtils;

using System;

namespace CifFile
{
    class Program
    {
        const string HelpOptionTemplate = "-? | -h | --h | --help";

        static void Main(string[] args)
        {
            ServiceProvider.Build();

            CommandLineApplication commandLineApplication =
               new CommandLineApplication(false);

            commandLineApplication.Command("parse", ParseCommand.Configure);
            commandLineApplication.HelpOption(HelpOptionTemplate);
            commandLineApplication.Execute(args);
        }
    }
}
