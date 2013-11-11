using System;
using Microsoft.VisualBasic.ApplicationServices;

namespace Trigger
{
	public partial class Main
	{
		#region Properties
		/// <summary>
		/// <para>Is triggered when the application is already running and then gets called with command line arguments</para>
		/// </summary>
		public event Events.EventPlugin.EventValue<string> OnCommandLineTrigger;
		#endregion

		#region Methods
		/// <summary>
		/// <para>Does what the constructor normally should do.</para>
		/// <para>This way the window stays hidden when we start a next instance</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Init(object sender, StartupEventArgs e)
		{
			InitializeComponent();

			this.Log = new Log(this);
			this.Log.Show();
			this.EventMgr = new Events.Manager(this);
			this.TaskMgr = new Tasks.Manager(this);
			this.Log.LogLine(new String('-', 32), Log.Type.Other);
		}

		/// <summary>
		/// <para>Handles the argument options that were passed to a new instance of this application</para>
		/// </summary>
		/// <param name="options"></param>
		public void HandleNewInstance(Options options)
		{
			if (options.Trigger != null && OnCommandLineTrigger != null)
				OnCommandLineTrigger(this, new Events.EventArgsValue<string>(options.Trigger));
		}
		#endregion
	}
}