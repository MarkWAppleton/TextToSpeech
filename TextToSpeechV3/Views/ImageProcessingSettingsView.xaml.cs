using System;
using System.Windows;
using System.Windows.Controls;
using TextToSpeech.ViewModels;

namespace TextToSpeech.Views
{
	/// <summary>
	/// Interaction logic for ImageProcessing.xaml
	/// </summary>
	public partial class ImageProcessingSettingsView : Window
	{
		public ImageProcessingSettingsView()
		{
			ImageProcessingSettingsViewModel vm = new ImageProcessingSettingsViewModel(this);
			DataContext = vm;
			if (vm.CloseAction == null)
			{
				vm.CloseAction = new Action(this.Close);
			}
			InitializeComponent();
		}
	}
}
