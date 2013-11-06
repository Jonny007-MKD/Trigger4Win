using System;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Tasker.Tasks
{
	class LogSystemEvents : TaskPlugin
	{
		private Log Log;
		public override bool Init(Main Main)
		{
			if (!Main.EventMgr.PluginExists<Events.System>())
			{
				this.Log.LogLine("Task \"LogSystemEvents\" is missing EventPlugin \"System\"!", Log.Type.Error);
				return false;
			}

			this.Log = Main.Log;

			Events.System sysEvents = Main.EventMgr.GetPlugin<Events.System>();

			sysEvents.InstalledFontsChanged += new Events.EventPlugin.Event(sysEvents_InstalledFontsChanged);
			sysEvents.FontAdded += new Events.EventPlugin.EventValue<FontFamily>(sysEvents_FontAdded);
			sysEvents.FontRemoved += new Events.EventPlugin.EventValue<FontFamily>(sysEvents_FontRemoved);

			//sysEvents.PowerModeChanged += new Events.EventPlugin.EventValue<PowerModes>(sysEvents_PowerModeChanged);
			sysEvents.Suspend += new Events.EventPlugin.Event(sysEvents_Suspend);
			sysEvents.Resume += new Events.EventPlugin.Event(sysEvents_Resume);
			sysEvents.PowerLineStatusChanged += new Events.EventPlugin.EventValues<PowerLineStatus>(sysEvents_PowerLineStatusChanged);
			sysEvents.BatteryAvailabilityChanged += new Events.EventPlugin.EventValue<bool?>(sysEvents_BatteryAvailabilityChanged);
			sysEvents.BatteryStatusChanged += new Events.EventPlugin.EventValues<BatteryChargeStatus>(sysEvents_BatteryStatusChanged);

			sysEvents.Logoff += new Events.EventPlugin.Event(sysEvents_Logoff);
			sysEvents.Shutdown += new Events.EventPlugin.Event(sysEvents_Shutdown);

			sysEvents.ConsoleConnect += new Events.EventPlugin.Event(sysEvents_ConsoleConnect);
			sysEvents.ConsoleDisconnect += new Events.EventPlugin.Event(sysEvents_ConsoleDisconnect);
			sysEvents.RemoteConnect += new Events.EventPlugin.Event(sysEvents_RemoteConnect);
			sysEvents.RemoteDisconnect += new Events.EventPlugin.Event(sysEvents_RemoteDisconnect);
			sysEvents.SessionLock += new Events.EventPlugin.Event(sysEvents_SessionLock);
			sysEvents.SessionLogoff += new Events.EventPlugin.Event(sysEvents_SessionLogoff);
			sysEvents.SessionLogon += new Events.EventPlugin.Event(sysEvents_SessionLogon);
			sysEvents.SessionRemoteControl += new Events.EventPlugin.Event(sysEvents_SessionRemoteControl);
			sysEvents.SessionUnlock += new Events.EventPlugin.Event(sysEvents_SessionUnlock);
			return true;
		}







		void sysEvents_InstalledFontsChanged(object sender, EventArgs e)
		{
			this.Log.LogLineDate("Tthe user added (a) font(s) to or removes (a) font(s) from the system", Log.Type.SystemEvent);
		}

		void sysEvents_FontAdded(object sender, Events.EventArgsValue<FontFamily> e)
		{
			this.Log.LogLineDate("The Font \"" + e.Value.Name + "\" was installed", Log.Type.SystemEvent);
		}

		void sysEvents_FontRemoved(object sender, Events.EventArgsValue<FontFamily> e)
		{
			this.Log.LogLineDate("The Font \"" + e.Value.Name + "\" was removed", Log.Type.SystemEvent);
		}



		void sysEvents_PowerModeChanged(object sender, Events.EventArgsValue<PowerModes> e)
		{
			if (e.Value == PowerModes.StatusChange)
				this.Log.LogLineDate("The PowerMode changed to: " + e.Value.ToString(), Log.Type.SystemEvent);
		}

		void sysEvents_Suspend(object sender, EventArgs e)
		{
			this.Log.LogLineDate("The system is being suspended", Log.Type.SystemEvent);
		}

		void sysEvents_Resume(object sender, EventArgs e)
		{
			this.Log.LogLineDate("The system is being resumed", Log.Type.SystemEvent);
		}

		void sysEvents_PowerLineStatusChanged(object sender, Events.EventArgsValues<PowerLineStatus> e)
		{
			if (e.NewValue == PowerLineStatus.Online)
				this.Log.LogLineDate("The computer was connected to the power network", Tasker.Log.Type.SystemEvent);
			else
				this.Log.LogLineDate("The computer was disconnected from the power network", Tasker.Log.Type.SystemEvent);
		}

		private void sysEvents_BatteryAvailabilityChanged(object sender, Events.EventArgsValue<bool?> e)
		{
			if (e.Value.Value == true)
				this.Log.LogLineDate("A battery was connected", Tasker.Log.Type.SystemEvent);
			else
				this.Log.LogLineDate("A battery was disconnected", Tasker.Log.Type.SystemEvent);
		}

		void sysEvents_BatteryStatusChanged(object sender, Events.EventArgsValues<BatteryChargeStatus> e)
		{
			this.Log.LogLineDate("The status of the battery changed: " + e.OldValue.ToString() + " -> " + e.NewValue.ToString() + " (" + Status.System.BatteryLifePercent.ToString() + "%)", Tasker.Log.Type.SystemEvent);
		}



		void sysEvents_Logoff(object sender, EventArgs e)
		{
			this.Log.LogLineDate("The user is trying to log off", Log.Type.SystemEvent);
		}

		void sysEvents_Shutdown(object sender, EventArgs e)
		{
			this.Log.LogLineDate("The user is trying to shutdown the system", Log.Type.SystemEvent);
		}



		void sysEvents_ConsoleConnect(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A session has been connected from the console", Log.Type.SystemEvent);
		}

		void sysEvents_ConsoleDisconnect(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A session has been disconnected from the console", Log.Type.SystemEvent);
		}

		void sysEvents_RemoteConnect(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A session has been connected from a remote connection", Log.Type.SystemEvent);
		}

		void sysEvents_RemoteDisconnect(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A session has been disconnected from a remote connection", Log.Type.SystemEvent);
		}

		void sysEvents_SessionLock(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A session has been locked", Log.Type.SystemEvent);
		}

		void sysEvents_SessionLogoff(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A user has logged off from a session", Log.Type.SystemEvent);
		}

		void sysEvents_SessionLogon(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A user has logged on to a session", Log.Type.SystemEvent);
		}

		void sysEvents_SessionRemoteControl(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A session has changed its status to or from remote controlled mode", Log.Type.SystemEvent);
		}

		void sysEvents_SessionUnlock(object sender, EventArgs e)
		{
			this.Log.LogLineDate("A session has been unlocked", Log.Type.SystemEvent);
		}
	}
}