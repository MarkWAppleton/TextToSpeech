using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Model.ImagePrcessing
{
	public class ImageProcessingStage
	{
		public IImageProcessingService ImageProcessingService { get; set; }
		public IImageProcessingConfig ImageProcessingConfig { get; set; }
	}
}
