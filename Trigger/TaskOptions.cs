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

			ObservableDictionary<string, bool> settings;
			if (String.IsNullOrEmpty(Properties.Settings.Default.PluginsEnabled))
				settings = new ObservableDictionary<string,bool>();
			else
			{
				XmlSerializer xmlSer = new XmlSerializer(typeof(ObservableDictionary<string, bool>));
				settings = (ObservableDictionary<string, bool>)xmlSer.Deserialize(new StringReader(Properties.Settings.Default.PluginsEnabled));
			}


			foreach (Type task in Main.TaskMgr.TaskPluginsAvailable)
			{
				string name = task.Name;
				bool enabled = true;
				if (settings.ContainsKey(name))
					enabled = settings[name];
				plugins.Add(name, enabled);
			}


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
			plugins["OnHibernate"] = true;
		}

		private void TaskOptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			StringBuilder str = new StringBuilder();
			XmlSerializer xmlSer = new XmlSerializer(typeof(ObservableDictionary<string, bool>));
			xmlSer.Serialize(new StringWriter(str), plugins);
			Properties.Settings.Default.PluginsEnabled = str.ToString();
			Properties.Settings.Default.Save();
		}

		private void msg(object sender, NotifyCollectionChangedEventArgs e)
		{
			MessageBox.Show(e.ToString());
		}
		#endregion
	}
}