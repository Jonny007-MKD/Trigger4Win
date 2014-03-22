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
		/// <summary>
		/// <para>Locks the workstation's display. Locking a workstation protects it from unauthorized use.</para>
		/// </summary>
		/// <returns>
		/// <para>If the function succeeds, the return value is nonzero. Because the function executes asynchronously, a nonzero return value indicates that the operation has been initiated. It does not indicate whether the workstation has been successfully locked.
		/// <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para>
		/// </returns>
		[DllImport("powrprof.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool LockWorkStation();
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
