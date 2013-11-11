using System;
using System.Runtime.InteropServices;

namespace Trigger.Classes.System
{
	/// <summary>
	/// <para>Sent with a power setting event (WM_POWERBROADCAST / PBT_POWERSETTINGSCHANGE) and contains data about the specific change.</para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PowerBroadcast_Setting
	{
		/// <summary><para>Indicates the power setting for which this notification is being delivered.</para></summary>
		public Guid PowerSetting;
		/// <summary><para>The size in bytes of the data in the Data member.</para></summary>
		public uint DataLength;
		/// <summary><para>The new value of the power setting.</para></summary>
		public byte Data;
	}

	/// <summary>
	/// <para>Power Management Events</para>
	/// </summary>
	public enum PBT : int
	{
		/// <summary>
		/// <para>[PBT_APMQUERYSUSPEND is available for use in Windows XP. Support for this event was removed in Windows Vista. Use SetThreadExecutionState instead.]</para>
		/// <para>Requests permission to suspend the computer. An application that grants permission should carry out preparations for the suspension before returning.</para>
		/// <para>lParam: The action flags. If bit 0 is 1, the application can prompt the user for directions on how to prepare for the suspension; otherwise, the application must prepare without user interaction. All other bit values are reserved.</para>
		/// </summary>
		/// <remarks>
		/// <para>An application should process this event as quickly as possible. The application can prompt the user for directions on how to prepare for suspension only if bit 0 in the Flags parameter is set. However, if this message is issued because the user is closing the laptop lid, it will not be possible to prompt the user. Applications should respect that the user expects a certain behavior when they close the laptop lid or press the power button and allow the transition to succeed.</para>
		/// <para>The system allows approximately 20 seconds for an application to remove the WM_POWERBROADCAST message that is sending the PBT_APMQUERYSUSPEND event from the application's message queue. If an application does not remove the message from its queue in less then 20 seconds, the system will assume that the application is in a non-responsive state, and that the application agrees to the sleep request. Applications that do not process their message queues may have their operations interrupted. After it removes the message from the message queue, an application can take as much time as needed to perform any required operations before entering the sleep state. Any operations that could take longer then 20 seconds should be performed at this time, since the system allows only 20 seconds for operations to complete during PBT_APMSUSPEND processing.</para>
		/// </remarks>
		APMQUERYSUSPEND = 0x0000,
		/// <summary>
		/// <para></para>
		/// </summary>
		APMQUERYSTANDBY = 0x0001,
		/// <summary>
		/// <para>[PBT_APMQUERYSUSPENDFAILED is available for use in Windows XP. Support for this event was removed in Windows Vista. Use SetThreadExecutionState instead.]</para>
		/// <para>Notifies applications that permission to suspend the computer was denied. This event is broadcast if any application or driver returned BROADCAST_QUERY_DENY to a previous PBT_APMQUERYSUSPEND event.</para>
		/// <para>lParam: Reserved, must be zero.</para>
		/// </summary>
		/// <remarks>
		/// <para>Applications typically respond to this event by resuming normal operation.</para>
		/// </remarks>
		APMQUERYSUSPENDFAILED = 0x0002,
		/// <summary>
		/// <para></para>
		/// </summary>
		APMQUERYSTANDBYFAILED = 0x0003,
		/// <summary>
		/// <para>Notifies applications that the computer is about to enter a suspended state. This event is typically broadcast when all applications and installable drivers have returned TRUE to a previous PBT_APMQUERYSUSPEND event.</para>
		/// <para>lParam: Reserved, must be zero.</para>
		/// </summary>
		/// <remarks>
		/// <para>An application should process this event by completing all tasks necessary to save data. This event may also be broadcast, without a prior PBT_APMQUERYSUSPEND event, if an application or device driver uses the SetSystemPowerState function to force suspension.</para>
		/// <para>The system allows approximately two seconds for an application to handle this notification. If an application is still performing operations after its time allotment has expired, the system may interrupt the application.</para>
		/// </remarks>
		APMSUSPEND = 0x0004,
		/// <summary>
		/// <para></para>
		/// </summary>
		APMSTANDBY = 0x0005,
		/// <summary>
		/// <para>[PBT_APMRESUMECRITICAL is available for use in Windows XP. Support for this event was removed in Windows Vista. Use PBT_APMRESUMEAUTOMATIC instead.]</para>
		/// <para>Notifies applications that the system has resumed operation. This event can indicate that some or all applications did not receive a PBT_APMSUSPEND event. For example, this event can be broadcast after a critical suspension caused by a failing battery.</para>
		/// <para>lParam: Reserved, must be zero.</para>
		/// </summary>
		/// <remarks>
		/// <para>Because a critical suspension occurs without prior notification, resources and data previously available may not be present when the application receives this event. The application should attempt to restore its state to the best of its ability. While in a critical suspension, the system maintains the state of the DRAM and local hard disks, but may not maintain net connections. An application may need to take action with respect to files that were open on the network before critical suspension.</para>
		/// </remarks>
		APMRESUMECRITICAL = 0x0006,
		/// <summary>
		/// <para>Notifies applications that the system has resumed operation after being suspended.</para>
		/// <para>lParam: Reserved, must be zero.</para>
		/// </summary>
		/// <remarks>
		/// <para>An application can receive this event only if it received the PBT_APMSUSPEND event before the computer was suspended. Otherwise, the application will receive a PBT_APMRESUMECRITICAL event.</para>
		/// <para>If the system wakes due to user activity (such as pressing the power button) or if the system detects user interaction at the physical console (such as mouse or keyboard input) after waking unattended, the system first broadcasts the PBT_APMRESUMEAUTOMATIC event, then it broadcasts the PBT_APMRESUMESUSPEND event. In addition, the system turns on the display. Your application should reopen files that it closed when the system entered sleep and prepare for user input.</para>
		/// <para>If the system wakes due to an external wake signal (remote wake), the system broadcasts only the PBT_APMRESUMEAUTOMATIC event. The PBT_APMRESUMESUSPEND event is not sent.</para>
		/// </remarks>
		APMRESUMESUSPEND = 0x0007,
		/// <summary>
		/// <para></para>
		/// </summary>
		APMRESUMESTANDBY = 0x0008,
		/// <summary>
		/// <para>[<see cref="PBT_APMBATTERYLOW"/> is available for use in Windows XP. Support for this event was removed in Windows Vista. Use <see cref="PBT_APMPOWERSTATUSCHANGE"/> instead.]</para>
		/// <para>lParam: Reserved, must be zero</para>
		/// </summary>
		/// <remarks>
		/// <para>This event is broadcast when a system's APM BIOS signals an APM battery low notification. Because some APM BIOS implementations do not provide notifications when batteries are low, this event may never be broadcast on some computers.</para>
		/// </remarks>
		APMBATTERYLOW = 0x0009,
		/// <summary>
		/// <para>Notifies applications of a change in the power status of the computer, such as a switch from battery power to A/C. The system also broadcasts this event when remaining battery power slips below the threshold specified by the user or if the battery power changes by a specified percentage.</para>
		/// <para>lParam: Reserved, must be zero.</para>
		/// </summary>
		/// <remarks>
		/// <para>An application should process this event by calling the GetSystemPowerStatus function to retrieve the current power status of the computer. In particular, the application should check the ACLineStatus, BatteryFlag, BatteryLifeTime, and BatteryLifePercent members of the SYSTEM_POWER_STATUS structure for any changes. This event can occur when battery life drops to less than 5 minutes, or when the percentage of battery life drops below 10 percent, or if the battery life changes by 3 percent.</para>
		/// </remarks>
		APMPOWERSTATUSCHANGE = 0x000A,
		/// <summary>
		/// <para>[<see cref="PBT_APMOEMEVENT"/> is available for use in Windows XP. Support for this event was removed in Windows Vista.]</para>
		/// <para>lParam: The OEM-defined event code that was signaled by the system's APM BIOS. OEM event codes are in the range 0200h - 02FFh.</para>
		/// </summary>
		/// <remarks>
		/// <para>Because not all APM BIOS implementations provide OEM event notifications, this event may never be broadcast on some computers.</para>
		/// </remarks>
		APMOEMEVENT = 0x000B,
		/// <summary>
		/// <para>Notifies applications that the system is resuming from sleep or hibernation. This event is delivered every time the system resumes and does not indicate whether a user is present.</para>
		/// <para>lParam: Reserved, must be zero.</para>
		/// </summary>
		/// <remarks>
		/// <para>If the system detects any user activity after broadcasting PBT_APMRESUMEAUTOMATIC, it will broadcast a PBT_APMRESUMESUSPEND event to let applications know they can resume full interaction with the user.</para>
		/// </remarks>
		APMRESUMEAUTOMATIC = 0x0012,
		/// <summary>
		/// <para>Power setting change event</para>
		/// <para>lParam: Pointer to a <see cref="POWERBROADCAST_SETTING"/> structure.</para>
		/// </summary>
		POWERSETTINGCHANGE = 0x8013,
	}
	/// <summary>
	/// <para>Device Broadcast Events</para>
	/// </summary>
	public enum DBT : int
	{
		///<summary><para>A request to change the current configuration (dock or undock) has been canceled.</para></summary>
		CONFIGCHANGECANCELED = 0x0019,
		///<summary><para>The current configuration has changed, due to a dock or undock.</para></summary>
		CONFIGCHANGED = 0x0018,
		///<summary><para>A custom event has occurred.</para></summary>
		CUSTOMEVENT = 0x8006,
		///<summary><para>A device or piece of media has been inserted and is now available.</para></summary>
		DEVICEARRIVAL = 0x8000,
		///<summary><para>Permission is requested to remove a device or piece of media. Any application can deny this request and cancel the removal.</para></summary>
		DEVICEQUERYREMOVE = 0x8001,
		///<summary><para>A request to remove a device or piece of media has been canceled.</para></summary>
		DEVICEQUERYREMOVEFAILED = 0x8002,
		///<summary><para>A device or piece of media has been removed.</para></summary>
		DEVICEREMOVECOMPLETE = 0x8004,
		///<summary><para>A device or piece of media is about to be removed. Cannot be denied.</para></summary>
		DEVICEREMOVEPENDING = 0x8003,
		///<summary><para>A device-specific event has occurred.</para></summary>
		DEVICETYPESPECIFIC = 0x8005,
		///<summary><para>A device has been added to or removed from the system.</para></summary>
		DEVNODES_CHANGED = 0x0007,
		///<summary><para>Permission is requested to change the current configuration (dock or undock).</para></summary>
		QUERYCHANGECONFIG = 0x0017,
		///<summary><para>The meaning of this message is user-defined.</para></summary>
		USERDEFINED = 0xFFFF,

		/// <summary><para>Class of devices. The <see cref="DEV_BROADCAST_HDR" /> structure is a <see cref="DEV_BROADCAST_DEVICEINTERFACE" /> structure.</para></summary>
		DEVTYP_DEVICEINTERFACE = 0x05,
		/// <summary><para>File system handle. The <see cref="DEV_BROADCAST_HDR" /> structure is a <see cref="DEV_BROADCAST_HANDLE"/> structure.</para></summary>
		DEVTYP_HANDLE = 0x06,
		/// <summary><para>OEM- or IHV-defined device type. The <see cref="DEV_BROADCAST_HDR" /> structure is a <see cref="DEV_BROADCAST_OEM"/> structure.</para></summary>
		DEVTYP_OEM = 0x00,
		/// <summary><para>Port device (serial or parallel). The <see cref="DEV_BROADCAST_HDR" /> structure is a <see cref="DEV_BROADCAST_PORT"/> structure.</para></summary>
		DEVTYP_PORT = 0x03,
		/// <summary><para>Logical dbcv. The <see cref="DEV_BROADCAST_HDR" /> structure is a <see cref="DEV_BROADCAST_VOLUME"/> structure.</para></summary>
		DEVTYP_VOLUME = 0x02,
	}

	/// <summary>
	/// <para>Serves as a standard header for information related to a device event reported through the WM_DEVICECHANGE message.</para>
	/// <para>The members of the <see cref="DEV_BROADCAST_HDR"/> structure are contained in each device management structure. To determine which structure you have received through WM_DEVICECHANGE, treat the structure as a <see cref="DEV_BROADCAST_HDR"/> structure and check its dbch_devicetype member.</para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DEV_BROADCAST_HDR
	{
		/// <summary>
		/// <para>The size of this structure, in bytes.</para>
		/// <para>If this is a user-defined event, this member must be the size of this header, plus the size of the variable-length data in the DEV_BROADCAST_USERDEFINED structure.</para>
		/// </summary>
		public int dbch_size;
		/// <summary>
		/// <para>The device type, which determines the event-specific information that follows the first three members.</para>
		/// </summary>
		/// <exexample>
		/// <para>DBT_DEVTYP_DEVICEINTERFACE (0x5): Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.</para>
		/// <para>DBT_DEVTYP_HANDLE (0x6): File system handle. This structure is a DEV_BROADCAST_HANDLE structure.</para>
		/// <para>DBT_DEVTYP_OEM (0x0): OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.</para>
		/// <para>DBT_DEVTYP_PORT (0x3): Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.</para>
		/// <para>DBT_DEVTYP_VOLUME (0x2): Logical dbcv. This structure is a DEV_BROADCAST_VOLUME structure.</para>
		/// </exexample>
		public int dbch_devicetype;
		/// <summary><para>Reserved; do not use.</para></summary>
		public int dbch_reserved;
	}

	/// <summary>
	/// <para></para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DEV_BROADCAST_VOLUME
	{
		/// <summary>
		/// <para>The size of this structure, in bytes.</para>
		/// <para>If this is a user-defined event, this member must be the size of this header, plus the size of the variable-length data in the DEV_BROADCAST_USERDEFINED structure.</para>
		/// </summary>
		public int dbcv_size;
		/// <summary>
		/// <para>The device type, which determines the event-specific information that follows the first three members.</para>
		/// </summary>
		/// <exexample>
		/// <para>DBT_DEVTYP_DEVICEINTERFACE (0x5): Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.</para>
		/// <para>DBT_DEVTYP_HANDLE (0x6): File system handle. This structure is a DEV_BROADCAST_HANDLE structure.</para>
		/// <para>DBT_DEVTYP_OEM (0x0): OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.</para>
		/// <para>DBT_DEVTYP_PORT (0x3): Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.</para>
		/// <para>DBT_DEVTYP_VOLUME (0x2): Logical dbcv. This structure is a DEV_BROADCAST_VOLUME structure.</para>
		/// </exexample>
		public int dbcv_devicetype;
		/// <summary><para>Reserved; do not use.</para></summary>
		public int dbcv_reserved;
		/// <summary><para>Bit 0=A, bit 1=B, and so on (bitmask)</para></summary>
		public int dbcv_unitmask;
		/// <summary><para>DBTF_MEDIA=0x01, DBTF_NET=0x02 (bitmask)</para></summary>
		public short dbcv_flags;
	}

	/// <summary>
	/// <para>Contains information about a OEM-defined device type.</para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DEV_BROADCAST_OEM
	{
		/// <summary>
		/// <para>The size of this structure, in bytes.</para>
		/// <para>If this is a user-defined event, this member must be the size of this header, plus the size of the variable-length data in the DEV_BROADCAST_USERDEFINED structure.</para>
		/// </summary>
		public int dbco_size;
		/// <summary>
		/// <para>The device type, which determines the event-specific information that follows the first three members.</para>
		/// </summary>
		/// <exexample>
		/// <para>DBT_DEVTYP_DEVICEINTERFACE (0x5): Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.</para>
		/// <para>DBT_DEVTYP_HANDLE (0x6): File system handle. This structure is a DEV_BROADCAST_HANDLE structure.</para>
		/// <para>DBT_DEVTYP_OEM (0x0): OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.</para>
		/// <para>DBT_DEVTYP_PORT (0x3): Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.</para>
		/// <para>DBT_DEVTYP_VOLUME (0x2): Logical dbcv. This structure is a DEV_BROADCAST_VOLUME structure.</para>
		/// </exexample>
		public int dbco_devicetype;
		/// <summary><para>Reserved; do not use.</para></summary>
		public int dbco_reserved;
		/// <summary><para>The OEM-specific identifier for the device.</para></summary>
		public int dbco_identifier;
		/// <summary><para>The OEM-specific function value. Possible values depend on the device.</para></summary>
		public int dbco_suppfunc;
	}

	/// <summary>
	/// <para>Contains information about a modem, serial, or parallel port.</para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DEV_BROADCAST_PORT
	{
		/// <summary>
		/// <para>The size of this structure, in bytes.</para>
		/// <para>If this is a user-defined event, this member must be the size of this header, plus the size of the variable-length data in the DEV_BROADCAST_USERDEFINED structure.</para>
		/// </summary>
		public int dbcp_size;
		/// <summary>
		/// <para>The device type, which determines the event-specific information that follows the first three members.</para>
		/// </summary>
		/// <exexample>
		/// <para>DBT_DEVTYP_DEVICEINTERFACE (0x5): Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.</para>
		/// <para>DBT_DEVTYP_HANDLE (0x6): File system handle. This structure is a DEV_BROADCAST_HANDLE structure.</para>
		/// <para>DBT_DEVTYP_OEM (0x0): OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.</para>
		/// <para>DBT_DEVTYP_PORT (0x3): Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.</para>
		/// <para>DBT_DEVTYP_VOLUME (0x2): Logical dbcv. This structure is a DEV_BROADCAST_VOLUME structure.</para>
		/// </exexample>
		public int dbcp_devicetype;
		/// <summary><para>Reserved; do not use.</para></summary>
		public int dbcp_reserved;
		/// <summary>
		/// <para>A null-terminated string specifying the friendly name of the port or the device connected to the port. Friendly names are intended to help the user quickly and accurately identify the device — for example, "COM1" and "Standard 28800 bps Modem" are considered friendly names.</para>
		/// </summary>
		public string dbcp_name;
}

	/// <summary>
	/// <para>Contains information about a file system handle.</para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DEV_BROADCAST_HANDLE
	{
		/// <summary>
		/// <para>The size of this structure, in bytes.</para>
		/// <para>If this is a user-defined event, this member must be the size of this header, plus the size of the variable-length data in the DEV_BROADCAST_USERDEFINED structure.</para>
		/// </summary>
		public int dbch_size;
		/// <summary>
		/// <para>The device type, which determines the event-specific information that follows the first three members.</para>
		/// </summary>
		/// <exexample>
		/// <para>DBT_DEVTYP_DEVICEINTERFACE (0x5): Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.</para>
		/// <para>DBT_DEVTYP_HANDLE (0x6): File system handle. This structure is a DEV_BROADCAST_HANDLE structure.</para>
		/// <para>DBT_DEVTYP_OEM (0x0): OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.</para>
		/// <para>DBT_DEVTYP_PORT (0x3): Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.</para>
		/// <para>DBT_DEVTYP_VOLUME (0x2): Logical dbcv. This structure is a DEV_BROADCAST_VOLUME structure.</para>
		/// </exexample>
		public int dbch_devicetype;
		/// <summary><para>Reserved; do not use.</para></summary>
		public int dbch_reserved;
		/// <summary><para>A handle to the device to be checked.</para></summary>
		public IntPtr dbch_handle;
		/// <summary><para>A handle to the device notification. This handle is returned by RegisterDeviceNotification.</para></summary>
		public IntPtr dbch_hdevnotify;
		/// <summary><para>The <see cref="Guid"/> for the custom event. For more information, see Device Events. Valid only for DBT_CUSTOMEVENT.</para></summary>
		public Guid dbch_eventguid;
		/// <summary><para>The offset of an optional string buffer. Valid only for DBT_CUSTOMEVENT.</para></summary>
		public long dbch_nameoffset;
		/// <summary><para>Optional binary data. This member is valid only for DBT_CUSTOMEVENT.</para></summary>
		public byte dbch_data;
		
}

	/// <summary>
	/// <para>Contains information about a class of devices.</para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DEV_BROADCAST_DEVICEINTERFACE
	{
		/// <summary>
		/// <para>The size of this structure, in bytes.</para>
		/// <para>If this is a user-defined event, this member must be the size of this header, plus the size of the variable-length data in the DEV_BROADCAST_USERDEFINED structure.</para>
		/// </summary>
		public int dbcc_size;
		/// <summary>
		/// <para>The device type, which determines the event-specific information that follows the first three members.</para>
		/// </summary>
		/// <exexample>
		/// <para>DBT_DEVTYP_DEVICEINTERFACE (0x5): Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.</para>
		/// <para>DBT_DEVTYP_HANDLE (0x6): File system handle. This structure is a DEV_BROADCAST_HANDLE structure.</para>
		/// <para>DBT_DEVTYP_OEM (0x0): OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure.</para>
		/// <para>DBT_DEVTYP_PORT (0x3): Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.</para>
		/// <para>DBT_DEVTYP_VOLUME (0x2): Logical dbcv. This structure is a DEV_BROADCAST_VOLUME structure.</para>
		/// </exexample>
		public int dbcc_devicetype;
		/// <summary><para>Reserved; do not use.</para></summary>
		public int dbcc_reserved;
		/// <summary><para>The <see cref="Guid"/> for the interface device class.</para></summary>
		public Guid dbcc_classguid;
		/// <summary><para>A null-terminated string that specifies the name of the device.</para></summary>
		public string dbcc_name;
	}
}
