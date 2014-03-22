using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trigger.Tasks.Plugins
{
	class LogCommandLineEvents : TaskPlugin
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
			this.Main = Main;
			this.Log = Main.Log;

			Main.OnCommandLineTrigger += new Events.EventPlugin.EventValue<string>(Main_OnCommandLineTrigger);
			return true;
		}

		public override void Dispose()
		{
			Main.OnCommandLineTrigger -= new Events.EventPlugin.EventValue<string>(Main_OnCommandLineTrigger);
		}
		#endregion

		#region Event handlers
		private void Main_OnCommandLineTrigger(object sender, Events.EventArgsValue<string> e)
		{
			this.Log.LogLineDate("Trigger from command line: \"" + e.Value + "\"", Trigger.Log.Type.CommandLineEvent);
		}
		#endregion
	}
}
