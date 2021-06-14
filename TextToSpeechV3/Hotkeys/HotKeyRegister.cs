using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace TextToSpeechV3.Hotkeys
{
	public class HotKeyRegister : IHotKeyRegister
	{
		public event EventHandler HotkeyTriggered;

		private readonly int _id;

		public HotKeyRegister(Window window, Key key, Modifiers modifier)
		{
			IntPtr hWnd = new WindowInteropHelper(window).Handle;
			uint uintKey = (uint)KeyInterop.VirtualKeyFromKey(key);
			int x = 1;
			_id = GetHashCode();
			RegisterHotKey(hWnd, _id, (uint)modifier, uintKey);
			ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
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
