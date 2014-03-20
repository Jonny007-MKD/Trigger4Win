using System;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using Trigger.Classes.Screen;

namespace Trigger.Tasks
{
	class LogScreenEvents : TaskPlugin
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
			if (!Main.EventMgr.PluginExists<Events.Screen>())
			{
				this.Log.LogLine("Task \"LogScreenEvents\" is missing EventPlugin \"Screen\"!", Log.Type.Error);
				return false;
			}

			this.Main = Main;
			this.Log = Main.Log;

			swInit.Stop();
			Events.Screen screenEvents = Main.EventMgr.GetPlugin<Events.Screen>();
			swInit.Start();

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

		public override void Dispose()
		{
			Events.Screen screenEvents = Main.EventMgr.GetPlugin<Events.Screen>();
			screenEvents.ScreenAdded -= new Events.EventPlugin.EventValue<ScreenEx>(screenEvents_ScreenAdded);
			screenEvents.ScreenRemoved -= new Events.EventPlugin.EventValue<ScreenEx>(screenEvents_ScreenRemoved);
			screenEvents.ScreenColorDepthChanged -= new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenColorDepthChanged);
			screenEvents.ScreenResolutionChanged -= new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenResolutionChanged);
			screenEvents.PrimaryScreenChanged -= new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_PrimaryScreenChanged);
			screenEvents.ScreenLocationChanged -= new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenLocationChanged);
			screenEvents.ScreenOrientationChanged -= new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenOrientationChanged);
			screenEvents.ScreenRefreshRateChanged -= new Events.EventPlugin.EventValues<ScreenEx>(screenEvents_ScreenRefreshRateChanged);
		}
		#endregion


		#region Event handlers
		void screenEvents_ScreenAdded(object sender, Events.EventArgsValue<ScreenEx> e)
		{
			this.Log.LogLineDate("The screen \"" + e.Value.Name + "\" was added (" + e.Value.Bounds.Width + "x" + e.Value.Bounds.Height + "  " + e.Value.BitsPerPixel + "bit)", Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenRemoved(object sender, Events.EventArgsValue<ScreenEx> e)
		{
			this.Log.LogLineDate("The screen \"" + e.Value.Name + "\" was removed", Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenColorDepthChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The screen color depth of \"" + e.NewValue.Name + "\" has changed: " + e.OldValue.BitsPerPixel + "bit -> " + e.NewValue.BitsPerPixel + "bit", Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenResolutionChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The resolution of \"" + e.NewValue.Name + "\" has changed: " + e.OldValue.Bounds.Width + "x" + e.OldValue.Bounds.Height + " -> " + e.NewValue.Bounds.Width + "x" + e.NewValue.Bounds.Height, Log.Type.ScreenEvent);
		}

		void screenEvents_PrimaryScreenChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The primary screen is not \"" + e.OldValue.Name + "\" anymore but now \"" + e.NewValue.Name + "\"", Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenLocationChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The Location of \"" + e.NewValue.Name + "\" has changed: " + e.OldValue.Bounds.X + "|" + e.OldValue.Bounds.Y + " -> " + e.NewValue.Bounds.X + "|" + e.NewValue.Bounds.Y, Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenOrientationChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The orientation of \"" + e.NewValue.Name + "\" has changed: " + e.OldValue.Orientation + " -> " + e.NewValue.Orientation, Log.Type.ScreenEvent);
		}

		void screenEvents_ScreenRefreshRateChanged(object sender, Events.EventArgsValues<ScreenEx> e)
		{
			this.Log.LogLineDate("The refresh rate of \"" + e.NewValue.Name + "\" has changed: " + e.OldValue.Frequency + "Hz -> " + e.NewValue.Frequency + "Hz", Log.Type.ScreenEvent);
		}
		#endregion
	}
}