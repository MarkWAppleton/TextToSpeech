using System.Text.Json;
using TextToSpeech.Model;
using TextToSpeech.Properties;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.Views;

namespace TextToSpeech.Services
{
	public class SetScreenshotLocation : ISetScreenshotLocation
	{
		private bool _takingScreenshot = false;

		void ISetScreenshotLocation.SetScreenshotLocation()
		{
			if (_takingScreenshot)
				return;

			_takingScreenshot = true;

			SnippingTool snippingTool = new SnippingTool();
			snippingTool.ShowDialog();
			_takingScreenshot = false;
			ObjectPositionAndSize screenshotDetails = snippingTool.Result;

			Settings.Default.ScreenshotSettings = JsonSerializer.Serialize(screenshotDetails);
			Settings.Default.Save();
		}
	}
}
