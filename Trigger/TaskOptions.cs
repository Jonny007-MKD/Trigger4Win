 using System;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Trigger
{
	public partial class TaskOptions : Form
	{
		#region Properties
		Main Main;
		BindingSource pluginBinding;
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

			plugins = GetPluginsAll(Main);

			pluginBinding = new BindingSource();
			pluginBinding.DataSource = plugins;
			this.dgvPlugins.DataSource = pluginBinding;
		}
		#endregion

		#region Event handler
		private void dgvPlugins_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 1 || e.RowIndex < 0)
				return;
			plugins[e.RowIndex] = !plugins[e.RowIndex];
			this.pluginBinding.DataSource = null;			// BindingSource does not register for OnCollectionChanged!?
			this.pluginBinding.DataSource = plugins;
		}

		private void TaskOptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			StringBuilder str = new StringBuilder();
			XmlSerializer xmlSer = new XmlSerializer(typeof(ObservableDictionary<string, bool>));
			xmlSer.Serialize(new StringWriter(str), plugins);
			Properties.Settings.Default.PluginsEnabled = str.ToString();
			Properties.Settings.Default.Save();

			this.Hide();
			this.Main.TaskMgr.Refresh(plugins);
		}
		#endregion

		#region Methods
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