using System.Text.Json;

namespace TextToSpeech.Utility
{
	public static class JsonUtility
	{
		public static T DeserializeOrDefault<T>(string json, T defaultValue)
		{
			try
			{
				return JsonSerializer.Deserialize<T>(json);
			}
			catch
			{
				return defaultValue;
			}
		}

		public static bool TryDeserialize<T>(string json, out T result)
		{
			try
			{
				result = JsonSerializer.Deserialize<T>(json);
				return true;
			}
			catch
			{
				result = default;
				return false;
			}
		}
	}
}
