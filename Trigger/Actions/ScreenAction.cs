using System.Runtime.InteropServices;
using Trigger.Classes.Screen;
using System;

namespace Trigger.Actions
{
	/// <summary>
	/// <para>Actions dealing with screens and screen settings</para>
	/// </summary>
	public class Screen
	{
		#region Enums
		internal enum DispChange : sbyte
		{
			/// <summary><para>The settings change was successful.</para></summary>
			SUCCESSFUL = 0,
			/// <summary><para>The computer must be restarted for the graphics mode to work.</para></summary>
			RESTART = 1,
			/// <summary><para>The display driver failed the specified graphics mode.</para></summary>
			FAILED = -1,
			/// <summary><para>The graphics mode is not supported.</para></summary>
			BADMODE = -2,
			/// <summary><para>Unable to write settings to the registry.</para></summary>
			NOTUPDATED = -3,
			/// <summary><para>An invalid set of flags was passed in.</para></summary>
			BADFLAGS = -4,
			/// <summary><para>An invalid parameter was passed in. This can include an invalid flag or combination of flags.</para></summary>
			BADPARAM = -5,
			/// <summary><para>The settings change was unsuccessful because the system is DualView capable.</para></summary>
			BADDUALVIEW = -6,
		}
		/// <summary>
		/// <para>Indicates how the graphics mode should be changed</para>
		/// </summary>
		[Flags]
		internal enum CDS : int
		{
			///<summary><para>The mode is temporary in nature.</para><para>If you change to and from another desktop, this mode will not be reset.</para></summary>
			FULLSCREEN = 0x00000004,
			///<summary><para>The settings will be saved in the global settings area so that they will affect all users on the machine. Otherwise, only the settings for the user are modified. This flag is only valid when specified with the UPDATEREGISTRY flag.</para></summary>
			GLOBAL = 0x00000008,
			///<summary><para>The settings will be saved in the registry, but will not take effect. This flag is only valid when specified with the UPDATEREGISTRY flag.</para></summary>
			NORESET = 0x10000000,
			///<summary><para>The settings should be changed, even if the requested settings are the same as the current settings.</para></summary>
			RESET = 0x40000000,
			/// <summary></summary>
			RESET_EX = 0x20000000,
			///<summary><para>This device will become the primary device.</para></summary>
			SET_PRIMARY = 0x00000010,
			///<summary><para>The system tests if the requested graphics mode could be set.</para></summary>
			TEST = 0x00000002,
			///<summary><para>The graphics mode for the current screen will be changed dynamically and the graphics mode will be updated in the registry. The mode information is stored in the USER profile.</para></summary>
			UPDATEREGISTRY = 0x00000001,
			///<summary><para>When set, the lParam parameter is a pointer to a VIDEOPARAMETERS structure.</para></summary>
			VIDEOPARAMETERS = 0x00000020,
			///<summary><para>Enables settings changes to unsafe graphics modes.</para></summary>
			ENABLE_UNSAFE_MODES = 0x00000100,
			///<summary><para>Disables settings changes to unsafe graphics modes.</para></summary>
			DISABLE_UNSAFE_MODES = 0x00000200,
		}
		#endregion

		#region Dll imports
		/// <summary>
		/// <para>The ChangeDisplaySettingsEx function changes the settings of the specified display device to the specified graphics mode.</para>
		/// </summary>
		/// <param name="lpszDeviceName">
		/// <para>String that specifies the display device whose graphics mode will change. Only display device names as returned by EnumDisplayDevices are valid.</para>
		/// <para>The <paramref name="lpszDeviceName"/> parameter can be NULL. A NULL value specifies the default display device. The default device can be determined by calling EnumDisplayDevices and checking for the DISPLAY_DEVICE_PRIMARY_DEVICE flag.</para>
		/// </param>
		/// <param name="lpDevMode"><para>A pointer to a <see cref="ScreenSettingsDevMode"/> structure that describes the new graphics mode. If <paramref name="lpDevMode"/> is NULL, all the values currently in the registry will be used for the display setting. Passing NULL for the <paramref name="lpDevMode"/> parameter and 0 for the <paramref name="dwFlags"/> parameter is the easiest way to return to the default mode after a dynamic mode change.</para></param>
		/// <param name="hwnd"></param>
		/// <param name="dwFlags"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		internal static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref ScreenSettingsDevMode lpDevMode, IntPtr hwnd, int dwFlags, IntPtr lParam);
		#endregion

		#region Methods
		/// <summary>
		/// <para>The ChangeDisplaySettingsEx function changes the settings of the specified display device to the specified graphics mode.</para>
		/// <para>Use this one if you are not sure</para>
		/// </summary>
		/// <param name="deviceName">
		/// <para>String that specifies the display device whose graphics mode will change. Only display device names as returned by EnumDisplayDevices are valid.</para>
		/// <para>The <paramref name="deviceName"/> parameter can be NULL. A NULL value specifies the default display device. The default device can be determined by calling EnumDisplayDevices and checking for the DISPLAY_DEVICE_PRIMARY_DEVICE flag.</para>
		/// </param>
		/// <param name="devMode"><para>A pointer to a <see cref="ScreenSettingsDevMode"/> structure that describes the new graphics mode. If <paramref name="devMode"/> is NULL, all the values currently in the registry will be used for the display setting. Passing NULL for the <paramref name="devMode"/> parameter and 0 for the <paramref name="dwFlags"/> parameter is the easiest way to return to the default mode after a dynamic mode change.</para></param>
		/// <param name="dwFlags"></param>
		/// <returns></returns>
		internal static DispChange ChangeDisplaySettingsEx(string deviceName, ref ScreenSettingsDevMode devMode, CDS dwFlags)
		{
			return (DispChange)ChangeDisplaySettingsEx(deviceName, ref devMode, IntPtr.Zero, (int)dwFlags, IntPtr.Zero);
		}

		/// <summary>
		/// <para>Updates the screen's settings</para>
		/// </summary>
		/// <param name="screen"></param>
		/// <returns></returns>
		public static bool UpdateScreen(ScreenEx screen)
		{
			ScreenSettingsDevMode devMode = screen.ToDEVMODE();
			DispChange result = ChangeDisplaySettingsEx(screen.Name, ref devMode, CDS.RESET | CDS.UPDATEREGISTRY);
			if (result >= 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// <para>Updates the screen's settings and sets it as primary screen</para>
		/// </summary>
		/// <param name="screen"></param>
		/// <returns></returns>
		public static bool UpdateScreenAndMakePrimary(ScreenEx screen)
		{
			ScreenSettingsDevMode devMode = screen.ToDEVMODE();
			return ChangeDisplaySettingsEx(screen.Name, ref devMode, CDS.RESET | CDS.UPDATEREGISTRY | CDS.SET_PRIMARY) >= 0;
		}
		#endregion
	}
}