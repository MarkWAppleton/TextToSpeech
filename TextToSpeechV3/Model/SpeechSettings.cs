using System;
using System.Collections.Generic;
using System.Text;
using TextToSpeechV3.SpeechManager;

namespace TextToSpeechV3.Model
{
	public class SpeechSettings
	{
		public string Voice { get; set; }
		public double Rate { get; set; }
		public double Volume { get; set; }
		public EnumSpeechEngine Engine { get; set; }

		public Dictionary<string, Hotkey> Hotkeys { get; set; }

		public SpeechSettings ShallowClone()
		{
			return (SpeechSettings)this.MemberwiseClone();
		}
	}
}
