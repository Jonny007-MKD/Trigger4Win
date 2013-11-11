namespace Trigger
{
	partial class Main
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmsNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmNotifyIcon_Status = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmNotifyIcon_Log = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmNotifyIcon_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsNotifyIcon.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.cmsNotifyIcon;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Tasker";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// cmsNotifyIcon
			// 
			this.cmsNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNotifyIcon_Log,
            this.tsmNotifyIcon_Status,
            this.tsmNotifyIcon_Exit});
			this.cmsNotifyIcon.Name = "cmsNotifyIcon";
			this.cmsNotifyIcon.Size = new System.Drawing.Size(153, 92);
			// 
			// tsmNotifyIcon_Status
			// 
			this.tsmNotifyIcon_Status.Image = global::Trigger.Properties.Resources.Chart_16x16;
			this.tsmNotifyIcon_Status.Name = "tsmNotifyIcon_Status";
			this.tsmNotifyIcon_Status.Size = new System.Drawing.Size(152, 22);
			this.tsmNotifyIcon_Status.Text = "Show status";
			this.tsmNotifyIcon_Status.Click += new System.EventHandler(this.tsmNotifyIcon_Status_Click);
			// 
			// tsmNotifyIcon_Log
			// 
			this.tsmNotifyIcon_Log.Image = global::Trigger.Properties.Resources.icon_16;
			this.tsmNotifyIcon_Log.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsmNotifyIcon_Log.Name = "tsmNotifyIcon_Log";
			this.tsmNotifyIcon_Log.Size = new System.Drawing.Size(152, 22);
			this.tsmNotifyIcon_Log.Text = "Show log";
			this.tsmNotifyIcon_Log.Visible = false;
			this.tsmNotifyIcon_Log.Click += new System.EventHandler(this.tmsNotifyIcon_Log_Click);
			// 
			// tsmNotifyIcon_Exit
			// 
			this.tsmNotifyIcon_Exit.Image = global::Trigger.Properties.Resources.RedCross_16x16;
			this.tsmNotifyIcon_Exit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsmNotifyIcon_Exit.Name = "tsmNotifyIcon_Exit";
			this.tsmNotifyIcon_Exit.Size = new System.Drawing.Size(152, 22);
			this.tsmNotifyIcon_Exit.Text = "Exit";
			this.tsmNotifyIcon_Exit.ToolTipText = "Close the Tasker application";
			this.tsmNotifyIcon_Exit.Click += new System.EventHandler(this.tsmNotifyIcon_Exit_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(161, 0);
			this.Enabled = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(177, 38);
			this.Name = "Main";
			this.Opacity = 0.8D;
			this.Text = "Tasker";
			this.Shown += new System.EventHandler(this.Main_Shown);
			this.cmsNotifyIcon.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip cmsNotifyIcon;
		private System.Windows.Forms.ToolStripMenuItem tsmNotifyIcon_Exit;
		/// <summary></summary>
		public System.Windows.Forms.ToolStripMenuItem tsmNotifyIcon_Log;
		private System.Windows.Forms.ToolStripMenuItem tsmNotifyIcon_Status;
	}
}