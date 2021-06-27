using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TextToSpeechV3.Hotkeys;

namespace TextToSpeechV3.Model
{
	public class Hotkey
	{
		public Keys Key { get; set; }
		public Modifiers Modifier { get; set; }

		public Hotkey()
		{

		}
		public Hotkey(Keys key, Modifiers modifier)
		{
			Key = key;
			Modifier = modifier;
		}
	}
}
