using System;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Utility;

namespace TextToSpeech.Services.ImagePrcessingServices
{
	public static class ImageProcessingConfigFactory
	{
		public static IImageProcessingConfig CreateConfig(EnumImageProcessingStages enumImageProcessingStages) =>
			enumImageProcessingStages switch
			{
				EnumImageProcessingStages.GrayScale => new GrayScaleImageProcessingConfig(),
				EnumImageProcessingStages.GaussianBlur => new GaussianBlurImageProcessingConfig(),
				EnumImageProcessingStages.MedianBlur => new MedianBlurImageProcessingConfig(),
				EnumImageProcessingStages.Threshholding => new ThresholdingImageProcessingConfig(),
				_ => throw new NotSupportedException(),
			};
	}
}
