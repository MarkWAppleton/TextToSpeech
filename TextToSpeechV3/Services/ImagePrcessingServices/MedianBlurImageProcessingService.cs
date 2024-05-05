using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.Model.ImagePrcessing;

namespace TextToSpeech.Services.ImagePrcessingStages
{
	public class MedianBlurImageProcessingService : IImageProcessingService
	{
		public Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null)
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
	}
}
