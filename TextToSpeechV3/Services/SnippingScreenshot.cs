using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechV3.Services.Interfaces;

namespace TextToSpeechV3.Services
{
	public class SnippingScreenshot : ISnippingScreenshot
	{
		private bool takingScreenshot = false;

		public object TakeSnippingScreenshot()
		{
			if (takingScreenshot)
				return null;

			takingScreenshot = true;


			throw new NotImplementedException();
		}
	}
}
