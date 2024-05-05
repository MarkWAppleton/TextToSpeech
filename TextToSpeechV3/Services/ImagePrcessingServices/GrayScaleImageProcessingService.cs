using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Services.ImagePrcessingStages
{
	public class GrayScaleImageProcessingService : IImageProcessingService
	{
		public Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null)
		{
			var innerConfig = config as GrayScaleImageProcessingConfig;
			if (innerConfig is null)
			{
				innerConfig = new GrayScaleImageProcessingConfig();
			}
			// Convert System.Drawing.Bitmap to OpenCvSharp's Mat
			Mat originalMat = original.ToMat();

			// Convert the image to grayscale
			Mat grayMat = new Mat();
			Cv2.CvtColor(originalMat, grayMat, ColorConversionCodes.BGR2GRAY);

			// Convert the Mat back to Bitmap
			return grayMat.ToBitmap();
		}
	}
}
