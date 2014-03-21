using System.Windows.Forms;
using System;
using System.Configuration;

namespace Trigger
{
	/// <summary>
	/// <para>A <see cref="Form"/> for logging purposes</para>
	/// </summary>
	public partial class Log : Form
	{
		/// <summary><para>The type of the logged message</para></summary>
		public enum Type
		{
			/// <summary><para>This is a network event</para></summary>
			NetworkEvent,
			/// <summary><para>This is a system event</para></summary>
			SystemEvent,
			/// <summary><para>This is a power event</para></summary>
			PowerEvent,
			/// <summary><para>This is a screen event</para></summary>
			ScreenEvent,
			/// <summary><para>This is a process event</para></summary>
			ProcessesEvent,
			/// <summary><para>This is a device event</para></summary>
			DeviceEvent,
			/// <summary><para>This is an action</para></summary>
			Action,
			/// <summary><para>This is an message without type</para></summary>
			Other,
			/// <summary><para>This is an error message</para></summary>
			Error,
		}
		/// <summary><para>Our <see cref="Main"/> <see cref="Form"/></para></summary>
		private Main Main;

		/// <summary></summary>
		/// <param name="Main"></param>
		public Log(Main Main)
		{
			InitializeComponent();

			this.Main = Main;
		}

		#region Methods
		/// <summary>
		/// <para>Appends the specified <paramref name="text"/> to the log box <see cref="txt"/></para>
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void LogText(string text, Type type)
		{
			if (this.txt.InvokeRequired)
			{
				this.txt.Invoke(new MethodInvoker(() => this.LogText(text, type)));
				return;
			}
			this.txt.AppendText(text);
		}
		/// <summary>
		/// <para>Appends the specified <paramref name="text"/> to the log box <see cref="txt"/> prepending the current time</para>
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void LogTextDate(string text, Type type)
		{
			this.LogText(DateTime.Now.ToLongTimeString() + " " + text, type);
		}

		/// <summary>
		/// <para>Appends the specified <paramref name="text"/> to the log box (<see cref="txt"/>) and opens a new line</para>
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void LogLine(string text, Type type)
		{
			if (this.txt.InvokeRequired)
			{
				this.txt.Invoke(new MethodInvoker(() => this.LogLine(text, type)));
				return;
			}
			this.txt.AppendText(text + "\n");
			this.txt.ScrollToCaret();
		}
		/// <summary>
		/// <para>Appends the specified <paramref name="text"/> to the log box (<see cref="txt"/>) prepending the current time and opens a new line</para>
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void LogLineDate(string text, Type type)
		{
			this.LogLine(DateTime.Now.ToLongTimeString() + " " + text, type);
		}
		/// <summary>
		/// <para>Creates a new line in the log box <see cref="txt"/></para>
		/// </summary>
		public void Line(Type type)
		{
			if (this.txt.InvokeRequired)
			{
				this.txt.Invoke(new MethodInvoker(() => this.Line(type)));
				return;
			}
			this.txt.AppendText("\n");
			this.txt.ScrollToCaret();
		}


		/// <summary>
		/// <para>Shows this <see cref="Log"/> in the foreground</para>
		/// </summary>
		new public void Show()
		{
			this.BringToFront();
			this.WindowState = FormWindowState.Normal;
			Actions.System.SetForegroundWindow(this);
			this.Main.tsmNotifyIcon_Log.Visible = false;
			base.Show();
		}
		/// <summary>
		/// <para>Hides this <see cref="Log"/></para>
		/// </summary>
		new public void Hide()
		{
			this.Main.tsmNotifyIcon_Log.Visible = true;
			base.Hide();
		}
		#endregion

		#region Event handlers
		/// <summary>
		/// <para>Prevent this form from closing, simply hide it</para>
		/// <para>This is neccessary because we may want to have a look at the log later again</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Log_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				this.Hide();
				e.Cancel = true;
			}
		}

		/// <summary>
		/// <para>Clears the log <see cref="txt"/></para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsb_Clear_Click(object sender, EventArgs e)
		{
			this.txt.Clear();
		}

		/// <summary>
		/// <para>Saves the settings which logs shall be displayed</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsb_Options_DropDownItem_CheckedChanged(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
			Properties.Settings.Default[tsmi.Name] = tsmi.Checked;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// <para>Opens the <see cref="StatusView"/></para>
		/// <para>See <see cref="Main"/>.RefreshStatus</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsb_Stats_Click(object sender, EventArgs e)
		{
			this.Main.RefreshStatus();
		}

		/// <summary>
		/// <para>Close the application</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsb_Exit_Click(object sender, EventArgs e)
		{
			this.Main.Close();
		}

		/// <summary>
		/// <para>Enable some keys</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txt_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.S:
					this.Main.RefreshStatus();
					e.Handled = true;
					break;
				case Keys.Escape:
					this.Hide();
					e.Handled = true;
					break;
			}
		}
		
		/// <summary>
		/// <para>Opens the <see cref="TaskOptions"/></para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void manageTasksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new TaskOptions(this.Main).Show();
		}
		#endregion
	}
}