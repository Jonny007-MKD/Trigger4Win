using System;
using CommandLine;
using CommandLine.Text;


namespace Trigger
{
	public class Options
    {
        [Option('t', "trigger", Required = false, HelpText = "The command line event that shall be triggered")]
        public string Trigger { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
