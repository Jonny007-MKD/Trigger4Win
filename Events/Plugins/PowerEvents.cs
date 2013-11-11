using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Win32_SystemEvents = Microsoft.Win32.SystemEvents;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using Tasker.Classes.Power;
using Tasker.Classes.System;

namespace Tasker.Events
{
	public class Power : EventPlugin
	{
		#region Dll Imports
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal extern static IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid, int Flags);
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal extern static IntPtr UnregisterPowerSettingNotification(IntPtr Handle);
		#endregion

		#region Constants, Structs & Enums
		internal enum EventType : byte
		{
			PowerModeChanged,
			PowerLineStatusChanged,
			BatteryStatusChanged,
			PowerSchemeChanged,
		}
		#endregion

		#region Properties
		internal static Main Main;
		internal Dictionary<EventType, object> oldValues = new Dictionary<EventType, object>();
		internal static Dictionary<EventType, object> oldStaticValues = new Dictionary<EventType, object>();
		
		#region Register Parent Events
		internal byte powerModeChangedEnabled;
		internal bool PowerModeChangedEnabled
		{
			get
			{
				return powerModeChangedEnabled > 0;
			}
			set
			{
				if (value)
					powerModeChangedEnabled++;
				else
					powerModeChangedEnabled--;
				if (value && powerModeChangedEnabled == 1)
					Win32_SystemEvents.PowerModeChanged += Win32_SystemEvents_PowerModeChanged;
				else if (powerModeChangedEnabled == 0)
					Win32_SystemEvents.PowerModeChanged -= Win32_SystemEvents_PowerModeChanged;
			}
		}
		internal static byte powerBroadcastChangedEnabled;
		internal static IntPtr powerBroadcastChangedHandle = IntPtr.Zero;
		internal static bool PowerBroadcastChangedEnabled
		{
			get
			{
				return powerBroadcastChangedEnabled > 0;
			}
			set
			{
				if (value)
					powerBroadcastChangedEnabled++;
				else
					powerBroadcastChangedEnabled--;

				if (value && powerBroadcastChangedEnabled == 1)
				{
					powerBroadcastChangedHandle = RegisterPowerSettingNotification(Main.Handle, ref Classes.Power.PowerScheme.GUID_POWERSCHEME_PERSONALITY, Main.DEVICE_NOTIFY_WINDOW_HANDLE);
					Main.RegisterEventForMessage(Classes.System.WindowsMessages.WM_POWERBROADCAST);
					Main.MessageReceived += Main_MessageReceived;
				}
				else if (powerBroadcastChangedEnabled == 0)
				{
					UnregisterPowerSettingNotification(powerBroadcastChangedHandle);
					powerBroadcastChangedHandle = IntPtr.Zero;
					Main.UnregisterEventForMessage(Classes.System.WindowsMessages.WM_POWERBROADCAST);
					Main.MessageReceived -= Main_MessageReceived;
				}
			}
		}
		#endregion

		#region Events
		#region PowerModeChanged
		internal EventPlugin.EventValue<PowerModes> OnPowerModeChanged;
		/// <summary><para>Occurs when the user suspends or resumes the system.</para></summary>
		public event EventPlugin.EventValue<PowerModes> PowerModeChanged
		{
			add
			{
				this.OnPowerModeChanged += value;
				PowerModeChangedEnabled = true;
			}
			remove
			{
				this.OnPowerModeChanged -= value;
				PowerModeChangedEnabled = false;
			}
		}
		#endregion
		#region - Suspend
		internal EventPlugin.Event OnSuspend;
		/// <summary><para>Occurs when the user suspends the system.</para></summary>
		public event EventPlugin.Event Suspend
		{
			add
			{
				this.OnSuspend += value;
				PowerModeChangedEnabled = true;
			}
			remove
			{
				this.OnSuspend -= value;
				PowerModeChangedEnabled = false;
			}
		}
		#endregion
		#region - Resume
		internal EventPlugin.Event OnResume;
		/// <summary><para>Occurs when the user resumes the system.</para></summary>
		public event EventPlugin.Event Resume
		{
			add
			{
				this.OnResume += value;
				PowerModeChangedEnabled = true;
			}
			remove
			{
				this.OnResume -= value;
				PowerModeChangedEnabled = false;
			}
		}
		#endregion
		#region - PowerLineStatusChanged
		internal EventPlugin.EventValues<PowerLineStatus> OnPowerLineStatusChanged;
		/// <summary><para>Occurs when the user connects or disconnects the system from/to the power network.</para></summary>
		public event EventPlugin.EventValues<PowerLineStatus> PowerLineStatusChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.PowerLineStatusChanged))
					oldValues[EventType.PowerLineStatusChanged] = Status.Power.PowerLineStatus;
				this.OnPowerLineStatusChanged += value;
				PowerModeChangedEnabled = true;
			}
			remove
			{
				this.OnPowerLineStatusChanged -= value;
				PowerModeChangedEnabled = false;
			}
		}
		#endregion
		#region - BatteryAvailabilityChanged
		internal EventPlugin.EventValue<bool?> OnBatteryAvailabilityChanged;
		/// <summary><para>Occurs when the user connects or disconnects the computer from the battery.</para></summary>
		public event EventPlugin.EventValue<bool?> BatteryAvailabilityChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.BatteryStatusChanged))
					oldValues[EventType.BatteryStatusChanged] = Status.Power.BatteryChargeStatus;
				this.OnBatteryAvailabilityChanged += value;
				PowerModeChangedEnabled = true;
			}
			remove
			{
				this.OnBatteryAvailabilityChanged -= value;
				PowerModeChangedEnabled = false;
			}
		}
		#endregion
		#region - BatteryStatusChanged
		internal EventPlugin.EventValues<BatteryChargeStatus> OnBatteryStatusChanged;
		/// <summary><para>Occurs when the user connects or disconnects the computer from the battery.</para></summary>
		public event EventPlugin.EventValues<BatteryChargeStatus> BatteryStatusChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.BatteryStatusChanged))
					oldValues[EventType.BatteryStatusChanged] = Status.Power.BatteryChargeStatus;
				this.OnBatteryStatusChanged += value;
				PowerModeChangedEnabled = true;
			}
			remove
			{
				this.OnBatteryStatusChanged -= value;
				PowerModeChangedEnabled = false;
			}
		}
		#endregion
		
		#region PowerSchemeChanged
		internal static EventPlugin.EventValues<PowerScheme> OnPowerSchemeChanged;
		/// <summary><para>Occurs when the user selects antoher power plan.</para></summary>
		public event EventPlugin.EventValues<PowerScheme> PowerSchemeChanged
		{
			add
			{
				if (!oldStaticValues.ContainsKey(EventType.PowerSchemeChanged))
					oldStaticValues[EventType.PowerSchemeChanged] = Status.Power.ActivePowerScheme;
				OnPowerSchemeChanged += value;
				PowerBroadcastChangedEnabled = true;
			}
			remove
			{
				OnPowerSchemeChanged -= value;
				PowerBroadcastChangedEnabled = false;
			}
		}
		#endregion
		#endregion

		#region Constructor & Destructor
		/// <summary>
		/// <para>Creates an instance of this class</para>
		/// </summary>
		/// <param name="Main"></param>
		public Power(Main main)
		{
			Main = main;
		}

		/// <summary>
		/// <para>Disables all SystemEvents because they are static</para>
		/// </summary>
		/// <remarks>http://msdn.microsoft.com/en-us/library/microsoft.win32.systemevents.displaysettingschanged%28v=vs.110%29.aspx</remarks>
		~Power()
		{
			if (PowerModeChangedEnabled)
				Win32_SystemEvents.PowerModeChanged -= Win32_SystemEvents_PowerModeChanged;
			if (PowerBroadcastChangedEnabled)
			{
				UnregisterPowerSettingNotification(powerBroadcastChangedHandle);
				Main.MessageReceived -= Main_MessageReceived;
			}
		}
		#endregion

		#region Methods
		public override EventList EventNames()
		{
			EventList list = new EventList();

			EventListRow row = list.NewEventListRow();
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

			return list;
		}

		public override TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Power");

			TreeNode tnEvents = tnMain.Nodes.Add("Registered events");

			if (PowerModeChangedEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("PowerModeChanged");
				tnEvent.ToolTipText = "Microsoft.Win32.SystemEvents.PowerModeChanged";
			}
			if (PowerBroadcastChangedEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("PowerBroadcastChanged");
				tnEvent.ToolTipText = "Registered in Win32 message service (WM_POWERBROADCAST)";
			}

			return tnMain;
		}
		#endregion

		#region Event Handler
		internal void Win32_SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
		{
			if (OnPowerModeChanged != null)
				OnPowerModeChanged(this, new EventArgsValue<PowerModes>(e.Mode));
			switch (e.Mode)
			{
				case PowerModes.Resume:
					if (OnResume != null)
						OnResume(this, new EventArgs());
					break;
				case PowerModes.Suspend:
					if (OnSuspend != null)
						OnSuspend(this, new EventArgs());
					break;
				case PowerModes.StatusChange:
					if (OnPowerLineStatusChanged != null)
					{
						PowerLineStatus oldValuePLS = (PowerLineStatus)oldValues[EventType.PowerLineStatusChanged];
						PowerLineStatus newValuePLS = Status.Power.PowerLineStatus;
						if (oldValuePLS != newValuePLS)
						{
							OnPowerLineStatusChanged(this, new EventArgsValues<PowerLineStatus>(oldValuePLS, newValuePLS));
							oldValues[EventType.PowerLineStatusChanged] = newValuePLS;
						}
					}
					BatteryChargeStatus oldValueBCS = (BatteryChargeStatus)oldValues[EventType.BatteryStatusChanged];
					BatteryChargeStatus newValueBCS = Status.Power.BatteryChargeStatus;
					if (OnBatteryStatusChanged != null)
					{
						if (oldValueBCS != newValueBCS)
						{
							OnBatteryStatusChanged(this, new EventArgsValues<BatteryChargeStatus>(oldValueBCS, newValueBCS));
							oldValues[EventType.BatteryStatusChanged] = newValueBCS;
						}
					}
					if (OnBatteryAvailabilityChanged != null)
					{
						bool? oldValueBCSb = null, newValueBCSb = null;
						if (oldValueBCS == BatteryChargeStatus.NoSystemBattery)
							oldValueBCSb = false;
						else if (oldValueBCS != BatteryChargeStatus.Unknown)
							oldValueBCSb = true;
						if (newValueBCS == BatteryChargeStatus.NoSystemBattery)
							newValueBCSb = false;
						else if (newValueBCS != BatteryChargeStatus.Unknown)
							newValueBCSb = true;

						if (oldValueBCSb != newValueBCSb)
						{
							OnBatteryAvailabilityChanged(this, new EventArgsValue<bool?>(newValueBCSb));
						}
					}
					break;
			}
		}

		internal static void Main_MessageReceived(object sender, EventArgsValue<Message> e)
		{
			if (e.Value.Msg != (int)Classes.System.WindowsMessages.WM_POWERBROADCAST)
				return;
			PBT wParam = (PBT)e.Value.WParam.ToInt32();
			switch (wParam)
			{
				case PBT.POWERSETTINGCHANGE:
					PowerBroadcast_Setting pbs = (PowerBroadcast_Setting)Marshal.PtrToStructure(e.Value.LParam, typeof(PowerBroadcast_Setting));
					PowerScheme oldPlan = (PowerScheme)oldStaticValues[EventType.PowerSchemeChanged];
					PowerScheme newPlan = Status.Power.ActivePowerScheme;
					if (oldPlan == newPlan)
						return;
					if (OnPowerSchemeChanged != null)
						OnPowerSchemeChanged(null, new EventArgsValues<PowerScheme>(oldPlan, newPlan));
					oldStaticValues[EventType.PowerSchemeChanged] = newPlan;
					break;
			}
		}
		#endregion
		#endregion
	}
}