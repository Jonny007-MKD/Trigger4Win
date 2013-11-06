using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Win32_SystemEvents = Microsoft.Win32.SystemEvents;
using System.Windows.Forms;
using System.Drawing;
using Tasker.Classes.Screen;

namespace Tasker.Events
{
	class Screen : EventPlugin
	{
		#region Properties
		private enum EventType : byte
		{
			DisplaySettingsChanged,
		}

		private Dictionary<EventType, object> oldValues = new Dictionary<EventType, object>();

		#region Register Parent Events
		private byte displaySettingsChangedEnabled;
		private bool DisplaySettingsChangedEnabled
		{
			set
			{
				if (value)
					displaySettingsChangedEnabled++;
				else
					displaySettingsChangedEnabled--;
				if (value && displaySettingsChangedEnabled == 1)
					Win32_SystemEvents.DisplaySettingsChanged += Win32_SystemEvents_DisplaySettingsChanged;
				else if (displaySettingsChangedEnabled == 0)
					Win32_SystemEvents.DisplaySettingsChanged -= Win32_SystemEvents_DisplaySettingsChanged;
			}
			get
			{
				return displaySettingsChangedEnabled > 0;
			}
		}
		#endregion

		#region Events
		#region DisplaySettingsChanged
		private EventPlugin.Event OnDisplaySettingsChanged;
		/// <summary><para>Occurs when the user changes the display settings (resolution, color depth, ...).</para></summary>
		public event EventPlugin.Event DisplaySettingsChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnDisplaySettingsChanged += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnDisplaySettingsChanged -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - ScreenAdded
		private EventPlugin.EventValue<ScreenEx> OnScreenAdded;
		/// <summary><para>Occurs when the user plugges a new screen.</para></summary>
		public event EventPlugin.EventValue<ScreenEx> ScreenAdded
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnScreenAdded += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnScreenAdded -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - ScreenRemoved
		private EventPlugin.EventValue<ScreenEx> OnScreenRemoved;
		/// <summary><para>Occurs when the user unplugges a screen.</para></summary>
		public event EventPlugin.EventValue<ScreenEx> ScreenRemoved
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnScreenRemoved += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnScreenRemoved -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - ScreenChanged
		private EventPlugin.EventValues<ScreenEx> OnScreenChanged;
		/// <summary><para>Occurs when the user changes the resolution or color depth of a screen.</para></summary>
		public event EventPlugin.EventValues<ScreenEx> ScreenChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnScreenChanged += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnScreenChanged -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - ScreenColorDepthChanged
		private EventPlugin.EventValues<ScreenEx> OnScreenColorDepthChanged;
		/// <summary><para>Occurs when the user changes the color depth of a screen.</para></summary>
		public event EventPlugin.EventValues<ScreenEx> ScreenColorDepthChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnScreenColorDepthChanged += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnScreenColorDepthChanged -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - ScreenResolutionChanged
		private EventPlugin.EventValues<ScreenEx> OnScreenResolutionChanged;
		/// <summary><para>Occurs when the user changes the resolution of a screen.</para></summary>
		public event EventPlugin.EventValues<ScreenEx> ScreenResolutionChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnScreenResolutionChanged += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnScreenResolutionChanged -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - ScreenRefreshRateChanged
		private EventPlugin.EventValues<ScreenEx> OnScreenRefreshRateChanged;
		/// <summary><para>Occurs when the user changes the RefreshRate of a screen.</para></summary>
		public event EventPlugin.EventValues<ScreenEx> ScreenRefreshRateChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnScreenRefreshRateChanged += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnScreenRefreshRateChanged -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - ScreenLocationChanged
		private EventPlugin.EventValues<ScreenEx> OnScreenLocationChanged;
		/// <summary><para>Occurs when the user changes the Position of a screen.</para></summary>
		public event EventPlugin.EventValues<ScreenEx> ScreenLocationChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnScreenLocationChanged += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnScreenLocationChanged -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - ScreenOrientationChanged
		private EventPlugin.EventValues<ScreenEx> OnScreenOrientationChanged;
		/// <summary><para>Occurs when the user changes the Orientation of a screen.</para></summary>
		public event EventPlugin.EventValues<ScreenEx> ScreenOrientationChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnScreenOrientationChanged += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnScreenOrientationChanged -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#region - PrimaryScreenChanged
		private EventPlugin.EventValues<ScreenEx> OnPrimaryScreenChanged;
		/// <summary><para>Occurs when the user The user chooses another screen as primary screen.</para></summary>
		public event EventPlugin.EventValues<ScreenEx> PrimaryScreenChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.DisplaySettingsChanged))
					oldValues[EventType.DisplaySettingsChanged] = new List<ScreenEx>(Status.Screen.AllScreens);
				this.OnPrimaryScreenChanged += value;
				DisplaySettingsChangedEnabled = true;
			}
			remove
			{
				this.OnPrimaryScreenChanged -= value;
				DisplaySettingsChangedEnabled = false;
			}
		}
		#endregion
		#endregion
		#endregion

		#region Constructor & Destructor
		/// <summary>
		/// <para>Disables all SystemEvents because they are static</para>
		/// </summary>
		/// <remarks>http://msdn.microsoft.com/en-us/library/microsoft.win32.systemevents.displaysettingschanged%28v=vs.110%29.aspx</remarks>
		~Screen()
		{
			if (displaySettingsChangedEnabled > 0)
				Win32_SystemEvents.DisplaySettingsChanged -= Win32_SystemEvents_DisplaySettingsChanged;
		}
		#endregion

		#region Methods
		public override EventList EventNames()
		{
			EventList list = new EventList();

			EventListRow row = list.NewEventListRow();
			row.Name = "DisplaySettingsChanged";
			row.Text = "Display settings changed";
			row.Description = "The user changed the display settings (resolution, color depth, ...)";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "ScreenAdded";
			row.Text = "Screen added";
			row.Description = "The user plugged in a new screen.";
			row.Type = Manager.EventTypes.SingleValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "ScreenRemoved";
			row.Text = "Screen removed";
			row.Description = "The user unplugged a screen.";
			row.Type = Manager.EventTypes.SingleValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "ScreenChanged";
			row.Text = "Screen settings changed";
			row.Description = "The user changed the resolution or color depth of a screen.";
			row.Type = Manager.EventTypes.ChangingValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "ScreenColorDepthChanged";
			row.Text = "Screen color depth changed";
			row.Description = "The user changed the color depth of a screen.";
			row.Type = Manager.EventTypes.ChangingValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "ScreenResolutionChanged";
			row.Text = "Screen resolution changed";
			row.Description = "The user changed the resolution of a screen.";
			row.Type = Manager.EventTypes.ChangingValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "PrimaryScreenChanged";
			row.Text = "Primary screen changed";
			row.Description = "The user chose another screen as primary screen.";
			row.Type = Manager.EventTypes.ChangingValue;
			list.AddEventListRow(row);

			return list;
		}

		public override TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Screen");

			TreeNode tnEvents = tnMain.Nodes.Add("Registered events");

			if (DisplaySettingsChangedEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("DisplaySettingsChanged");
				tnEvent.ToolTipText = "Microsoft.Win32.SystemEvents.DisplaySettingsChanged";
			}

			return tnMain;
		}
		#endregion

		#region Event Handlers
		void Win32_SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			if (OnDisplaySettingsChanged != null)
				OnDisplaySettingsChanged(this, e);
			List<ScreenEx> oldScreens = (List<ScreenEx>)oldValues[EventType.DisplaySettingsChanged];
			List<ScreenEx> newScreens = Status.Screen.AllScreens;
			ScreenEx oldPrimary = null;

			foreach (ScreenEx newScreen in newScreens)
			{
				ScreenEx found = oldScreens.Find(new Predicate<ScreenEx>(item => { return item.DeviceName == newScreen.DeviceName; }));
				if (found == null)
				{
					if (OnScreenAdded != null)
						OnScreenAdded(this, new EventArgsValue<ScreenEx>(newScreen));
				}
				else
				{
					bool changed = false;
					EventArgsValues<ScreenEx> eav = new EventArgsValues<ScreenEx>(found, newScreen);
					if (newScreen.BitsPerPixel != found.BitsPerPixel)
					{
						changed = true;
						if (OnScreenColorDepthChanged != null)
							OnScreenColorDepthChanged(this, eav);
					}
					if (newScreen.Bounds.Size != found.Bounds.Size)
					{
						changed = true;
						if (OnScreenResolutionChanged != null)
							OnScreenResolutionChanged(this, eav);
					}
					if (newScreen.Bounds.Location != found.Bounds.Location)
					{
						changed = true;
						if (OnScreenLocationChanged != null)
							OnScreenLocationChanged(this, eav);
					}
					if (newScreen.Frequency != found.Frequency)
					{
						changed = true;
						if (OnScreenRefreshRateChanged != null)
							OnScreenRefreshRateChanged(this, eav);
					}
					if (newScreen.Orientation != found.Orientation)
					{
						changed = true;
						if (OnScreenOrientationChanged != null)
							OnScreenOrientationChanged(this, eav);
					}

					if (changed && OnScreenChanged != null)
						OnScreenChanged(this, eav);

					if (found.Primary)
						oldPrimary = found;

					oldScreens.Remove(found);
				}
			}
			if (OnScreenRemoved != null)
				foreach (ScreenEx oldScreen in oldScreens)
					OnScreenRemoved(this, new EventArgsValue<ScreenEx>(oldScreen));

			ScreenEx newPrimary = Status.Screen.PrimaryScreen;
			if (OnPrimaryScreenChanged != null && oldPrimary.DeviceName != newPrimary.DeviceName)
				OnPrimaryScreenChanged(this, new EventArgsValues<ScreenEx>(oldPrimary, newPrimary));

			oldValues[EventType.DisplaySettingsChanged] = newScreens;
		}
		#endregion
	}
}