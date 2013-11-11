using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Trigger.Events
{
	sealed class Network : EventPlugin
	{
		#region Properties
		private enum EventType : byte
		{
			NetworkAvailabilityChanged,
			NetworkInterfaceAdded = 0,
			NetworkInterfaceRemoved = 1,
			IpAddrChanged = 2,
			NetworkInterfaces,
		}

		/// <summary>A dictionary containing all the values before the event occurs</summary>
		private Dictionary<EventType, object> oldValues = new Dictionary<EventType, object>();

		/// <summary>Whether the event <see cref="NetworkAddressChanged"/></summary>
		private byte networkAddressChangedEnabled;
		private bool NetworkAddressChangedEnabled
		{
			set
			{
				if (value)
					networkAddressChangedEnabled++;
				else
					networkAddressChangedEnabled--;
				if (value && networkAddressChangedEnabled == 1)
					NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
				else if (networkAddressChangedEnabled == 0)
					NetworkChange.NetworkAddressChanged -= NetworkChange_NetworkAddressChanged;
			}
			get
			{
				return networkAddressChangedEnabled > 0;
			}
		}
		private byte networkAvailabilityChangedEnabled;
		private bool NetworkAvailabilityChangedEnabled
		{
			set
			{
				if (value)
					networkAvailabilityChangedEnabled++;
				else
					networkAvailabilityChangedEnabled--;
				if (value && networkAvailabilityChangedEnabled == 1)
					NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
				else if (networkAvailabilityChangedEnabled == 0)
					NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAvailabilityChanged;
			}
			get
			{
				return networkAvailabilityChangedEnabled > 0;
			}
		}

		#region Events
		#region NetworkAvailabilityChanged
		private EventPlugin.EventValue<bool> OnNetworkAvailabilityChanged;
		public event EventPlugin.EventValue<bool> NetworkAvailabilityChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.NetworkAvailabilityChanged))
					oldValues[EventType.NetworkAvailabilityChanged] = Status.Network.IsNetworkAvailable;
				this.OnNetworkAvailabilityChanged += value;
				NetworkAvailabilityChangedEnabled = true;
			}
			remove
			{
				this.OnNetworkAvailabilityChanged -= value;
				NetworkAvailabilityChangedEnabled = false;
			}
		}
		#endregion

		#region NetworkAddressChanged
		private EventPlugin.Event OnNetworkAddressChanged;
		/// <summary><para>Occurs when the IP address of a network interface changes.</para></summary>
		public event EventPlugin.Event NetworkAddressChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.NetworkInterfaces))
					oldValues[EventType.NetworkInterfaces] = Status.Network.AllNetworkInterfaces;
				this.OnNetworkAddressChanged += value;
				NetworkAddressChangedEnabled = true;
			}
			remove
			{
				this.OnNetworkAddressChanged -= value;
				NetworkAddressChangedEnabled = false;	// does not have to occur
			}
		}
		#endregion
		#region - NetworkInterfaceAdded
		private EventPlugin.EventValue<NetworkInterface> OnNetworkInterfaceAdded;
		/// <summary><para>Occurs when a new network interface was detected</para></summary>
		public event EventPlugin.EventValue<NetworkInterface> NetworkInterfaceAdded
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.NetworkInterfaces))
					oldValues[EventType.NetworkInterfaces] = Status.Network.AllNetworkInterfaces;
				this.OnNetworkInterfaceAdded += value;
				NetworkAddressChangedEnabled = true;
			}
			remove
			{
				this.OnNetworkInterfaceAdded -= value;
				NetworkAddressChangedEnabled = false;	// does not have to occur
			}
		}
		#endregion
		#region - NetworkInterfaceRemoved
		private EventPlugin.EventValue<NetworkInterface> OnNetworkInterfaceRemoved;
		/// <summary><para>Occurs when a network interface was removed</para></summary>
		public event EventPlugin.EventValue<NetworkInterface> NetworkInterfaceRemoved
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.NetworkInterfaces))
					oldValues[EventType.NetworkInterfaces] = Status.Network.AllNetworkInterfaces;
				this.OnNetworkInterfaceRemoved += value;
				NetworkAddressChangedEnabled = true;
			}
			remove
			{
				this.OnNetworkInterfaceRemoved -= value;
				NetworkAddressChangedEnabled = false;	// does not have to occur
			}
		}
		#endregion
		#region - IpAddressChanged
		private EventPlugin.EventValues<NetworkInterface> OnIpAddressChanged;
		/// <summary><para>Occurs when the IP address of a specific network interface was changed</para></summary>
		public event EventPlugin.EventValues<NetworkInterface> IpAddressChanged
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.NetworkInterfaces))
					oldValues[EventType.NetworkInterfaces] = Status.Network.AllNetworkInterfaces;
				this.OnIpAddressChanged += value;
				NetworkAddressChangedEnabled = true;
			}
			remove
			{
				this.OnIpAddressChanged -= value;
				NetworkAddressChangedEnabled = false;	// does not have to occur
			}
		}
		#endregion
		#endregion
		#endregion

		#region Methods
		public override EventList EventNames()
		{
			EventList list = new EventList();
			EventListRow row = list.NewEventListRow();
			row.Name = "NetworkAvailabilityChanged";
			row.Text = "Network availability changed";
			row.Description = "The availability of the network changed";
			row.Type = Manager.EventTypes.ChangingValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "NetworkAddressChanged";
			row.Text = "Network address changed";
			row.Description = "The IP address of a network interface changed";
			row.Type = Manager.EventTypes.ChangingValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "NetworkInterfaceAdded";
			row.Text = "Network Interface added";
			row.Description = "A new network interface was detected";
			row.Type = Manager.EventTypes.ChangingValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "NetworkInterfaceRemoved";
			row.Text = "Network Interface removed";
			row.Description = "A network interface was removed";
			row.Type = Manager.EventTypes.ChangingValue;
			list.AddEventListRow(row);

			row = list.NewEventListRow();
			row.Name = "IPAddressChanged";
			row.Text = "IP address changed";
			row.Description = "The IP address of a specific network interface was changed";
			row.Type = Manager.EventTypes.ChangingValue; 
			list.AddEventListRow(row);

			return list;
		}

		private void handleIpAddresses(NetworkInterface oldNI, NetworkInterface newNI)
		{
			UnicastIPAddressInformationCollection ipsOld = oldNI.GetIPProperties().UnicastAddresses;
			UnicastIPAddressInformationCollection ipsNew = newNI.GetIPProperties().UnicastAddresses;
			List<UnicastIPAddressInformation> ipsNewList = new List<UnicastIPAddressInformation>(ipsNew);
			foreach (UnicastIPAddressInformation ipOld in ipsOld)
			{
				UnicastIPAddressInformation foundIP = null;
				foreach (UnicastIPAddressInformation ipNew in ipsNewList)
				{
					if (ipOld.Address.ToString() == ipNew.Address.ToString())
					{
						foundIP = ipNew;
						break;
					}
				}
				if (foundIP == null)
				{
					this.OnIpAddressChanged(this, new EventArgsValues<NetworkInterface>(oldNI, newNI));
					return;
				}
				else
				{
					ipsNewList.Remove(foundIP);
				}
			}
			if (ipsNewList.Count > 0)
				this.OnIpAddressChanged(this, new EventArgsValues<NetworkInterface>(oldNI, newNI));
		}

		public override TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Network");

			TreeNode tnEvents = tnMain.Nodes.Add("Registered events");

			if (NetworkAddressChangedEnabled)
				tnEvents.Nodes.Add(new TreeNode("NetworkAddressChanged"));
			if (NetworkAvailabilityChangedEnabled)
				tnEvents.Nodes.Add(new TreeNode("NetworkAvailabilityChanged"));

			return tnMain;
		}
		#endregion

		#region Events
		/// <summary>
		/// <para>EventHandler of <see cref="NetworkChange.NetworkAddressChanged"/></para>
		/// <para>Checks all interfaces and calls events accordingly</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
		{
			if (OnNetworkAddressChanged != null)
				OnNetworkAddressChanged(this, e);
			List<NetworkInterface> newNI = Status.Network.AllNetworkInterfaces;
			List<NetworkInterface> oldNI = (List<NetworkInterface>)oldValues[EventType.NetworkInterfaces];
			if (newNI.Equals(oldNI))
				return;
			foreach (NetworkInterface ni in newNI)
			{
				NetworkInterface found = oldNI.Find((item) => { return item.Id == ni.Id; });
				if (found == null)
				{
					if (OnNetworkInterfaceAdded != null)
						OnNetworkInterfaceAdded(this, new EventArgsValue<NetworkInterface>(ni));
				}
				else
				{
					this.handleIpAddresses(found, ni);

					oldNI.Remove(found);
				}
			}
			if (OnNetworkInterfaceRemoved != null)
				foreach (NetworkInterface ni in oldNI)
					OnNetworkInterfaceRemoved(this, new EventArgsValue<NetworkInterface>(ni));
			oldValues[EventType.NetworkInterfaces] = newNI;
		}

		/// <summary>
		/// <para>EventHandler of <see cref="NetworkChange.NetworkAvailabilityChanged"/></para>
		/// <para>Throws the event <see cref="OnNetworkAvailabilityChanged"/> if neccessary</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
		{
			if (e.IsAvailable.Equals(oldValues[EventType.NetworkAvailabilityChanged]))
				return;
			if (OnNetworkAvailabilityChanged != null)
				OnNetworkAvailabilityChanged(this, new EventArgsValue<bool>(e.IsAvailable));
			oldValues[EventType.NetworkAvailabilityChanged] = e.IsAvailable;
		}
		#endregion
	}
}