using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Trigger
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


			TreeNode tnStatus = this.treeView.Nodes.Add("System Status");
			List<Type> statusPlugins = this.getAvailableStatus();
			foreach (Type statusPlugin in statusPlugins)
			{
				MethodInfo getStatus = statusPlugin.GetMethod("GetStatus");
				if (getStatus != null)
					tnStatus.Nodes.Add((TreeNode)getStatus.Invoke(null, null));
			}


			TreeNode tnProcess = this.treeView.Nodes.Add("Process data");
			Process process = Process.GetCurrentProcess();
			tnProcess.Nodes.Add("Nonpaged Memory usage: " + (process.NonpagedSystemMemorySize64 / 1024).ToString() + "kB");
			tnProcess.Nodes.Add("Paged    Memory usage: " + (process.PagedSystemMemorySize64 / 1024).ToString() + "kB");
			tnProcess.Nodes.Add("Virtual  Memory usage: " + (process.VirtualMemorySize64 / 1024 / 1024).ToString() + "MB");
			tnProcess.Nodes.Add("Working  Memory usage: " + (process.WorkingSet64 / 1024 / 1024).ToString() + "MB");
			tnProcess.Nodes.Add("Total Processor time:  " + process.TotalProcessorTime.TotalSeconds.ToString() + "s");
			tnProcess.Nodes.Add("User Processor time:   " + process.UserProcessorTime.TotalSeconds.ToString() + "s");

			base.Refresh();
		}


		/// <summary>
		/// <para>Gets a list of all available Status using Reflection</para>
		/// </summary>
		/// <returns></returns>
		private List<Type> getAvailableStatus()
		{
			List<Type> types = new List<Type>(System.Reflection.Assembly.GetExecutingAssembly().GetTypes());
			types = types.FindAll(new Predicate<Type>(item => { return item.IsClass && item.Namespace.StartsWith("Trigger.Status"); }));
			return types;
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
