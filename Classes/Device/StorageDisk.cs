using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Tasker.Classes.Device
{
	public class StorageDisk : Device
	{
		#region Properties
		internal const uint KB = 1024;
		internal const uint MB = KB * 1024;
		internal const uint GB = MB * 1024;

		public List<Partition> Partitions = new List<Partition>();

		/// <summary><para>Gets the name of this disk. This is the Windows identifier, drive letter.</para></summary>
		public List<char> DriveLetters
		{
			get
			{
				List<char> letters = new List<char>(Partitions.Count);
				Partitions.ForEach(new Action<Partition>(partition => letters.Add(partition.DriveLetter)));
				return letters;
			}
		}

		/// <summary><para>Gets the total size of the disk, specified in bytes.</para></summary>
		public ulong Size
		{
			get;
			internal set;
		}

		/// <summary><para>Gets the available free space on the disk, specified in bytes.</para></summary>
		public ulong FreeSpace
		{
			get
			{
				ulong sum = 0;
				Partitions.ForEach(new Action<Partition>(partition => sum += partition.FreeSpace));
				return sum;
			}
		}
		#endregion

		#region Constructors
		public StorageDisk(int UnitMask) : base("")
		{
			char letter = FirstDriveFromMask(UnitMask);
			this.Id = "";
		}
		public StorageDisk(ManagementObject mo) : base(mo["DeviceID"].ToString())
		{
			this.Model = mo["Model"].ToString();
			this.Name = mo["Name"].ToString();
			this.Size = Convert.ToUInt64(mo["Size"]);
		}
		public StorageDisk(string Id, string Model, string Name) : base(Id)
		{
			this.Model = Model;
			this.Name = Name;
		}
		#endregion

		#region Methods
		#region Instance
		/// <summary>
		/// <para>Pretty print the disk.</para>
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(this.Type.ToString());
			builder.Append(": ");
			builder.Append(this.Model);
			builder.Append(" (");
			builder.Append(StorageDisk.FormatByte(this.Size));
			builder.Append(" in ");
			builder.Append(this.Partitions.Count);
			builder.Append(" partition");
			if (this.Partitions.Count != 1)
				builder.Append("s");
			builder.Append(")");
			return builder.ToString();
		}

		/// <summary>
		/// <para>Adds a new partition</para>
		/// </summary>
		/// <param name="part"></param>
		public void AddPartition(Partition part)
		{
			this.Partitions.Add(part);
		}

		/// <summary>
		/// <para>Compares this <see cref="StorageDisk"/> with another object</para>
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			StorageDisk that = obj as StorageDisk;
			if (that == null)
				return false;

			return this.Type == that.Type && this.Name == that.Name;
		}

		public override int GetHashCode()
		{
			return this.DriveLetters.GetHashCode() + this.Type.GetHashCode() + this.Name.GetHashCode();
		}
		#endregion

		#region Static
		/// <summary>
		/// <para>Converts the specified amount of <paramref name="bytes"/> and returns a string where it is formatted as a human readable size</para>
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public static string FormatByte(ulong bytes)
		{
			if (bytes < KB)
				return String.Format("{0} Bytes", bytes);
			else if (bytes < MB)
				return String.Format("{0} KB", (bytes / (float)KB).ToString("N"));
			else if (bytes < GB)
				return String.Format("{0} MB", (bytes / (float)MB).ToString("N1"));
			else
				return String.Format("{0} GB", (bytes / (float)GB).ToString("N1"));
		}

		/// <summary>
		/// <para>Finds the first valid drive letter from a mask of drive letters.</para>
		/// </summary>
		/// <param name="unitmask">The mask must be in the format bit 0 = A, bit 1 = B, bit 2 = C, and so on. A valid drive letter is defined when the corresponding bit is set to 1.</param>
		/// <returns>Returns the first drive letter that was found.</returns>
		internal static char FirstDriveFromMask(int unitmask)
		{
			int i;

			for (i = (char)0; i < 26; ++i)
			{
				if ((unitmask & 0x1) == 0x1)
					break;
				unitmask = unitmask >> 1;
			}

			return (char)(i + (int)'A');
		}

		/// <summary>
		/// <para>Creates a new <see cref="StorageDisk"/> from the specified <paramref name="UnitMask"/></para>
		/// </summary>
		/// <param name="UnitMask"></param>
		/// <returns></returns>
		internal static StorageDisk FromUnitMask(int UnitMask)
		{
			char Letter = FirstDriveFromMask(UnitMask);
			StorageDisk sd = null;

			ManagementObjectCollection partitions = new ManagementObjectSearcher(String.Format("ASSOCIATORS OF {{Win32_LogicalDisk.DeviceID='{0}:'}} WHERE AssocClass = Win32_LogicalDiskToPartition", Letter)).Get();
			foreach (ManagementObject partition in partitions)
			{
				ManagementObjectCollection disks = new ManagementObjectSearcher(String.Format("ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{0}'}} WHERE AssocClass = Win32_DiskDriveToDiskPartition", partition["DeviceID"])).Get();
				foreach (ManagementObject disk in disks)
				{
					sd = CreateStorageDiskFromDrive(disk);
					break;
				}
			}
			if (sd == null)
			{
				ManagementObjectCollection cdroms = new ManagementObjectSearcher(String.Format("SELECT * FROM Win32_CdRomDrive WHERE Drive = '{0}:\'", Letter)).Get();
				foreach (ManagementObject cdrom in cdroms)
				{
					sd = new CdRomDrive(cdrom);
					break;
				}
			}

			ManagementObjectCollection volumes = new ManagementObjectSearcher(String.Format("SELECT * FROM Win32_LogicalDisk WHERE DeviceID = '{0}:'", Letter)).Get();
			foreach (ManagementObject volume in volumes)
			{
				sd.AddPartition(new Partition(volume));
				break;
			}
			return sd;
		}

		/// <summary>
		/// <para>Gets all available disks</para>
		/// </summary>
		/// <returns></returns>
		public static List<StorageDisk> GetAvailableDisks()
		{
			return GetAvailableDisks(DeviceType.Any);
		}
		/// <summary>
		/// <para>Gets all available disks of the specified <paramref name="Type"/></para>
		/// </summary>
		/// <param name="Type"></param>
		/// <returns></returns>
		public static List<StorageDisk> GetAvailableDisks(DeviceType Type)
		{
			string query = "SELECT * FROM Win32_DiskDrive";
			if (Type != DeviceType.Any)
				query += " WHERE InterfaceType = '" + Type.ToString() + "'";
			ManagementObjectCollection drives = new ManagementObjectSearcher(query).Get();


			List<StorageDisk> disks = new List<StorageDisk>();

			foreach (ManagementObject drive in drives)	// browse all queried WMI physical disks
			{
				StorageDisk diskDrive = CreateStorageDiskFromDrive(drive);
				disks.Add(diskDrive);

				ManagementObjectCollection partitions = new ManagementObjectSearcher(String.Format("ASSOCIATORS OF {{Win32_DiskDrive.DeviceID='{0}'}} WHERE AssocClass = Win32_DiskDriveToDiskPartition", drive["DeviceID"])).Get();	 // associate physical disks with partitions

				foreach (ManagementObject partition in partitions)
				{
					ManagementObjectCollection logicals = new ManagementObjectSearcher(String.Format("ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{0}'}} WHERE AssocClass = Win32_LogicalDiskToPartition", partition["DeviceID"])).Get();  // associate partitions with logical disks (drive letter volumes)

					foreach (ManagementObject logical in logicals)
					{
						ManagementObjectCollection volumes = new ManagementObjectSearcher(String.Format("SELECT * FROM Win32_LogicalDisk WHERE Name='{0}'", logical["Name"])).Get();
						if (volumes.Count == 0)
							diskDrive.AddPartition(new Partition(logical));
						else
						{
							foreach (ManagementObject volume in volumes)
							{
								diskDrive.AddPartition(new Partition(volume));
								break;
							}
						}
					}
				}
			}

			return disks;
		}

		private static StorageDisk CreateStorageDiskFromDrive(ManagementObject drive)
		{
			switch ((DeviceType)Enum.Parse(typeof(DeviceType), drive["InterfaceType"].ToString()))
			{
				case DeviceType.USB:
					return new UsbDisk(drive);
				case DeviceType.IDE:
					return new IdeDisk(drive);
				default:
					return new StorageDisk(drive);
			}
		}
		#endregion
		#endregion
	}
}
