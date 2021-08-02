using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextToSpeech.Hotkeys;
using TextToSpeech.SpeechManager;
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
