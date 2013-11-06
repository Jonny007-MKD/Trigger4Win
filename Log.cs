﻿using System.Windows.Forms;
using System;
using System.Configuration;

namespace Tasker
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
			/// <summary><para>This is a screen event</para></summary>
			ScreenEvent,
			/// <summary><para>This is a process started event</para></summary>
			ProcessStartedEvent,
			/// <summary><para>This is a network action</para></summary>
			NetworkAction,
			/// <summary><para>This is a system action</para></summary>
			SystemAction,
			/// <summary><para>This is a screen action</para></summary>
			ScreenAction,
			/// <summary><para>This is a process started action</para></summary>
			ProcessStartedAction,
			/// <summary><para>This is an message without type</para></summary>
			Other,
			/// <summary><para>This is an error message</para></summary>
			Error,
		}

		private Main Main;

		/// <summary></summary>
		/// <param name="Main"></param>
		public Log(Main Main)
		{
			InitializeComponent();
			this.tsb_Options_EnableLoggingTasks.Checked = Properties.Settings.Default.LoadLoggingTasks;

			this.Main = Main;

			// Add a menu item for each log ("LogNameHereEnabled") setting
			foreach (SettingsProperty setting in Properties.Settings.Default.Properties)
			{
				if (!setting.Name.StartsWith("Log"))
					continue;
				ToolStripMenuItem tsmi = new ToolStripMenuItem("Show " + setting.Name.Substring(3, setting.Name.IndexOf("Enabled") - 3) + " messages");
				tsmi.CheckOnClick = true;
				tsmi.Checked = Convert.ToBoolean(Properties.Settings.Default[setting.Name]);
				tsmi.Name = setting.Name;
				tsmi.CheckedChanged += tsb_Options_DropDownItem_CheckedChanged;
				tsmi.Enabled = Properties.Settings.Default.LoadLoggingTasks;
				tsb_Options.DropDownItems.Add(tsmi);
			}
		}

		#region Methods
		private bool isLoggingDisabled(Type type)
		{
			if (type == Type.Other || type == Type.Error)
				return false;
			string typeStart = type.ToString();
			if (typeStart.EndsWith("Event"))
				return !(bool)Properties.Settings.Default["Log" + typeStart.Substring(0, typeStart.Length - 5) + "Enabled"];
			else
				return false;
		}

		/// <summary>
		/// <para>Appends the specified <paramref name="text"/> to the log box <see cref="txt"/></para>
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void LogText(string text, Type type)
		{
			if (isLoggingDisabled(type))
				return;
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
			if (isLoggingDisabled(type))
				return;
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
			if (isLoggingDisabled(type))
				return;
			if (this.txt.InvokeRequired)
			{
				this.txt.Invoke(new MethodInvoker(() => this.Line(type)));
				return;
			}
			this.txt.AppendText("\n");
			this.txt.ScrollToCaret();
		}
		#endregion

		#region Events
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
				this.Main.tsmNotifyIcon_Log.Visible = true;
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
		/// <para>See <see cref="Main.RefreshStatus"/></para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsb_Stats_Click(object sender, EventArgs e)
		{
			this.Main.RefreshStatus();
		}

		/// <summary>
		/// <para>Saves the setting whether the logging tasks shall be loaded and restarts the <see cref="Application"/> if neccessary</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbOptions_EnableLoggingTasks_CheckedChanged(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
			if (Properties.Settings.Default.LoadLoggingTasks == tsmi.Checked)
				return;
			Properties.Settings.Default.LoadLoggingTasks = tsmi.Checked;
			Properties.Settings.Default.Save();
			if (tsmi.Checked)
			{
				this.Main.TaskMgr.LoadLoggingTasks();
				
				// Enable logging display settings
				foreach (SettingsProperty setting in Properties.Settings.Default.Properties)
				{
					if (!setting.Name.StartsWith("Log"))
						continue;
					ToolStripItem tsmiSetting = this.tsb_Options.DropDownItems.Find(setting.Name, false)[0];
					tsmiSetting.Enabled = true;
				}
			}
			else
				Application.Restart();
		}
		#endregion
	}
}