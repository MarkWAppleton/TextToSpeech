using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.Model.ImagePrcessing
{
	public record GaussianBlurImageProcessingConfig : IImageProcessingConfig
	{
		public int Width { get; set; } = 5;
		public int Height { get; set; } = 5;
		public double sigmaX { get; set; } = 0;
		public double SigmaY { get; set; } = 0;
	}
}
