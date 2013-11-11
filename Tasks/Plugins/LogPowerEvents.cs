using System;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using Tasker.Classes.Power;

namespace Tasker.Tasks
{
	class LogPowerEvents : TaskPlugin
	{
		private Log Log;

		public override bool Init(Main Main)
		{
			if (!Main.EventMgr.PluginExists<Events.Power>())
			{
				this.Log.LogLine("Task \"LogPowerEvents\" is missing EventPlugin \"Power\"!", Log.Type.Error);
				return false;
			}

			this.Log = Main.Log;

			Events.Power pwrEvents = Main.EventMgr.GetPlugin<Events.Power>(new object[] {Main});

			pwrEvents.PowerModeChanged += new Events.EventPlugin.EventValue<PowerModes>(pwrEvents_PowerModeChanged);
			pwrEvents.Suspend += new Events.EventPlugin.Event(pwrEvents_Suspend);
			pwrEvents.Resume += new Events.EventPlugin.Event(pwrEvents_Resume);
			pwrEvents.PowerLineStatusChanged += new Events.EventPlugin.EventValues<PowerLineStatus>(pwrEvents_PowerLineStatusChanged);
			pwrEvents.BatteryAvailabilityChanged += new Events.EventPlugin.EventValue<bool?>(pwrEvents_BatteryAvailabilityChanged);
			pwrEvents.BatteryStatusChanged += new Events.EventPlugin.EventValues<BatteryChargeStatus>(pwrEvents_BatteryStatusChanged);

			pwrEvents.PowerSchemeChanged += new Events.EventPlugin.EventValues<PowerScheme>(pwrEvents_PowerSchemeChanged);
			return true;
		}










		private void pwrEvents_PowerModeChanged(object sender, Events.EventArgsValue<PowerModes> e)
		{
			if (e.Value == PowerModes.StatusChange)
				this.Log.LogLineDate("The PowerMode changed to: " + e.Value.ToString(), Log.Type.PowerEvent);
		}

		private void pwrEvents_Suspend(object sender, EventArgs e)
		{
			this.Log.LogLineDate("The system is being suspended", Log.Type.PowerEvent);
		}

		private void pwrEvents_Resume(object sender, EventArgs e)
		{
			this.Log.LogLineDate("The system is being resumed", Log.Type.PowerEvent);
		}

		private void pwrEvents_PowerLineStatusChanged(object sender, Events.EventArgsValues<PowerLineStatus> e)
		{
			if (e.NewValue == PowerLineStatus.Online)
				this.Log.LogLineDate("The computer was connected to the power network", Tasker.Log.Type.PowerEvent);
			else
				this.Log.LogLineDate("The computer was disconnected from the power network", Tasker.Log.Type.PowerEvent);
		}

		private void pwrEvents_BatteryAvailabilityChanged(object sender, Events.EventArgsValue<bool?> e)
		{
			if (e.Value.Value == true)
				this.Log.LogLineDate("A battery was connected", Tasker.Log.Type.PowerEvent);
			else
				this.Log.LogLineDate("A battery was disconnected", Tasker.Log.Type.PowerEvent);
		}

		private void pwrEvents_BatteryStatusChanged(object sender, Events.EventArgsValues<BatteryChargeStatus> e)
		{
			this.Log.LogLineDate("The status of the battery changed: " + e.OldValue.ToString() + " -> " + e.NewValue.ToString() + " (" + Status.Power.BatteryLifePercent.ToString() + "%)", Tasker.Log.Type.PowerEvent);
		}







		private void pwrEvents_PowerSchemeChanged(object sender, Events.EventArgsValues<PowerScheme> e)
		{
			this.Log.LogLineDate("Power scheme changed: " + e.OldValue.Name + " --> " + e.NewValue.Name, Tasker.Log.Type.PowerEvent);
		}
	}
}