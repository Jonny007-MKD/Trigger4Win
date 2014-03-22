using System;
using System.Net;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Management;
using System.Net.NetworkInformation;



namespace Trigger.Actions
{
	/// <summary>
	/// <para>Actions that can be done with the network.</para>
	/// <para>This includes </para>
	/// </summary>
	class Network
	{
		#region Enums, structs, consts
		/// <summary>
		/// <para>The path to the internet settings in registry</para>
		/// </summary>
		/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/aa385328%28v=vs.85%29.aspx</remarks>
		internal const string RegistryInternetSettings = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";

		/// <summary>
		/// <para>The following option flags are used with the InternetQueryOption and InternetSetOption functions. All valid option flags have a value greater than or equal to INTERNET_FIRST_OPTION and less than or equal to INTERNET_LAST_OPTION.</para>
		/// </summary>
		public enum InternetOption : int
		{
			/// <summary>
			/// <para>Sets or retrieves the Boolean value that determines if the system should check the network for newer content and overwrite edited cache entries if a newer version is found. If set to True, the system checks the network for newer content and overwrites the edited cache entry with the newer version. The default is False, which indicates that the edited cache entry should be used without checking the network. This is used by InternetQueryOption and InternetSetOption. It is valid only in Microsoft Internet Explorer 5 and later.</para>
			/// </summary>
			BypassEditedEntry = 64,
			/// <summary>
			/// <para>No longer supported.</para>
			/// </summary>
			CacheStreamHandle = 27,
			/// <summary>
			/// <para>Retrieves an INTERNET_CACHE_TIMESTAMPS structure that contains the LastModified time and Expires time from the resource stored in the Internet cache. This value is used by InternetQueryOption.</para>
			/// </summary>
			CacheTimestamps = 69,
			/// <summary>
			/// <para>Sets or retrieves the address of the callback function defined for this handle. This option can be used on all HINTERNET handles. Used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			Callback = 1,
			/// <summary>
			/// <para>This flag is not supported by InternetQueryOption. The lpBuffer parameter must be a pointer to a CERT_CONTEXT structure and not a pointer to a CERT_CONTEXT pointer. If an application receives ERROR_INTERNET_CLIENT_AUTH_CERT_NEEDED, it must call InternetErrorDlg or use InternetSetOption to supply a certificate before retrying the request. CertDuplicateCertificateContext is then called so that the certificate context passed can be independently released by the application.</para>
			/// </summary>
			ClientCertContext = 84,
			/// <summary>
			/// <para>By default, the host or authority portion of the Unicode URL is encoded according to the IDN specification. Setting this option on the request, or connection handle, when IDN is disabled, specifies a code page encoding scheme for the host portion of the URL. The lpBuffer parameter in the call to InternetSetOption contains the desired DBCS code page. If no code page is specified in lpBuffer, WinINet uses the default system code page (CP_ACP). Note: This option is ignored if IDN is not disabled. For more information about how to disable IDN, see the IDN option.</para>
			/// <para>Windows XP with SP2 and Windows Server 2003 with SP1:  This flag is not supported.</para>
			/// </summary>
			Codepage = 68,
			/// <summary>
			/// <para>By default, the path portion of the URL is UTF8 encoded. The WinINet API performs escape character (%) encoding on the high-bit characters. Setting this option on the request, or connection handle, disables the UTF8 encoding and sets a specific code page. The lpBuffer parameter in the call to InternetSetOption contains the desired DBCS codepage for the path. If no code page is specified in lpBuffer, WinINet uses the default CP_UTF8.</para>
			/// <para>Windows XP with SP2 and Windows Server 2003 with SP1:  This flag is not supported.</para>
			/// </summary>
			CodepagePath = 100,
			/// <summary>
			/// <para>By default, the path portion of the URL is the default system code page (CP_ACP). The escape character (%) conversions are not performed on the extra portion. Setting this option on the request, or connection handle disables the CP_ACP encoding. The lpBuffer parameter in the call to InternetSetOption contains the desired DBCS codepage for the extra portion of the URL. If no code page is specified in lpBuffer, WinINet uses the default system code page (CP_ACP).</para>
			/// <para>Windows XP with SP2 and Windows Server 2003 with SP1:  This flag is not supported.</para>
			/// </summary>
			CodepageExtra = 101,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the number of times WinINet attempts to resolve and connect to a host. It only attempts once per IP address. For example, if you attempt to connect to a multihome host that has ten IP addresses and CONNECT_RETRIES is set to seven, WinINet only attempts to resolve and connect to the first seven IP addresses. Conversely, given the same set of ten IP addresses, if CONNECT_RETRIES is set to 20, WinINet attempts each of the ten only once. If a host has only one IP address and the first connection attempt fails, there are no further attempts. If a connection attempt still fails after the specified number of attempts, the request is canceled. The default value for CONNECT_RETRIES is five attempts. This option can be used on any HINTERNET handle, including a NULL handle. It is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			ConnectRetries = 3,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the time-out value, in milliseconds, to use for Internet connection requests. Setting this option to infinite (0xFFFFFFFF) will disable this timer.</para>
			/// <para>If a connection request takes longer than this time-out value, the request is canceled. When attempting to connect to multiple IP addresses for a single host (a multihome host), the timeout limit is cumulative for all of the IP addresses. This option can be used on any HINTERNET handle, including a NULL handle. It is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			ConnectTimeout = 2,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the connected state. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			ConnectedState = 50,
			/// <summary>
			/// <para>Sets or retrieves a DWORD_PTR that contains the address of the context value associated with this HINTERNET handle. This option can be used on any HINTERNET handle. This is used by InternetQueryOption and InternetSetOption. Previously, this set the context value to the address stored in the lpBuffer pointer. This has been corrected so that the value stored in the buffer is used and the CONTEXT_VALUE flag is assigned a new value. The old value, 10, has been preserved so that applications written for the old behavior are still supported.</para>
			/// </summary>
			ContextValue = 45,
			/// <summary>
			/// <para>Identical to RECEIVE_TIMEOUT. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			ControlReceiveTimeout = 6,
			/// <summary>
			/// <para>Identical to SEND_TIMEOUT. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			ControlSendTimeout = 5,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the time-out value, in milliseconds, to receive a response to a request for the data channel of an FTP transaction. If the response takes longer than this time-out value, the request is canceled. This option can be used on any HINTERNET handle, including a NULL handle. It is used by InternetQueryOption and InternetSetOption.</para>
			/// <para>This flag has no impact on HTTP functionality.</para>
			/// </summary>
			DataReceiveTimeout = 8,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value, in milliseconds, that contains the time-out value to send a request for the data channel of an FTP transaction. If the send takes longer than this time-out value, the send is canceled. This option can be used on any HINTERNET handle, including a NULL handle. It is used by InternetQueryOption and InternetSetOption.</para>
			/// <para>This flag has no impact on HTTP functionality.</para>
			/// </summary>
			DataSendTimeout = 7,
			/// <summary>
			/// <para>Retrieves a string value that contains the name of the file backing a downloaded entity. This flag is valid after InternetOpenUrl, FtpOpenFile, GopherOpenFile, or HttpOpenRequest has completed. This option can only be queried by InternetQueryOption.</para>
			/// </summary>
			DatafileName = 33,
			/// <summary>
			/// <para>Sets a string value that contains the extension of the file backing a downloaded entity. This flag should be set before calling InternetOpenUrl, FtpOpenFile, GopherOpenFile, or HttpOpenRequest. This option can only be set by InternetSetOption.</para>
			/// </summary>
			DatafileExt = 96,
			/// <summary>
			/// <para>Retrieves an INTERNET_DIAGNOSTIC_SOCKET_INFO structure that contains data about a specified HTTP Request. This flag is used by InternetQueryOption.</para>
			/// <para>Windows 7:  This option is no longer supported.</para>
			/// </summary>
			DiagnosticSocketInfo = 67,
			/// <summary>
			/// <para>Causes the system to log off the Digest authentication SSPI package, purging all of the credentials created for the process. No buffer is required for this option. It is used by InternetSetOption.</para>
			/// </summary>
			DigestAuthUnload = 76,
			/// <summary>
			/// <para>Flushes entries not in use from the password cache on the hard disk drive. Also resets the cache time used when the synchronization mode is once-per-session. No buffer is required for this option. This is used by InternetSetOption.</para>
			/// </summary>
			EndBrowserSession = 42,
			/// <summary>
			/// <para>Sets an unsigned long integer value that contains the error masks that can be handled by the client application.</para>
			/// </summary>
			ErrorMask = 62,
			/// <summary>
			/// <para>Retrieves an unsigned long integer value that contains a Winsock error code mapped to the ERROR_INTERNET_ error messages last returned in this thread context. This option is used on a NULLHINTERNET handle by InternetQueryOption.</para>
			/// </summary>
			ExtendedError = 24,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the amount of time the system should wait for a response to a network request before checking the cache for a copy of the resource. If a network request takes longer than the time specified and the requested resource is available in the cache, the resource is retrieved from the cache. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			FromCacheTimeout = 63,
			/// <summary>
			/// <para>Retrieves an unsigned long integer value that contains the type of the HINTERNET handles passed in. This is used by InternetQueryOption on any HINTERNET handle.</para>
			/// </summary>
			HandleType = 9,
			/// <summary>
			/// <para>Enables WinINet to perform decoding for the gzip and deflate encoding schemes. For more information, see Content Encoding.</para>
			/// </summary>
			HttpDecoding = 65,
			/// <summary>
			/// <para>Sets or retrieves an HTTP_VERSION_INFO structure that contains the supported HTTP version. This must be used on a NULL handle. This is used by InternetQueryOption and InternetSetOption.</para>
			/// <para>On Windows 7, Windows Server 2008 R2, and later, the value of the dwMinorVersion member in the HTTP_VERSION_INFO structure is overridden by Internet Explorer settings. EnableHttp1_1 is a registry value under HKLM\Software\Microsoft\InternetExplorer\AdvacnedOptions\HTTP\GENABLE controlled by Internet Options set in Internet Explorer for the system. The EnableHttp1_1 value defaults to 1. The HTTP_VERSION_INFO structure is ignored for any HTTP version less than 1.1 if EnableHttp1_1 is set to 1.</para>
			/// </summary>
			IdleState = 51,
			/// <summary>
			/// <para>By default, the host or authority portion of the URL is encoded according to the IDN specification for both direct and proxy connections. This option can be used on the request, or connection handle to enable or disable IDN. When IDN is disabled, WinINet uses the system codepage to encode the host or authority portion of the URL. To disable IDN host conversion, set the lpBuffer parameter in the call to InternetSetOption to zero. To enable IDN conversion on only the direct connection, specify INTERNET_FLAG_IDN_DIRECT in the lpBuffer parameter in the call to InternetSetOption. To enable IDN conversion on only the proxy connection, specify INTERNET_FLAG_IDN_PROXY in the lpBuffer parameter in the call to InternetSetOption.</para>
			/// <para>Windows XP with SP2 and Windows Server 2003 with SP1:  This flag is not supported.</para>
			/// </summary>
			IDN = 102,
			/// <summary>
			/// <para>Sets or retrieves whether the global offline flag should be ignored for the specified request handle. No buffer is required for this option. This is used by InternetQueryOption and InternetSetOption with a request handle. This option is only valid in Internet Explorer 5 and later.</para>
			/// </summary>
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the maximum number of connections allowed per HTTP/1.0 server. This is used by InternetQueryOption and InternetSetOption. This option is only valid in Internet Explorer 5 and later.</para>
			/// </summary>
			MaxConnsPer_1_0_Server = 74,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the maximum number of connections allowed per CERN proxy. When this option is set or retrieved, the hInternet parameter must set to a null handle value. A null handle value indicates that the option should be set or queried for the current process. When calling InternetSetOption with this option, all existing proxy objects will receive the new value. This value is limited to a range of 2 to 128, inclusive.</para>
			/// <para>Version:  Requires Internet Explorer 8.0.</para>
			/// </summary>
			MaxConnsPerProxy = 103,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the maximum number of connections allowed per server. This is used by InternetQueryOption and InternetSetOption. This option is only valid in Internet Explorer 5 and later.</para>
			/// </summary>
			MaxConnsPerServer = 73,
			/// <summary>
			/// <para>Retrieves the parent handle to this handle. This option can be used on any HINTERNET handle by InternetQueryOption.</para>
			/// </summary>
			ParentHandle = 21,
			/// <summary>
			/// <para>Sets or retrieves a string value that contains the password associated with a handle returned by InternetConnect. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			Password = 29,
			/// <summary>
			/// <para>Sets or retrieves an INTERNET_PER_CONN_OPTION_LIST structure that specifies a list of options for a particular connection. This is used by InternetQueryOption and InternetSetOption. This option is only valid in Internet Explorer 5 and later.</para>
			/// <para>Note  PER_CONNECTION_OPTION causes the settings to be changed on a system-wide basis when a NULL handle is used in the call to InternetSetOption. To refresh the global proxy settings, you must call InternetSetOption with the REFRESH option flag.</para>
			/// </summary>
			/// <remarks>
			/// <para>Note  To change proxy information for the entire process without affecting the global settings in Internet Explorer 5 and later, use this option on the handle that is returned from InternetOpen. The following code example changes the proxy for the whole process even though the HINTERNET handle is closed and is not used by any requests.</para>
			/// <para>For more information and code examples, see KB article 226473.</para>
			/// </remarks>
			PerConnectionOption = 75,
			/// <summary>
			/// <para>Sets or retrieves an INTERNET_PROXY_INFO structure that contains the proxy data for an existing InternetOpen handle when the HINTERNET handle is not NULL. If the HINTERNET handle is NULL, the function sets or queries the global proxy data. This option can be used on the handle returned by InternetOpen. It is used by InternetQueryOption and InternetSetOption.</para>
			/// <para>Note  It is recommended that PER_CONNECTION_OPTION be used instead of PROXY. For more information, see KB article 226473.</para>
			/// </summary>
			Proxy = 38,
			/// <summary>
			/// <para>Sets or retrieves a string value that contains the password used to access the proxy. This is used by InternetQueryOption and InternetSetOption. This option can be set on the handle returned by InternetConnect or HttpOpenRequest.</para>
			/// </summary>
			ProxyPassword = 44,
			/// <summary>
			/// <para>Alerts the current WinInet instance that proxy settings have changed and that they must update with the new settings. To alert all available WinInet instances, set the Buffer parameter of InternetSetOption to NULL and BufferLength to 0 when passing this option. This option can be set on the handle returned by InternetConnect or HttpOpenRequest.</para>
			/// </summary>
			ProxySettingsChanged = 95,
			/// <summary>
			/// <para>Sets or retrieves a string value that contains the user name used to access the proxy. This is used by InternetQueryOption and InternetSetOption. This option can be set on the handle returned by InternetConnect or HttpOpenRequest.</para>
			/// </summary>
			ProxyUsername = 43,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the size of the read buffer. This option can be used on HINTERNET handles returned by FtpOpenFile, FtpFindFirstFile, and InternetConnect (FTP session only). This option is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			ReadBufferSize = 12,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the time-out value, in milliseconds, to receive a response to a request. If the response takes longer than this time-out value, the request is canceled. This option can be used on any HINTERNET handle, including a NULL handle. It is used by InternetQueryOption and InternetSetOption.</para>
			/// <para>When used in reference to an FTP transaction, this option refers to the control channel.</para>
			/// </summary>
			ReceiveTimeout = 6,
			/// <summary>
			/// <para>Causes the proxy data to be reread from the registry for a handle. No buffer is required. This option can be used on the HINTERNET handle returned by InternetOpen. It is used by InternetSetOption.</para>
			/// </summary>
			Refresh = 37,
			/// <summary>
			/// <para>Retrieves an unsigned long integer value that contains the special status flags that indicate the status of the download in progress. This is used by InternetQueryOption.</para>
			/// </summary>
			RequestFlags = 23,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the priority of requests that compete for a connection on an HTTP handle. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			RequestPriority = 58,
			/// <summary>
			/// <para>Starts a new cache session for the process. No buffer is required. This is used by InternetSetOption.</para>
			/// </summary>
			ResetUrlcacheSession = 60,
			/// <summary>
			/// <para>Sets or retrieves a string value that contains the secondary cache key. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			SecondaryCacheKey = 53,
			/// <summary>
			/// <para>Retrieves the certificate for an SSL/PCT (Secure Sockets Layer/Private Communications Technology) server into a formatted string. This is used by InternetQueryOption.</para>
			/// </summary>
			SecurityCertificate = 35,
			/// <summary>
			/// <para>Retrieves the certificate for an SSL/PCT server into the INTERNET_CERTIFICATE_INFO structure. This is used by InternetQueryOption.</para>
			/// </summary>
			SecurityCertificateStruct = 32,
			/// <summary>
			/// <para>Retrieves an unsigned long integer value that contains the security flags for a handle. This option is used by InternetQueryOption.</para>
			/// </summary>
			SecurityFlags = 31,
			/// <summary>
			/// <para>Retrieves an unsigned long integer value that contains the bit size of the encryption key. The larger the number, the greater the encryption strength used. This is used by InternetQueryOption. Be aware that the data retrieved this way relates to a transaction that has already occurred, whose security level can no longer be changed.</para>
			/// </summary>
			SecurityKeyBitness = 36,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value, in milliseconds, that contains the time-out value to send a request. If the send takes longer than this time-out value, the send is canceled. This option can be used on any HINTERNET handle, including a NULL handle. It is used by InternetQueryOption and InternetSetOption.</para>
			/// <para>When used in reference to an FTP transaction, this option refers to the control channel.</para>
			/// </summary>
			SendTimeout = 5,
			/// <summary>
			/// <para>Retrieves the server’s certificate-chain context as a duplicated PCCERT_CHAIN_CONTEXT. You may pass this duplicated context to any Crypto API function which takes a PCCERT_CHAIN_CONTEXT. You must call CertFreeCertificateChain on the returned PCCERT_CHAIN_CONTEXT when you are done with the certificate-chain context.</para>
			/// <para>Version:  Requires Internet Explorer 8.0.</para>
			/// </summary>
			ServerCertChainContext = 105,
			/// <summary>
			/// <para>Notifies the system that the registry settings have been changed so that it verifies the settings on the next call to InternetConnect. This is used by InternetSetOption.</para>
			/// </summary>
			SettingsChanged = 39,
			/// <summary>
			/// <para>Sets an HTTP request object such that it will not logon to origin servers, but will perform automatic logon to HTTP proxy servers. This option differs from the Request flag INTERNET_FLAG_NO_AUTH, which prevents authentication to both proxy servers and origin servers.</para>
			/// <para>Setting this mode will suppress the use of any credential material (either previously provided username/password or client SSL certificate) when communicating with an origin server. However, if the request must transit via an authenticating proxy, WinINet will still perform automatic authentication to the HTTP proxy per the Intranet Zone settings for the user. The default Intranet Zone setting is to permit automatic logon using the user’s default credentials.</para>
			/// </summary>
			/// <remarks>
			/// <para>To ensure suppression of all identifying information, the caller should combine SUPPRESS_SERVER_AUTH with the INTERNET_FLAG_NO_COOKIES request flag.</para>
			/// <para>This option may only be set on request objects before they have been sent. Attempts to set this option after the request has been sent will return ERROR_INTERNET_INCORRECT_HANDLE_STATE.</para>
			/// <para>No buffer is required for this option. This is used by InternetSetOption on handles returned by HttpOpenRequest only.</para>
			/// <para>Version:  Requires Internet Explorer 8.0 or later.</para>
			/// </remarks>
			SuppressServerAuth = 104,
			/// <summary>
			/// <para>A general purpose option that is used to suppress behaviors on a process-wide basis. The lpBuffer parameter of the function must be a pointer to a DWORD containing the specific behavior to suppress. This option cannot be queried with InternetQueryOption.</para>
			/// </summary>
			SuppressBehavior = 81,
			/// <summary>
			/// <para>Retrieves a string value that contains the full URL of a downloaded resource. If the original URL contained any extra data, such as search strings or anchors, or if the call was redirected, the URL returned differs from the original. This option is valid on HINTERNET handles returned by InternetOpenUrl, FtpOpenFile, GopherOpenFile, or HttpOpenRequest. It is used by InternetQueryOption.</para>
			/// </summary>
			URL = 34,
			/// <summary>
			/// <para>Sets or retrieves the user agent string on handles supplied by InternetOpen and used in subsequent HttpSendRequest functions, as long as it is not overridden by a header added by HttpAddRequestHeaders or HttpSendRequest. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			UserAgent = 41,
			/// <summary>
			/// <para>Sets or retrieves a string that contains the user name associated with a handle returned by InternetConnect. This is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			Username = 28,
			/// <summary>
			/// <para>Retrieves an INTERNET_VERSION_INFO structure that contains the version number of Wininet.dll. This option can be used on a NULLHINTERNET handle by InternetQueryOption.</para>
			/// </summary>
			Version = 40,
			/// <summary>
			/// <para>Sets or retrieves an unsigned long integer value that contains the size, in bytes, of the write buffer. This option can be used on HINTERNET handles returned by FtpOpenFile and InternetConnect (FTP session only). It is used by InternetQueryOption and InternetSetOption.</para>
			/// </summary>
			WriteBufferSize = 13,
		}
		#endregion
		
		#region WMI Methods
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapter <see cref="ManagementObject"/> that represents the specified <paramref name="ni"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="ni"></param>
		/// <returns></returns>
		internal static ManagementObject getWin32_NetworkAdapter(NetworkInterface ni)
		{
			return getWin32_NetworkAdapter(ni.Id);
		}
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapter <see cref="ManagementObject"/> that has the specified <paramref name="GUID"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns></returns>
		internal static ManagementObject getWin32_NetworkAdapter(string GUID)
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
		internal static ManagementObject getWin32_NetworkAdapterConfiguration(NetworkInterface ni)
		{
			return getWin32_NetworkAdapterConfiguration(ni.Id);
		}
		/// <summary>
		/// <para>Searches for the WMI Win32_NetworkAdapterConfiguration <see cref="ManagementObject"/> that belongs to the Win32_NetworkAdapter with the specified <paramref name="GUID"/>.</para>
		/// <para>If it is not found, null will be returned.</para>
		/// </summary>
		/// <param name="GUID"><see cref="NetworkInterface"/>.Id (like {123-456-789})</param>
		/// <returns></returns>
		internal static ManagementObject getWin32_NetworkAdapterConfiguration(string GUID)
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

		#region Dll Imports
		/// <summary>
		/// <para>Sets an Internet option.</para>
		/// </summary>
		/// <param name="hInternet">Handle on which to set information. Can be <see cref="IntPtr"/>.Zero.</param>
		/// <param name="dwOption">Internet option to be set.</param>
		/// <param name="lpBuffer">Pointer to a buffer that contains the option setting.</param>
		/// <param name="dwBufferLength">Size of the <paramref name="lpBuffer"/> buffer. If <paramref name="lpBuffer"/> contains a string, the size is in TCHARs. If <paramref name="lpBuffer"/> contains anything other than a string, the size is in bytes.</param>
		/// <returns>Returns TRUE if successful, or FALSE otherwise. To get a specific error message, call GetLastError.</returns>
		/// <remarks>
		/// <para>GetLastError will return the error ERROR_INVALID_PARAMETER if an option flag that cannot be set is specified.</para>
		/// <para>Like all other aspects of the WinINet API, this function cannot be safely called from within DllMain or the constructors and destructors of global objects.</para>
		/// </remarks>
		[DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool InternetSetOption(IntPtr hInternet, InternetOption dwOption, IntPtr lpBuffer, int dwBufferLength);
		#endregion

		#region Proxy
		/// <summary>
		/// <para>Disable the global proxy configuration</para>
		/// </summary>
		public static void ProxyDisable()
		{
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey(RegistryInternetSettings, writable: true);
			regKey.SetValue("ProxyEnable", 0);
			ProxyRefresh();
		}
		/// <summary>
		/// <para>Enables the global proxy configuration</para>
		/// </summary>
		/// <param name="proxy">A <see cref="WebProxy"/> with the required Server:Port and ByPass Addresses</param>
		public static void ProxyEnable(WebProxy proxy)
		{
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey(RegistryInternetSettings, writable: true);
			regKey.SetValue("ProxyEnable", 1);
			regKey.SetValue("ProxyServer", proxy.Address.OriginalString);
			regKey.SetValue("ProxyOverride", String.Join<string>(";", proxy.BypassList).Replace(@"\", ""));
			
			ProxyRefresh();
		}
		/// <summary>
		/// <para>Causes the OS to refresh the settings, causing IP, proxy etc. to really update</para>
		/// </summary>
		internal static void ProxyRefresh()
		{
			bool settingsReturn = InternetSetOption(IntPtr.Zero, InternetOption.SettingsChanged, IntPtr.Zero, 0);
			bool refreshReturn = InternetSetOption(IntPtr.Zero, InternetOption.Refresh, IntPtr.Zero, 0);
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
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8******* or net helpmsg %ret% otherwise</para>
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
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8******* or net helpmsg %ret% otherwise</para>
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
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8******* or net helpmsg %ret% otherwise</para>
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
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8******* or net helpmsg %ret% otherwise</para>
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
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8******* or net helpmsg %ret% otherwise</para>
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
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8******* or net helpmsg %ret% otherwise</para>
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
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8******* or net helpmsg %ret% otherwise</para>
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
		/// <para>see http://msdn.microsoft.com/en-us/library/aa394559%28v=vs.85%29.aspx for 0x8******* or net helpmsg %ret% otherwise</para>
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
		/// <para>Disables the <see cref="NetworkInterface"/> <paramref name="ni"/></para>
		/// <para>This method requires admin privileges (<see cref="Main.RequireAdministrator"/>)</para>
		/// </summary>
		/// <param name="ni"><see cref="NetworkInterface"/></param>
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
