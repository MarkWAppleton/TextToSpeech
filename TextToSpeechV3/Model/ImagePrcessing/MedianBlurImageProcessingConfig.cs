using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.Model.ImagePrcessing
{
	public record MedianBlurImageProcessingConfig : IImageProcessingConfig
	{
		public int Size { get; set; } = 5;
	}
}
