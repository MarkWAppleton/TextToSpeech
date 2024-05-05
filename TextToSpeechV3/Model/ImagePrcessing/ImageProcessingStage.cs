using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Model.ImagePrcessing
{
	public class ImageProcessingStage
	{
		public IImageProcessingService ImageProcessingService { get; set; }
		public IImageProcessingConfig ImageProcessingConfig { get; set; }
	}
}
