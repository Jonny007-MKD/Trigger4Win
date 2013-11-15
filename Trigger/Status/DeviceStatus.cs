using System.Collections.Generic;
using Trigger.Classes.Device;
using System.Windows.Forms;
using System;

namespace Trigger.Status
{
	/// <summary>
	/// <para>Provides methods to get the status of the system regarding devices</para>
	/// </summary>
	public static class Device
	{
		/// <summary>
		/// <para>Gets all available disks on the system</para>
		/// </summary>
		public static List<StorageDisk> AvailableDisks
		{
			get
			{
				return StorageDisk.GetAvailableDisks();
			}
		}

		/// <summary>
		/// <para>Gets the status related to devices</para>
		/// </summary>
		/// <returns></returns>
		public static TreeNode GetStatus()
		{
			List<StorageDisk> availableDisks = AvailableDisks;
			TreeNode tnMain = new TreeNode("Device (" + availableDisks.Count + ")");
			availableDisks.ForEach(new Action<StorageDisk>(sd => tnMain.Nodes.Add(sd.ToString())));

			return tnMain;
		}
	}
}
