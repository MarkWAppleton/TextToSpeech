using System;
using System.Collections.Generic;
using System.Text;
using TextToSpeechV3.Model;

namespace TextToSpeechV3.SpeechManager
{
	static class SpeechManagerFactory
	{
		public static ISpeechManager CreateSpeechManager(EnumSpeechEngine speechEngine)
		{
			switch (speechEngine)
			{
				case EnumSpeechEngine.Legacy:
					return new SAPI();
				case EnumSpeechEngine.Windows10:
					return new WindowsMediaSpeechSynthesis();
				default:
					return null;
			}
		}
		public static ISpeechManager CreateSpeechManager(SpeechSettings settings)
		{
			ISpeechManager sm = null;
			switch (settings.Engine)
			{
				case EnumSpeechEngine.Legacy:
					sm = new SAPI();
					break;
				case EnumSpeechEngine.Windows10:
					sm = new WindowsMediaSpeechSynthesis();
					break;
				default:
					return null;
			}
			sm.SetAllSettings(settings);
			return sm;
		}
	}
}
