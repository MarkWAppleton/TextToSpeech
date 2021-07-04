using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechV3.Services.Interfaces;

namespace TextToSpeechV3.Services
{
	public class ImageProcessingService : IImageProcessingService
	{

		public Bitmap ProcessImage(Bitmap original)
		{
			using Mat mat1 = OpenCvSharp.Extensions.BitmapConverter.ToMat(original);
			//Converts to grayscale;
			Cv2.CvtColor(mat1, mat1, ColorConversionCodes.BGR2GRAY);

			return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat1);
		}

	}
}
