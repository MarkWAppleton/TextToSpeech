using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.Model;
using TextToSpeech.Views;

namespace TextToSpeech.Services
{
	public class SnippingScreenshot : ISnippingScreenshot
	{
		private bool _takingScreenshot;

		public SnippingScreenshot()
		{
			_takingScreenshot = false;
		}

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

			Rectangle rect = screenshotDetails.ToRectangle();
			Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bmp);
			graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

			return bmp;
		}
	}
}
