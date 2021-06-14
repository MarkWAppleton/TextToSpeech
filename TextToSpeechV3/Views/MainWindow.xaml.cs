using System;
using System.Collections.Generic;
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
using TextToSpeechV3.Hotkeys;
using TextToSpeechV3.SpeechManager;

namespace TextToSpeechV3.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ISpeechManager speechManager;

		public MainWindow()
		{
			InitializeComponent();
			speechManager = new WindowsMediaSpeechSynthesis();
			lbVoices.ItemsSource = speechManager.GetVoices();
			lbVoices.SelectedIndex = 0;
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			await speechManager.PlayAudio(textBox.Text,lbVoices.SelectedItem.ToString(), Double.Parse(txtSpeechRate.Text));
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			IHotKeyRegister hotKeyRegister = new HotKeyRegister(this, Key.NumPad9, Modifiers.Control);

			hotKeyRegister.HotkeyTriggered += SpeakTextHotkeyPressed;
			//System.GC.Collect();
		}

		private void SpeakTextHotkeyPressed(object sender, EventArgs e)
		{
			SendKeys.SendWait("^(c)");
			Thread.Sleep(80);//TODO make this configurable
			string copiedText = System.Windows.Clipboard.GetText();
			txtCopyResults.Text = copiedText;
			speechManager.PlayAudio(copiedText, lbVoices.SelectedItem.ToString(), Double.Parse(txtSpeechRate.Text));
		}
	}
}
