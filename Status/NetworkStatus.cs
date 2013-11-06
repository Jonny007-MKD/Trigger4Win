using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Tasker.Status
{
	static class Network
	{
		#region Properties
		#region wininet.dll
		[DllImport("wininet.dll")]
		private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
		private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
		private const int INTERNET_OPTION_REFRESH = 37;
		#endregion

		/// <summary>
		/// <para><see cref="NetworkInterface.GetAllNetworkInterfaces"/></para>
		/// </summary>
		public static List<NetworkInterface> AllNetworkInterfaces { get { return new List<NetworkInterface>(NetworkInterface.GetAllNetworkInterfaces()); } }

		/// <summary>
		/// <para><see cref="NetworkInterface.GetIsNetworkAvailable"/></para>
		/// </summary>
		public static bool IsNetworkAvailable { get { return NetworkInterface.GetIsNetworkAvailable(); } }

		/// <summary>
		/// <para>Returns the current Proxy configuration</para>
		/// </summary>
		public static WebProxy ProxyConfiguration
		{
			get
			{
				RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", writable: false);
				if (Convert.ToBoolean(regKey.GetValue("ProxyEnable")))
				{
					string server = regKey.GetValue("ProxyServer").ToString();
					string bypass = regKey.GetValue("ProxyOverride").ToString().Replace("*", @"\*").Replace("+", @"\+");
					return new WebProxy(server, !String.IsNullOrWhiteSpace(bypass), bypass.Split(';'));
				}
				else
					return new WebProxy();
			}
		}

		// BUG: NetworkAvailable: Not working
		/// <summary>
		/// <para>Gets a value indicating whether a network connection is present</para>
		/// </summary>
		public static bool NetworkAvailable
		{
			get
			{
				return SystemInformation.Network;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Gets a List of <see cref="IPAddress"/>es of the <see cref="NetworkInterface"/> with the specified <paramref name="networkInterfaceID"/></para>
		/// <para><see cref="GetIpAddresses(NetworkInterface)"/></para>
		/// </summary>
		/// <param name="networkInterfaceID">The ID of the <see cref="NetworkInterface"/></param>
		/// <returns></returns>
		public static List<IPAddress> GetIpAddresses(string networkInterfaceID)
		{
			return GetIpAddresses(GetNetworkInterface(networkInterfaceID));
		}
		/// <summary>
		/// <para>Gets a List of <see cref="IPAddress"/>es of the specified <see cref="NetworkInterface"/></para>
		/// </summary>
		/// <param name="ni">The <see cref="NetworkInterface"/></param>
		/// <returns></returns>
		public static List<IPAddress> GetIpAddresses(NetworkInterface ni)
		{
			List<IPAddress> ipAddrs = new List<IPAddress>();
			UnicastIPAddressInformationCollection ipInfos = ni.GetIPProperties().UnicastAddresses;
			foreach (UnicastIPAddressInformation ipInfo in ipInfos)
				ipAddrs.Add(ipInfo.Address);
			return ipAddrs;
		}

		/// <summary>
		/// <para>Gets the <see cref="NetworkInterface"/> with the specified <paramref name="networkInterfaceID"/></para>
		/// </summary>
		/// <param name="networkInterfaceID"></param>
		/// <returns></returns>
		public static NetworkInterface GetNetworkInterface(string networkInterfaceID)
		{
			List<NetworkInterface> nis = AllNetworkInterfaces;
			return nis.Find(new Predicate<NetworkInterface>((item) => { return item.Id == networkInterfaceID; }));
		}

		/// <summary>
		/// <para>Checks whether the <see cref="NetworkInterface"/> has an IP address that begins with the specified <paramref name="IP"/></para>
		/// </summary>
		/// <param name="networkInterfaceID"><see cref="NetworkInterface"/>.Id</param>
		/// <param name="IP">IP Address or just the beginning of a IP Address</param>
		/// <returns></returns>
		public static bool NetworkInterfaceHasIP(string networkInterfaceID, string IP)
		{
			return NetworkInterfaceHasIP(GetNetworkInterface(networkInterfaceID), IP);
		}
		/// <summary>
		/// <para>Checks whether the specified <paramref name="ni"/> has an IP address that begins with the specified <paramref name="IP"/></para>
		/// </summary>
		/// <param name="ni"><see cref="NetworkInterface"/></param>
		/// <param name="IP">IP Address or just the beginning of a IP Address</param>
		/// <returns></returns>
		public static bool NetworkInterfaceHasIP(NetworkInterface ni, string IP)
		{
			if (ni == null)
				return false;
			return GetIpAddresses(ni).Exists(new Predicate<IPAddress>((item) => { return item.ToString().StartsWith(IP); }));
		}

		/// <summary>
		/// <para>Checks whether the <see cref="NetworkInterface"/> has this specified DNS <paramref name="suffix"/></para>
		/// </summary>
		/// <param name="networkInterfaceID"><see cref="NetworkInterface"/>.Id</param>
		/// <param name="suffix">DNS suffix</param>
		/// <returns></returns>
		public static bool NetworkInterfaceHasDnsSuffix(string networkInterfaceID, string suffix)
		{
			return NetworkInterfaceHasDnsSuffix(GetNetworkInterface(networkInterfaceID), suffix);
		}
		/// <summary>
		/// <para>Checks whether the <see cref="NetworkInterface"/> has this specified DNS <paramref name="suffix"/></para>
		/// </summary>
		/// <param name="ni"><see cref="NetworkInterface"/></param>
		/// <param name="suffix">DNS suffix</param>
		/// <returns></returns>
		public static bool NetworkInterfaceHasDnsSuffix(NetworkInterface ni, string suffix)
		{
			if (ni == null)
				return false;
			return ni.GetIPProperties().DnsSuffix.Equals(suffix, StringComparison.InvariantCultureIgnoreCase);
		}



		/// <summary>
		/// <para>Returns the current Network status</para>
		/// </summary>
		/// <returns></returns>
		public static TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Network");

			tnMain.Nodes.Add("(not working) Network available: " + NetworkAvailable);

			List<NetworkInterface> nis = AllNetworkInterfaces;			
			TreeNode tnInterfaces = tnMain.Nodes.Add("Network interfaces: " + nis.Count);
			nis.ForEach(new Action<NetworkInterface>(ni =>
					{
						TreeNode tnInterface = tnInterfaces.Nodes.Add(ni.Name);
						tnInterface.Nodes.Add("Id: " + ni.Id);
						tnInterface.Nodes.Add("Speed: " + ni.Speed / 1024 + "kb/s");
						List<IPAddress> ips = Status.Network.GetIpAddresses(ni);
						TreeNode tnIP = tnInterface.Nodes.Add("IP addresses (" + ips.Count + ")");
						ips.ForEach(new Action<IPAddress>(ip => tnIP.Nodes.Add(ip.ToString())));
					}));

			WebProxy wp = ProxyConfiguration;
			if (wp.Address == null)
				tnMain.Nodes.Add("Proxy disabled");
			else
			{
				TreeNode tnProxy = tnMain.Nodes.Add("Proxy enabled");
				tnProxy.Nodes.Add("Server: " + wp.Address);
				TreeNode tnByPass = tnProxy.Nodes.Add("ByPass URLs (" + wp.BypassList.Length + ")");
				Array.ForEach<string>(wp.BypassList, new Action<string>((item) => tnByPass.Nodes.Add(item.Replace(@"\*", "*").Replace(@"\+", ""))));
			}

			return tnMain;
		}
		#endregion
	}
}
