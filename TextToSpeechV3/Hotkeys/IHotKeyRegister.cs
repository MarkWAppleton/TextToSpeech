using System;
using System.Collections.Generic;
using System.Text;

namespace TextToSpeech.Hotkeys
{
	public interface IHotKeyRegister
	{
		event EventHandler HotkeyTriggered;

		void UnregisterHotkey();
	}
}
