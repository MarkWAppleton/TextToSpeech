//using OpenCvSharp;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TextToSpeechV3.Services.Interfaces;

//namespace TextToSpeechV3.Services
//{
//	/// <summary>
//	/// Designed to work with https://github.com/shimat/opencvsharp however results didn't improve.
//	/// Keeping around incase I want to re-try
//	/// OpenCvSharp4
//	/// OpenCvSharp4.runtime.win
//	/// </summary>
//	public class ImageProcessingService : IImageProcessingService
//	{

//		public Bitmap ProcessImage(Bitmap original, out List<Bitmap> processingSteps)
//		{
//			processingSteps = new List<Bitmap>();

//			byte[,] test = new byte[,]
//			{
//				{1,1,1,1,1},
//				{1,1,1,1,1},
//				{1,1,1,1,1},
//				{1,1,1,1,1},
//				{1,1,1,1,1}
//			};
//			using Mat kurnal = new Mat(5, 5, MatType.CV_8UC1, test);

//			using Mat mat1 = OpenCvSharp.Extensions.BitmapConverter.ToMat(original);

//			//Converts to grayscale;
//			Cv2.CvtColor(mat1, mat1, ColorConversionCodes.BGR2GRAY);
//			processingSteps.Add(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat1));

//			Cv2.Threshold(mat1, mat1, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
//			processingSteps.Add(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat1));

//			return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat1);
//		}

//	}
//}
