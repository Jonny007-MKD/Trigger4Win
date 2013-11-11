using System;

namespace Trigger.Events
{
	/// <summary>
	/// <para><see cref="EventArgs"/> that carry the new value of the event</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EventArgsValue<T> : EventArgs
	{
		#region Properties
		/// <summary>The new value (after the event occured)</summary>
		public T Value { get; private set; }
		#endregion

		#region Constructors
		/// <summary></summary>
		/// <param name="Value">The new value after the event occured</param>
		public EventArgsValue(T Value)
		{
			this.Value = Value;
		}
		#endregion
	}

	/// <summary>
	/// <para><see cref="EventArgs"/> that carry the new value and the old value of the event</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EventArgsValues<T> : EventArgsValues<T, T>
	{
		/// <summary></summary>
		/// <param name="oldValue">The value before the event occured</param>
		/// <param name="newValue">The value after the event occured</param>
		public EventArgsValues(T oldValue, T newValue) : base(oldValue, newValue) { }
	}
	/// <summary>
	/// <para><see cref="EventArgs"/> that carry the new value and the old value of the event</para>
	/// </summary>
	/// <typeparam name="N"></typeparam>
	/// <typeparam name="O"></typeparam>
	public class EventArgsValues<O, N> : EventArgs
	{
		#region Properties
		/// <summary>The old value (before the event occured)</summary>
		public O OldValue { get; private set; }

		/// <summary>The new value (after the event occured)</summary>
		public N NewValue { get; private set; }
		#endregion

		#region Constructors
		/// <summary></summary>
		/// <param name="oldValue">The value before the event occured</param>
		/// <param name="newValue">The value after the event occured</param>
		public EventArgsValues(O oldValue, N newValue)
		{
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
		#endregion
	}

	/// <summary>
	/// <para><see cref="EventArgs"/> that carry a specific reason (is very similar to <see cref="EventArgsValue&lt;T&gt;"/></para>
	/// </summary>
	/// <typeparam name="R"></typeparam>
	public class EventArgsReason<R> : EventArgs
	{
		#region Properties
		/// <summary>The reason why the event occured</summary>
		public R Reason { get; private set; }
		#endregion

		#region Constructors
		/// <summary></summary>
		/// <param name="reason">The reason why the event occured</param>
		public EventArgsReason(R reason)
		{
			this.Reason = reason;
		}
		#endregion
	}
}
