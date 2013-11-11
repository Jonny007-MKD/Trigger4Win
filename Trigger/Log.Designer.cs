namespace Trigger
{
	partial class Log
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Log));
			this.txt = new System.Windows.Forms.RichTextBox();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.tsb_Clear = new System.Windows.Forms.ToolStripButton();
			this.tsb_Stats = new System.Windows.Forms.ToolStripButton();
			this.tsb_Options = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsb_Options_EnableLoggingTasks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// txt
			// 
			this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txt.Cursor = System.Windows.Forms.Cursors.Default;
			this.txt.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt.Location = new System.Drawing.Point(0, 28);
			this.txt.Name = "txt";
			this.txt.ReadOnly = true;
			this.txt.Size = new System.Drawing.Size(548, 305);
			this.txt.TabIndex = 0;
			this.txt.Text = "";
			this.txt.WordWrap = false;
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Clear,
            this.tsb_Stats,
            this.tsb_Options});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(548, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip";
			// 
			// tsb_Clear
			// 
			this.tsb_Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsb_Clear.Image = global::Trigger.Properties.Resources.icon_22;
			this.tsb_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_Clear.Name = "tsb_Clear";
			this.tsb_Clear.Size = new System.Drawing.Size(23, 22);
			this.tsb_Clear.Text = "Clear log";
			this.tsb_Clear.Click += new System.EventHandler(this.tsb_Clear_Click);
			// 
			// tsb_Stats
			// 
			this.tsb_Stats.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsb_Stats.Image = global::Trigger.Properties.Resources.Chart_16x16;
			this.tsb_Stats.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_Stats.Name = "tsb_Stats";
			this.tsb_Stats.Size = new System.Drawing.Size(23, 22);
			this.tsb_Stats.Text = "Show current statistics";
			this.tsb_Stats.Click += new System.EventHandler(this.tsb_Stats_Click);
			// 
			// tsb_Options
			// 
			this.tsb_Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsb_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Options_EnableLoggingTasks});
			this.tsb_Options.Image = global::Trigger.Properties.Resources.Settings_24x24;
			this.tsb_Options.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_Options.Name = "tsb_Options";
			this.tsb_Options.Size = new System.Drawing.Size(29, 22);
			this.tsb_Options.Text = "Options";
			// 
			// tsb_Options_EnableLoggingTasks
			// 
			this.tsb_Options_EnableLoggingTasks.CheckOnClick = true;
			this.tsb_Options_EnableLoggingTasks.Name = "tsb_Options_EnableLoggingTasks";
			this.tsb_Options_EnableLoggingTasks.Size = new System.Drawing.Size(188, 22);
			this.tsb_Options_EnableLoggingTasks.Text = "Enable Logging Tasks";
			this.tsb_Options_EnableLoggingTasks.ToolTipText = "(requires restart)";
			this.tsb_Options_EnableLoggingTasks.CheckedChanged += new System.EventHandler(this.tsbOptions_EnableLoggingTasks_CheckedChanged);
			// 
			// Log
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(548, 333);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.txt);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Log";
			this.Text = "Log";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox txt;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton tsb_Clear;
		private System.Windows.Forms.ToolStripDropDownButton tsb_Options;
		private System.Windows.Forms.ToolStripButton tsb_Stats;
		private System.Windows.Forms.ToolStripMenuItem tsb_Options_EnableLoggingTasks;


	}
}

