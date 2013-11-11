using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Trigger.Tasks
{
	/// <summary>
	/// <para>This <see cref="Manager"/> takes care of all available <see cref="TaskPlugin"/>s and automatically loads them on startup</para>
	/// </summary>
	public class Manager
	{
		#region Properties
		private Main Main;

		private List<TaskPlugin> TaskPluginInstances = new List<TaskPlugin>();
		private List<Type> TaskPluginsAvailable;
		#endregion

		#region Constructors
		/// <summary></summary>
		/// <param name="Main"></param>
		public Manager(Main Main)
		{
			this.Main = Main;

			bool loggingEnabled = Properties.Settings.Default.LoadLoggingTasks;

			List<Type> types = this.getAvailableTasks();
			this.TaskPluginsAvailable = types;

			if (!loggingEnabled)
				types = types.FindAll(new Predicate<Type>(item => { return !item.Name.StartsWith("Log"); }));

			types.ForEach(new Action<Type>(item => loadTask(item)));
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Returns the current status of this <see cref="Manager"/></para>
		/// <para>This includes the lists with available and with loaded <see cref="TaskPlugin"/>s</para>
		/// </summary>
		/// <returns></returns>
		public TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Task Manager");

			TreeNode tnAvailable = tnMain.Nodes.Add("Available Tasks (" + this.TaskPluginsAvailable.Count + ")");
			this.TaskPluginsAvailable.ForEach(new Action<Type>((item) => tnAvailable.Nodes.Add(new TreeNode(item.Name))));

			TreeNode tnLoaded = tnMain.Nodes.Add("Loaded Tasks (" + this.TaskPluginInstances.Count + ")");
			this.TaskPluginInstances.ForEach(new Action<TaskPlugin>((tp) => tnLoaded.Nodes.Add(tp.GetStatus())));

			return tnMain;
		}

		/// <summary>
		/// <para>Gets a list of all available <see cref="TaskPlugin"/>s using Reflection</para>
		/// </summary>
		/// <returns></returns>
		private List<Type> getAvailableTasks()
		{
			List<Type> types = new List<Type>(System.Reflection.Assembly.GetExecutingAssembly().GetTypes());
			types = types.FindAll(new Predicate<Type>(item => { return item.BaseType == typeof(TaskPlugin) && item.Namespace.StartsWith("Trigger.Tasks"); }));
			return types;
		}

		/// <summary>
		/// <para>Loads all logging tasks (task whose name start with "Log")</para>
		/// <para>Caution: There is no check whether a task is already loaded!</para>
		/// </summary>
		public void LoadLoggingTasks()
		{
			List<Type> types = TaskPluginsAvailable.FindAll(new Predicate<Type>((item) => { return item.Name.StartsWith("Log"); }));
			types.ForEach(new Action<Type>((item) => loadTask(item)));
		}

		/// <summary>
		/// <para>Creates an instance of the specified <paramref name="type"/></para>
		/// </summary>
		/// <param name="type"></param>
		private void loadTask(Type type)
		{
			try
			{
#if DEBUG
			System.Diagnostics.Stopwatch swInitEvent = new System.Diagnostics.Stopwatch();
			swInitEvent.Start();
#endif
				TaskPlugin task = (TaskPlugin)Activator.CreateInstance(type);
#if DEBUG
				if (new List<System.Reflection.MethodInfo>(type.GetMethods()).Find(new Predicate<System.Reflection.MethodInfo>(m => { return m.Name == "Init" && m.GetParameters().Length == 2; })) != null && task.Init(this.Main, swInitEvent) || task.Init(this.Main))
#else
				if (task.Init(this.Main))
#endif
				{
					this.TaskPluginInstances.Add(task);

#if DEBUG
					swInitEvent.Stop();
					this.Main.Log.LogLine("Loaded Task  plugin \"" + type.Name + "\" in " + swInitEvent.ElapsedMilliseconds + "ms", Log.Type.Other);
#else
			this.Main.Log.LogLine("Loaded Task  plugin \"" + type.Name + "\"", Log.Type.Other);
#endif
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message + "\n\n" + e.StackTrace, "Error when loading Task \"" + type.Name + "\"!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion
	}
}
