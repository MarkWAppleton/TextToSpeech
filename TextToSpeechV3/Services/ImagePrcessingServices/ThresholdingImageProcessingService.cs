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
	public class ThresholdingImageProcessingService : IImageProcessingService
	{
		public Bitmap ProcessImage(Bitmap original,IImageProcessingConfig? config = null)
		{
			try
			{
				// Convert System.Drawing.Bitmap to OpenCvSharp's Mat
				Mat originalMat = original.ToMat();

				// Apply Gaussian blur
				Mat thresholdedMat = new Mat();

				/*
				 * Adjusting the thresholding parameters and choosing the appropriate thresholding method can help enhance the visibility of text in the image.
				 * You might need to experiment with different thresholding methods and parameters to achieve the best results for your specific images.
				 */

				Cv2.Threshold(originalMat, thresholdedMat, thresh: 0, maxval: 255, type: ThresholdTypes.Binary | ThresholdTypes.Otsu);

				// Convert the Mat back to Bitmap
				return thresholdedMat.ToBitmap();
			}
			catch 
			{ 
				//TODO log
				return original;
			}
		}
	}
}
