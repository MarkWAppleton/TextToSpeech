using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TextToSpeechV3.Model;

namespace TextToSpeechV3.Hotkeys
{
	public class HotKeyRegister : IHotKeyRegister
	{
		public event EventHandler HotkeyTriggered;

		private readonly IntPtr _hWnd;
		private readonly int _id;

		public HotKeyRegister(Window window, Hotkey hotkey)
		{
			_hWnd = new WindowInteropHelper(window).Handle;
			uint uintKey = (uint)KeyInterop.VirtualKeyFromKey((Key)hotkey.Key);
			_id = GetHashCode();
			RegisterHotKey(_hWnd, _id, (uint)hotkey.Modifier, uintKey);
			ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
		}

		public void UnregisterHotkey()
		{
			UnregisterHotKey(_hWnd, _id);
		}

		private void ThreadPreprocessMessageMethod(ref MSG msg, ref bool handled)
		{
			if (!handled)
			{
				if (msg.message == WmHotKey && (int)(msg.wParam) == _id)
				{
					HotkeyTriggered?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public const int WmHotKey = 0x0312;
		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
	}
}
