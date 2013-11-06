using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tasker
{
	/// <summary>
	/// <para>This is a <see cref="Form"/> that displays all available information from the <see cref="Events.Manager"/> and the <see cref="Tasks.Manager"/></para>
	/// </summary>
	public partial class StatusView : Form
	{
		#region Properties
		private Main Main;
		#endregion

		#region Constructors
		/// <summary></summary>
		/// <param name="Main"></param>
		public StatusView(Main Main)
		{
			InitializeComponent();

			this.Main = Main;
			this.Refresh();
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Refreshes all elements in the <see cref="TreeView"/> <see cref="treeView"/></para>
		/// </summary>
		public override void Refresh()
		{
			this.treeView.Nodes.Clear();
			this.treeView.Nodes.Add(this.Main.EventMgr.GetStatus());
			this.treeView.Nodes.Add(this.Main.TaskMgr.GetStatus());

			TreeNode tnProcess = this.treeView.Nodes.Add("Process data");
			Process process = Process.GetCurrentProcess();
			tnProcess.Nodes.Add("Nonpaged Memory usage: " + (process.NonpagedSystemMemorySize64 / 1024).ToString() + "kB");
			tnProcess.Nodes.Add("Paged    Memory usage: " + (process.PagedSystemMemorySize64 / 1024).ToString() + "kB");
			tnProcess.Nodes.Add("Virtual  Memory usage: " + (process.VirtualMemorySize64 / 1024 / 1024).ToString() + "MB");
			tnProcess.Nodes.Add("Working  Memory usage: " + (process.WorkingSet64 / 1024 / 1024).ToString() + "MB");
			tnProcess.Nodes.Add("Total Processor time:  " + process.TotalProcessorTime.TotalSeconds.ToString() + "s");
			tnProcess.Nodes.Add("User Processor time:   " + process.UserProcessorTime.TotalSeconds.ToString() + "s");

			TreeNode tnStatus = this.treeView.Nodes.Add("System Status");
			tnStatus.Nodes.Add(Status.System.GetStatus());
			tnStatus.Nodes.Add(Status.Network.GetStatus());
			tnStatus.Nodes.Add(Status.Screen.GetStatus());
			base.Refresh();
		}
		#endregion

		#region Events
		private void treeView_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					this.Refresh();
					break;
				case Keys.Escape:
					this.Close();
					break;
				case Keys.C:
					System.Windows.Forms.Clipboard.SetText(treeView.SelectedNode.Text);
					break;
			}
		}
		#endregion
	}
}
