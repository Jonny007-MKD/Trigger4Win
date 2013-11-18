using System;
using System.Net;

namespace Trigger //.Classes.Extensions
{
	public static class IPAddressExtensions
	{
		/// <summary>
		/// <para>Gets the subnet mask out of the specified <paramref name="ipAddress"/></para>
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <returns>A subnet mask</returns>
		public static IPAddress GetSubnet(this IPAddress ipAddress)
		{
			byte firstByte = ipAddress.GetAddressBytes()[0];
			if (firstByte <= 127)		// 0.0.0.0		to 127.255.255.255	/8	Class A
				return IPAddress.Parse("255.0.0.0");
			if (firstByte <= 191)		// 128.0.0.0	to 191.255.255.255	/16	Class B
				return IPAddress.Parse("255.255.0.0");
			if (firstByte <= 223)		// 192.0.0.0	to 223.255.255.255	/24	Class C
				return IPAddress.Parse("255.255.255.0");
			else						// 224.0.0.0	to 255.255.255.255	undefined	Classes D & E
				return IPAddress.Parse("0.0.0.0");
		}
	}
}
