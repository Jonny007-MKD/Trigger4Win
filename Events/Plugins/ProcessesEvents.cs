using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;
using System.Diagnostics;
using Diag = System.Diagnostics;

namespace Trigger.Events
{
	public class Processes : EventPlugin
	{
		#region Properties
		private enum EventType : byte
		{
			ProcessCreated,
			ProcessExited,
		}

		private Dictionary<EventType, object> oldValues = new Dictionary<EventType, object>();

		private static ManagementEventWatcher instanceCreationEventWatcher = null;
		private static ManagementEventWatcher instanceDeletionEventWatcher = null;
		#endregion

		#region Register Parent Events
		private byte instanceCreationEventEnabled;
		private bool InstanceCreationEventEnabled
		{
			set
			{
				if (value)
					instanceCreationEventEnabled++;
				else
					instanceCreationEventEnabled--;
				if (value && instanceCreationEventEnabled == 1)
				{
					if (instanceCreationEventWatcher == null)
					{
						WqlEventQuery query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");

						instanceCreationEventWatcher = new ManagementEventWatcher();
						instanceCreationEventWatcher.Query = query;
						instanceCreationEventWatcher.EventArrived += new EventArrivedEventHandler(instanceCreationEventWatcher_EventArrived);
						instanceCreationEventWatcher.Start();
					}
				}
				else if (instanceCreationEventEnabled == 0)
				{
					instanceCreationEventWatcher.EventArrived -= new EventArrivedEventHandler(instanceCreationEventWatcher_EventArrived);
					instanceCreationEventWatcher.Dispose();
					instanceCreationEventWatcher = null;
				}
			}
			get
			{
				return instanceCreationEventEnabled > 0;
			}
		}
		private byte instanceDeletionEventEnabled;
		private bool InstanceDeletionEventEnabled
		{
			set
			{
				if (value)
					instanceDeletionEventEnabled++;
				else
					instanceDeletionEventEnabled--;
				if (value && instanceDeletionEventEnabled == 1)
				{
					if (instanceDeletionEventWatcher == null)
					{
						WqlEventQuery query = new WqlEventQuery("__InstanceDeletionEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");

						instanceDeletionEventWatcher = new ManagementEventWatcher();
						instanceDeletionEventWatcher.Query = query;
						instanceDeletionEventWatcher.EventArrived += new EventArrivedEventHandler(instanceDeletionEventWatcher_EventArrived);
						instanceDeletionEventWatcher.Start();
					}
				}
				else if (instanceDeletionEventEnabled == 0)
				{
					instanceDeletionEventWatcher.EventArrived -= new EventArrivedEventHandler(instanceDeletionEventWatcher_EventArrived);
					instanceDeletionEventWatcher.Dispose();
					instanceDeletionEventWatcher = null;
				}
			}
			get
			{
				return instanceDeletionEventEnabled > 0;
			}
		}
		#endregion

		#region Events
		#region OnProcessCreated
		/// <summary><para>Occurs when a new instance of Win32_Process is created</para></summary>
		private EventPlugin.EventValue<Process> OnProcessCreated;
		/// <summary><para>Occurs when a new instance of Win32_Process is created</para></summary>
		public event EventPlugin.EventValue<Process> ProcessCreated
		{
			add
			{
				this.OnProcessCreated += value;
				InstanceCreationEventEnabled = true;
			}
			remove
			{
				this.OnProcessCreated -= value;
				InstanceCreationEventEnabled = false;
			}
		}
		#endregion
		#region OnProcessExited
		/// <summary><para>Occurs when a new instance of Win32_Process is Exited</para></summary>
		private EventPlugin.EventValue<Process> OnProcessExited;
		/// <summary><para>Occurs when a new instance of Win32_Process is Exited</para></summary>
		public event EventPlugin.EventValue<Process> ProcessExited
		{
			add
			{
				if (!oldValues.ContainsKey(EventType.ProcessExited))
					oldValues[EventType.ProcessExited] = Status.Processes.RunningDict;
				this.OnProcessExited += value;
				InstanceDeletionEventEnabled = true;
			}
			remove
			{
				this.OnProcessExited -= value;
				InstanceDeletionEventEnabled = false;
			}
		}
		#endregion
		#endregion

		/*#region Constructor & Destructor
		#endregion*/

		#region Methods
		public override EventList EventNames()
		{
			EventList list = new EventList();

			EventListRow row = list.NewEventListRow();
			row.Name = "ProcessCreated";
			row.Text = "Process created";
			row.Description = "A new process was created";
			row.Type = Manager.EventTypes.Simple;
			list.AddEventListRow(row);

			return list;
		}

		public override TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("ProcessStarted");

			TreeNode tnEvents = tnMain.Nodes.Add("Registered events");

			if (InstanceCreationEventEnabled)
			{
				TreeNode tnEvent = tnEvents.Nodes.Add("InstanceCreationEventEnabled");
			}

			return tnMain;
		}
		#endregion

		#region Event Handlers
		private void instanceCreationEventWatcher_EventArrived(object sender, EventArrivedEventArgs e)
		{
			Process newProc = null;
			try
			{
				newProc = Process.GetProcessById(Convert.ToInt32(((ManagementBaseObject)e.NewEvent["TargetInstance"])["Handle"]));
			}
			catch (Exception) { }

			if (newProc != null)
			{
				if (OnProcessCreated != null)
					OnProcessCreated(this, new EventArgsValue<Process>(newProc));
				//oldValues[EventType.ProcessExited] = Status.Processes.RunningDict;
				((Dictionary<int, Process>)oldValues[EventType.ProcessExited])[newProc.Id] = newProc;
			}
		}

		private void instanceDeletionEventWatcher_EventArrived(object sender, EventArrivedEventArgs e)
		{
			Dictionary<int, Process> oldProcesses = (Dictionary<int, Process>)oldValues[EventType.ProcessExited];
			int pid = Convert.ToInt32(((ManagementBaseObject)e.NewEvent["TargetInstance"])["Handle"]);
			if (!oldProcesses.ContainsKey(pid))		// we have not found this process. Somehow we have missed the creation event...
			{
				oldValues[EventType.ProcessExited] = Status.Processes.RunningDict;
				return;
			}
			Process exitedProc = oldProcesses[pid];
			if (OnProcessExited != null)
				OnProcessExited(this, new EventArgsValue<Process>(exitedProc));
			oldProcesses.Remove(exitedProc.Id);
		}
		#endregion
	}
}