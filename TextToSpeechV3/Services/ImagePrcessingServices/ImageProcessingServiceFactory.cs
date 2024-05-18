using System;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.Utility;

namespace TextToSpeech.Services.ImagePrcessingStages
{
	public static class ImageProcessingServiceFactory
	{
		public static IImageProcessingService CreateService(EnumImageProcessingStages enumImageProcessingStages) =>
			enumImageProcessingStages switch
			{
				EnumImageProcessingStages.Resize => new ResizeImageProcessingService(),
				EnumImageProcessingStages.GrayScale => new GrayScaleImageProcessingService(),
				EnumImageProcessingStages.GaussianBlur => new GaussianBlurImageProcessingService(),
				EnumImageProcessingStages.MedianBlur => new MedianBlurImageProcessingService(),
				EnumImageProcessingStages.Threshholding => new ThresholdingImageProcessingService(),
				_ => throw new NotSupportedException(),
			};
	}
}
