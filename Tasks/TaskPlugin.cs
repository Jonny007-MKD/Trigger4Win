using System.Windows.Forms;

namespace Tasker.Tasks
{
	abstract class TaskPlugin
	{
		#region Properties
		#endregion

		#region Override Methods
		/// <summary>
		/// <para>Registers the event</para>
		/// </summary>
		public abstract bool Init(Main Main);

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
