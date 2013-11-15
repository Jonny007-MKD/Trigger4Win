using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;

namespace Trigger.Actions
{
	/// <summary>
	/// <para>Actions dealing with the system</para>
	/// </summary>
	public class System
	{
		#region Properties
		/// <summary>Whether <see cref="getShutdownPrivilege"/> was already called</summary>
		private static bool hasShutdownPrivilege = false;
		#endregion

		#region Enums, Structs
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct TokPriv1Luid
		{
			public int Count;
			public long Luid;
			public int Attr;
		}

		[Flags]
		private enum EWX : uint
		{
			/// <summary>
			/// <para>Beginning with Windows 8:  You can prepare the system for a faster startup by combining the EWX_HYBRID_SHUTDOWN flag with the EWX_SHUTDOWN flag.</para>
			/// </summary>
			HYBRID_SHUTDOWN = 0x00400000,
			/// <summary>
			/// <para>Shuts down all processes running in the logon session of the process that called the ExitWindowsEx function. Then it logs the user off.</para>
			/// <para>This flag can be used only by processes running in an interactive user's logon session.</para>
			/// </summary>
			LOGOFF = 0x00,
			/// <summary>
			/// <para>Shuts down the system and turns off the power. The system must support the power-off feature. </para>
			/// <para>The calling process must have the SE_SHUTDOWN_NAME privilege.</para>
			/// </summary>
			/// <remarks>To shut down or restart the system, the calling process must use the AdjustTokenPrivileges function to enable the SE_SHUTDOWN_NAME privilege.</remarks>
			POWEROFF = 0x08,
			/// <summary>
			/// <para>Shuts down the system and then restarts the system.</para>
			/// <para>The calling process must have the SE_SHUTDOWN_NAME privilege.</para>
			/// </summary>
			/// <remarks>To shut down or restart the system, the calling process must use the AdjustTokenPrivileges function to enable the SE_SHUTDOWN_NAME privilege.</remarks>
			REBOOT = 0x02,
			/// <summary>
			/// <para>Shuts down the system and then restarts it, as well as any applications that have been registered for restart using the RegisterApplicationRestart function. These application receive the WM_QUERYENDSESSION message with lParam set to the ENDSESSION_CLOSEAPP value. For more information, see Guidelines for Applications.</para>
			/// </summary>
			RESTARTAPPS = 0x40,
			/// <summary>
			/// <para>Shuts down the system to a point at which it is safe to turn off the power. All file buffers have been flushed to disk, and all running processes have stopped.</para>
			/// <para>The calling process must have the SE_SHUTDOWN_NAME privilege.</para>
			/// <para>Specifying this flag will not turn off the power even if the system supports the power-off feature. You must specify <see cref="POWEROFF"/> to do this.</para>
			/// </summary>
			/// <remarks>Windows XP with SP1:  If the system supports the power-off feature, specifying this flag turns off the power.</remarks>
			SHUTDOWN = 0x01,
			/// <summary>
			/// <para>This flag has no effect if terminal services is enabled. Otherwise, the system does not send the WM_QUERYENDSESSION message. This can cause applications to lose data. Therefore, you should only use this flag in an emergency.</para>
			/// </summary>
			/// <remarks>During a shutdown or log-off operation, running applications are allowed a specific amount of time to respond to the shutdown request. If this time expires before all applications have stopped, the system displays a user interface that allows the user to forcibly shut down the system or to cancel the shutdown request. If the EWX_FORCE value is specified, the system forces running applications to stop when the time expires.</remarks>
			FORCE = 0x04,
			/// <summary>
			/// <para>Forces processes to terminate if they do not respond to the WM_QUERYENDSESSION or WM_ENDSESSION message within the timeout interval.</para>
			/// </summary>
			/// <remarks>If the EWX_FORCEIFHUNG value is specified, the system forces hung applications to close and does not display the dialog box.</remarks>
			FORCEIFHUNG = 0x10,
		}
		#endregion

		#region Dll Imports
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		private static extern bool LockWorkStation();

		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		private static extern bool ExitWindowsEx(uint uFlags, int dwReason);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern IntPtr GetCurrentProcess();

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

		/// <summary>
		/// <para>Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.</para>
		/// </summary>
		/// <param name="hWnd">A handle to the window that should be activated and brought to the foreground. </param>
		/// <returns>If the window was brought to the foreground, the return value is nonzero. If the window was not brought to the foreground, the return value is zero.</returns>
		/// <remarks>
		/// <para>The system restricts which processes can set the foreground window. A process can set the foreground window only if one of the following conditions is true:
		/// <list>
		/// <item>The process is the foreground process.</item>
		/// <item>The process was started by the foreground process.</item>
		/// <item>The process received the last input event.</item>
		/// <item>There is no foreground process.</item>
		/// <item>The process is being debugged.</item>
		/// <item>The foreground process is not a Modern Application or the Start Screen.</item>
		/// <item>The foreground is not locked.</item>
		/// <item>The foreground lock time-out has expired.</item>
		/// <item>No menus are active.</item>
		/// </list>
		/// </para>
		/// <para>An application cannot force a window to the foreground while the user is working with another window. Instead, Windows flashes the taskbar button of the window to notify the user.</para>
		/// </remarks>
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		/// <summary>
		/// <para>Retrieves a handle to the top-level window whose class name and window name match the specified strings. This function does not search child windows. This function does not perform a case-sensitive search.</para>
		/// </summary>
		/// <param name="lpClassName">
		/// <para>The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names. This is not your .NET class name!</para>
		/// <para>If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.</para>
		/// </param>
		/// <param name="lpWindowName">The window name (the window's title). If this parameter is NULL, all window names match. This may be the Text property of your <see cref="Form"/></param>
		/// <returns>If the function succeeds, the return value is a handle to the window that has the specified class name and window name.</returns>
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr FindWindowW(string lpClassName, string lpWindowName);
		#endregion

		#region Methods
		/// <summary>
		/// <para>Gets the shutdown privilege for this process</para>
		/// <para><see cref="EWX"/></para>
		/// </summary>
		private static void getShutdownPrivilege()
		{
			if (hasShutdownPrivilege)
				return;
			bool ok;
			TokPriv1Luid tp;
			IntPtr hproc = GetCurrentProcess();
			IntPtr htok = IntPtr.Zero;
			ok = OpenProcessToken(hproc, 0x20 /*TOKEN_ADJUST_PRIVILEGES*/ | 0x08 /*TOKEN_QUERY*/, ref htok);
			tp.Count = 1;
			tp.Luid = 0;
			tp.Attr = 0x02 /*SE_PRIVILEGE_ENABLED*/;
			ok = LookupPrivilegeValue(null, "SeShutdownPrivilege", ref tp.Luid);
			ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
			hasShutdownPrivilege = true;
		}

		/// <summary>
		/// <para>Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.</para>
		/// </summary>
		/// <param name="form">The <see cref="Form"/> that shall be brought to the foreground</param>
		/// <returns>If the window was brought to the foreground, the return value is nonzero. If the window was not brought to the foreground, the return value is zero.</returns>
		/// <remarks>
		/// <para>The system restricts which processes can set the foreground window. A process can set the foreground window only if one of the following conditions is true:
		/// <list>
		/// <item>The process is the foreground process.</item>
		/// <item>The process was started by the foreground process.</item>
		/// <item>The process received the last input event.</item>
		/// <item>There is no foreground process.</item>
		/// <item>The process is being debugged.</item>
		/// <item>The foreground process is not a Modern Application or the Start Screen.</item>
		/// <item>The foreground is not locked.</item>
		/// <item>The foreground lock time-out has expired.</item>
		/// <item>No menus are active.</item>
		/// </list>
		/// </para>
		/// <para>An application cannot force a window to the foreground while the user is working with another window. Instead, Windows flashes the taskbar button of the window to notify the user.</para>
		/// </remarks>
		public static bool SetForegroundWindow(Form form)
		{
			return SetForegroundWindow(form.Handle);
		}

		#region Lock, Logoff, Shutdown, ...
		/// <summary>
		/// <para>Locks the workstation's display. Locking a workstation protects it from unauthorized use.</para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool LockWorkstation()
		{
			return LockWorkStation();
		}

		/// <summary>
		/// <para>Shuts down the system and turns off the power</para>
		/// <para><see cref="EWX.POWEROFF"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool Shutdown()
		{
			getShutdownPrivilege();
			return ExitWindowsEx((uint)EWX.POWEROFF, 0);
		}

		/// <summary>
		/// <para>Shuts down the system and turns off the power</para>
		/// <para>Beginning with Windows 8:  You can prepare the system for a faster startup.</para>
		/// <para><see cref="EWX.POWEROFF"/>, <see cref="EWX.HYBRID_SHUTDOWN"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool HybridShutdown()
		{
			if (Status.System.IsWindows8)
			{
				getShutdownPrivilege();
				return ExitWindowsEx((uint)(EWX.POWEROFF | EWX.HYBRID_SHUTDOWN), 0);
			}
			else
				return Shutdown();
		}

		/// <summary>
		/// <para>Shuts down the system and then restarts the system</para>
		/// <para><see cref="EWX.REBOOT"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool Reboot()
		{
			getShutdownPrivilege();
			return ExitWindowsEx((uint)EWX.REBOOT, 0);
		}

		/// <summary>
		/// <para>Shuts down the system and turns off the power</para>
		/// <para>The system forces running applications to stop when the allowed response time expires</para>
		/// <para><see cref="EWX.POWEROFF"/>, <see cref="EWX.FORCE"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool ForceShutdown()
		{
			getShutdownPrivilege();
			return ExitWindowsEx((uint)(EWX.FORCE | EWX.POWEROFF), 0);
		}

		/// <summary>
		/// <para>Shuts down the system and then restarts the system</para>
		/// <para>The system forces running applications to stop when the allowed response time expires</para>
		/// <para><see cref="EWX.REBOOT"/>, <see cref="EWX.FORCE"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool ForceReboot()
		{
			getShutdownPrivilege();
			return ExitWindowsEx((uint)(EWX.FORCE | EWX.REBOOT), 0);
		}

		/// <summary>
		/// <para>Shuts down the system and turns off the power</para>
		/// <para>Forces processes to terminate if they do not respond to the WM_QUERYENDSESSION or WM_ENDSESSION message within the timeout interval</para>
		/// <para><see cref="EWX.POWEROFF"/>, <see cref="EWX.FORCEIFHUNG"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool ForceShutdownIfHung()
		{
			getShutdownPrivilege();
			return ExitWindowsEx((uint)(EWX.FORCEIFHUNG | EWX.POWEROFF), 0);
		}

		/// <summary>
		/// <para>Shuts down the system and then restarts the system</para>
		/// <para>Forces processes to terminate if they do not respond to the WM_QUERYENDSESSION or WM_ENDSESSION message within the timeout interval</para>
		/// <para><see cref="EWX.REBOOT"/>, <see cref="EWX.FORCEIFHUNG"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool ForceRebootIfHung()
		{
			getShutdownPrivilege();
			return ExitWindowsEx((uint)(EWX.FORCEIFHUNG | EWX.REBOOT), 0);
		}

		/// <summary>
		/// <para>Shuts down all processes running in the logon session of the process that called the ExitWindowsEx function. Then it logs the user off.</para>
		/// <para><see cref="EWX.LOGOFF"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool Logoff()
		{
			getShutdownPrivilege();
			return ExitWindowsEx((uint)EWX.LOGOFF, 0);
		}
		#endregion
		#endregion
	}
}
