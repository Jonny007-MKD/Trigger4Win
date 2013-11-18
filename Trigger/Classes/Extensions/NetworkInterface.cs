using System;
using System.Net.NetworkInformation;
using Trigger.Classes.WMI;

namespace Trigger //.Classes.Extensions
{
	public static class NetworkInterfaceExtension
	{
		public static NetworkAdapter GetNetworkAdapter(this NetworkInterface ni)
		{
			NetworkAdapter.NetworkAdapterCollection adapters = NetworkAdapter.GetInstances("GUID = \"" + ni.Id + "\"");
			if (adapters == null || adapters.Count == 0)
				return null;
			return adapters[0];
		}
	}
}
