using System.Management;

namespace Trigger.Classes.Device
{
	public class UsbDisk : StorageDisk
	{
		/// <summary>
		/// <para>Initialize a new instance with the given values.</para>
		/// </summary>
		/// <param name="DriveLetter">The Windows drive letter assigned to this device.</param>
		public UsbDisk(ManagementObject mo) : base(mo)
		{
			this.Type = DeviceType.USB;
		}
	}
}
