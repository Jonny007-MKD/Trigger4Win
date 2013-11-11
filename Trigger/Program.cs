using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Messaging;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;

namespace Trigger
{
	static class Program
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetForegroundWindow(IntPtr hWnd);

		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);  
			SingleInstanceApplication.Run(new Main(), NewInstanceHandler);
		}

		private class SingleInstanceApplication : WindowsFormsApplicationBase
		{
			private SingleInstanceApplication()
			{
				base.IsSingleInstance = true;
			}

			public static void Run(Form f, StartupNextInstanceEventHandler startupHandler)
			{
				SingleInstanceApplication app = new SingleInstanceApplication();
				app.MainForm = f;
				app.StartupNextInstance += startupHandler;

				Options options = new Options();

				if (!CommandLine.Parser.Default.ParseArguments(Environment.GetCommandLineArgs(), options))
					Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);

				app.Startup += new StartupEventHandler(((Main)f).Init);

				app.Run(Environment.GetCommandLineArgs());
			}
		}

		private static void NewInstanceHandler(object sender, StartupNextInstanceEventArgs e)
		{
			Options options = new Options();

			
			string[] args = new string[e.CommandLine.Count];
			for (int i = 0; i < e.CommandLine.Count; i++)
				args[i] = e.CommandLine[i];
			if (!CommandLine.Parser.Default.ParseArguments(args, options))
				return;

			SingleInstanceApplication app = (SingleInstanceApplication)sender;
			foreach (Form form in app.OpenForms)
			{
				if (form.Name == "Main")
				{
					((Main)form).HandleNewInstance(options);
					break;
				}
			}
		}
	}
}
