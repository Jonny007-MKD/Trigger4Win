namespace Trigger
{
	partial class TaskOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskOptions));
			this.dgvPlugins = new System.Windows.Forms.DataGridView();
			this.dgvPlugins_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvPlugins_Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgvPlugins)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvPlugins
			// 
			this.dgvPlugins.AllowUserToAddRows = false;
			this.dgvPlugins.AllowUserToDeleteRows = false;
			this.dgvPlugins.AllowUserToResizeColumns = false;
			this.dgvPlugins.AllowUserToResizeRows = false;
			this.dgvPlugins.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dgvPlugins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvPlugins.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvPlugins_Name,
            this.dgvPlugins_Enabled});
			this.dgvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvPlugins.Location = new System.Drawing.Point(0, 0);
			this.dgvPlugins.MultiSelect = false;
			this.dgvPlugins.Name = "dgvPlugins";
			this.dgvPlugins.RowHeadersWidth = 10;
			this.dgvPlugins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvPlugins.Size = new System.Drawing.Size(217, 300);
			this.dgvPlugins.TabIndex = 0;
			this.dgvPlugins.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlugins_CellClick);
			// 
			// dgvPlugins_Name
			// 
			this.dgvPlugins_Name.DataPropertyName = "Key";
			this.dgvPlugins_Name.HeaderText = "Name";
			this.dgvPlugins_Name.Name = "dgvPlugins_Name";
			this.dgvPlugins_Name.ReadOnly = true;
			this.dgvPlugins_Name.Width = 60;
			// 
			// dgvPlugins_Enabled
			// 
			this.dgvPlugins_Enabled.DataPropertyName = "Value";
			this.dgvPlugins_Enabled.FalseValue = "False";
			this.dgvPlugins_Enabled.HeaderText = "Enabled";
			this.dgvPlugins_Enabled.Name = "dgvPlugins_Enabled";
			this.dgvPlugins_Enabled.TrueValue = "True";
			this.dgvPlugins_Enabled.Width = 52;
			// 
			// TaskOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(217, 300);
			this.Controls.Add(this.dgvPlugins);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TaskOptions";
			this.Text = "Manage your plugins";
			((System.ComponentModel.ISupportInitialize)(this.dgvPlugins)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvPlugins;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvPlugins_Name;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dgvPlugins_Enabled;
	}
}