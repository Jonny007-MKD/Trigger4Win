﻿using System;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using Trigger.Classes.Power;

namespace Trigger.Tasks
{
	class LogPowerEvents : TaskPlugin
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
			if (!Main.EventMgr.PluginExists<Events.Power>())
			{
				this.Log.LogLine("Task \"LogPowerEvents\" is missing EventPlugin \"Power\"!", Log.Type.Error);
				return false;
			}

			this.Main = Main;
			this.Log = Main.Log;

			swInit.Stop();
			Events.Power pwrEvents = Main.EventMgr.GetPlugin<Events.Power>(new object[] {Main}, true);
			swInit.Start();

			pwrEvents.PowerModeChanged += new Events.EventPlugin.EventValue<PowerModes>(pwrEvents_PowerModeChanged);
			pwrEvents.Suspend += new Events.EventPlugin.Event(pwrEvents_Suspend);
			pwrEvents.Resume += new Events.EventPlugin.Event(pwrEvents_Resume);
			pwrEvents.PowerLineStatusChanged += new Events.EventPlugin.EventValues<PowerLineStatus>(pwrEvents_PowerLineStatusChanged);
			pwrEvents.BatteryAvailabilityChanged += new Events.EventPlugin.EventValue<bool?>(pwrEvents_BatteryAvailabilityChanged);
			pwrEvents.BatteryStatusChanged += new Events.EventPlugin.EventValues<BatteryChargeStatus>(pwrEvents_BatteryStatusChanged);

			pwrEvents.PowerSchemeChanged += new Events.EventPlugin.EventValues<PowerScheme>(pwrEvents_PowerSchemeChanged);
			return true;
		}

		public override void Dispose()
		{
			Events.Power pwrEvents = Main.EventMgr.GetPlugin<Events.Power>(new object[] {Main}, true);
			pwrEvents.PowerModeChanged -= new Events.EventPlugin.EventValue<PowerModes>(pwrEvents_PowerModeChanged);
			pwrEvents.Suspend -= new Events.EventPlugin.Event(pwrEvents_Suspend);
			pwrEvents.Resume -= new Events.EventPlugin.Event(pwrEvents_Resume);
			pwrEvents.PowerLineStatusChanged -= new Events.EventPlugin.EventValues<PowerLineStatus>(pwrEvents_PowerLineStatusChanged);
			pwrEvents.BatteryAvailabilityChanged -= new Events.EventPlugin.EventValue<bool?>(pwrEvents_BatteryAvailabilityChanged);
			pwrEvents.BatteryStatusChanged -= new Events.EventPlugin.EventValues<BatteryChargeStatus>(pwrEvents_BatteryStatusChanged);

			pwrEvents.PowerSchemeChanged -= new Events.EventPlugin.EventValues<PowerScheme>(pwrEvents_PowerSchemeChanged);
		}
		#endregion


		#region Event handlers
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
				this.Log.LogLineDate("The computer was connected to the power network", Trigger.Log.Type.PowerEvent);
			else
				this.Log.LogLineDate("The computer was disconnected from the power network", Trigger.Log.Type.PowerEvent);
		}

		private void pwrEvents_BatteryAvailabilityChanged(object sender, Events.EventArgsValue<bool?> e)
		{
			if (e.Value.Value == true)
				this.Log.LogLineDate("A battery was connected", Trigger.Log.Type.PowerEvent);
			else
				this.Log.LogLineDate("A battery was disconnected", Trigger.Log.Type.PowerEvent);
		}

		private void pwrEvents_BatteryStatusChanged(object sender, Events.EventArgsValues<BatteryChargeStatus> e)
		{
			this.Log.LogLineDate("The status of the battery changed: " + e.OldValue.ToString() + " -> " + e.NewValue.ToString() + " (" + Status.Power.BatteryLifePercent.ToString() + "%)", Trigger.Log.Type.PowerEvent);
		}







		private void pwrEvents_PowerSchemeChanged(object sender, Events.EventArgsValues<PowerScheme> e)
		{
			this.Log.LogLineDate("Power scheme changed: " + e.OldValue.Name + " --> " + e.NewValue.Name, Trigger.Log.Type.PowerEvent);
		}
		#endregion
	}
}