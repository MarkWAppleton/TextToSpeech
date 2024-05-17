using System.Drawing;
using Tesseract;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Services
{
	public class TesseractOcrEngine : IOcrEngine
	{
		public string RunOcr(Bitmap image)
		{
			string result = string.Empty;

			try
			{
				using (var engine = new Tesseract.TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
				//using (var engine = new Tesseract.TesseractEngine(@"./tessdata", "eng-best", EngineMode.Default))
				{
					using (Page page = engine.Process(image))
					{
						result = page.GetText();
					}
				}
			}
			catch
			{
				result = string.Empty;
			}

			return result;
		}
	}
}
