using System;
using System.Management;
using System.Text;

namespace Trigger.Classes.Device
{
	/// <summary>
	/// <para>A partition of a disk</para>
	/// </summary>
	public class Partition
	{
		#region Properties
		/// <summary><para>The name of the <see cref="Device"/></para></summary>
		public string Name
		{
			get;
			internal set;
		}

		/// <summary><para>Gets the available free space on the disk, specified in bytes.</para></summary>
		public ulong FreeSpace
		{
			get;
			internal set;
		}

		/// <summary><para>Gets the name of this disk. This is the Windows identifier, drive letter.</para></summary>
		public char DriveLetter
		{
			get;
			internal set;
		}

		/// <summary><para>Gets the total size of the disk, specified in bytes.</para></summary>
		public ulong Size
		{
			get;
			internal set;
		}
		#endregion

		#region Constructors
		/// <summary>
		/// </summary>
		/// <param name="mo"></param>
		public Partition(ManagementObject mo)
		{
			if (mo["VolumeName"] != null)
				this.Name = mo["VolumeName"].ToString();
			this.DriveLetter = mo["DeviceID"].ToString()[0];
			if (mo["FreeSpace"] != null)
				this.FreeSpace = Convert.ToUInt64(mo["FreeSpace"]);
			if (mo["Size"] != null)
				this.Size = Convert.ToUInt64(mo["Size"]);
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Pretty print the disk.</para>
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(this.DriveLetter);
			builder.Append(": ");
			builder.Append(this.Name);
			builder.Append(" (");
			builder.Append(StorageDisk.FormatByte(this.FreeSpace));
			builder.Append(" free of ");
			builder.Append(StorageDisk.FormatByte(this.Size));
			builder.Append(")");
			return builder.ToString();
		}
		#endregion
	}
}
