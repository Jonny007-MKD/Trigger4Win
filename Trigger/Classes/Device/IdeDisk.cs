using System.Management;

namespace Trigger.Classes.Device
{
	/// <summary>
	/// <para>An IDE disk. This can be a hard drive or solid stade disk</para>
	/// </summary>
	public class IdeDisk : StorageDisk
	{
		/// <summary>
		/// <para>Initialize a new instance with the given values.</para>
		/// </summary>
		/// <param name="mo"></param>
		public IdeDisk(ManagementObject mo) : base(mo)
		{
			this.Type = DeviceType.IDE;
		}
	}
}
