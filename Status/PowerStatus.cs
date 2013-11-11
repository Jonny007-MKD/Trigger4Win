using System;
using Systemm = System;
using System.Windows.Forms;
using Tasker.Classes.Power;
using System.Runtime.InteropServices;
using System.Text;
using System.ComponentModel;

namespace Tasker.Status
{
	static class Power
	{
		#region Properties
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
				int fullLifeTime = SystemInformation.PowerStatus.BatteryFullLifetime;
				int lifeRemaining = SystemInformation.PowerStatus.BatteryLifeRemaining;
				if (fullLifeTime == -1 && lifeRemaining != -1)
					return new TimeSpan(0, 0, (int)(lifeRemaining / SystemInformation.PowerStatus.BatteryLifePercent));
				else
					return new TimeSpan(0, 0, fullLifeTime);
			}
		}

		/// <summary>
		/// <para>Gets the approximate amount of full battery charge remaining</para>
		/// </summary>
		public static byte BatteryLifePercent
		{
			get
			{
				return (byte)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
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
				return new TimeSpan(0, 0, life);
			}
		}

		

		public static PowerScheme ActivePowerScheme
		{
			get
			{
				return PowerScheme.Active;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Returns the current Power status</para>
		/// </summary>
		/// <returns></returns>
		public static TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Power");
			tnMain.Nodes.Add("Active power scheme: " + ActivePowerScheme.Name + " (" + ActivePowerScheme.Guid.ToString() + ")");
			tnMain.Nodes.Add("Power Line Status: " + PowerLineStatus.ToString());
			tnMain.Nodes.Add("Battery charge status: " + BatteryChargeStatus.ToString());
			tnMain.Nodes.Add("Battery available: " + BatteryAvailable.ToString());
			tnMain.Nodes.Add("Battery full life time: " + BatteryFullLifetime.ToString());
			tnMain.Nodes.Add("Battery remaining life time: " + BatteryLifeRemaining.ToString());
			tnMain.Nodes.Add("Battery charge percent: " + BatteryLifePercent + "%");
			return tnMain;
		}
		#endregion
	}
}
