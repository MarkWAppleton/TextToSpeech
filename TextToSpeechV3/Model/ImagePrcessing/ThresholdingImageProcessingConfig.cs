using OpenCvSharp;

namespace TextToSpeech.Model.ImagePrcessing
{
	public class ThresholdingImageProcessingConfig : IImageProcessingConfig
	{
		public double Thresh { get; set; } = 0;
		public double Maxval { get; set; } = 255;
		public ThresholdTypes Type { get; set; } = ThresholdTypes.Binary | ThresholdTypes.Otsu;
	}
}
