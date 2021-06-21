using System;
using System.Collections.Generic;
using System.Text;

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
	}
}
