using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trigger
{
	public partial class TaskOptions : Form
	{
		#region Properties
		internal ListDictionary plugins = new ListDictionary();
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Create a new instance of <see cref="TaskOptions"/></para>
		/// </summary>
		/// <param name="Main"></param>
		public TaskOptions(Main Main)
		{
			InitializeComponent();

			foreach (Type task in Main.TaskMgr.TaskPluginsAvailable)
			{
				string name = task.Name;
				bool enabled = true;
				if (Properties.Settings.Default.PluginsEnabled != null && Properties.Settings.Default.PluginsEnabled.Contains(name))
					enabled = Convert.ToBoolean(Properties.Settings.Default.PluginsEnabled[name]);
				plugins.Add(name, enabled);
			}

			Properties.Settings.Default.PluginsEnabled = plugins;
			Properties.Settings.Default.Save();

			BindingSource pluginBinding = new BindingSource();
			pluginBinding.DataSource = plugins;
			this.dgvPlugins.DataSource = pluginBinding;
		}
		#endregion

		#region Event handler

		private void dgvPlugins_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 1 || e.RowIndex < 0)
				return;
			DataGridView dgv = (DataGridView)sender;
			DataGridViewCell cellEnabled = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			cellEnabled.Value = !Convert.ToBoolean(cellEnabled.Value);
		}
		#endregion
	}
}