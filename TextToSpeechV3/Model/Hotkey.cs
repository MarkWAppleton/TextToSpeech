using TextToSpeech.Hotkeys;

namespace TextToSpeech.Model
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
