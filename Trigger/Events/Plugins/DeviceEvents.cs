using System;
using Systemm = System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Trigger.Classes.System;
using Trigger.Classes.Device;

namespace Trigger.Events
{
	/// <summary>
	/// <para>A <see cref="EventPlugin"/> that provides events for Devices</para>
	/// <para>It catches arriving and removed devices as well as inserted media and new network volumes</para>
	/// </summary>
	public class Device : EventPlugin
	{
		#region Constants, Structs & Enums
		internal enum EventType : byte
		{
			DeviceArrived,
			DeviceRemoved,
		}
		#endregion

		#region Properties
		internal static Main Main;
		internal Dictionary<EventType, object> oldValues = new Dictionary<EventType, object>();
		internal static Dictionary<EventType, object> oldStaticValues = new Dictionary<EventType, object>();
		
		#region Register Parent Events
		internal static byte deviceChangedEnabled;
		internal static bool DeviceChangedEnabled
		{
			get
			{
				return deviceChangedEnabled > 0;
			}
			set
			{
				if (value)
					deviceChangedEnabled++;
				else
					deviceChangedEnabled--;

				if (value && deviceChangedEnabled == 1)
				{
					Main.RegisterEventForMessage(Classes.System.WindowsMessages.WM_DEVICECHANGE);
					Main.MessageReceived += Main_MessageReceived;
				}
				else if (deviceChangedEnabled == 0)
				{
					Main.UnregisterEventForMessage(Classes.System.WindowsMessages.WM_DEVICECHANGE);
					Main.MessageReceived -= Main_MessageReceived;
				}
			}
		}
		#endregion

		#region Events
		#region DeviceArrived
		internal static EventPlugin.EventValue<Classes.Device.Device> OnDeviceArrived;
		/// <summary><para>A device (no media) has been inserted and becomes available.</para></summary>
		public event EventPlugin.EventValue<Classes.Device.Device> DeviceArrived
		{
			add
			{
				OnDeviceArrived += value;
				DeviceChangedEnabled = true;
			}
			remove
			{
				OnDeviceArrived -= value;
				DeviceChangedEnabled = false;
			}
		}
		#endregion
		#region DeviceQueryRemove
		internal static EventPlugin.EventValue<Classes.Device.Device> OnDeviceQueryRemove;
		/// <summary><para>The system broadcasts the DBT_DEVICEQUERYREMOVE device event to request permission to remove a device or piece of media. This message is the last chance for applications and drivers to prepare for this removal. However, any application can deny this request and cancel the operation.</para></summary>
		public event EventPlugin.EventValue<Classes.Device.Device> DeviceQueryRemove
		{
			add
			{
				OnDeviceQueryRemove += value;
				DeviceChangedEnabled = true;
			}
			remove
			{
				OnDeviceQueryRemove -= value;
				DeviceChangedEnabled = false;
			}
		}
		#endregion
		#region DeviceQueryRemoveFailed
		internal static EventPlugin.EventValue<Classes.Device.Device> OnDeviceQueryRemoveFailed;
		/// <summary><para>A request to remove a device or piece of media has been canceled.</para></summary>
		public event EventPlugin.EventValue<Classes.Device.Device> DeviceQueryRemoveFailed
		{
			add
			{
				OnDeviceQueryRemoveFailed += value;
				DeviceChangedEnabled = true;
			}
			remove
			{
				OnDeviceQueryRemoveFailed -= value;
				DeviceChangedEnabled = false;
			}
		}
		#endregion
		#region DeviceRemoved
		internal static EventPlugin.EventValue<Classes.Device.Device> OnDeviceRemoved;
		/// <summary><para>A device or piece of media has been physically removed.</para></summary>
		public event EventPlugin.EventValue<Classes.Device.Device> DeviceRemoved
		{
			add
			{
				if (!oldStaticValues.ContainsKey(EventType.DeviceRemoved))
					oldStaticValues[EventType.DeviceRemoved] = Status.Device.AvailableDisks;
				OnDeviceRemoved += value;
				DeviceChangedEnabled = true;
			}
			remove
			{
				OnDeviceRemoved -= value;
				DeviceChangedEnabled = false;
			}
		}
		#endregion
		#region DeviceRemovePending
		internal static EventPlugin.EventValue<Classes.Device.Device> OnDeviceRemovePending;
		/// <summary><para>A device or piece of media is being removed and is no longer available for use.</para></summary>
		public event EventPlugin.EventValue<Classes.Device.Device> DeviceRemovePending
		{
			add
			{
				OnDeviceRemovePending += value;
				DeviceChangedEnabled = true;
			}
			remove
			{
				OnDeviceRemovePending -= value;
				DeviceChangedEnabled = false;
			}
		}
		#endregion
		#region MediaInserted
		internal static EventPlugin.EventValue<Classes.Device.Device> OnMediaInserted;
		/// <summary><para>A piece of media (CD, DVD, ...) has been inserted and becomes available</para></summary>
		public event EventPlugin.EventValue<Classes.Device.Device> MediaInserted
		{
			add
			{
				OnMediaInserted += value;
				DeviceChangedEnabled = true;
			}
			remove
			{
				OnMediaInserted -= value;
				DeviceChangedEnabled = false;
			}
		}
		#endregion
		#region NetworkVolumeArrived
		internal static EventPlugin.EventValue<Classes.Device.Device> OnNetworkVolumeArrived;
		/// <summary><para>A piece of media (CD, DVD, ...) has been inserted and becomes available</para></summary>
		public event EventPlugin.EventValue<Classes.Device.Device> NetworkVolumeArrived
		{
			add
			{
				OnNetworkVolumeArrived += value;
				DeviceChangedEnabled = true;
			}
			remove
			{
				OnNetworkVolumeArrived -= value;
				DeviceChangedEnabled = false;
			}
		}
		#endregion
		#endregion
		#endregion

		#region Constructor & Destructor
		/// <summary>
		/// <para>Creates an instance of this class</para>
		/// </summary>
		/// <param name="main"></param>
		public Device(Main main)
		{
			Main = main;
		}

		/// <summary>
		/// <para>Disables all SystemEvents because they are static</para>
		/// </summary>
		/// <remarks>http://msdn.microsoft.com/en-us/library/microsoft.win32.systemevents.displaysettingschanged%28v=vs.110%29.aspx</remarks>
		~Device()
		{
			if (DeviceChangedEnabled)
			{
				Main.UnregisterEventForMessage(Classes.System.WindowsMessages.WM_DEVICECHANGE);
				Main.MessageReceived -= Main_MessageReceived;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Get all events that are provided</para>
		/// </summary>
		/// <returns></returns>
		public override EventList EventNames()
		{
			EventList list = new EventList();

			EventListRow row = list.NewEventListRow();

			return list;
		}

		/// <summary>
		/// <para>Get the current status of this <see cref="EventPlugin"/></para>
		/// </summary>
		/// <returns></returns>
		public override TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Device");

			TreeNode tnEvents = tnMain.Nodes.Add("Registered events");

			if (DeviceChangedEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("DeviceChanged");
				tnEvent.ToolTipText = "Registered in Win32 message service (WM_DEVICECHANGED)";
			}

			return tnMain;
		}
		#endregion

		#region Event Handler
		internal static void Main_MessageReceived(object sender, EventArgsValue<Message> e)
		{
			if (e.Value.Msg != (int)WindowsMessages.WM_DEVICECHANGE || e.Value.LParam == IntPtr.Zero)
				return;


			DEV_BROADCAST_VOLUME dbcv = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(e.Value.LParam, typeof(DEV_BROADCAST_VOLUME));
			if (dbcv.dbcv_devicetype != (int)DBT.DEVTYP_VOLUME)
				return;		// we were not even allowed to transform to DEV_BROADCAST_VOLUME =) But who cares...

			switch ((DBT)((int)e.Value.WParam))
			{
				#region case DBT.DEVICEARRIVLE:
				case DBT.DEVICEARRIVAL:
					StorageDisk device = StorageDisk.FromUnitMask(dbcv.dbcv_unitmask);
					((List<StorageDisk>)oldStaticValues[EventType.DeviceRemoved]).Add(device);
					if (dbcv.dbcv_flags == 1 /*DBFT_MEDIA*/)
					{
						if (OnMediaInserted != null)
							OnMediaInserted(null, new EventArgsValue<Classes.Device.Device>(device));
					}
					else if (dbcv.dbcv_flags == 2 /*DBFT_NET*/)
					{
						if (OnNetworkVolumeArrived != null)
							OnNetworkVolumeArrived(null, new EventArgsValue<Classes.Device.Device>(device));
					}
					else
					{
						if (OnDeviceArrived != null)
							OnDeviceArrived(null, new EventArgsValue<Classes.Device.Device>(device));
					}
					break;
				#endregion
				#region case DBT.DEVICEQUERYREMOVE:
				case DBT.DEVICEQUERYREMOVE:
					throw new NotImplementedException("\"Device Query remove\" is not implemented");
				#endregion
				#region case DBT.DEVICEQUERYREMOVEFAILED:
				case DBT.DEVICEQUERYREMOVEFAILED:
					throw new NotImplementedException("\"Device Query remove failed\" is not implemented");
				#endregion
				#region case DBT.DEVICEREMOVECOMPLETE:
				case DBT.DEVICEREMOVECOMPLETE:
					if (OnDeviceRemoved == null)
						break;
					char removedDiskLetter = StorageDisk.FirstDriveFromMask(dbcv.dbcv_unitmask);
					List<StorageDisk> oldDisks = (List<StorageDisk>)oldStaticValues[EventType.DeviceRemoved];
					foreach (StorageDisk oldDisk in oldDisks)
					{
						if (oldDisk.DriveLetters.Contains(removedDiskLetter))
						{
							OnDeviceRemoved(null, new EventArgsValue<Classes.Device.Device>(oldDisk));
							oldDisks.Remove(oldDisk);
							break;
						}
					}
					break;
				#endregion
			}
		}
		#endregion
	}
}