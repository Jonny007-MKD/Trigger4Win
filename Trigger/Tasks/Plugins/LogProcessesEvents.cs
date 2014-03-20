using System.Diagnostics;
using Trigger.Events;

namespace Trigger.Tasks
{
	class LogProcessesEvents : TaskPlugin
	{
		#region Properties
		internal Main Main;
		internal Log Log;
		#endregion

		#region Methods
		public override bool Init(Main Main)
		{
			return Init(Main, new System.Diagnostics.Stopwatch());
		}
		public override bool Init(Main Main, System.Diagnostics.Stopwatch swInit)
		{
			if (!Main.EventMgr.PluginExists<Events.Processes>())
			{
				this.Log.LogLine("Task \"LogProcessStartedEvents\" is missing EventPlugin \"ProcessStarted\"!", Log.Type.Error);
				return false;
			}

			this.Main = Main;
			this.Log = Main.Log;

			swInit.Stop();
			Events.Processes procEvents = Main.EventMgr.GetPlugin<Events.Processes>();
			swInit.Start();

			procEvents.ProcessCreated += new Events.EventPlugin.EventValue<Process>(procEvents_ProcessCreated);
			procEvents.ProcessExited += new EventPlugin.EventValue<Process>(procEvents_ProcessExited);
			return true;
		}

		public override void Dispose()
		{
			Events.Processes procEvents = Main.EventMgr.GetPlugin<Events.Processes>();
			procEvents.ProcessCreated -= new Events.EventPlugin.EventValue<Process>(procEvents_ProcessCreated);
			procEvents.ProcessExited -= new EventPlugin.EventValue<Process>(procEvents_ProcessExited);
		}
		#endregion


		#region Event handlers
		private void procEvents_ProcessCreated(object sender, EventArgsValue<Process> e)
		{
			try { this.Log.LogLineDate("A new process was created: " + e.Value.ProcessName + " (" + e.Value.Id + ")", Trigger.Log.Type.ProcessesEvent); }
			catch { this.Log.LogLineDate("A new process was created", Trigger.Log.Type.ProcessesEvent); };
		}

		private void procEvents_ProcessExited(object sender, EventArgsValue<Process> e)
		{
			string exitCode = "";
			try { exitCode = " Code " + e.Value.ExitCode.ToString(); } catch { }
			this.Log.LogLineDate("A process has exited: " + e.Value.ProcessName + " (" + e.Value.Id + ")" + exitCode, Trigger.Log.Type.ProcessesEvent);
		}
		#endregion
	}
}