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
			this.tsb_Clear = new System.Windows.Forms.ToolStripButton();
			this.tsb_Stats = new System.Windows.Forms.ToolStripButton();
			this.tsb_Options = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsb_Exit = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.manageTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1.SuspendLayout();
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
			this.txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
			// 
			// tsb_Clear
			// 
			this.tsb_Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsb_Clear.Image = global::Trigger.Properties.Resources.Brush_16x16;
			this.tsb_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_Clear.Name = "tsb_Clear";
			this.tsb_Clear.Size = new System.Drawing.Size(23, 22);
			this.tsb_Clear.Text = "Clear log";
			this.tsb_Clear.Click += new System.EventHandler(this.tsb_Clear_Click);
			// 
			// tsb_Stats
			// 
			this.tsb_Stats.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsb_Stats.Image = global::Trigger.Properties.Resources.Statistics_24x24;
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
            this.manageTasksToolStripMenuItem});
			this.tsb_Options.Image = global::Trigger.Properties.Resources.Settings_24x24;
			this.tsb_Options.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_Options.Name = "tsb_Options";
			this.tsb_Options.Size = new System.Drawing.Size(29, 22);
			this.tsb_Options.Text = "Options";
			// 
			// tsb_Exit
			// 
			this.tsb_Exit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsb_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsb_Exit.Image = global::Trigger.Properties.Resources.Quit_24x24;
			this.tsb_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_Exit.Name = "tsb_Exit";
			this.tsb_Exit.Size = new System.Drawing.Size(23, 22);
			this.tsb_Exit.Text = "Exit Trigger4Win";
			this.tsb_Exit.Click += new System.EventHandler(this.tsb_Exit_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Clear,
            this.tsb_Stats,
            this.tsb_Options,
            this.tsb_Exit});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(548, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// manageTasksToolStripMenuItem
			// 
			this.manageTasksToolStripMenuItem.Name = "manageTasksToolStripMenuItem";
			this.manageTasksToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.manageTasksToolStripMenuItem.Text = "Manage &Tasks";
			this.manageTasksToolStripMenuItem.Click += new System.EventHandler(this.manageTasksToolStripMenuItem_Click);
			// 
			// Log
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(548, 333);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.txt);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Log";
			this.Text = "Log";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox txt;
		private System.Windows.Forms.ToolStripButton tsb_Clear;
		private System.Windows.Forms.ToolStripDropDownButton tsb_Options;
		private System.Windows.Forms.ToolStripButton tsb_Stats;
		private System.Windows.Forms.ToolStripButton tsb_Exit;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripMenuItem manageTasksToolStripMenuItem;
	}
}

