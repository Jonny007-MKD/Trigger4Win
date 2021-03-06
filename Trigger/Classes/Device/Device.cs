﻿using System;

namespace Trigger.Classes.Device
{
	/// <summary>
	/// <para>A device. This is very abstract</para>
	/// </summary>
	public class Device
	{
		#region Enums
		/// <summary><para>Type of the <see cref="Device"/></para></summary>
		public enum DeviceType : byte
		{
			/// <summary><para>This is an IDE disk (HDD, SDD, ...)</para></summary>
			IDE,
			/// <summary><para>This is a floppy disk</para></summary>
			Floppy,
			/// <summary><para>This is a compact disk</para></summary>
			CD,
			/// <summary><para>This is a digital video disk</para></summary>
			DVD,
			/// <summary><para>This is a blue ray disk</para></summary>
			BD,
			/// <summary><para>This is a USB disk</para></summary>
			USB,
			/// <summary><para>This can be any disk</para></summary>
			Any,
			/// <summary><para>This is a screen</para></summary>
			Screen,
		}
		#endregion

		#region Properties
		#region Instance
		/// <summary><para>The name of the <see cref="Device"/></para></summary>
		public string Name
		{
			get;
			internal set;
		}

		/// <summary><para>The Id of the <see cref="Device"/></para></summary>
		public string Id
		{
			get;
			internal set;
		}

		/// <summary><para>The type of the <see cref="Device"/></para></summary>
		public DeviceType Type
		{
			get;
			internal set;
		}

		/// <summary><para>Get the model of this disk. This is the manufacturer's name.</para></summary>
		public string Model
		{
			get;
			internal set;
		}


		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Creates an instance of this <see cref="Device"/></para>
		/// </summary>
		public Device(string Id)
		{
			this.Id = Id;
		}
		#endregion

		#region Operators
		/// <summary>
		/// <para>Gets a unique code of this <see cref="Device"/></para>
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		/// <summary></summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns></returns>
		public static bool operator ==(Device A, Device B)
		{
			if ((object)A == null)
				return (object)B == null;
			if ((object)B == null)
				return false;

			return A.Id == B.Id;
		}
		/// <summary></summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns></returns>
		public static bool operator !=(Device A, Device B)
		{
			return !(A == B);
		}

		/// <summary>
		/// <para>Compares to <see cref="Device"/>s (only the IDs)</para>
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			Device that = obj as Device;
			if (that == null)
				return false;
			return this.Id == that.Id;
		}
		#endregion
	}
}
