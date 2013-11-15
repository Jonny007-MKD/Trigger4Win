using System;
using System.Windows.Forms;
using System.Diagnostics;

//System.IO.Ports.SerialData
//System.Management.ManagementEventWatcher
//System.IO.FileSystemWatcher


namespace Trigger
{
	/// <summary>
	/// <para>The main <see cref="Form"/></para>
	/// </summary>
	/// <remarks>This <see cref="Form"/> is invisible</remarks>
	public partial class Main : Form
	{
		/// <summary>The logging <see cref="Form"/></summary>
		public Log Log;
		/// <summary>The global EventManager</summary>
		public Events.Manager EventMgr;
		/// <summary>The global TaskManager</summary>
		public Tasks.Manager TaskMgr;
		private StatusView StatusView = null;

		#region Constructor
		/// <summary></summary>
		public Main()
		{
			this.Visible = false;
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Creates a new or refreshes the existing <see cref="StatusView"/></para>
		/// </summary>
		public void RefreshStatus()
		{
			if (this.StatusView == null || this.StatusView.IsDisposed)
				this.StatusView = new StatusView(this);
			else
				this.StatusView.Refresh();
			this.StatusView.Show();
		}

		public bool RequireAdministrator()
		{
			bool isAdmin = false;
			System.Security.Principal.WindowsIdentity winId = System.Security.Principal.WindowsIdentity.GetCurrent();
			if (winId != null)
				isAdmin = new System.Security.Principal.WindowsPrincipal(winId).IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);

			if (isAdmin)
				return true;
			
			Process me = Process.GetCurrentProcess();
			ProcessStartInfo procStart = new ProcessStartInfo();
			procStart.FileName = me.MainModule.FileName;
			procStart.UseShellExecute = true;
			procStart.Verb = "runas";	// run as Admin

			this.Log.LogLineDate("Requiring admin privileges...", Trigger.Log.Type.Other);

			try
			{
				if (Process.Start(procStart) == null)
					return false;
				Process.GetCurrentProcess().Kill();
				return true;
			}
			catch (System.ComponentModel.Win32Exception e)
			{
				if (e.NativeErrorCode != 1223)		// 1223 = user aborted UAC
					throw;
			}
			return false;
		}

		new public void Close()
		{
			this.Close(true);
		}
		public void Close(bool ask)
		{
			if (ask)
				if (DialogResult.Yes != MessageBox.Show("Do you really want to exit Trigger4Win?", "Quit Trigger4Win", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					return;
			base.Close();
		}
		#endregion

		#region Event handler
		/// <summary>
		/// <para>Do not show this <see cref="Form"/></para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Main_Shown(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		/// <summary>
		/// <para>Exit application</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmNotifyIcon_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// <para>Show the Log</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tmsNotifyIcon_Log_Click(object sender, EventArgs e)
		{
			this.Log.Show();
			this.Log.BringToFront();
			((ToolStripMenuItem)sender).Visible = false;
		}

		/// <summary>
		/// <para>Show the log</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Log.Show();
		}

		/// <summary>
		/// <para>Show <see cref="StatusView"/>: <see cref="RefreshStatus"/></para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmNotifyIcon_Status_Click(object sender, EventArgs e)
		{
			this.RefreshStatus();
		}
		#endregion
	}
}
