using System;
using System.Management;

namespace Trigger.Classes.Device
{
	/// <summary>
	/// <para>The Win32_CDROMDrive WMI class represents a CD-ROM drive on a computer system running Windows. Be aware that the name of the drive does not correspond to the logical drive letter assigned to the device.</para>
	/// </summary>
	public class CdRomDrive : StorageDisk
	{
		#region Enums
		public enum Availabilities : ushort
		{
			Other = 0x1,
			Unknown = 0x2,
			/// <summary><para>Running / Full power</para></summary>
			Running_FullPower = 0x3,
			Warning = 0x4,
			InTest = 0x5,
			NotApplicable = 0x6,
			PowerOff = 0x7,
			OffLine = 0x8,
			OffDuty = 0x9,
			Degraded = 0xA,
			NotInstalled = 0xB,
			InstallError = 0xC,
			/// <summary><para>The device is known to be in a power save mode, but its exact status is unknown.</para></summary>
			PowerSave_Unknown = 0xD,
			/// <summary><para>The device is in a power save state but still functioning, and may exhibit degraded performance.</para></summary>
			PowerSave_LowPowerMode = 0xE,
			/// <summary><para>The device is not functioning, but could be brought to full power quickly.</para></summary>
			PowerSave_Standby = 0xF,
			PowerCycle = 0x10,
			/// <summary><para>The device is in a warning state, though also in a power save mode.</para></summary>
			PowerSave_Warning = 0x11,
		}

		public enum Capabilitiy : ushort
		{
			Unknown = 0,
			Other = 1,
			SequentialAccess = 2,
			RandomAccess = 3,
			SupportsWriting = 4,
			Encryption = 5,
			Compression = 6,
			SupportsRemovableMedia = 7,
			ManualCleaning = 8,
			AutomaticCleaning = 9,
			SMARTNotification = 10,
			SupportsDualSidedMedia = 11,
			PredismountEjectNotRequired = 12,
		}

		public enum ConfigManagerErrorCodes : uint
		{
			///<summary><param>Device is working properly.</param></summary>
			OK = 0x0,
			///<summary><param>Device is not configured correctly.</param></summary>
			Misconfigured = 0x1,
			///<summary><param>Windows cannot load the driver for this device.</param></summary>
			DriversNotLoaded = 0x2,
			///<summary><param>Driver for this device might be corrupted, or the system may be low on memory or other resources.</param></summary>
			CorruptDrivers = 0x3,
			///<summary><param>Device is not working properly. One of its drivers or the registry might be corrupted.</param></summary>
			DriverOrRegistryCorrupted = 0x4,
			///<summary><param>Driver for the device requires a resource that Windows cannot manage.</param></summary>
			UnmanagableResourceRequired = 0x5,
			///<summary><param>Boot configuration for the device conflicts with other devices.</param></summary>
			BootConfigConflict = 0x6,
			///<summary><param>Cannot filter.</param></summary>
			Unfilterable = 0x7,
			///<summary><param>Driver loader for the device is missing.</param></summary>
			DriverLoaderMissing = 0x8,
			///<summary><param>Device is not working properly. The controlling firmware is incorrectly reporting the resources for the device.</param></summary>
			FirmwareError = 0x9,
			///<summary><param>Device cannot start.</param></summary>
			CannotStart = 0xA,
			///<summary><param>Device failed.</param></summary>
			Failed = 0xB,
			///<summary><param>Device cannot find enough free resources to use.</param></summary>
			NoMemory = 0xC,
			///<summary><param>Windows cannot verify the device's resources.</param></summary>
			VerifyResourcesFailed = 0xD,
			///<summary><param>Device cannot work properly until the computer is restarted.</param></summary>
			RestartRequired = 0xE,
			///<summary><param>Device is not working properly due to a possible re-enumeration problem.</param></summary>
			ReEnumerationProblem = 0xF,
			///<summary><param>Windows cannot identify all of the resources that the device uses.</param></summary>
			NotAllResourcesIdentified = 0x10,
			///<summary><param>Device is requesting an unknown resource type.</param></summary>
			UnknownResourceRequested = 0x11,
			///<summary><param>Device drivers must be reinstalled.</param></summary>
			ReinstallDriver = 0x12,
			///<summary><param>Failure using the VxD loader.</param></summary>
			VxDLoaderFailed = 0x13,
			///<summary><param>Registry might be corrupted.</param></summary>
			RegistryCorrupted = 0x14,
			///<summary><param>System failure. If changing the device driver is ineffective, see the hardware documentation. Windows is removing the device.</param></summary>
			SystemFailure = 0x15,
			///<summary><param>Device is disabled.</param></summary>
			DeviceDisabled = 0x16,
			///<summary><param>System failure. If changing the device driver is ineffective, see the hardware documentation.</param></summary>
			SystemFailure2 = 0x17,
			///<summary><param>Device is not present, not working properly, or does not have all of its drivers installed.</param></summary>
			NotPresent = 0x18,
			///<summary><param>Windows is still setting up the device.</param></summary>
			SettingUp = 0x19,
			///<summary><param>Windows is still setting up the device.</param></summary>
			SettingUp2 = 0x1A,
			///<summary><param>Device does not have valid log configuration.</param></summary>
			InvalidLogConfig = 0x1B,
			///<summary><param>Device drivers are not installed.</param></summary>
			NoDrivers = 0x1C,
			///<summary><param>Device is disabled. The device firmware did not provide the required resources.</param></summary>
			FirmwareResourceError = 0x1D,
			///<summary><param>Device is using an IRQ resource that another device is using.</param></summary>
			IrqResourceConflict = 0x1E,
			///<summary><param>Device is not working properly. Windows cannot load the required device drivers.</param></summary>
			DriversNotLoaded2 = 0x1F,
		}
		#endregion
		#region Properties
		/// <summary><para>Availabilities and status of the device.</para></summary>
		public Availabilities Availability
		{
			get;
			internal set;
		}
		/// <summary><para>Array of capabilities of the media access device.</para></summary>
		public ushort[] Capabilities
		{
			get;
			internal set;
		}
		/// <summary><para>Short description of the object—a one-line string.</para></summary>
		public string Caption
		{
			get;
			internal set;
		}
		/// <summary><para>Windows Configuration Manager error code.</para></summary>
		public ConfigManagerErrorCodes ConfigManagerErrorCode
		{
			get;
			internal set;
		}
		/// <summary>Manufacturer of the Windows CD-ROM drive.</summary>
		public string Manufacturer
		{
			get;
			internal set;
		}
		/// <summary><para>Maximum size, in kilobytes, of media supported by this device.</para></summary>
		public ulong MaxMediaSize
		{
			get;
			internal set;
		}
		/// <summary><para>If True, a CD-ROM is in the drive.</para></summary>
		public bool MediaLoaded
		{
			get;
			internal set;
		}
		/// <summary>
		/// <para>Type of media that can be used or accessed by this device.</para>
		/// </summary>
		/// <example>CdRomOnly, CdRomWrite, DVDRomOnly, DVDRomWrite</example>
		public string MediaType
		{
			get;
			internal set;
		}
		/// <summary>
		/// <para>Transfer rate of the CD-ROM drive. A value of -1 indicates that the rate cannot be determined. When this happens, the CD is not in the drive. (kB/s)</para>
		/// </summary>
		public double TransferRate
		{
			get;
			internal set;
		}
		#endregion

		#region Constructors
		/// <summary>
		/// <para>Initialize a new instance with the given values.</para>
		/// </summary>
		/// <param name="DriveLetter">The Windows drive letter assigned to this device.</param>
		public CdRomDrive(ManagementObject mo) : base(mo["DeviceId"].ToString(), mo["Name"].ToString(), mo["Caption"].ToString())
		{
			this.Type = DeviceType.CD;
			this.Availability = (Availabilities)mo["Availability"];
			this.Capabilities = (ushort[])mo["Capabilities"];
			this.Caption = mo["Caption"].ToString();
			this.ConfigManagerErrorCode = (ConfigManagerErrorCodes)mo["ConfigManagerErrorCode"];
			this.Manufacturer = mo["Manufacturer"].ToString();
			this.MediaLoaded = Convert.ToBoolean(mo["MediaLoaded"]);
			this.MediaType = mo["MediaType"].ToString();
			this.TransferRate = (double)mo["TransferRate"];
		}
		#endregion
	}
}
