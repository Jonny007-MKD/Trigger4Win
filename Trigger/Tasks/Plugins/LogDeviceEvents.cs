﻿using System;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using Trigger.Classes.Device;

namespace Trigger.Tasks
{
	class LogDeviceEvents : TaskPlugin
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
			if (!Main.EventMgr.PluginExists<Events.Device>())
			{
				this.Log.LogLine("Task \"LogDeviceEvents\" is missing EventPlugin \"Device\"!", Log.Type.Error);
				return false;
			}

			this.Main = Main;
			this.Log = Main.Log;

			swInit.Stop();
			Events.Device deviceEvents = Main.EventMgr.GetPlugin<Events.Device>(new object[] {Main}, true);
			swInit.Start();

			deviceEvents.DeviceArrived += new Events.EventPlugin.EventValue<Device>(devEvents_DeviceArrived);
			deviceEvents.DeviceQueryRemove += new Events.EventPlugin.EventValue<Device>(devEvents_DeviceQueryRemove);
			deviceEvents.DeviceQueryRemoveFailed += new Events.EventPlugin.EventValue<Device>(devEvents_DeviceQueryRemoveFailed);
			deviceEvents.DeviceRemoved += new Events.EventPlugin.EventValue<Device>(devEvents_DeviceRemoved);
			deviceEvents.MediaInserted += new Events.EventPlugin.EventValue<Device>(devEvents_MediaInserted);
			deviceEvents.NetworkVolumeArrived += new Events.EventPlugin.EventValue<Device>(devEvents_NetworkVolumeArrived);
			return true;
		}

		public override void Dispose()
		{
			Events.Device devEvents = this.Main.EventMgr.GetPlugin<Events.Device>(new object[] {Main}, true);
			devEvents.DeviceArrived -= new Events.EventPlugin.EventValue<Device>(devEvents_DeviceArrived);
			devEvents.DeviceQueryRemove -= new Events.EventPlugin.EventValue<Device>(devEvents_DeviceQueryRemove);
			devEvents.DeviceQueryRemoveFailed -= new Events.EventPlugin.EventValue<Device>(devEvents_DeviceQueryRemoveFailed);
			devEvents.DeviceRemoved -= new Events.EventPlugin.EventValue<Device>(devEvents_DeviceRemoved);
			devEvents.MediaInserted -= new Events.EventPlugin.EventValue<Device>(devEvents_MediaInserted);
			devEvents.NetworkVolumeArrived -= new Events.EventPlugin.EventValue<Device>(devEvents_NetworkVolumeArrived);
		}
		#endregion


		#region Event handlers
		private void devEvents_DeviceArrived(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("New device arrived: " + e.Value.Model, Trigger.Log.Type.DeviceEvent);
		}

		private void devEvents_DeviceQueryRemove(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("Device is queried to be removed: " + e.Value.Model, Trigger.Log.Type.DeviceEvent);
		}

		private void devEvents_DeviceQueryRemoveFailed(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("Device could not be removed: " + e.Value.Model, Trigger.Log.Type.DeviceEvent);
		}

		private void devEvents_DeviceRemoved(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("A device was removed: " + e.Value.Model, Trigger.Log.Type.DeviceEvent);
		}

		private void devEvents_MediaInserted(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("Media inserted in " + e.Value.Model, Trigger.Log.Type.DeviceEvent);
		}

		private void devEvents_NetworkVolumeArrived(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("New network volume arrived: " + e.Value.Model, Trigger.Log.Type.DeviceEvent);
		}
		#endregion
	}
}