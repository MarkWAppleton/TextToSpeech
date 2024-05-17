using System;

namespace TextToSpeech.Hotkeys
{
	public interface IHotKeyRegister
	{
		event EventHandler HotkeyTriggered;

		void UnregisterHotkey();
	}
}
