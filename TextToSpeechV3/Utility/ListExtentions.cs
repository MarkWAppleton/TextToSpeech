using System.Collections.Generic;

namespace TextToSpeech.Utility
{
	public static class ListExtentions
	{
		public static void MoveItem<T>(this List<T> values, T item, int nowPosition)
		{
			values.Remove(item);
			values.Insert(nowPosition, item);
		}
	}
}
