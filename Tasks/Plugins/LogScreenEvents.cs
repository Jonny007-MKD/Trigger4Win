using System;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using Tasker.Classes.Screen;

namespace Tasker.Tasks
{
	class LogScreenEvents : TaskPlugin
	{
		private Log Log;
		public override bool Init(Main Main)
		{
			if (!Main.EventMgr.PluginExists<Events.Screen>())
			{
				this.Log.LogLine("Task \"LogScreenEvents\" is missing EventPlugin \"Screen\"!", Log.Type.Error);
				return false;
			}

			this.Log = Main.Log;

			Events.Screen screenEvents = Main.EventMgr.GetPlugin<Events.Screen>();

			screenEvents.ScreenAdded += new Events.EventPlugin.EventValue<ScreenEx>(screenEvents_ScreenAdded);
			screenEvents.ScreenRemoved += new Events.EventPlugin.EventValue<ScreenEx>(screenEvents_ScreenRemoved);
			screenEvents.ScreenColorDepthChanged += new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenColorDepthChanged);
			screenEvents.ScreenResolutionChanged += new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenResolutionChanged);
			screenEvents.PrimaryScreenChanged += new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_PrimaryScreenChanged);
			screenEvents.ScreenLocationChanged += new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenLocationChanged);
			screenEvents.ScreenOrientationChanged += new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenOrientationChanged);
			screenEvents.ScreenRefreshRateChanged += new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenRefreshRateChanged);
			return true;
		}




		void screenEvents_ScreenAdded(object sender, Events.EventArgsValue<ScreenEx> e)
		{
			this.Log.LogLineDate("The screen \"" + e.Value.DeviceName + "\" was added (" + e.Value.Bounds.Width + "x" + e.Value.Bounds.Height + "  " + e.Value.BitsPerPixel + "bit)", Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenRemoved(object sender, Events.EventArgsValue<ScreenEx> e)
		{
			this.Log.LogLineDate("The screen \"" + e.Value.DeviceName + "\" was removed", Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenColorDepthChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The screen color depth of \"" + e.NewValue.DeviceName + "\" has changed: " + e.OldValue.BitsPerPixel + "bit -> " + e.NewValue.BitsPerPixel + "bit", Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenResolutionChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The resolution of \"" + e.NewValue.DeviceName + "\" has changed: " + e.OldValue.Bounds.Width + "x" + e.OldValue.Bounds.Height + " -> " + e.NewValue.Bounds.Width + "x" + e.NewValue.Bounds.Height, Log.Type.ScreenEvent);
		}

		void screenEvents_PrimaryScreenChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The primary screen is not \"" + e.OldValue.DeviceName + "\" anymore but now \"" + e.NewValue.DeviceName + "\"", Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenLocationChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The Location of \"" + e.NewValue.DeviceName + "\" has changed: " + e.OldValue.Bounds.X + "|" + e.OldValue.Bounds.Y + " -> " + e.NewValue.Bounds.X + "|" + e.NewValue.Bounds.Y, Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenOrientationChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The orientation of \"" + e.NewValue.DeviceName + "\" has changed: " + e.OldValue.Orientation + " -> " + e.NewValue.Orientation, Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenRefreshRateChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The refresh rate of \"" + e.NewValue.DeviceName + "\" has changed: " + e.OldValue.Frequency + "Hz -> " + e.NewValue.Frequency + "Hz", Log.Type.ScreenEvent);
		}
	}
}