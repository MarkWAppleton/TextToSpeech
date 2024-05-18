using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.Model.ImagePrcessing
{
    public class ResizeImageProcessingConfig : IImageProcessingConfig
	{
		public int WidthMultiplier { get; set; } = 2;
		public int HeightMultiplier { get; set; } = 2;
	}
}
