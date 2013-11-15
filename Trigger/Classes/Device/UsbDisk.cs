using System.Management;

namespace Trigger.Classes.Device
{
	/// <summary>
	/// <para>A USB stick/disk</para>
	/// </summary>
	public class UsbDisk : StorageDisk
	{
		/// <summary>
		/// <para>Initialize a new instance with the given values.</para>
		/// </summary>
		/// <param name="mo"></param>
		public UsbDisk(ManagementObject mo) : base(mo)
		{
			this.Type = DeviceType.USB;
		}
	}
}
