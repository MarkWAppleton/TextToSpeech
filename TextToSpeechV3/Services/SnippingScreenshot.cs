using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using TextToSpeechV3.Services.Interfaces;
using TextToSpeechV3.Model;
using TextToSpeechV3.Views;

namespace TextToSpeechV3.Services
{
	public class SnippingScreenshot : ISnippingScreenshot
	{
		public Bitmap TakeSnippingScreenshot()
		{
			SnippingTool snippingTool = new SnippingTool();
			snippingTool.ShowDialog();
			ObjectPositionAndSize screenshotDetails = snippingTool.Result;

			Rectangle rect = screenshotDetails.ToRectangle();
			Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bmp);
			graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

			return bmp;
		}
	}
}
