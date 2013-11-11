using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Diag = System.Diagnostics;
using Trigger.Events;

namespace Trigger.Status
{
	public static class Processes
	{
		/// <summary>
		/// <para>Gets a dictionary with the currently running processes, indexed by PID</para>
		/// </summary>
		public static Dictionary<int, Process> RunningDict
		{
			get
			{
				Process[] processes = Process.GetProcesses();
				Dictionary<int, Process> x = new Dictionary<int, Process>(processes.Length);
				foreach (Process process in processes)
					x.Add(process.Id, process);
				return x;
			}
		}
		/// <summary>
		/// <para>Gets a list with the currently running processes</para>
		/// </summary>
		public static List<Process> RunningList
		{
			get
			{
				return new List<Process>(Process.GetProcesses());
			}
		}

	}
}
