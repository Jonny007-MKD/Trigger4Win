﻿using System;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using Tasker.Classes.Device;

namespace Tasker.Tasks
{
	class LogDeviceEvents : TaskPlugin
	{
		private Log Log;

		public override bool Init(Main Main)
		{
			if (!Main.EventMgr.PluginExists<Events.Device>())
			{
				this.Log.LogLine("Task \"LogDeviceEvents\" is missing EventPlugin \"Device\"!", Log.Type.Error);
				return false;
			}

			this.Log = Main.Log;

			Events.Device devEvents = Main.EventMgr.GetPlugin<Events.Device>(new object[] {Main});

			devEvents.DeviceArrived += new Events.EventPlugin.EventValue<Device>(devEvents_DeviceArrived);
			devEvents.DeviceQueryRemove += new Events.EventPlugin.EventValue<Device>(devEvents_DeviceQueryRemove);
			devEvents.DeviceQueryRemoveFailed += new Events.EventPlugin.EventValue<Device>(devEvents_DeviceQueryRemoveFailed);
			devEvents.DeviceRemoved += new Events.EventPlugin.EventValue<Device>(devEvents_DeviceRemoved);
			devEvents.MediaInserted += new Events.EventPlugin.EventValue<Device>(devEvents_MediaInserted);
			devEvents.NetworkVolumeArrived += new Events.EventPlugin.EventValue<Device>(devEvents_NetworkVolumeArrived);
			return true;
		}






		private void devEvents_DeviceArrived(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("New device arrived: " + e.Value.Model, Tasker.Log.Type.DeviceEvent);
		}

		private void devEvents_DeviceQueryRemove(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("Device is queried to be removed: " + e.Value.Model, Tasker.Log.Type.DeviceEvent);
		}

		private void devEvents_DeviceQueryRemoveFailed(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("Device could not be removed: " + e.Value.Model, Tasker.Log.Type.DeviceEvent);
		}

		private void devEvents_DeviceRemoved(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("A device was removed: " + e.Value.Model, Tasker.Log.Type.DeviceEvent);
		}

		private void devEvents_MediaInserted(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("Media inserted in " + e.Value.Model, Tasker.Log.Type.DeviceEvent);
		}

		private void devEvents_NetworkVolumeArrived(object sender, Events.EventArgsValue<Device> e)
		{
			this.Log.LogLineDate("New network volume arrived: " + e.Value.Model, Tasker.Log.Type.DeviceEvent);
		}
	}
}