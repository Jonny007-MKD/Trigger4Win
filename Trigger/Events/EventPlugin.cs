using System;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Trigger.Events
{
	/// <summary>
	/// <para>An abstract class that every event plugin has to inherit from</para>
	/// </summary>
	public abstract class EventPlugin
	{
		#region Events Delegates
		/// <summary>
		/// <para>A simple Event without any special arguments</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void Event(object sender, EventArgs e);
		/// <summary>
		/// <para>An event that also passes the new value</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void EventValue<T>(object sender, EventArgsValue<T> e);
		/// <summary>
		/// <para>An event that passes both the old and the new value</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void EventValues<T>(object sender, EventArgsValues<T> e);
		/// <summary>
		/// <para>The event that gives a reason why it occured</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void EventReason<T>(object sender, EventArgsReason<T> e);
		#endregion


		#region Methods
		/// <summary>
		/// <para>Returns an <see cref="EventList"/> with the supported events</para>
		/// </summary>
		/// <returns></returns>
		public abstract EventList EventNames();

		/// <summary>
		/// <para>Returns the current status of the <see cref="EventPlugin"/></para>
		/// </summary>
		/// <returns></returns>
		public virtual TreeNode GetStatus()
		{
			return new TreeNode(this.GetType().Name);
		}
		#endregion
	}
}
