using System;
using System.Windows;
using TextToSpeech.ViewModels;
using MessageBox = System.Windows.MessageBox;

namespace TextToSpeech.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			notifyIcon.Icon = Properties.Resources.TextToSpeachIcon;
			MainWindowViewModel vm = new MainWindowViewModel(this);
			this.DataContext = vm;
			if (vm.CloseAction == null)
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
