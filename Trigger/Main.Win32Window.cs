using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Trigger.Events;

namespace Trigger
{
	partial class Main : Form
	{
		#region Structs & Constants
		internal const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;
		internal const int DEVICE_NOTIFY_SERVICE_HANDLE = 0x00000001;
		#endregion

		#region Properties
		/// <summary><para>Takes care of thread safe access to this class </para></summary>
		private ReaderWriterLock _lock = new ReaderWriterLock();

		/// <summary><para>Which messages shall be processed</para></summary>
		private Dictionary<int, byte> _messageSet = new Dictionary<int, byte>();
		#endregion

		#region Methods
		/// <summary>
		/// <para>Enable the specified <paramref name="messageID"/> for being processed and forwared</para>
		/// </summary>
		/// <param name="messageID"></param>
		public void RegisterEventForMessage(Classes.System.WindowsMessages messageID)
		{
			_lock.AcquireWriterLock(Timeout.Infinite);
			if (!_messageSet.ContainsKey((int)messageID))
				_messageSet.Add((int)messageID, 1);
			else
				_messageSet[(int)messageID]++;
			_lock.ReleaseWriterLock();
		}
		/// <summary>
		/// <para>Disable the specified <paramref name="messageID"/> for being processed and forwared</para>
		/// </summary>
		/// <param name="messageID"></param>
		public void UnregisterEventForMessage(Classes.System.WindowsMessages messageID)
		{
			_lock.AcquireWriterLock(Timeout.Infinite);
			if (_messageSet.ContainsKey((int)messageID) && _messageSet[(int)messageID] > 1)
				_messageSet[(int)messageID]--;
			_lock.ReleaseWriterLock();
		}

		/// <summary>
		/// <para>The actual method that is being called when a <see cref="Message"/> is broadcasted</para>
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			_lock.AcquireWriterLock(Timeout.Infinite);
			bool handleEvent = _messageSet.ContainsKey(m.Msg) && _messageSet[m.Msg] > 0;
			_lock.ReleaseWriterLock();

			if (handleEvent && OnMessageReceived != null)
			{
				Message mm = m;
				ThreadPool.QueueUserWorkItem(s => OnMessageReceived(null, new EventArgsValue<Message>(mm)));
			}
		}
		#endregion

		#region Events
		private static event EventPlugin.EventValue<Message> OnMessageReceived;
		/// <summary>
		/// <para>When a Win32 <see cref="Message"/> was received</para>
		/// <para>You may have to register this Form for receiving such messages first</para>
		/// </summary>
		public static event EventPlugin.EventValue<Message> MessageReceived
		{
			add
			{
				OnMessageReceived += value;
			}
			remove
			{
				OnMessageReceived -= value;
			}
		}
		#endregion
	}
}
