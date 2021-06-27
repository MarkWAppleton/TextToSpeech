using System;
using System.Collections.Generic;
using System.Text;
using TextToSpeechV3.SpeechManager;
using TextToSpeechV3.Utility;

namespace TextToSpeechV3.Model
{
	public class SpeechSettings
	{
		public string Voice { get; set; }
		public double Rate { get; set; }
		public double Volume { get; set; }
		public EnumSpeechEngine Engine { get; set; }
		public Dictionary<EnumFeature, Hotkey> Hotkeys { get; set; }

		public SpeechSettings()
		{
			Hotkeys = new Dictionary<EnumFeature, Hotkey>();
		}

		public SpeechSettings ShallowClone()
		{
			return (SpeechSettings)this.MemberwiseClone();
		}
	}
}
