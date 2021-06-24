using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TextToSpeechV3.Hotkeys;

namespace TextToSpeechV3.Model
{
	public class Hotkey
	{
		public Key Key { get; private set; }
		public Modifiers Modifier { get; private set; }

		public Hotkey(Key key, Modifiers modifier)
		{
			Key = key;
			Modifier = modifier;
		}
	}
}
