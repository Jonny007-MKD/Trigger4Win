using System;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Management;
using System.Net.NetworkInformation;
using Trigger.Classes.WMI;



namespace Trigger.Actions
{
	public static class Network
	{
		#region WMI Methods
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapter <see cref="ManagementObject"/> that represents the specified <paramref name="ni"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="ni"></param>
		/// <returns></returns>
		private static NetworkAdapter getWin32_NetworkAdapter(NetworkInterface ni)
		{
			return getWin32_NetworkAdapter(ni.Id);
		}
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapter <see cref="ManagementObject"/> that has the specified <paramref name="GUID"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns></returns>
		private static NetworkAdapter getWin32_NetworkAdapter(string GUID)
		{
			NetworkAdapter.NetworkAdapterCollection naObjects = NetworkAdapter.GetInstances("GUID = \"" + GUID + "\"");

			foreach (NetworkAdapter naObject in naObjects)
				if (naObject.GUID != null && naObject.GUID.Equals(GUID))
					return naObject;
			return null;
		}
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapterConfiguration <see cref="ManagementObject"/> that belongs to the specified Win32_NetworkAdapter <paramref name="ni"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="ni"></param>
		/// <returns></returns>
		private static NetworkAdapterConfiguration getWin32_NetworkAdapterConfiguration(NetworkInterface ni)
		{
			return getWin32_NetworkAdapterConfiguration(ni.Id);
		}
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapterConfiguration <see cref="ManagementObject"/> that belongs to the Win32_NetworkAdapter with the specified <paramref name="GUID"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns></returns>
		private static NetworkAdapterConfiguration getWin32_NetworkAdapterConfiguration(string GUID)
		{
			NetworkAdapter Win32_NetworkAdapter = getWin32_NetworkAdapter(GUID);
			if (Win32_NetworkAdapter == null)
				return null;

			return Win32_NetworkAdapter.Configuration;
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
	}
}