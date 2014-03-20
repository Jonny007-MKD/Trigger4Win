using System;
using System.Windows.Forms;

namespace Trigger.Tasks
{
	abstract class TaskPlugin : IDisposable
	{
		#region Properties
		#endregion

		#region Override Methods
		/// <summary>
		/// <para>Registers the event</para>
		/// </summary>
		/// <param name="Main">The <see cref="Main"/> form</param>
		public abstract bool Init(Main Main);
		/// <summary>
		/// <para>Registers the event and should take care to only measure the time it is taking for it's own initialization</para>
		/// </summary>
		/// <param name="Main">The <see cref="Main"/> form</param>
		/// <param name="swInit"></param>
		/// <remarks><paramref name="swInit"/> should be stopped before loading an event and continued afterwards.</remarks>
		public virtual bool Init(Main Main, System.Diagnostics.Stopwatch swInit)
		{
			return Init(Main);
		}
		/// <summary>
		/// <para>Unregisters all events so the task can be unloaded</para>
		/// </summary>
		public abstract void Dispose();


		/// <summary>
		/// <para>Gets the current status of this <see cref="TaskPlugin"/></para>
		/// <para>It may give some status information or only the name</para>
		/// </summary>
		/// <returns></returns>
		public virtual TreeNode GetStatus()
		{
			return new TreeNode(this.GetType().Name);
		}
		#endregion
}
}
