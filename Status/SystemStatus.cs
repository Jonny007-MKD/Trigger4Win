using System;
using Systemm = System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tasker.Status
{
	static class System
	{
		#region Properties
		#region Fonts
		/// <summary>
		/// <para>Gets a list with all installed <see cref="FontFamily"/>s</para>
		/// </summary>
		public static List<FontFamily> InstalledFonts
		{
			get
			{
				InstalledFontCollection fonts = new InstalledFontCollection();
				return new List<FontFamily>(fonts.Families);
			}
		}
		#endregion

		#region Power
		/// <summary>
		/// <para>Gets the current system power status</para>
		/// </summary>
		public static PowerLineStatus PowerLineStatus
		{
			get
			{
				return SystemInformation.PowerStatus.PowerLineStatus;
			}
		}

		/// <summary>
		/// <para>Gets the current battery charge status.</para>
		/// </summary>
		public static BatteryChargeStatus BatteryChargeStatus
		{
			get
			{
				return SystemInformation.PowerStatus.BatteryChargeStatus;
			}
		}

		/// <summary>
		/// <para>Checks whether a battery is available on the system</para>
		/// <para>May be null if the <see cref="Systemm.Windows.Forms.BatteryChargeStatus"/> is unknown</para>
		/// </summary>
		public static bool? BatteryAvailable
		{
			get
			{
				BatteryChargeStatus bcs = BatteryChargeStatus;
				if (bcs == Systemm.Windows.Forms.BatteryChargeStatus.Unknown)
					return null;
				return bcs != Systemm.Windows.Forms.BatteryChargeStatus.NoSystemBattery;
			}
		}


		/// <summary>
		/// <para>Gets the reported full charge lifetime of the primary battery power source</para>
		/// </summary>
		public static TimeSpan BatteryFullLifetime
		{
			get
			{
				return new TimeSpan(0, 0, SystemInformation.PowerStatus.BatteryFullLifetime);
			}
		}

		/// <summary>
		/// <para>Gets the approximate amount of full battery charge remaining</para>
		/// </summary>
		public static byte BatteryLifePercent
		{
			get
			{
				return (byte)(SystemInformation.PowerStatus.BatteryLifePercent*100);
			}
		}

		/// <summary>
		/// <para>Gets the approximate <see cref="TimeSpan"/> of battery time remaining.</para>
		/// </summary>
		public static TimeSpan BatteryLifeRemaining
		{
			get
			{
				int life = SystemInformation.PowerStatus.BatteryLifeRemaining;
				if (life >= 0)
					return new TimeSpan(0, 0, life);
				else
					return new TimeSpan();
			}
		}
		#endregion

		#region User
		/// <summary>
		/// <para>Gets the name of the domain the user belongs to</para>
		/// </summary>
		public static string UserDomainName
		{
			get
			{
				return SystemInformation.UserDomainName;
			}
		}

		/// <summary>
		/// <para>Gets the user name associated with the current thread</para>
		/// </summary>
		public static string UserName
		{
			get
			{
				return SystemInformation.UserName;
			}
		}
		#endregion

		#region OSVersion
		/// <summary>
		/// <para>Information about the <see cref="OperationSystem"/> that we are running on</para>
		/// </summary>
		public static OperatingSystem OperatingSystem
		{
			get
			{
				return Environment.OSVersion;
			}
		}

		/// <summary>
		/// <para>The <see cref="Version"/> of this OS</para>
		/// </summary>
		public static Version WindowsVersion
		{
			get
			{
				return Environment.OSVersion.Version;
			}
		}

		/// <summary>
		/// <para>Whether this is Windows 8.1</para>
		/// </summary>
		public static bool IsWindows81
		{
			get
			{
				return WindowsVersion.Major == 6 && WindowsVersion.Minor == 3;
			}
		}

		/// <summary>
		/// <para>Whether this is Windows 8</para>
		/// </summary>
		public static bool IsWindows8
		{
			get
			{
				return WindowsVersion.Major == 6 && WindowsVersion.Minor == 2;
			}
		}

		/// <summary>
		/// <para>Whether this is Windows 7</para>
		/// </summary>
		public static bool IsWindows7
		{
			get
			{
				return WindowsVersion.Major == 6 && WindowsVersion.Minor == 1;
			}
		}

		/// <summary>
		/// <para>Whether this is Windows Vista</para>
		/// </summary>
		public static bool IsWindowsVista
		{
			get
			{
				return WindowsVersion.Major == 6 && WindowsVersion.Minor == 0;
			}
		}

		/// <summary>
		/// <para>Whether this is Windows XP</para>
		/// </summary>
		public static bool IsWindowsXP
		{
			get
			{
				return WindowsVersion.Major == 5 && WindowsVersion.Minor == 1;
			}
		}

		/// <summary>
		/// <para>Whether this is Windows 2000</para>
		/// </summary>
		public static bool IsWindows2000
		{
			get
			{
				return WindowsVersion.Major == 5 && WindowsVersion.Minor == 0;
			}
		}

		/// <summary>
		/// <para>Whether the current process is running as 32bit process</para>
		/// </summary>
		public static bool Is64BitProcess
		{
			get
			{
				return Environment.Is64BitProcess;
			}
		}

		/// <summary>
		/// <para>Checks whether the operating system is 64bit</para>
		/// </summary>
		public static bool Is64BitOS
		{
			get
			{
				return Environment.Is64BitOperatingSystem;
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
			TreeNode tnMain = new TreeNode("System");
			tnMain.Nodes.Add("Windows version: " + WindowsVersion.ToString());
			tnMain.Nodes.Add("Is 64bit OS: " + Is64BitOS.ToString());
			tnMain.Nodes.Add("Fonts installed: " + InstalledFonts.Count);
			tnMain.Nodes.Add("Power Line Status: " + PowerLineStatus.ToString());
			tnMain.Nodes.Add("Battery charge status: " + BatteryChargeStatus.ToString());
			tnMain.Nodes.Add("Battery available: " + BatteryAvailable.ToString());
			tnMain.Nodes.Add("Battery full life time: " + BatteryFullLifetime.ToString());
			tnMain.Nodes.Add("Battery remaining life time: " + BatteryLifeRemaining.ToString());
			tnMain.Nodes.Add("Battery charge percent: " + BatteryLifePercent + "%");
			tnMain.Nodes.Add("User name: " + UserName);
			tnMain.Nodes.Add("User domain name: " + UserDomainName);
			return tnMain;
		}
		#endregion
	}
}
