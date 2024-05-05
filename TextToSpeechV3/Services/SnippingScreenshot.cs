using System.Drawing;
using System.Drawing.Imaging;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.Model;
using TextToSpeech.Views;

namespace TextToSpeech.Services
{
	public class SnippingScreenshot : ISnippingScreenshot
	{
		private readonly ICreateBitmapService _createBitmapService = new CreateBitmapService();

		private bool _takingScreenshot = false;

		public Bitmap TakeSnippingScreenshot()
		{
			if (_takingScreenshot)
				return null;

			_takingScreenshot = true;

			SnippingTool snippingTool = new SnippingTool();
			snippingTool.ShowDialog();
			_takingScreenshot = false;
			ObjectPositionAndSize screenshotDetails = snippingTool.Result;

			if (screenshotDetails == null)
				return null;

			return _createBitmapService.CreateBitmap(screenshotDetails.ToRectangle());
		}
	}
}
