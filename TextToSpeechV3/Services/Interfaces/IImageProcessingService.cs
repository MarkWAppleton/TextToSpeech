using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechV3.Services.Interfaces
{
	public interface IImageProcessingService
	{
		Bitmap ProcessImage(Bitmap original);
	}
}
