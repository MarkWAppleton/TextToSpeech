using System;
using System.Collections.Generic;
using System.Text;

namespace TextToSpeechV3.Hotkeys
{
	public interface IHotKeyRegister
	{
		event EventHandler HotkeyTriggered;

		void UnregisterHotkey();
	}
}
