using System.Windows;
using System.Windows.Controls;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Services.ImagePrcessingStages;

namespace TextToSpeech.View.DataTemplateSelectors
{
	public class ImageProcessingStepDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate ResizeImageProcessingConfigTemplate { get; set; }
		public DataTemplate GrayScaleImageProcessingConfigTemplate { get; set; }
		public DataTemplate GaussianBlurImageProcessingConfigTemplate { get; set; }
		public DataTemplate MedianBlurImageProcessingConfigTemplate { get; set; }
		public DataTemplate ThresholdingImageProcessingConfigTemplate { get; set; }
		
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if(item is ImageProcessingStage stage)
			{
				return stage.ImageProcessingService switch
				{
					ResizeImageProcessingService => ResizeImageProcessingConfigTemplate,
					GrayScaleImageProcessingService => GrayScaleImageProcessingConfigTemplate,
					GaussianBlurImageProcessingService => GaussianBlurImageProcessingConfigTemplate,
					MedianBlurImageProcessingService => MedianBlurImageProcessingConfigTemplate,
					ThresholdingImageProcessingService => ThresholdingImageProcessingConfigTemplate,
					_ => base.SelectTemplate(item, container)
				};
			}

			return base.SelectTemplate(item, container);
		}
	}
}
