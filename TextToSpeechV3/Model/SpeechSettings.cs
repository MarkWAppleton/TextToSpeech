using System.Collections.Generic;
using System.Linq;
using TextToSpeech.SpeechManager;
using TextToSpeech.Utility;

namespace TextToSpeech.Model
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

		public void SetHotkey(EnumFeature feature, Hotkey hotkey)
		{
			if (!Hotkeys.ContainsKey(feature))
			{
				Hotkeys.Add(feature, new Hotkey());
			}
			Hotkeys[feature] = hotkey;
		}

		public SpeechSettings ShallowClone()
		{
			return (SpeechSettings)this.MemberwiseClone();
		}

		public bool IsValid(out List<string> errors)
		{
			errors = new List<string>();
			var duplicates = Hotkeys.Where(w => Hotkeys.Any(a => a.Key != w.Key && a.Value.Key == w.Value.Key && a.Value.Modifier == w.Value.Modifier));
			if(duplicates.Count() > 0)
			{
				errors.Add("All Hotkeys must be unique.");
				return false;
			}
			return true;
		}
	}
}
