using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace Tasker.Tasks
{
	class LogNetworkEvents : TaskPlugin
	{
		internal Log Log;

		#region Methods
		public override bool Init(Main Main)
		{
			return Init(Main, new System.Diagnostics.Stopwatch());
		}
		public override bool Init(Main Main, System.Diagnostics.Stopwatch swInit)
		{
			this.Log = Main.Log;

			if (!Main.EventMgr.PluginExists<Events.Network>())
			{
				this.Log.LogLine("Task \"LogNetworkEvents\" is missing EventPlugin \"Network\"!", Log.Type.Error);
				return false;
			}

			swInit.Stop();
			Events.Network networkEvents = Main.EventMgr.GetPlugin<Events.Network>();
			swInit.Start();

			networkEvents.NetworkAvailabilityChanged += networkEvents_NetworkAvailabilityChanged;
			networkEvents.NetworkInterfaceAdded += networkEvents_NetworkInterfaceAdded;
			networkEvents.NetworkInterfaceRemoved += networkEvents_NetworkInterfaceRemoved;
			networkEvents.IpAddressChanged += networkEvents_IpAddrChanged;
			return true;
		}
		#endregion


		#region Event handlers
		private void networkEvents_IpAddrChanged(object sender, Events.EventArgsValues<NetworkInterface> e)
		{
			this.Log.LogLineDate("IP changed: " + e.OldValue.Name + ":", Log.Type.NetworkEvent);
			UnicastIPAddressInformationCollection ipsOld = e.OldValue.GetIPProperties().UnicastAddresses;
			UnicastIPAddressInformationCollection ipsNew = e.NewValue.GetIPProperties().UnicastAddresses;
			for (int i = 0; i < Math.Max(ipsOld.Count, ipsNew.Count); i++)
			{
				if (ipsOld.Count > i)
					this.Log.LogTextDate(ipsOld[i].Address.ToString().PadRight(40), Log.Type.NetworkEvent);
				else
					this.Log.LogTextDate(new String(' ', 40), Log.Type.NetworkEvent);
				this.Log.LogText(" | ", Log.Type.NetworkEvent);
				if (ipsNew.Count > i)
					this.Log.LogText(ipsNew[i].Address.ToString(), Log.Type.NetworkEvent);
				this.Log.Line(Log.Type.NetworkEvent);
			}
		}

		private void networkEvents_NetworkInterfaceRemoved(object sender, Events.EventArgsValue<NetworkInterface> e)
		{
			this.Log.LogLineDate("Interface removed: " + e.Value.Name + "(" + e.Value.Id + ")", Log.Type.NetworkEvent);
		}

		private void networkEvents_NetworkInterfaceAdded(object sender, Events.EventArgsValue<NetworkInterface> e)
		{
			this.Log.LogLineDate("Interface added: " + e.Value.Name + "(" + e.Value.Id + ")", Log.Type.NetworkEvent);
		}

		private void networkEvents_NetworkAvailabilityChanged(object sender, Events.EventArgsValue<bool> e)
		{
			this.Log.LogLineDate("Network available: " + e.Value, Log.Type.NetworkEvent);
		}
		#endregion
	}
}