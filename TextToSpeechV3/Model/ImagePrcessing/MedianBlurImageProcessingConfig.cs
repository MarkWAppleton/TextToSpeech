namespace TextToSpeech.Model.ImagePrcessing
{
	public record MedianBlurImageProcessingConfig : IImageProcessingConfig
	{
		public int Size { get; set; } = 5;
	}
}
