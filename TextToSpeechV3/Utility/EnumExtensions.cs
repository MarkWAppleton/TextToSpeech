using System;
using System.Collections.Generic;
using System.Text;

namespace TextToSpeech.Utility
{
	public static class EnumExtensions<T> where T : struct, IConvertible
	{
		public static Dictionary<T, string> ToDictionary() 
		{
			if(!typeof(T).IsEnum)
			{
				throw new ArgumentException("T must be an enum");
			}

			Dictionary<T, string> result = new Dictionary<T, string>();

			foreach(object x in Enum.GetValues(typeof(T)))
			{
				if (!result.ContainsKey((T)x))
				{
					result.Add((T)x, x.ToString());
				}
			}

			return result;
		}
	}
}
