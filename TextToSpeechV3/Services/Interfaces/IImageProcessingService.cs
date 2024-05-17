using System.Drawing;
using TextToSpeech.Model.ImagePrcessing;

namespace TextToSpeech.Services.Interfaces
{
	public interface IImageProcessingService
	{
		Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null);
	}
}
