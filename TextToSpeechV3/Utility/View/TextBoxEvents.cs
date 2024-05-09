using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TextToSpeech.Utility.View
{
	public static class TextBoxEvents
	{
		public static void TextBox_PreviewTextInput_Numeric_Int(object sender, TextCompositionEventArgs e)
		{
			e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
		}
		public static void TextBox_PreviewTextInput_Numeric_Double(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]*(?:\.[0-9]*)?$");
		}
	}
}
