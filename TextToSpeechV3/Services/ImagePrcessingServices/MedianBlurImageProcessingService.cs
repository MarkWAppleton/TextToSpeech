using OpenCvSharp.Extensions;
using OpenCvSharp;
using System.Drawing;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.Model.ImagePrcessing;

namespace TextToSpeech.Services.ImagePrcessingStages
{
	public class MedianBlurImageProcessingService : IImageProcessingService
	{
		public Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null)
		{
			try
			{
				var innerConfig = config as MedianBlurImageProcessingConfig;
				if (innerConfig is null)
				{
					innerConfig = new MedianBlurImageProcessingConfig();
				}
				// Convert System.Drawing.Bitmap to OpenCvSharp's Mat
				Mat originalMat = original.ToMat();

				// Apply Gaussian blur
				Mat medianBlurredMat = new Mat();
				Cv2.MedianBlur(originalMat, medianBlurredMat, ksize: innerConfig.Size);

				// Convert the Mat back to Bitmap
				return medianBlurredMat.ToBitmap();
			}
			catch
			{
				return original;
			}
		}
	}
}
