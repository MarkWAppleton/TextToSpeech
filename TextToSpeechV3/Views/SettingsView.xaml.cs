using System;
using System.Windows;
using TextToSpeech.Model;
using TextToSpeech.ViewModels;

namespace TextToSpeech.Views
{
	/// <summary>
	/// Interaction logic for SettingsView.xaml
	/// </summary>
	public partial class SettingsView : Window
	{
		public SpeechSettings SpeechSettings { get; set; }

		public SettingsView(SpeechSettings speechSettings)
		{
			InitializeComponent();
			SettingsViewModel vm = new SettingsViewModel(this,speechSettings);
			DataContext = vm;
			if(vm.CloseAction == null)
			{
				vm.CloseAction = new Action(this.Close);
			}
		}
		public void ShowError(string message)
		{
			MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
