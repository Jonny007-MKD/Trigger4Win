using Systemm = System;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tasker.Events
{
	/// <summary>
	/// <para>This <see cref="Manager"/> takes care of all available <see cref="EventPlugin"/>s and provides access to instances of them when a <see cref="Tasks.TaskPlugin"/> needs it</para>
	/// </summary>
	public class Manager
	{
		#region Properties
		/// <summary>A list of the <see cref="EventPlugin"/>s</summary>
		private Dictionary<Type, EventPlugin> EventPluginInstances = new Dictionary<Type, EventPlugin>();
		private List<EventPlugin> EventPluginInstancesAll = new List<EventPlugin>();
		private List<Type> EventPluginTypes = new List<Type>();

		/// <summary>
		/// <para>The type of the event</para>
		/// </summary>
		public enum EventTypes
		{
			/// <summary>A simple event with no further information</summary>
			Simple,
			/// <summary>An event which gives only one value (like a new item's name)</summary>
			SingleValue,
			/// <summary>An event which gives the previous and post status of a value</summary>
			ChangingValue,
		}

		private Log Log;
		#endregion

		#region Constructors
		/// <summary></summary>
		/// <param name="Main"></param>
		public Manager(Main Main)
		{
			this.Log = Main.Log;

			List<Type> types = new List<Systemm.Type>(Systemm.Reflection.Assembly.GetExecutingAssembly().GetTypes());
			this.EventPluginTypes = types.FindAll(new Systemm.Predicate<Systemm.Type>(item => { return item.BaseType == typeof(EventPlugin) && item.Namespace == "Tasker.Events"; }));
		}
		#endregion

		#region Methods
		/// <summary>
		/// <para>Checks whether the <see cref="EventPlugin"/> with the specified <paramref name="type"/> exists</para>
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool PluginExists(Type type)
		{
			return this.EventPluginTypes.Contains(type);
		}
		/// <summary>
		/// <para>Checks whether the <see cref="EventPlugin"/> <typeparamref name="T"/> exists</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool PluginExists<T>() where T : EventPlugin
		{
			return this.EventPluginTypes.Contains(typeof(T));
		}

		/// <summary>
		/// <para>Searches for an existing instance of the <see cref="EventPlugin"/> <typeparamref name="T"/>. If non is found, a new one is created.</para>
		/// </summary>
		/// <typeparam name="T">The <see cref="EventPlugin"/> that shall be loaded</typeparam>
		/// <returns></returns>
		/// <exception cref="ArgumentException">The specified <typeparamref name="T"/> does not exist!</exception>
		public T GetPlugin<T>() where T : EventPlugin
		{
			return this.GetPlugin<T>(null, true);
		}
		/// <summary>
		/// <para>Creates a new instance of the specified <see cref="EventPlugin"/> <typeparamref name="T"/> passing the specified <paramref name="args"/></para>
		/// </summary>
		/// <param name="args">Additional arguments that shall be passed to the constructor</param>
		/// <typeparam name="T">The <see cref="EventPlugin"/> that shall be loaded</typeparam>
		/// <returns></returns>
		/// <exception cref="ArgumentException">The specified <typeparamref name="T"/> does not exist!</exception>
		public T GetPlugin<T>(object[] args) where T:EventPlugin
		{
			return GetPlugin<T>(args, false);
		}
		/// <summary>
		/// <para>Returns an instance of the <see cref="EventPlugin"/> <typeparamref name="T"/> passing the specified <paramref name="args"/></para>
		/// </summary>
		/// <param name="args">Additional arguments that shall be passed to the constructor</param>
		/// <param name="reuseExisting">Whether an existing instance of this class shall be reused.</param>
		/// <typeparam name="T">The <see cref="EventPlugin"/> that shall be loaded</typeparam>
		/// <returns></returns>
		/// <exception cref="ArgumentException">The specified <typeparamref name="T"/> does not exist!</exception>
		public T GetPlugin<T>(object[] args, bool reuseExisting) where T : EventPlugin
		{
			Type primKey = typeof(T);

			if (this.PluginExists(primKey) == false)
				throw new ArgumentException("The EventPlugin \"" + primKey.Name + "\" does not exist!", "name");

			if (reuseExisting && this.EventPluginInstances.ContainsKey(primKey))
				return (T)this.EventPluginInstances[primKey];

#if DEBUG
			Systemm.Diagnostics.Stopwatch swInitEvent = new Systemm.Diagnostics.Stopwatch();
			swInitEvent.Start();
#endif
			T plugin = (T)Systemm.Activator.CreateInstance(primKey, args);
			if (!this.EventPluginInstances.ContainsKey(primKey))		// do we really want to store the plugin?
				this.EventPluginInstances.Add(primKey, plugin);
			this.EventPluginInstancesAll.Add(plugin);

#if DEBUG
			swInitEvent.Stop();
			this.Log.LogLine("Loaded Event plugin \"" + primKey.Name + "\" in " + swInitEvent.ElapsedMilliseconds + "ms", Log.Type.Other);
#else
			this.Log.LogLine("Loaded Event plugin \"" + primKey.Name + "\"", Log.Type.Other);
#endif
			return plugin;
		}

		/// <summary>
		/// <para>Returns the current status of this <see cref="Manager"/></para>
		/// <para>This includes the lists with available and with loaded <see cref="EventPlugin"/>s</para>
		/// </summary>
		/// <returns></returns>
		public TreeNode GetStatus()
		{
			TreeNode tnMain = new TreeNode("Event Manager");


			TreeNode tnTypes = new TreeNode("Available types (" + this.EventPluginTypes.Count + ")");
			tnMain.Nodes.Add(tnTypes);

			this.EventPluginTypes.ForEach(new Action<Type>((item) =>
						tnTypes.Nodes.Add(new TreeNode(item.Name))
					));


			TreeNode tnClasses = new TreeNode("Loaded classes (" + this.EventPluginInstancesAll.Count + ")");
			tnMain.Nodes.Add(tnClasses);

			foreach (EventPlugin ep in this.EventPluginInstancesAll)
				tnClasses.Nodes.Add(ep.GetStatus());

			return tnMain;
		}
		#endregion
	}
}
