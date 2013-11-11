using System.Collections.Generic;
using Tasker.Classes.Device;

namespace Tasker.Status
{
	public static class Device
	{
		/// <summary>
		/// <para>Gets all available disks on the system</para>
		/// </summary>
		public static List<StorageDisk> AvailableDisks
		{
			get
			{
				return StorageDisk.GetAvailableDisks();
			}
		}
	}
}
