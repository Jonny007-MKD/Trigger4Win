 using System;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Trigger
{
	/// <summary>
	/// <para>A <see cref="Form"/> that enables the user to choose which <see cref="TaskPlugin"/>s shall be enabled and disabled</para>
	/// </summary>
	public partial class TaskOptions : Form
	{
		#region Properties
		/// <summary><para>Our <see cref="Main"/> <see cref="Form"/></para></summary>
		internal Main Main;
		/// <summary><para>A <see cref="BindingSource"/> to display the <see cref="ObservableDictionary&lt;string, bool&gt;"/> in the <see cref="DataGridView"/></para></summary>
		internal BindingSource pluginBinding;
		/// <summary><para>A list with all available plugins and whether they are enabled</para></summary>
		internal ObservableDictionary<string, bool> plugins = new ObservableDictionary<string, bool>();
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Create a new instance of <see cref="TaskOptions"/></para>
		/// </summary>
		/// <param name="Main"></param>
		public TaskOptions(Main Main)
		{
			InitializeComponent();
			this.Main = Main;

			// get plugins from Settings
			plugins = GetPluginsAll(Main);

			// show them in the DataGridView
			pluginBinding = new BindingSource();
			pluginBinding.DataSource = plugins;
			this.dgvPlugins.DataSource = pluginBinding;
		}
		#endregion

		#region Event handler
		/// <summary>
		/// <para>CellClick-Event of <see cref="dgvPlugins"/></para>
		/// <para>Update the <see cref="plugins"/> dictionary and refresh the <see cref="DataGridView"/></para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvPlugins_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 1 || e.RowIndex < 0)		// Only first column
				return;

			plugins[e.RowIndex] = !plugins[e.RowIndex];		// Toggle enabled value of plugin. Why does the BindingSource not do this?
			this.pluginBinding.DataSource = null;			// TODO: BindingSource does not register for OnCollectionChanged!?
			this.pluginBinding.DataSource = plugins;
		}

		/// <summary>
		/// <para>FormClosing-Event of <see cref="TaskOptions"/></para>
		/// <para>Save the plugin settings and notify the <see cref="TaskManager"/></para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TaskOptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			PutPluginsSetting(plugins);

			this.Hide();
			this.Main.TaskMgr.Refresh(plugins);
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Fetches the settings and convert them to a <see cref="ObservableDictionary<string, bool>"/></para>
		/// </summary>
		/// <returns></returns>
		public static ObservableDictionary<string, bool> GetPluginsSetting()
		{
			ObservableDictionary<string, bool> setting;
			if (String.IsNullOrEmpty(Properties.Settings.Default.PluginsEnabled))
				setting = new ObservableDictionary<string, bool>();
			else
			{
				XmlSerializer xmlSer = new XmlSerializer(typeof(ObservableDictionary<string, bool>));
				setting = (ObservableDictionary<string, bool>)xmlSer.Deserialize(new StringReader(Properties.Settings.Default.PluginsEnabled));
			}
			return setting;
		}

		/// <summary>
		/// <para>Saves the <see cref="ObservableDictionary<string, bool>"/> in the settings</para>
		/// </summary>
		/// <param name="plugins"></param>
		public static void PutPluginsSetting(ObservableDictionary<string, bool> plugins)
		{
			StringBuilder str = new StringBuilder();
			XmlSerializer xmlSer = new XmlSerializer(typeof(ObservableDictionary<string, bool>));
			xmlSer.Serialize(new StringWriter(str), plugins);
			Properties.Settings.Default.PluginsEnabled = str.ToString();
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// <para>Creates a <see cref="ObservableDictionary<string, bool>"/> that contains all available <see cref="TaskPlugin"/>s and loads their values from the settings</para>
		/// </summary>
		/// <param name="Main"></param>
		/// <returns></returns>
		public static ObservableDictionary<string, bool> GetPluginsAll(Main Main)
		{
			ObservableDictionary<string, bool> setting = GetPluginsSetting();

			ObservableDictionary<string, bool> plugins = new ObservableDictionary<string, bool>();
			foreach (Type task in Main.TaskMgr.TaskPluginsAvailable)
			{
				string name = task.Name;
				bool enabled = true;
				if (setting.ContainsKey(name))
					enabled = setting[name];
				plugins.Add(name, enabled);
			}

			return plugins;
		}
		#endregion
	}
}