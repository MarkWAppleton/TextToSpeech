namespace TextToSpeech.Model.ImagePrcessing
{
	public record GaussianBlurImageProcessingConfig : IImageProcessingConfig
	{
		public int Width { get; set; } = 5;
		public int Height { get; set; } = 5;
		public double SigmaX { get; set; } = 0;
		public double SigmaY { get; set; } = 0;
	}
}
