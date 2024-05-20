using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Media.Ocr;

namespace TextToSpeech.Utility
{
	public static class TextProcessing
	{
		public static string ProcessTextForSpeech(string text)
		{
			string unescapedText = Regex.Unescape(text);
			return unescapedText.Replace("\n", " ");
		}
	}
}
