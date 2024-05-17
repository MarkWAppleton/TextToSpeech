using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Services.ImagePrcessingStages
{
	public class GrayScaleImageProcessingService : IImageProcessingService
	{
		public Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null)
		{
			try
			{
				var innerConfig = config as GrayScaleImageProcessingConfig;
				if (innerConfig is null)
				{
					innerConfig = new GrayScaleImageProcessingConfig();
				}
				// Convert System.Drawing.Bitmap to OpenCvSharp's Mat
				Mat originalMat = original.ToMat();

				if (originalMat.Channels() == 1)
				{
					return originalMat.ToBitmap();
				}

				// Convert the image to grayscale
				Mat grayMat = new Mat();
				Cv2.CvtColor(originalMat, grayMat, ColorConversionCodes.BGR2GRAY);

				// Convert the Mat back to Bitmap
				return grayMat.ToBitmap();
			}
			catch
			{
				return original;
			}
		}
	}
}
