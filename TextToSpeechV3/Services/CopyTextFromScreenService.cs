using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TextToSpeechV3.Services.Interfaces;

namespace TextToSpeechV3.Services
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
