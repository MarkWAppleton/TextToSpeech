using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Services
{
	public class CopyTextFromScreenService : ICopyTextFromScreenService
	{
		public string GetTextFromScreen()
		{
			SendKeys.SendWait("^(c)");
			Thread.Sleep(80);//TODO make this configurable
			string copiedText = System.Windows.Clipboard.GetText();
			return copiedText;
		}
	}
}
