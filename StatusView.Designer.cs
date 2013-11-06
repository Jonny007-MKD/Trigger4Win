namespace Tasker
{
	partial class StatusView
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Knoten6");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Knoten8");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Knoten7", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			this.treeView = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			treeNode1.Name = "Knoten6";
			treeNode1.Text = "Knoten6";
			treeNode2.Name = "Knoten8";
			treeNode2.Text = "Knoten8";
			treeNode3.Name = "Knoten7";
			treeNode3.Text = "Knoten7";
			this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
			this.treeView.Size = new System.Drawing.Size(284, 262);
			this.treeView.TabIndex = 0;
			this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyDown);
			// 
			// StatusView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.treeView);
			this.Name = "StatusView";
			this.Text = "Status";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView;
	}
}