using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TextToSpeechV3.Model;
using TextToSpeechV3.SpeechManager;
using TextToSpeechV3.ViewModels;

namespace TextToSpeechV3.Views
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
	}
}
