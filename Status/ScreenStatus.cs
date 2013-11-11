using System;
using Systemm = System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Forms = System.Windows.Forms;
using System.Runtime.InteropServices;
using Tasker.Classes.Screen;

namespace Tasker.Status
{
	static class Screen
	{
		#region DllImports
		// PInvoke declaration for EnumDisplaySettings Win32 API
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref ScreenSettingsDevMode lpDevMode);
		#endregion

		#region Properties
		#region Screen
		/// <summary><para>Gets a list of all displays on the system</para></summary>
		public static List<ScreenEx> AllScreens
		{
			get
			{
				List<Forms.Screen> allScreens = new List<Forms.Screen>(Forms.Screen.AllScreens);
				List<ScreenEx> allScreensEx = new List<ScreenEx>(allScreens.Count);
				allScreens.ForEach(new Action<Forms.Screen>(screen => allScreensEx.Add(GetScreenSettings(screen))));
				return allScreensEx;
			}
		}
		/// <summary><para>Gets the primary screen</para></summary>
		public static ScreenEx PrimaryScreen
		{
			get
			{
				return GetScreenSettings(Forms.Screen.PrimaryScreen);
			}
		}

		/// <summary>
		/// <para>Gets the orientation of the screen</para>
		/// <para>But which screen?</para>
		/// </summary>
		public static ScreenOrientation ScreenOrientation
		{
			get
			{
				return SystemInformation.ScreenOrientation;
			}
		}
		#endregion
		#endregion

		#region Methods
		/// <summary>
		/// <para>Returns the current System status</para>
		/// </summary>
		/// <returns></returns>
		public static TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Screens (" + AllScreens.Count + ")");
			AllScreens.ForEach(new Action<ScreenEx>(screen =>
			{
				TreeNode tnScreen = tnMain.Nodes.Add(screen.DeviceName);
				tnScreen.Nodes.Add("Primary: " + screen.Primary.ToString());
				tnScreen.Nodes.Add("Color depth: " + screen.BitsPerPixel + "bit");
				tnScreen.Nodes.Add("Resolution: " + screen.Bounds.Width + "x" + screen.Bounds.Height);
				tnScreen.Nodes.Add("Position: " + screen.Bounds.X + " | " + screen.Bounds.Y);
				tnScreen.Nodes.Add("Display orientation: " + screen.Orientation.ToString());
				tnScreen.Nodes.Add("Refresh rate: " + screen.Frequency);
			}));
			return tnMain;
		}

		public static ScreenEx GetScreenSettings(Forms.Screen screen)
		{
			ScreenSettingsDevMode DevMode = new ScreenSettingsDevMode(true);
			if (EnumDisplaySettings(screen.DeviceName, -1 /*current settings*/, ref DevMode) == 0)
				throw new Exception("EnumDisplaySettings (user32.dll) returned 0");
			ScreenEx screenSettings = new ScreenEx(screen, DevMode);
			return screenSettings;
		}
		#endregion
	}
}
