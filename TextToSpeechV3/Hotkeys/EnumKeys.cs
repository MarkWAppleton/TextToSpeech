using System;
using System.Collections.Generic;
using System.Text;

namespace TextToSpeechV3.Hotkeys
{

	[Flags]
	public enum Keys
	{
		//
		// Summary:
		//     No key pressed.
		None = 0,
		//
		// Summary:
		//     The left mouse button.
		LButton = 1,
		//
		// Summary:
		//     The right mouse button.
		RButton = 2,

		//
		// Summary:
		//     The BACKSPACE key.
		Back = 8,
		//
		// Summary:
		//     The TAB key.
		Tab = 9,
		//
		// Summary:
		//     The LINEFEED key.
		LineFeed = 10,
		//
		// Summary:
		//     The CLEAR key.
		Clear = 12,
		//
		// Summary:
		//     The ENTER key.
		Enter = 13,
		//
		// Summary:
		//     The RETURN key.
		Return = 13,
		//
		// Summary:
		//     The ALT key.
		Menu = 18,
		//
		// Summary:
		//     The PAUSE key.
		Pause = 19,
		//
		// Summary:
		//     The ESC key.
		Escape = 27,
		//
		// Summary:
		//     The SPACEBAR key.
		Space = 32,
		//
		// Summary:
		//     The PAGE UP key.
		Prior = 33,
		//
		// Summary:
		//     The PAGE UP key.
		PageUp = 33,
		//
		// Summary:
		//     The PAGE DOWN key.
		Next = 34,
		//
		// Summary:
		//     The PAGE DOWN key.
		PageDown = 34,
		//
		// Summary:
		//     The END key.
		End = 35,
		//
		// Summary:
		//     The HOME key.
		Home = 36,
		//
		// Summary:
		//     The LEFT ARROW key.
		Left = 37,
		//
		// Summary:
		//     The UP ARROW key.
		Up = 38,
		//
		// Summary:
		//     The RIGHT ARROW key.
		Right = 39,
		//
		// Summary:
		//     The DOWN ARROW key.
		Down = 40,
		//
		// Summary:
		//     The SELECT key.
		Select = 41,
		//
		// Summary:
		//     The PRINT key.
		Print = 42,
		//
		// Summary:
		//     The EXECUTE key.
		Execute = 43,
		//
		// Summary:
		//     The PRINT SCREEN key.
		Snapshot = 44,
		//
		// Summary:
		//     The INS key.
		Insert = 45,
		//
		// Summary:
		//     The DEL key.
		Delete = 46,
		//
		// Summary:
		//     The HELP key.
		Help = 47,
		//
		// Summary:
		//     The 0 key.
		D0 = 48,
		//
		// Summary:
		//     The 1 key.
		D1 = 49,
		//
		// Summary:
		//     The 2 key.
		D2 = 50,
		//
		// Summary:
		//     The 3 key.
		D3 = 51,
		//
		// Summary:
		//     The 4 key.
		D4 = 52,
		//
		// Summary:
		//     The 5 key.
		D5 = 53,
		//
		// Summary:
		//     The 6 key.
		D6 = 54,
		//
		// Summary:
		//     The 7 key.
		D7 = 55,
		//
		// Summary:
		//     The 8 key.
		D8 = 56,
		//
		// Summary:
		//     The 9 key.
		D9 = 57,
		//
		// Summary:
		//     The A key.
		A = 65,
		//
		// Summary:
		//     The B key.
		B = 66,
		//
		// Summary:
		//     The C key.
		C = 67,
		//
		// Summary:
		//     The D key.
		D = 68,
		//
		// Summary:
		//     The E key.
		E = 69,
		//
		// Summary:
		//     The F key.
		F = 70,
		//
		// Summary:
		//     The G key.
		G = 71,
		//
		// Summary:
		//     The H key.
		H = 72,
		//
		// Summary:
		//     The I key.
		I = 73,
		//
		// Summary:
		//     The J key.
		J = 74,
		//
		// Summary:
		//     The K key.
		K = 75,
		//
		// Summary:
		//     The L key.
		L = 76,
		//
		// Summary:
		//     The M key.
		M = 77,
		//
		// Summary:
		//     The N key.
		N = 78,
		//
		// Summary:
		//     The O key.
		O = 79,
		//
		// Summary:
		//     The P key.
		P = 80,
		//
		// Summary:
		//     The Q key.
		Q = 81,
		//
		// Summary:
		//     The R key.
		R = 82,
		//
		// Summary:
		//     The S key.
		S = 83,
		//
		// Summary:
		//     The T key.
		T = 84,
		//
		// Summary:
		//     The U key.
		U = 85,
		//
		// Summary:
		//     The V key.
		V = 86,
		//
		// Summary:
		//     The W key.
		W = 87,
		//
		// Summary:
		//     The X key.
		X = 88,
		//
		// Summary:
		//     The Y key.
		Y = 89,
		//
		// Summary:
		//     The Z key.
		Z = 90,
		//
		// Summary:
		//     The 0 key on the numeric keypad.
		NumPad0 = 96,
		//
		// Summary:
		//     The 1 key on the numeric keypad.
		NumPad1 = 97,
		//
		// Summary:
		//     The 2 key on the numeric keypad.
		NumPad2 = 98,
		//
		// Summary:
		//     The 3 key on the numeric keypad.
		NumPad3 = 99,
		//
		// Summary:
		//     The 4 key on the numeric keypad.
		NumPad4 = 100,
		//
		// Summary:
		//     The 5 key on the numeric keypad.
		NumPad5 = 101,
		//
		// Summary:
		//     The 6 key on the numeric keypad.
		NumPad6 = 102,
		//
		// Summary:
		//     The 7 key on the numeric keypad.
		NumPad7 = 103,
		//
		// Summary:
		//     The 8 key on the numeric keypad.
		NumPad8 = 104,
		//
		// Summary:
		//     The 9 key on the numeric keypad.
		NumPad9 = 105,
		//
		// Summary:
		//     The multiply key.
		Multiply = 106,
		//
		// Summary:
		//     The add key.
		Add = 107,
		//
		// Summary:
		//     The separator key.
		Separator = 108,
		//
		// Summary:
		//     The subtract key.
		Subtract = 109,
		//
		// Summary:
		//     The decimal key.
		Decimal = 110,
		//
		// Summary:
		//     The divide key.
		Divide = 111,
		//
		// Summary:
		//     The F1 key.
		F1 = 112,
		//
		// Summary:
		//     The F2 key.
		F2 = 113,
		//
		// Summary:
		//     The F3 key.
		F3 = 114,
		//
		// Summary:
		//     The F4 key.
		F4 = 115,
		//
		// Summary:
		//     The F5 key.
		F5 = 116,
		//
		// Summary:
		//     The F6 key.
		F6 = 117,
		//
		// Summary:
		//     The F7 key.
		F7 = 118,
		//
		// Summary:
		//     The F8 key.
		F8 = 119,
		//
		// Summary:
		//     The F9 key.
		F9 = 120,
		//
		// Summary:
		//     The F10 key.
		F10 = 121,
		//
		// Summary:
		//     The F11 key.
		F11 = 122,
		//
		// Summary:
		//     The F12 key.
		F12 = 123,
		//
		// Summary:
		//     The F13 key.
		F13 = 124,
		//
		// Summary:
		//     The F14 key.
		F14 = 125,
		//
		// Summary:
		//     The F15 key.
		F15 = 126,
		//
		// Summary:
		//     The F16 key.
		F16 = 127,
		//
		// Summary:
		//     The F17 key.
		F17 = 128,
		//
		// Summary:
		//     The F18 key.
		F18 = 129,
		//
		// Summary:
		//     The F19 key.
		F19 = 130,
		//
		// Summary:
		//     The F20 key.
		F20 = 131,
		//
		// Summary:
		//     The F21 key.
		F21 = 132,
		//
		// Summary:
		//     The F22 key.
		F22 = 133,
		//
		// Summary:
		//     The F23 key.
		F23 = 134,
		//
		// Summary:
		//     The F24 key.
		F24 = 135,
	}
}
