using System.Drawing;
using System.Drawing.Imaging;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Services
{
	public class CreateBitmapService : ICreateBitmapService
	{
		public Bitmap CreateBitmap(Rectangle screenLocation)
		{
			Bitmap bmp = new Bitmap(screenLocation.Width, screenLocation.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bmp);
			graphics.CopyFromScreen(screenLocation.Left, screenLocation.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
			return bmp;
		}
	}
}
