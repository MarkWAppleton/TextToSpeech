using System;
using System.Windows;
using TextToSpeech.Model;
using TextToSpeech.ViewModels;

namespace TextToSpeech.Views
{
	/// <summary>
	/// Interaction logic for ImageProcessing.xaml
	/// </summary>
	public partial class ImageProcessingSettingsView : Window
	{
		public ImageProcessingSettingsView(SpeechSettings speechSettings)
		{
			ImageProcessingSettingsViewModel vm = new ImageProcessingSettingsViewModel(this, speechSettings);
			DataContext = vm;
			if (vm.CloseAction == null)
			{
				vm.CloseAction = new Action(this.Close);
			}
			InitializeComponent();
		}
	}
}
