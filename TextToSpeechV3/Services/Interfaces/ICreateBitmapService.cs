using System.Drawing;

namespace TextToSpeech.Services.Interfaces
{
	public interface ICreateBitmapService
	{
		Bitmap CreateBitmap(Rectangle screenLocation);
	}
}
