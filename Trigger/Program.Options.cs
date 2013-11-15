using System;
using CommandLine;
using CommandLine.Text;


namespace Trigger
{
	/// <summary>
	/// <para>Command line options</para>
	/// </summary>
	public class Options
    {
		/// <summary>
		/// <para>The name of the event that shall be triggered</para>
		/// <para>Every registered event handler has to compare this string to the event name it is waiting for</para>
		/// </summary>
        [Option('t', "trigger", Required = false, HelpText = "The command line event that shall be triggered")]
        public string Trigger { get; set; }

		/// <summary>
		/// <para>Prints some help text</para>
		/// </summary>
		/// <returns></returns>
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
