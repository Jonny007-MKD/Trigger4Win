using System.Diagnostics;
using Tasker.Events;

namespace Tasker.Tasks
{
	class LogProcessStartedEvents : TaskPlugin
	{
		private Log Log;
		public override bool Init(Main Main)
		{
			if (!Main.EventMgr.PluginExists<Events.Processes>())
			{
				this.Log.LogLine("Task \"LogProcessStartedEvents\" is missing EventPlugin \"ProcessStarted\"!", Log.Type.Error);
				return false;
			}

			this.Log = Main.Log;

			Events.Processes procEvents = Main.EventMgr.GetPlugin<Events.Processes>();

			procEvents.ProcessCreated += new Events.EventPlugin.EventValue<Process>(procEvents_ProcessCreated);
			procEvents.ProcessExited += new EventPlugin.EventValue<Process>(procEvents_ProcessExited);
			return true;
		}


		void procEvents_ProcessCreated(object sender, EventArgsValue<Process> e)
		{
			this.Log.LogLineDate("A new process was created: " + e.Value.ProcessName + " (" + e.Value.Id + ")", Tasker.Log.Type.ProcessStartedEvent);
		}

		void procEvents_ProcessExited(object sender, EventArgsValue<Process> e)
		{
			string exitCode = "";
			try { exitCode = " Code " + e.Value.ExitCode.ToString(); } catch { }
			this.Log.LogLineDate("A process has exited: " + e.Value.ProcessName + " (" + e.Value.Id + ")" + exitCode, Tasker.Log.Type.ProcessStartedEvent);
		}
	}
}