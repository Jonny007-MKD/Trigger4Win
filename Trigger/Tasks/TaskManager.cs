﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace Trigger.Tasks
{
	/// <summary>
	/// <para>This <see cref="Manager"/> takes care of all available <see cref="TaskPlugin"/>s and automatically loads them on startup</para>
	/// </summary>
	public class Manager
	{
		#region Properties
		/// <summary><para>Our <see cref="Main"/> <see cref="Form"/></para></summary>
		private Main Main;
		
		/// <summary><para>A List of available <see cref="TaskPlugin"/>s</para></summary>
		public List<Type> TaskPluginsAvailable;
		/// <summary><para>A List of actually loaded (=active) <see cref="TaskPlugin"/>s</para></summary>
		public List<Type> TaskPluginsLoaded = new List<Type>();
		/// <summary><para>A List with the instances of the <see cref="TaskPlugin"/>s</para></summary>
		private List<TaskPlugin> TaskPluginInstances = new List<TaskPlugin>();
		#endregion

		#region Constructors
		/// <summary></summary>
		/// <param name="Main"></param>
		public Manager(Main Main)
		{
			this.Main = Main;

			List<Type> types = this.getAvailableTasks();
			this.TaskPluginsAvailable = types;

			this.Refresh();
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Returns the current status of this <see cref="Manager"/></para>
		/// <para>This includes the lists with available and with loaded <see cref="TaskPlugin"/>s</para>
		/// </summary>
		/// <returns></returns>
		public TreeNode GetStatus(TreeView tv)
		{
			TreeNode tnMain = new TreeNode("Task Manager");

			TreeNode tnAvailable = tnMain.Nodes.Add("Available Tasks (" + this.TaskPluginsAvailable.Count + ")");
			this.TaskPluginsAvailable.ForEach(new Action<Type>((item) => tnAvailable.Nodes.Add(new TreeNode(item.Name))));

			TreeNode tnLoaded = tnMain.Nodes.Add("Loaded Tasks (" + this.TaskPluginInstances.Count + ")");
			this.TaskPluginInstances.ForEach(new Action<TaskPlugin>((tp) => tnLoaded.Nodes.Add(tp.GetStatus(tv))));

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
		/// <para>Loads all <see cref="TaskPlugin"/>s that are not found or enabled in settings</para>
		/// </summary>
		private void Refresh()
		{
			ObservableDictionary<string, bool> plugins = TaskOptions.GetPluginsSetting();

			List<Type> types = new List<Type>(this.TaskPluginsAvailable);
			types = types.FindAll(new Predicate<Type>(item => { return !plugins.ContainsKey(item.Name) || plugins[item.Name]; }));

			types.ForEach(new Action<Type>(item => loadTask(item)));
		}
		/// <summary>
		/// <para>Loads all <see cref="TaskPlugin"/>s that are enabled in <paramref name="plugins"/> and not yet loaded and
		/// unloads all <see cref="TaskPlugin"/>s that are disabled in <paramref name="plugins"/> and but still loaded</para>
		/// <para>This method is used by <see cref="TaskOptions"/> to notify us about changes</para>
		/// <param name="plugins">The list of plugins and whether they shall be enabled</param>
		/// </summary>
		public void Refresh(ObservableDictionary<string, bool> plugins)
		{
			foreach (KeyValuePair<string, bool> plugin in plugins)
			{
				Type type = this.TaskPluginsLoaded.Find(new Predicate<Type>(item => { return item.Name == plugin.Key; }));
				if (plugin.Value && type == null)		// task is enabled and not loaded
					this.loadTask(plugin.Key);
				if (!plugin.Value && type != null)
					this.unloadTask(type);
			}
		}

		/// <summary>
		/// <para>Creates an instance of the specified <paramref name="type"/></para>
		/// </summary>
		/// <param name="type"></param>
		private void loadTask(string type)
		{
			this.loadTask(this.TaskPluginsAvailable.Find(new Predicate<Type>(item => { return item.Name == type; })));
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
					this.TaskPluginsLoaded.Add(type);

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

		/// <summary>
		/// <para>Destructs the instance of the specified <paramref name="type"/></para>
		/// </summary>
		/// <param name="type"></param>
		private void unloadTask(Type type)
		{
			try
			{
				TaskPlugin task = this.TaskPluginInstances.Find(new Predicate<TaskPlugin>(item => { return item.GetType() == type; }));				
				task.Dispose();
				this.TaskPluginInstances.Remove(task);
				this.TaskPluginsLoaded.Remove(type);

				this.Main.Log.LogLine("Unloaded Task  plugin \"" + type.Name + "\"", Log.Type.Other);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message + "\n\n" + e.StackTrace, "Error when unloading Task \"" + type.Name + "\"!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion
	}
}
