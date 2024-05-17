using System.Drawing;

namespace TextToSpeech.Services.Interfaces
{
	public interface IOcrEngine
	{
		string RunOcr(Bitmap image);
	}
}
