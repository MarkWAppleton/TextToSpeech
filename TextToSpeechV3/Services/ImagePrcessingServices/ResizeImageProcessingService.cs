using System.Drawing;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Services.ImagePrcessingStages
{
	public class ResizeImageProcessingService : IImageProcessingService
    {
        public Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null)
        {
            try
			{
				var innerConfig = config as ResizeImageProcessingConfig;
				if (innerConfig is null)
				{
					innerConfig = new ResizeImageProcessingConfig();
				}

				return new Bitmap(original, new Size(original.Width * innerConfig.WidthMultiplier, original.Height * innerConfig.HeightMultiplier));
            }
            catch
            {
                return original;
            }
        }
    }
}
