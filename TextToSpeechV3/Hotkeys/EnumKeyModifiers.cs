using System;

namespace TextToSpeech.Hotkeys
{
	[Flags]
	public enum Modifiers : uint
	{
		None = 0,
		Alt = 1,
		Control = 2,
		Shift = 4,
	}
}
