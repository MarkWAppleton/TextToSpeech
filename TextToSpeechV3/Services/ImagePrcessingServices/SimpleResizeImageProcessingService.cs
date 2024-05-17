using System.Drawing;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Services.ImagePrcessingStages
{
	public class SimpleResizeImageProcessingService : IImageProcessingService
    {
        public Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null)
        {
            try
            {
                return new Bitmap(original, new Size(original.Width * 2, original.Height * 2));
            }
            catch
            {
                return original;
            }
        }
    }
}
