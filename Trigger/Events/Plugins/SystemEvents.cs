using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Win32_SystemEvents = Microsoft.Win32.SystemEvents;
using System.Windows.Forms;
using System.Drawing;
using Trigger.Classes.Screen;

namespace Trigger.Events
{
	class System : EventPlugin
	{
		#region Properties
		private enum EventType : byte
		{
			InstalledFontsChanged,
			PaletteChanged,
			PowerModeChanged,
			PowerLineStatusChanged,
			BatteryStatusChanged,
			SessionEnding,
			SessionSwitch,
			UserPreferenceChanged,
		}

		private Dictionary<EventType, object> oldValues = new Dictionary<EventType, object>();

		#region Register Parent Events
		private byte installedFontsChangedEnabled;
		private bool InstalledFontsChangedEnabled
		{
			set
			{
				if (value)
					installedFontsChangedEnabled++;
				else
					installedFontsChangedEnabled--;
				if (value && installedFontsChangedEnabled == 1)
					Win32_SystemEvents.InstalledFontsChanged += Win32_SystemEvents_InstalledFontsChanged;
				else if (installedFontsChangedEnabled == 0)
					Win32_SystemEvents.InstalledFontsChanged -= Win32_SystemEvents_InstalledFontsChanged;
			}
			get
			{
				return installedFontsChangedEnabled > 0;
			}
		}
		private byte paletteChangedEnabled;
		private bool PaletteChangedEnabled
		{
			set
			{
				if (value)
					paletteChangedEnabled++;
				else
					paletteChangedEnabled--;
				if (value && paletteChangedEnabled == 1)
					Win32_SystemEvents.PaletteChanged += Win32_SystemEvents_PaletteChanged;
				else if (paletteChangedEnabled == 0)
					Win32_SystemEvents.PaletteChanged -= Win32_SystemEvents_PaletteChanged;
			}
			get
			{
				return paletteChangedEnabled > 0;
			}
		}
		private byte sessionEndingEnabled;
		private bool SessionEndingEnabled
		{
			set
			{
				if (value)
					sessionEndingEnabled++;
				else
					sessionEndingEnabled--;
				if (value && sessionEndingEnabled == 1)
					Win32_SystemEvents.SessionEnding += Win32_SystemEvents_SessionEnding;
				else if (sessionEndingEnabled == 0)
					Win32_SystemEvents.SessionEnding -= Win32_SystemEvents_SessionEnding;
			}
			get
			{
				return sessionEndingEnabled > 0;
			}
		}
		private byte sessionSwitchEnabled;
		private bool SessionSwitchEnabled
		{
			set
			{
				if (value)
					sessionSwitchEnabled++;
				else
					sessionSwitchEnabled--;
				if (value && sessionSwitchEnabled == 1)
					Win32_SystemEvents.SessionSwitch += Win32_SystemEvents_SessionSwitch;
				else if (sessionSwitchEnabled == 0)
					Win32_SystemEvents.SessionSwitch -= Win32_SystemEvents_SessionSwitch;
			}
			get
			{
				return sessionEndingEnabled > 0;
			}
		}
		private byte userPreferenceChangedEnabled;
		private bool UserPreferenceChangedEnabled
		{
			set
			{
				if (value)
					userPreferenceChangedEnabled++;
				else
					userPreferenceChangedEnabled--;
				if (value && userPreferenceChangedEnabled == 1)
					Win32_SystemEvents.UserPreferenceChanged += Win32_SystemEvents_UserPreferenceChanged;
				else if (userPreferenceChangedEnabled == 0)
					Win32_SystemEvents.UserPreferenceChanged -= Win32_SystemEvents_UserPreferenceChanged;
			}
			get
			{
				return userPreferenceChangedEnabled > 0;
			}
		}
		#endregion

		#region Events
		#region InstalledFontsChanged
		private EventPlugin.Event OnInstalledFontsChanged;
		/// <summary><para>Occurs when the user adds fonts to or removes fonts from the system.</para></summary>
		public event EventPlugin.Event InstalledFontsChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.InstalledFontsChanged))
					oldValues[EventType.InstalledFontsChanged] = Status.System.InstalledFonts;
				this.OnInstalledFontsChanged += value;
				InstalledFontsChangedEnabled = true;
			}
			remove
			{
				this.OnInstalledFontsChanged -= value;
				InstalledFontsChangedEnabled = false;
			}
		}
		#endregion
		#region - FontAdded
		private EventPlugin.EventValue<FontFamily> OnFontAdded;
		/// <summary><para>Occurs when the user adds fonts to the system.</para></summary>
		public event EventPlugin.EventValue<FontFamily> FontAdded
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.InstalledFontsChanged))
					oldValues[EventType.InstalledFontsChanged] = Status.System.InstalledFonts;
				this.OnFontAdded += value;
				InstalledFontsChangedEnabled = true;
			}
			remove
			{
				this.OnFontAdded -= value;
				InstalledFontsChangedEnabled = false;
			}
		}
		#endregion
		#region - FontRemoved
		private EventPlugin.EventValue<FontFamily> OnFontRemoved;
		/// <summary><para>Occurs when the user removes fonts from the system.</para></summary>
		public event EventPlugin.EventValue<FontFamily> FontRemoved
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.InstalledFontsChanged))
					oldValues[EventType.InstalledFontsChanged] = Status.System.InstalledFonts;
				this.OnFontRemoved += value;
				InstalledFontsChangedEnabled = true;
			}
			remove
			{
				this.OnFontRemoved -= value;
				InstalledFontsChangedEnabled = false;
			}
		}
		#endregion

		#region PaletteChanged
		/// <summary><para>Occurs when the user switches to an application that uses a different palette.</para></summary>
		private EventPlugin.Event OnPaletteChanged;
		public event EventPlugin.Event PaletteChanged
		{
			add
			{
				this.OnPaletteChanged += value;
				PaletteChangedEnabled = true;
			}
			remove
			{
				this.OnPaletteChanged -= value;
				PaletteChangedEnabled = false;
			}
		}
		#endregion
		
		#region SessionEnding
		private EventPlugin.EventReason<SessionEndReasons> OnSessionEnding;
		/// <summary><para>Occurs when the user is trying to log off or shut down the system.</para></summary>
		public event EventPlugin.EventReason<SessionEndReasons> SessionEnding
		{
			add
			{
				this.OnSessionEnding += value;
				SessionEndingEnabled = true;
			}
			remove
			{
				this.OnSessionEnding -= value;
				SessionEndingEnabled = false;
			}
		}
		#endregion
		#region - Logoff
		private EventPlugin.Event OnLogoff;
		/// <summary><para>Occurs when the user is trying to log off.</para></summary>
		public event EventPlugin.Event Logoff
		{
			add
			{
				this.OnLogoff += value;
				SessionEndingEnabled = true;
			}
			remove
			{
				this.OnLogoff -= value;
				SessionEndingEnabled = false;
			}
		}
		#endregion
		#region - Shutdown
		private EventPlugin.Event OnShutdown;
		/// <summary><para>Occurs when the user is trying to shut down the system.</para></summary>
		public event EventPlugin.Event Shutdown
		{
			add
			{
				this.OnShutdown += value;
				SessionEndingEnabled = true;
			}
			remove
			{
				this.OnShutdown -= value;
				SessionEndingEnabled = false;
			}
		}
		#endregion

		#region SessionSwitch
		private EventPlugin.EventReason<SessionSwitchReason> OnSessionSwitch;
		/// <summary><para>Occurs when the currently logged-in user has changed.</para></summary>
		public event EventPlugin.EventReason<SessionSwitchReason> SessionSwitch
		{
			add
			{
				this.OnSessionSwitch += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnSessionSwitch -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - ConsoleConnect
		private EventPlugin.Event OnConsoleConnect;
		/// <summary><para>A session has been connected from the console.</para></summary>
		public event EventPlugin.Event ConsoleConnect
		{
			add
			{
				this.OnConsoleConnect += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnConsoleConnect -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - ConsoleDisconnect
		private EventPlugin.Event OnConsoleDisconnect;
		/// <summary><para>A session has been disconnected from the console.</para></summary>
		public event EventPlugin.Event ConsoleDisconnect
		{
			add
			{
				this.OnConsoleDisconnect += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnConsoleDisconnect -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - RemoteConnect
		private EventPlugin.Event OnRemoteConnect;
		/// <summary><para>A session has been connected from a remote connection.</para></summary>
		public event EventPlugin.Event RemoteConnect
		{
			add
			{
				this.OnRemoteConnect += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnRemoteConnect -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - RemoteDisconnect
		private EventPlugin.Event OnRemoteDisconnect;
		/// <summary><para>A session has been disconnected from a remote connection.</para></summary>
		public event EventPlugin.Event RemoteDisconnect
		{
			add
			{
				this.OnRemoteDisconnect += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnRemoteDisconnect -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - SessionLock
		private EventPlugin.Event OnSessionLock;
		/// <summary><para>A session has been locked.</para></summary>
		public event EventPlugin.Event SessionLock
		{
			add
			{
				this.OnSessionLock += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnSessionLock -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - SessionLogoff
		private EventPlugin.Event OnSessionLogoff;
		/// <summary><para>A user has logged off from a session.</para></summary>
		public event EventPlugin.Event SessionLogoff
		{
			add
			{
				this.OnSessionLogoff += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnSessionLogoff -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - SessionLogon
		private EventPlugin.Event OnSessionLogon;
		/// <summary><para>A user has logged on to a session.</para></summary>
		public event EventPlugin.Event SessionLogon
		{
			add
			{
				this.OnSessionLogon += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnSessionLogon -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - SessionRemoteControl
		private EventPlugin.Event OnSessionRemoteControl;
		/// <summary><para>A session has changed its status to or from remote controlled mode.</para></summary>
		public event EventPlugin.Event SessionRemoteControl
		{
			add
			{
				this.OnSessionRemoteControl += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnSessionRemoteControl -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion
		#region - SessionUnlock
		private EventPlugin.Event OnSessionUnlock;
		/// <summary><para>A session has been unlocked.</para></summary>
		public event EventPlugin.Event SessionUnlock
		{
			add
			{
				this.OnSessionUnlock += value;
				SessionSwitchEnabled = true;
			}
			remove
			{
				this.OnSessionUnlock -= value;
				SessionSwitchEnabled = false;
			}
		}
		#endregion

		#region UserPreferenceChanged
		private EventPlugin.EventReason<UserPreferenceCategory> OnUserPreferenceChanged;
		public event EventPlugin.EventReason<UserPreferenceCategory> UserPreferenceChanged
		{
			add
			{
				this.OnUserPreferenceChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceAccessibilityChanged
		private EventPlugin.Event OnUserPreferenceAccessibilityChanged;
		public event EventPlugin.Event UserPreferenceAccessibilityChanged
		{
			add
			{
				this.OnUserPreferenceAccessibilityChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceAccessibilityChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceColorChanged
		private EventPlugin.Event OnUserPreferenceColorChanged;
		public event EventPlugin.Event UserPreferenceColorChanged
		{
			add
			{
				this.OnUserPreferenceColorChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceColorChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceDesktopChanged
		private EventPlugin.Event OnUserPreferenceDesktopChanged;
		public event EventPlugin.Event UserPreferenceDesktopChanged
		{
			add
			{
				this.OnUserPreferenceDesktopChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceDesktopChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceGeneralChanged
		private EventPlugin.Event OnUserPreferenceGeneralChanged;
		public event EventPlugin.Event UserPreferenceGeneralChanged
		{
			add
			{
				this.OnUserPreferenceGeneralChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceGeneralChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceIconChanged
		private EventPlugin.Event OnUserPreferenceIconChanged;
		public event EventPlugin.Event UserPreferenceIconChanged
		{
			add
			{
				this.OnUserPreferenceIconChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceIconChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceKeyboardChanged
		private EventPlugin.Event OnUserPreferenceKeyboardChanged;
		public event EventPlugin.Event UserPreferenceKeyboardChanged
		{
			add
			{
				this.OnUserPreferenceKeyboardChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceKeyboardChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceLocaleChanged
		private EventPlugin.Event OnUserPreferenceLocaleChanged;
		public event EventPlugin.Event UserPreferenceLocaleChanged
		{
			add
			{
				this.OnUserPreferenceLocaleChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceLocaleChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceMenuChanged
		private EventPlugin.Event OnUserPreferenceMenuChanged;
		public event EventPlugin.Event UserPreferenceMenuChanged
		{
			add
			{
				this.OnUserPreferenceMenuChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceMenuChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceMouseChanged
		private EventPlugin.Event OnUserPreferenceMouseChanged;
		public event EventPlugin.Event UserPreferenceMouseChanged
		{
			add
			{
				this.OnUserPreferenceMouseChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceMouseChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferencePolicyChanged
		private EventPlugin.Event OnUserPreferencePolicyChanged;
		public event EventPlugin.Event UserPreferencePolicyChanged
		{
			add
			{
				this.OnUserPreferencePolicyChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferencePolicyChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferencePowerChanged
		private EventPlugin.Event OnUserPreferencePowerChanged;
		public event EventPlugin.Event UserPreferencePowerChanged
		{
			add
			{
				this.OnUserPreferencePowerChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferencePowerChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceScreensaverChanged
		private EventPlugin.Event OnUserPreferenceScreensaverChanged;
		public event EventPlugin.Event UserPreferenceScreensaverChanged
		{
			add
			{
				this.OnUserPreferenceScreensaverChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceScreensaverChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceVisualStyleChanged
		private EventPlugin.Event OnUserPreferenceVisualStyleChanged;
		public event EventPlugin.Event UserPreferenceVisualStyleChanged
		{
			add
			{
				this.OnUserPreferenceVisualStyleChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceVisualStyleChanged -= value;
				UserPreferenceChangedEnabled = false;
			}
		}
		#endregion
		#region - UserPreferenceWindowChanged
		private EventPlugin.Event OnUserPreferenceWindowChanged;
		public event EventPlugin.Event UserPreferenceWindowChanged
		{
			add
			{
				this.OnUserPreferenceWindowChanged += value;
				UserPreferenceChangedEnabled = true;
			}
			remove
			{
				this.OnUserPreferenceWindowChanged -= value;
				UserPreferenceChangedEnabled = false;
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
		~System()
		{
			if (installedFontsChangedEnabled > 0)
				Win32_SystemEvents.InstalledFontsChanged -= Win32_SystemEvents_InstalledFontsChanged;
			if (paletteChangedEnabled > 0)
				Win32_SystemEvents.PaletteChanged -= Win32_SystemEvents_PaletteChanged;
			if (sessionEndingEnabled > 0)
				Win32_SystemEvents.SessionEnding -= Win32_SystemEvents_SessionEnding;
			if (sessionSwitchEnabled > 0)
				Win32_SystemEvents.SessionSwitch -= Win32_SystemEvents_SessionSwitch;
			if (userPreferenceChangedEnabled > 0)
				Win32_SystemEvents.UserPreferenceChanged -= Win32_SystemEvents_UserPreferenceChanged;
		}
		#endregion

		#region Methods
		public override EventList EventNames()
		{
			EventList list = new EventList();
			#region done
			EventListRow row = list.NewEventListRow();
			row.Name = "InstalledFontsChanged";
			row.Text = "Installed fonts changed";
			row.Description = "The user added fonts to or removed fonts from the system.";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "FontAdded";
			row.Text = "Font added";
			row.Description = "The user added a font to the system";
			row.Type = Manager.EventTypes.SingleValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "FontRemoved";
			row.Text = "Font removed";
			row.Description = "The user removed a font from the system";
			row.Type = Manager.EventTypes.SingleValue;
			list.AddEventListRow(row);



			row = list.NewEventListRow();
			row.Name = "PowerModeChanged";
			row.Text = "Power mode changed";
			row.Description = "The user suspends or resumes the system";
			row.Type = Manager.EventTypes.SingleValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "Suspend";
			row.Text = "Suspend";
			row.Description = "The user suspends the system";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "Resume";
			row.Text = "Resume";
			row.Description = "The user resumes the system";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);



			row = list.NewEventListRow();
			row.Name = "SessionEnding";
			row.Text = "Session ending";
			row.Description = "The user is trying to log off or shut down the system";
			row.Type = Manager.EventTypes.SingleValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "Logoff";
			row.Text = "Log off";
			row.Description = "The user is trying to log off";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "Shutdown";
			row.Text = "Shut down";
			row.Description = "The user is trying to shut down the system";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);



			row = list.NewEventListRow();
			row.Name = "SessionSwitch";
			row.Text = "Session switch";
			row.Description = "The currently logged-in user has changed";
			row.Type = Manager.EventTypes.SingleValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "ConsoleConnect";
			row.Text = "Console connect";
			row.Description = "A session has been connected from the console";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "ConsoleDisconnect";
			row.Text = "Console disconnect";
			row.Description = "A session has been disconnected from the console";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "RemoteConnect";
			row.Text = "Remote connect";
			row.Description = "A session has been connected from a remote connection";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "RemoteDisconnect";
			row.Text = "Remote disconnect";
			row.Description = "A session has been disconnected from a remote connection";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "SessionLock";
			row.Text = "Session locked";
			row.Description = "A session has been locked";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "SessionLogoff";
			row.Text = "Session logged off";
			row.Description = "A user has logged off from a session";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "SessionLogon";
			row.Text = "Session logged on";
			row.Description = "A user has logged on to a session";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "SessionRemoteControl";
			row.Text = "Session remote controlled mode changed";
			row.Description = "A session has changed its status to or from remote controlled mode";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "SessionUnlock";
			row.Text = "Session unlocked";
			row.Description = "A session has been unlocked";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);



			row = list.NewEventListRow();
			row.Name = "PaletteChanged";
			row.Text = "Palette changed";
			row.Description = "The user switched to an application that uses a different palette.";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);



			#endregion

			row = list.NewEventListRow();
			row.Name = "AAAA";
			row.Text = "AAAA";
			row.Description = "AAAAAAAAAAAAAAA";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "AAAA";
			row.Text = "AAAA";
			row.Description = "AAAAAAAAAAAAAAA";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "AAAA";
			row.Text = "AAAA";
			row.Description = "AAAAAAAAAAAAAAA";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			return list;
		}

		public override TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("System");

			TreeNode tnEvents = tnMain.Nodes.Add("Registered events");

			if (InstalledFontsChangedEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("InstalledFontsChanged");
				tnEvent.ToolTipText = "Microsoft.Win32.SystemEvents.InstalledFontsChanged";
			}
			if (PaletteChangedEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("PaletteChanged");
				tnEvent.ToolTipText = "Microsoft.Win32.SystemEvents.PaletteChanged";
			}
			if (SessionEndingEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("SessionEnding");
				tnEvent.ToolTipText = "Microsoft.Win32.SystemEvents.SessionEnding";
			}
			if (SessionSwitchEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("SessionSwitch");
				tnEvent.ToolTipText = "Microsoft.Win32.SystemEvents.SessionSwitch";
			}
			if (UserPreferenceChangedEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("UserPreferenceChanged");
				tnEvent.ToolTipText = "Microsoft.Win32.SystemEvents.UserPreferenceChanged";
			}

			return tnMain;
		}
		#endregion

		#region Events
		void Win32_SystemEvents_InstalledFontsChanged(object sender, EventArgs e)
		{
			if (OnInstalledFontsChanged != null)
				OnInstalledFontsChanged(this, e);
			List<FontFamily> oldFonts = (List<FontFamily>)oldValues[EventType.InstalledFontsChanged];
			List<FontFamily> newFonts = Status.System.InstalledFonts;

			foreach (FontFamily newFont in newFonts)
			{
				FontFamily found = oldFonts.Find(new Predicate<FontFamily>(item => { return item.Equals(newFont); }));
				if (found == null)
				{
					if (OnFontAdded != null)
						OnFontAdded(this, new EventArgsValue<FontFamily>(newFont));
				}
				else
					oldFonts.Remove(found);
			}
			if (OnFontRemoved != null)
				foreach (FontFamily oldFont in oldFonts)
					OnFontRemoved(this, new EventArgsValue<FontFamily>(oldFont));

			oldValues[EventType.InstalledFontsChanged] = newFonts;
		}

		void Win32_SystemEvents_PaletteChanged(object sender, EventArgs e)
		{
			if (OnPaletteChanged != null)
				OnPaletteChanged(this, e);
		}

		void Win32_SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
		{
			if (OnSessionEnding != null)
				OnSessionEnding(this, new EventArgsReason<SessionEndReasons>(e.Reason));
			if (e.Reason == SessionEndReasons.Logoff)
			{
				if (OnLogoff != null)
					OnLogoff(this, new EventArgs());
			}
			else if (e.Reason == SessionEndReasons.SystemShutdown)
			{
				if (OnShutdown != null)
					OnShutdown(this, new EventArgs());
			}
		}

		void Win32_SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
		{
			if (OnSessionSwitch != null)
				OnSessionSwitch(this, new EventArgsReason<SessionSwitchReason>(e.Reason));
			switch (e.Reason)
			{
				case SessionSwitchReason.ConsoleConnect:
					if (OnConsoleConnect != null)
						OnConsoleConnect(this, new EventArgs());
					break;
				case SessionSwitchReason.ConsoleDisconnect:
					if (OnConsoleDisconnect != null)
						OnConsoleDisconnect(this, new EventArgs());
					break;
				case SessionSwitchReason.RemoteConnect:
					if (OnRemoteConnect != null)
						OnRemoteConnect(this, new EventArgs());
					break;
				case SessionSwitchReason.RemoteDisconnect:
					if (OnRemoteDisconnect != null)
						OnRemoteDisconnect(this, new EventArgs());
					break;
				case SessionSwitchReason.SessionLock:
					if (OnSessionLock != null)
						OnSessionLock(this, new EventArgs());
					break;
				case SessionSwitchReason.SessionLogoff:
					if (OnSessionLogoff != null)
						OnSessionLogoff(this, new EventArgs());
					break;
				case SessionSwitchReason.SessionLogon:
					if (OnSessionLogon != null)
						OnSessionLogon(this, new EventArgs());
					break;
				case SessionSwitchReason.SessionRemoteControl:
					if (OnSessionRemoteControl != null)
						OnSessionRemoteControl(this, new EventArgs());
					break;
				case SessionSwitchReason.SessionUnlock:
					if (OnSessionUnlock != null)
						OnSessionUnlock(this, new EventArgs());
					break;
			}
		}

		void Win32_SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (OnUserPreferenceChanged != null)
				OnUserPreferenceChanged(this, new EventArgsReason<UserPreferenceCategory>(e.Category));
			switch (e.Category)
			{
				case UserPreferenceCategory.Accessibility:
					if (OnUserPreferenceAccessibilityChanged != null)
						OnUserPreferenceAccessibilityChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Color:
					if (OnUserPreferenceColorChanged != null)
						OnUserPreferenceColorChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Desktop:
					if (OnUserPreferenceDesktopChanged != null)
						OnUserPreferenceDesktopChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.General:
					if (OnUserPreferenceGeneralChanged != null)
						OnUserPreferenceGeneralChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Icon:
					if (OnUserPreferenceIconChanged != null)
						OnUserPreferenceIconChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Keyboard:
					if (OnUserPreferenceKeyboardChanged != null)
						OnUserPreferenceKeyboardChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Locale:
					if (OnUserPreferenceLocaleChanged != null)
						OnUserPreferenceLocaleChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Menu:
					if (OnUserPreferenceMenuChanged != null)
						OnUserPreferenceMenuChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Mouse:
					if (OnUserPreferenceMouseChanged != null)
						OnUserPreferenceMouseChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Policy:
					if (OnUserPreferencePolicyChanged != null)
						OnUserPreferencePolicyChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Power:
					if (OnUserPreferencePowerChanged != null)
						OnUserPreferencePowerChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Screensaver:
					if (OnUserPreferenceScreensaverChanged != null)
						OnUserPreferenceScreensaverChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.VisualStyle:
					if (OnUserPreferenceVisualStyleChanged != null)
						OnUserPreferenceVisualStyleChanged(this, new EventArgs());
					break;
				case UserPreferenceCategory.Window:
					if (OnUserPreferenceWindowChanged != null)
						OnUserPreferenceWindowChanged(this, new EventArgs());
					break;
			}
		}
		#endregion
	}
}