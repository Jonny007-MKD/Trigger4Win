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
		/// <summary>
		/// <para>This structure contains information about a set of privileges for an access token.</para>
		/// </summary>
		/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/aa379630%28v=vs.85%29.aspx</remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct TokenPrivileges
		{
			/// <summary>
			/// <para>This must be set to the number of entries in the Privileges array.</para>
			/// </summary>
			public int Count;
			public long Luid;
			public int Attr;
		}

		/// <summary>
		/// <para>ExitWindowsEx parameters</para>
		/// </summary>
		[Flags]
		internal enum EWX : uint
		{
			/// <summary>
			/// <para>Beginning with Windows 8:  You can prepare the system for a faster startup by combining the EWX_HYBRID_SHUTDOWN flag with the EWX_SHUTDOWN flag.</para>
			/// </summary>
			HybridShutdown = 0x00400000,
			/// <summary>
			/// <para>Shuts down all processes running in the logon session of the process that called the ExitWindowsEx function. Then it logs the user off.</para>
			/// <para>This flag can be used only by processes running in an interactive user's logon session.</para>
			/// </summary>
			LogOff = 0x00,
			/// <summary>
			/// <para>Shuts down the system and turns off the power. The system must support the power-off feature. </para>
			/// <para>The calling process must have the SE_SHUTDOWN_NAME privilege.</para>
			/// </summary>
			/// <remarks>To shut down or restart the system, the calling process must use the AdjustTokenPrivileges function to enable the SE_SHUTDOWN_NAME privilege.</remarks>
			PowerOff = 0x08,
			/// <summary>
			/// <para>Shuts down the system and then restarts the system.</para>
			/// <para>The calling process must have the SE_SHUTDOWN_NAME privilege.</para>
			/// </summary>
			/// <remarks>To shut down or restart the system, the calling process must use the AdjustTokenPrivileges function to enable the SE_SHUTDOWN_NAME privilege.</remarks>
			Reboot = 0x02,
			/// <summary>
			/// <para>Shuts down the system and then restarts it, as well as any applications that have been registered for restart using the RegisterApplicationRestart function. These application receive the WM_QUERYENDSESSION message with lParam set to the ENDSESSION_CLOSEAPP value. For more information, see Guidelines for Applications.</para>
			/// </summary>
			RestartApps = 0x40,
			/// <summary>
			/// <para>Shuts down the system to a point at which it is safe to turn off the power. All file buffers have been flushed to disk, and all running processes have stopped.</para>
			/// <para>The calling process must have the SE_SHUTDOWN_NAME privilege.</para>
			/// <para>Specifying this flag will not turn off the power even if the system supports the power-off feature. You must specify <see cref="POWEROFF"/> to do this.</para>
			/// </summary>
			/// <remarks>Windows XP with SP1:  If the system supports the power-off feature, specifying this flag turns off the power.</remarks>
			ShutDown = 0x01,
			/// <summary>
			/// <para>This flag has no effect if terminal services is enabled. Otherwise, the system does not send the WM_QUERYENDSESSION message. This can cause applications to lose data. Therefore, you should only use this flag in an emergency.</para>
			/// </summary>
			/// <remarks>During a shutdown or log-off operation, running applications are allowed a specific amount of time to respond to the shutdown request. If this time expires before all applications have stopped, the system displays a user interface that allows the user to forcibly shut down the system or to cancel the shutdown request. If the EWX_FORCE value is specified, the system forces running applications to stop when the time expires.</remarks>
			Force = 0x04,
			/// <summary>
			/// <para>Forces processes to terminate if they do not respond to the WM_QUERYENDSESSION or WM_ENDSESSION message within the timeout interval.</para>
			/// </summary>
			/// <remarks>If the EWX_FORCEIFHUNG value is specified, the system forces hung applications to close and does not display the dialog box.</remarks>
			ForceIfHung = 0x10,
		}

		/// <summary>
		/// <para>The thread's execution requirements.</para>
		/// </summary>
		[Flags]
		internal enum ExecutionState : uint
		{
			/// <summary>
			/// <para>Informs the system that the state being set should remain in effect until the next call that uses ES_CONTINUOUS and one of the other state flags is cleared.</para>
			/// </summary>
			Continuous =			0x80000000,
			/// <summary>
			/// <para>The opposite of Continuous, used for masking</para>
			/// </summary>
			NotContinuous =			0x7FFFFFFF,
			/// <summary>
			/// <para>Enables away mode. This value must be specified with ES_CONTINUOUS.</para>
			/// <para>Away mode should be used only by media-recording and media-distribution applications that must perform critical background processing on desktop computers while the computer appears to be sleeping.</para>
			/// </summary>
			AwayMode_Required =		0x00000040,
			/// <summary>
			/// <para>The opposite of AwayMode_Required, used for masking</para>
			/// </summary>
			AwayMode_NotRequired =	0xFFFFFFBF,
			/// <summary>
			/// <para>This value is not supported!</para>
			/// </summary>
			/// <remarks>Windows Server 2003 and Windows XP: Informs the system that a user is present and resets the display and system idle timers. UserPresent must be called with Continuous.</remarks>
			UserPresent =			0x00000004,
			/// <summary>
			/// <para>Forces the display to be on by resetting the display idle timer.</para>
			/// </summary>
			/// <remarks><para>Windows 8: This flag can only keep a display turned on, it can't turn on a display that's currently off.</para></remarks>
			Display_Required =		0x00000002,
			/// <summary>
			/// <para>The opposite of Display_Required, used for masking</para>
			/// </summary>
			Display_NotRequired =	0xFFFFFFFD,
			/// <summary>
			/// <para>Forces the system to be in the working state by resetting the system idle timer.</para>
			/// </summary>
			System_Required =		0x00000001,
			/// <summary>
			/// <para>The opposite of System_Required, used for masking</para>
			/// </summary>
			System_NotRequired =	0xFFFFFFFE,
			/// <summary>
			/// <para>No state at all</para>
			/// </summary>
			None =					0x00000000,
		}	
		#endregion

		#region Dll Imports
		/// <summary>
		/// <para>Locks the workstation's display. Locking a workstation protects it from unauthorized use.</para>
		/// </summary>
		/// <returns>
		/// <para>If the function succeeds, the return value is nonzero. Because the function executes asynchronously, a nonzero return value indicates that the operation has been initiated. It does not indicate whether the workstation has been successfully locked.</para>
		/// <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para>
		/// </returns>
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern bool LockWorkStation();

		/// <summary>
		/// <para>Logs off the interactive user, shuts down the system, or shuts down and restarts the system. It sends the WM_QUERYENDSESSION message to all applications to determine if they can be terminated.</para>
		/// </summary>
		/// <param name="uFlags">The shutdown type</param>
		/// <param name="dwReason">The reason for initiating the shutdown. This parameter must be one of the system shutdown reason codes.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is nonzero. Because the function executes asynchronously, a nonzero return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</para>
		/// <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para>
		/// </returns>
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern bool ExitWindowsEx(EWX uFlags, int dwReason);

		/// <summary>
		/// <para>Retrieves a pseudo handle for the current process.</para>
		/// </summary>
		/// <returns>The return value is a pseudo handle to the current process.</returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern IntPtr GetCurrentProcess();

		/// <summary>
		/// <para> The OpenProcessToken function opens the access token associated with a process.</para>
		/// </summary>
		/// <param name="handle">A handle to the process whose access token is opened. The process must have the PROCESS_QUERY_INFORMATION access permission.</param>
		/// <param name="desiredAccess">Specifies an access mask that specifies the requested types of access to the access token. These requested access types are compared with the discretionary access control list (DACL) of the token to determine which accesses are granted or denied.</param>
		/// <param name="phtoken">A pointer to a handle that identifies the newly opened access token when the function returns.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
		/// <remarks>Close the access token handle returned through the TokenHandle parameter by calling CloseHandle.</remarks>
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern bool OpenProcessToken(IntPtr handle, int desiredAccess, ref IntPtr phToken);

		/// <summary>
		/// <para>The LookupPrivilegeValue function retrieves the locally unique identifier (LUID) used on a specified system to locally represent the specified privilege name.</para>
		/// </summary>
		/// <param name="host">A pointer to a null-terminated string that specifies the name of the system on which the privilege name is retrieved. If a null string is specified, the function attempts to find the privilege name on the local system.</param>
		/// <param name="name">A pointer to a null-terminated string that specifies the name of the privilege, as defined in the Winnt.h header file. For example, this parameter could specify the constant, SE_SECURITY_NAME, or its corresponding string, "SeSecurityPrivilege".</param>
		/// <param name="pluid">A pointer to a variable that receives the LUID by which the privilege is known on the system specified by the lpSystemName parameter.</param>
		/// <returns>If the function succeeds, the function returns nonzero. If the function fails, it returns zero. To get extended error information, call GetLastError.</returns>
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

		/// <summary>
		/// <para>The AdjustTokenPrivileges function enables or disables privileges in the specified access token. Enabling or disabling privileges in an access token requires TOKEN_ADJUST_PRIVILEGES access.</para>
		/// </summary>
		/// <param name="hToken">A handle to the access token that contains the privileges to be modified. The handle must have TOKEN_ADJUST_PRIVILEGES access to the token. If the PreviousState parameter is not NULL, the handle must also have TOKEN_QUERY access.</param>
		/// <param name="disableAllPrivileges">Specifies whether the function disables all of the token's privileges. If this value is TRUE, the function disables all privileges and ignores the NewState parameter. If it is FALSE, the function modifies privileges based on the information pointed to by the NewState parameter.</param>
		/// <param name="newState">A pointer to a TOKEN_PRIVILEGES structure that specifies an array of privileges and their attributes. If the DisableAllPrivileges parameter is FALSE, the AdjustTokenPrivileges function enables, disables, or removes these privileges for the token. The following table describes the action taken by the AdjustTokenPrivileges function, based on the privilege attribute.</param>
		/// <param name="len">Specifies the size, in bytes, of the buffer pointed to by the <paramref name="previousState"/> parameter. This parameter can be zero if the <paramref name="previousState"/> parameter is NULL</param>
		/// <param name="previousState">A pointer to a buffer that the function fills with a <see cref="TokenPrivileges"/> structure that contains the previous state of any privileges that the function modifies. That is, if a privilege has been modified by this function, the privilege and its previous state are contained in the TOKEN_PRIVILEGES structure referenced by PreviousState. If the Count member of <see cref="TokenPrivileges"/> is zero, then no privileges have been changed by this function. This parameter can be NULL.</param>
		/// <param name="returnLen">A pointer to a variable that receives the required size, in bytes, of the buffer pointed to by the <paramref name="previousState"/> parameter. This parameter can be NULL if <paramref name="previousState"/> is NULL.</param>
		/// <returns></returns>
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern bool AdjustTokenPrivileges(IntPtr hToken, bool disableAllPrivileges, ref TokenPrivileges newState, int len, IntPtr previousState, IntPtr returnLen);

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

		/// <summary>
		/// <para>Enables an application to inform the system that it is in use, thereby preventing the system from entering sleep or turning off the display while the application is running.</para>
		/// </summary>
		/// <param name="es"></param>
		/// <returns>If the function succeeds, the return value is the previous thread execution state, otherwise NULL</returns>
		/// <remarks>The system automatically detects activities such as local keyboard or mouse input, server activity, and changing window focus. Activities that are not automatically detected include disk or CPU activity and video display.</para>
		/// <para>Calling <see cref="SetThreadExecutionState"/> without Continues simply resets the idle timer; to keep the display or system in the working state, the thread must call <see cref="SetThreadExecutionState"/> periodically.</para>
		/// <para></para>To run properly on a power-managed computer, applications such as fax servers, answering machines, backup agents, and network management applications must use both System_Required and Continuous when they process events. Multimedia applications, such as video players and presentation applications, must use Display_Required when they display video for long periods of time without user input. Applications such as word processors, spreadsheets, browsers, and games do not need to call <see cref="SetThreadExecutionState"/>.</para>
		/// <para>The AwayMode_Required value should be used only when absolutely necessary by media applications that require the system to perform background tasks such as recording television content or streaming media to other devices while the system appears to be sleeping. Applications that do not require critical background processing or that run on portable computers should not enable away mode because it prevents the system from conserving power by entering true sleep.</para>
		/// </remarks>
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		internal static extern ExecutionState SetThreadExecutionState(ExecutionState es);
		#endregion

		#region Methods
		/// <summary>
		/// <para>Gets the shutdown privilege for this process</para>
		/// <para><see cref="EWX"/></para>
		/// </summary>
		internal static void getShutdownPrivilege()
		{
			if (hasShutdownPrivilege)
				return;
			bool ok;
			TokenPrivileges tp;
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
			return ExitWindowsEx(EWX.PowerOff, 0);
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
				return ExitWindowsEx(EWX.PowerOff | EWX.HybridShutdown, 0);
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
			return ExitWindowsEx(EWX.Reboot, 0);
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
			return ExitWindowsEx(EWX.Force | EWX.PowerOff, 0);
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
			return ExitWindowsEx(EWX.Force | EWX.Reboot, 0);
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
			return ExitWindowsEx(EWX.ForceIfHung | EWX.PowerOff, 0);
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
			return ExitWindowsEx(EWX.ForceIfHung | EWX.Reboot, 0);
		}

		/// <summary>
		/// <para>Shuts down all processes running in the logon session of the process that called the ExitWindowsEx function. Then it logs the user off.</para>
		/// <para><see cref="EWX.LOGOFF"/></para>
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. Because the function executes asynchronously, a true return value indicates that the shutdown has been initiated. It does not indicate whether the shutdown will succeed. It is possible that the system, the user, or another application will abort the shutdown.</returns>
		public static bool Logoff()
		{
			getShutdownPrivilege();
			return ExitWindowsEx(EWX.LogOff, 0);
		}
		#endregion

		#region User Inactivity: ExecutionState
		/// <summary>
		/// <para>This allows the system to power down the display according to the power plan.</para>
		/// </summary>
		public void DisplayPowerdownAllow()
		{
			ExecutionState prev = SetThreadExecutionState(ExecutionState.Continuous);		// Reset all ExecutionStates

			if ((prev & ExecutionState.NotContinuous & ExecutionState.Display_NotRequired) != ExecutionState.None)		// If previous ExecutionState was not only Display_Required
				SetThreadExecutionState(prev & ExecutionState.Display_NotRequired | ExecutionState.Continuous);			// preserve that state
		}

		/// <summary>
		/// <para>This prevents the system from powering down the display. It will be kept on until <see cref="DisplayPowerdownAllow"/> is called, then the user inactivity timer starts according to the power plan.</para>
		/// </summary>
		public void DisplayPowerdownPrevent()
		{
			ExecutionState prev = SetThreadExecutionState(ExecutionState.Display_Required | ExecutionState.Continuous);		// Set display required

			if ((prev & ExecutionState.NotContinuous & ExecutionState.Display_NotRequired) != ExecutionState.None)		// If previous ExecutionState contained other states
				SetThreadExecutionState(prev | ExecutionState.Display_Required | ExecutionState.Continuous);			// preserve that state and add display state
		}

		/// <summary>
		/// <para>This resets the timer that will power down the display after a certain amount of user inactivity.</para>
		/// </summary>
		public void DisplayPowerdownResetTimer()
		{
			SetThreadExecutionState(ExecutionState.Display_Required);
		}

		/// <summary>
		/// <para>This prevents the system from going to sleep mode, even if the user tells it to. Instead the system will only appear to be sleeping and perform task in the background (such as video recording or heavy calculation operations)</para>
		/// </summary>
		public bool SleepPrevent()
		{
			ExecutionState prev = SetThreadExecutionState(ExecutionState.AwayMode_Required | ExecutionState.Continuous);

			if (prev == ExecutionState.None)
				return false;

			if ((prev & ExecutionState.NotContinuous & ExecutionState.AwayMode_NotRequired) != ExecutionState.None)		// If previous ExecutionState contained other states
				prev = SetThreadExecutionState(prev | ExecutionState.AwayMode_Required | ExecutionState.Continuous);			// preserve that state and add awaymode state

			return prev != ExecutionState.None;
		}

		/// <summary>
		/// <para>This allows the system to go to sleep when the user or the power plan wishes to (see <see cref="SleepPrevent"/></para>
		/// </summary>
		public bool SleepAllow()
		{
			ExecutionState prev = SetThreadExecutionState(ExecutionState.Continuous);		// Reset all ExecutionStates

			if (prev == ExecutionState.None)
				return false;

			if ((prev & ExecutionState.NotContinuous & ExecutionState.AwayMode_NotRequired) != ExecutionState.None)		// If previous ExecutionState was not only AwayMode_Required
				prev = SetThreadExecutionState(prev & ExecutionState.AwayMode_NotRequired | ExecutionState.Continuous);			// preserve that state

			return prev != ExecutionState.None;
		}

		/// <summary>
		/// <para>This allows the system to power down according to the power plan.</para>
		/// </summary>
		public void SystemPowerdownAllow()
		{
			ExecutionState prev = SetThreadExecutionState(ExecutionState.Continuous);		// Reset all ExecutionStates

			if ((prev & ExecutionState.NotContinuous & ExecutionState.System_NotRequired) != ExecutionState.None)		// If previous ExecutionState was not only System_Required
				SetThreadExecutionState(prev & ExecutionState.System_NotRequired | ExecutionState.Continuous);			// preserve that state
		}

		/// <summary>
		/// <para>This prevents the system from powering down. It will be kept on until <see cref="SystemPowerdownAllow"/> is called, then the user inactivity timer starts according to the power plan.</para>
		/// </summary>
		public void SystemPowerdownPrevent()
		{
			ExecutionState prev = SetThreadExecutionState(ExecutionState.System_Required | ExecutionState.Continuous);		// Set system required

			if ((prev & ExecutionState.NotContinuous & ExecutionState.System_NotRequired) != ExecutionState.None)		// If previous ExecutionState contained other states
				SetThreadExecutionState(prev | ExecutionState.System_Required | ExecutionState.Continuous);			// preserve that state and add display state
		}

		/// <summary>
		/// <para>This resets the timer that will power down the system after a certain amount of user inactivity.</para>
		/// </summary>
		public void SystemPowerdownResetTimer()
		{
			SetThreadExecutionState(ExecutionState.System_Required);
		}
		#endregion
		#endregion
	}
}
