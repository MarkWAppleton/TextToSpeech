using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechV3.Services.Interfaces;

namespace TextToSpeechV3.Services
{
	public class SimpleResizeImageProcessingService : IImageProcessingService
	{
		public Bitmap ProcessImage(Bitmap original, out List<Bitmap> processingSteps)
		{
			processingSteps = new List<Bitmap>();
			return new Bitmap(original, new Size(original.Width * 2, original.Height * 2));
		}
	}
}
