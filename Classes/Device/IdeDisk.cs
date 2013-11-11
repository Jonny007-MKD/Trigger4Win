using System.Management;

namespace Tasker.Classes.Device
{
	public class IdeDisk : StorageDisk
	{
		/// <summary>
		/// <para>Initialize a new instance with the given values.</para>
		/// </summary>
		/// <param name="DriveLetter">The Windows drive letter assigned to this device.</param>
		public IdeDisk(ManagementObject mo) : base(mo)
		{
			this.Type = DeviceType.IDE;
		}
	}
}
