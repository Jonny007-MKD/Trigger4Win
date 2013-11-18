using System;
using System.Net;

namespace Trigger //.Classes.Extensions
{
	public static class ArrayExtensions
	{
		/// <summary>
		/// <para>Converts the specified <paramref name="array"/> to a string <see cref="Array"/></para>
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static string[] ToStrings(this object[] array)
		{
			string[] stringArray = new string[array.LongLength];
			for (long i = 0; i < array.LongLength; i++)
				stringArray[i] = array[i].ToString();
			return stringArray;
		}
	}
}
