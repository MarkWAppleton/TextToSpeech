using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Services.Interfaces;
using Size = OpenCvSharp.Size;

namespace TextToSpeech.Services.ImagePrcessingStages
{
	public class GaussianBlurImageProcessingService : IImageProcessingService
	{
		/// <summary>
		/// Applys Gaussian blur noice reduction.
		/// Does this require a grayscale image?
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null)
		{
			var innerConfig = config as GaussianBlurImageProcessingConfig;
			if (innerConfig is null)
			{
				innerConfig = new GaussianBlurImageProcessingConfig();
			}

			// Convert System.Drawing.Bitmap to OpenCvSharp's Mat
			Mat originalMat = original.ToMat();

			// Apply Gaussian blur
			Mat gaussianBlurredMat = new Mat();
			Cv2.GaussianBlur(originalMat, gaussianBlurredMat, new Size(innerConfig.Width, innerConfig.Height), sigmaX: innerConfig.SigmaX, sigmaY: innerConfig.SigmaY);

			// Convert the Mat back to Bitmap
			return gaussianBlurredMat.ToBitmap();
		}
	}
}
