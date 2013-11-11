using System;
using System.Net;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Management;
using System.Net.NetworkInformation;



namespace Trigger.Actions
{
	class Network
	{
		#region WMI Methods
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapter <see cref="ManagementObject"/> that represents the specified <paramref name="ni"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="ni"></param>
		/// <returns></returns>
		private static ManagementObject getWin32_NetworkAdapter(NetworkInterface ni)
		{
			return getWin32_NetworkAdapter(ni.Id);
		}
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapter <see cref="ManagementObject"/> that has the specified <paramref name="GUID"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns></returns>
		private static ManagementObject getWin32_NetworkAdapter(string GUID)
		{
			ManagementObjectCollection mgmtObjects = new ManagementClass("Win32_NetworkAdapter").GetInstances();

			foreach (ManagementObject mgmtObject in mgmtObjects)
				if (mgmtObject["GUID"] != null && mgmtObject["GUID"].Equals(GUID))
					return mgmtObject;
			return null;
		}
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapterConfiguration <see cref="ManagementObject"/> that belongs to the specified Win32_NetworkAdapter <paramref name="ni"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="ni"></param>
		/// <returns></returns>
		private static ManagementObject getWin32_NetworkAdapterConfiguration(NetworkInterface ni)
		{
			return getWin32_NetworkAdapterConfiguration(ni.Id);
		}
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapterConfiguration <see cref="ManagementObject"/> that belongs to the Win32_NetworkAdapter with the specified <paramref name="GUID"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns></returns>
		private static ManagementObject getWin32_NetworkAdapterConfiguration(string GUID)
		{
			ManagementObject Win32_NetworkAdapter = getWin32_NetworkAdapter(GUID);
			if (Win32_NetworkAdapter == null)
				return null;
			ManagementObject Win32_NetworkAdapterConfiguration;

			ManagementObjectCollection NACs = Win32_NetworkAdapter.GetRelated("Win32_NetworkAdapterConfiguration");
			foreach (ManagementObject NAC in NACs)
				return Win32_NetworkAdapterConfiguration = NAC;
			return null;
		}
		#endregion

		#region Proxy
		[DllImport("wininet.dll")]
		public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
		public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
		public const int INTERNET_OPTION_REFRESH = 37;

		/// <summary>
		/// <para>Disable the global proxy configuration</para>
		/// </summary>
		public static void ProxyDisable()
		{
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", writable: true);
			regKey.SetValue("ProxyEnable", 0);
			ProxyRefresh();
		}
		/// <summary>
		/// <para>Enables the global proxy configuration</para>
		/// </summary>
		/// <param name="proxy">A <see cref="WebProxy"/> with the required Server:Port and ByPass Addresses</param>
		public static void ProxyEnable(WebProxy proxy)
		{
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", writable: true);
			regKey.SetValue("ProxyEnable", 1);
			regKey.SetValue("ProxyServer", proxy.Address.OriginalString);
			regKey.SetValue("ProxyOverride", String.Join<string>(";", proxy.BypassList).Replace("\\", ""));
			
			ProxyRefresh();
		}
		/// <summary>
		/// <para>Causes the OS to refresh the settings, causing IP to really update</para>
		/// </summary>
		private static void ProxyRefresh()
		{
			bool settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
			bool refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
		}
		#endregion

		#region IP Settings (IP, Gateway, DNS, WINS)
		/// <summary>
		/// <para>Gets the subnet mask out of the specified <paramref name="ipAddress"/></para>
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <returns>A subnet mask</returns>
		public static IPAddress GetSubnet(IPAddress ipAddress)
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

		/// <summary>
		/// <para>Sets a new IP Address and its Submask of the local machine</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <param name="ip">The IP Address</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa390383%28v=vs.85%29.aspx</remarks>
		public static uint SetStaticIP(NetworkInterface ni, IPAddress ip)
		{
			ManagementObject NetAdapterConfig = getWin32_NetworkAdapterConfiguration(ni.Id);
			if (NetAdapterConfig == null)
				return 65;
			if ((bool)NetAdapterConfig["IPEnabled"] == false)
				return 84;

			try
			{
				ManagementBaseObject newIP = NetAdapterConfig.GetMethodParameters("EnableStatic");

				newIP["IPAddress"] = new string[] { ip.ToString() };
				newIP["SubnetMask"] = new string[] { GetSubnet(ip).ToString() };

				ManagementBaseObject setIP = NetAdapterConfig.InvokeMethod("EnableStatic", newIP, null);
				return (uint)setIP["ReturnValue"];
				// 0x80041003: Current user does not have permission to perform the action.
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// <para>Enables the configuration of the ip addresses and subnet mask through an DHCP server.</para>
		/// <para>This is the opposite of <see cref="SetStaticIP"/></para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa390378%28v=vs.85%29.aspx</remarks>
		public static uint EnableDHCP(NetworkInterface ni)
		{
			ManagementObject NetAdapterConfig = getWin32_NetworkAdapterConfiguration(ni.Id);
			if (NetAdapterConfig == null)
				return 65;
			if ((bool)NetAdapterConfig["IPEnabled"] == false)
				return 84;

			try
			{
				ManagementBaseObject setDHCP = NetAdapterConfig.InvokeMethod("EnableDHCP", null, null);
				return (uint)setDHCP["ReturnValue"];
				// 0x80041003: Current user does not have permission to perform the action.
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// <para>Set the <see cref="IPAddress"/> of the gateway</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <param name="gateway">The <see cref="IPAddress"/> of the gateway that shall be set</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393301%28v=vs.85%29.aspx</remarks>
		public static uint SetGateway(NetworkInterface ni, IPAddress gateway)
		{
			return SetGateway(ni, new IPAddress[] { gateway }, new ushort[] { 1 });
		}
		/// <summary>
		/// <para>Sets the <see cref="IPAddress"/> of up to 5 Gateways</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <param name="gateways">The <see cref="IPAddress"/> of the gateway that shall be set. No more than 5 IPAdresses are allowed!</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393301%28v=vs.85%29.aspx</remarks>
		public static uint SetGateway(NetworkInterface ni, IPAddress[] gateways)
		{
			if (gateways.Length > 5)
				return 69;

			ushort[] costs = new ushort[gateways.Length];
			for (byte i = 0; i < gateways.Length; i++)
				costs[i] = 1;

			return SetGateway(ni, gateways, costs);
		}
		/// <summary>
		/// <para>Sets the <see cref="IPAddress"/> of up to 5 Gateways</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <param name="gateways">The <see cref="IPAddress"/> of the gateway that shall be set. No more than 5 IPAdresses are allowed!</param>
		/// <param name="costs">The cost metric of the gateway used for route calculation</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393301%28v=vs.85%29.aspx</remarks>
		public static uint SetGateway(NetworkInterface ni, IPAddress[] gateways, ushort[] costs)
		{
			if (gateways.Length > 5)
				return 69;
			if (gateways.Length != costs.Length)
				return 90;

			ManagementObject NetAdapterConfig = getWin32_NetworkAdapterConfiguration(ni.Id);
			if (NetAdapterConfig == null)
				return 65;
			if ((bool)NetAdapterConfig["IPEnabled"] == false)
				return 84;

			string[] gateWays = new string[gateways.Length];
			for (byte i = 0; i < gateways.Length; i++)
				gateWays[i] = gateways[i].ToString();

			try
			{
				ManagementBaseObject newGateway = NetAdapterConfig.GetMethodParameters("SetGateways");

				newGateway["DefaultIPGateway"] = gateWays;
				newGateway["GatewayCostMetric"] = costs;

				ManagementBaseObject setGateway = NetAdapterConfig.InvokeMethod("SetGateways", newGateway, null);
				return (uint)setGateway["ReturnValue"];
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// <para>Resets the DNS server search order</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393295%28v=vs.85%29.aspx</remarks>
		public static uint ResetDNS(NetworkInterface ni)
		{
			ManagementObject NetAdapterConfig = getWin32_NetworkAdapterConfiguration(ni.Id);
			if (NetAdapterConfig == null)
				return 65;
			if ((bool)NetAdapterConfig["IPEnabled"] == false)
				return 84;

			try
			{
				ManagementBaseObject setDNS = NetAdapterConfig.InvokeMethod("SetDNSServerSearchOrder", null, null);
				return (uint)setDNS["ReturnValue"];
			}
			catch (Exception)
			{
				throw;
			}
		}
		/// <summary>
		/// <para>Sets the specified <paramref name="DNS"/> server as default server for DNS requests</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <param name="DNS">The DNS server address</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393295%28v=vs.85%29.aspx</remarks>
		public static uint SetDNS(NetworkInterface ni, IPAddress DNS)
		{
			return SetDNS(ni, new IPAddress[] { DNS });
		}
		/// <summary>
		/// <para>Sets the specified <paramref name="DNS"/> servers as default servers for DNS requests</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <param name="DNS">The DNS server addresses in descending? priority</param>
		/// <returns><para>An WMI Error Constant or some other error type</para>
		/// <para>0x80041003: Current user does not have permission to perform the action.</para>
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8*******</para>
		/// <para>or net helpmsg %ret% otherwise</para>
		/// </returns>
		/// <remarks>http://msdn.microsoft.com/en-us/library/aa393295%28v=vs.85%29.aspx</remarks>
		public static uint SetDNS(NetworkInterface ni, IPAddress[] DNS)
		{
			ManagementObject NetAdapterConfig = getWin32_NetworkAdapterConfiguration(ni.Id);
			if (NetAdapterConfig == null)
				return 65;
			if ((bool)NetAdapterConfig["IPEnabled"] == false)
				return 84;

			string[] dns = new string[DNS.Length];
			for (int i = 0; i < DNS.Length; i++)
				dns[i] = DNS[i].ToString();

			try
			{
				ManagementBaseObject newDNS = NetAdapterConfig.GetMethodParameters("SetDNSServerSearchOrder");

				newDNS["DNSServerSearchOrder"] = dns;

				ManagementBaseObject setDNS = NetAdapterConfig.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
				return (uint)setDNS["ReturnValue"];
			}
			catch (Exception)
			{
				throw;
			}
		}
		
		/// <summary>
		/// <para>Set's WINS (Windows Internet Name Service = DNS in LANs) servers of the local machine</para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/> that shall be modified</param>
		/// <param name="priWINS">Primary WINS server address</param>
		/// <param name="secWINS">Secondary WINS server address</param>
		public static uint SetWINS(NetworkInterface ni, IPAddress priWINS, IPAddress secWINS)
		{
			ManagementObject NetAdapterConfig = getWin32_NetworkAdapterConfiguration(ni.Id);
			if (NetAdapterConfig == null)
				return 65;
			if ((bool)NetAdapterConfig["IPEnabled"] == false)
				return 84;

			try
			{
				ManagementBaseObject wins = NetAdapterConfig.GetMethodParameters("SetWINSServer");

				wins["WINSPrimaryServer"] = priWINS.ToString();
				wins["WINSSecondaryServer"] = secWINS.ToString();

				ManagementBaseObject setWINS = NetAdapterConfig.InvokeMethod("SetWINSServer", wins, null);
				return (uint)setWINS["ReturnValue"];
			}
			catch (Exception)
			{
				throw;
			}
		} 
		#endregion

		#region Enable/Disable
		/// <summary>
		/// <para>Enables the given <see cref="NetworkInterface"/></para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni"></param>
		/// <returns></returns>
		public static bool Enable(NetworkInterface ni)
		{
			return Enable(ni.Id);
		}
		/// <summary>
		/// <para>Enables the <see cref="NetworkInterface"/> with the given <paramref name="GUID"/></para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns>
		/// <para>Whether the call was successful or not</para>
		/// <para>Sometimes this method works but a error code is returned anyway...</para>
		/// </returns>
		public static bool Enable(string GUID)
		{
			ManagementObject mgmtObject = getWin32_NetworkAdapter(GUID);
			uint ret = (uint)mgmtObject.InvokeMethod("Enable", null);
			return (int)ret == 0;		// net helpmsg %ret% may help =)
		}
		/// <summary>
		/// <para>Disables the <see cref="NetworkInterface"/> with the given <paramref name="GUID"/></para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns>
		/// <para>Whether the call was successful or not</para>
		/// <para>Sometimes this method works but a error code is returned anyway...</para>
		/// </returns>
		public static bool Disable(NetworkInterface ni)
		{
			return Disable(ni.Id);
		}
		/// <summary>
		/// <para>Enables the <see cref="NetworkInterface"/> with the given <paramref name="GUID"/></para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns>
		/// <para>Whether the call was successful or not</para>
		/// <para>Sometimes this method works but a error code is returned anyway...</para>
		/// </returns>
		public static bool Disable(string GUID)
		{
			ManagementObject mgmtObject = getWin32_NetworkAdapter(GUID);
			uint ret = (uint)mgmtObject.InvokeMethod("Disable", null);
			return ret == 0;		// net helpmsg %ret% may help =)
		}
		#endregion
	}
}
