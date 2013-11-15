using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Trigger.Actions
{
	/// <summary>
	/// <para>Actions dealing with power and energy</para>
	/// </summary>
	public class Power
	{
		#region Properties
		#endregion

		#region Enums, Structs
		#endregion

		#region Dll Imports
		[DllImport("powrprof.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool LockWorkStation();
		#endregion

		#region Methods
		/// <summary>
		/// <para>Suspends the system by shutting power down and entering a suspend (sleep) state.</para>
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// <para>force has no effect</para>
		/// <para><see cref="Application.SetSuspendState"/></para>
		/// </remarks>
		public static bool Suspend()
		{
			return Application.SetSuspendState(PowerState.Suspend, force: false, disableWakeEvent: false);
		}

		/// <summary>
		/// <para>Suspends the system by shutting power down and entering hibernation (S4).</para>
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// <para>force has no effect</para>
		/// <para><see cref="Application.SetSuspendState"/></para>
		/// </remarks>
		public static bool Hibernate()
		{
			return Application.SetSuspendState(PowerState.Hibernate, force: false, disableWakeEvent: false);
		}
		#endregion
	}
}
