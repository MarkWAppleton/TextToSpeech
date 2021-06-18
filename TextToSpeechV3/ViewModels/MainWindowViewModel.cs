using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TextToSpeechV3.Hotkeys;
using TextToSpeechV3.Model;
using TextToSpeechV3.Services;
using TextToSpeechV3.SpeechManager;
using TextToSpeechV3.Utility;
using TextToSpeechV3.Views;

namespace TextToSpeechV3.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		#region PRIVATE PROPERTIES
		private ISpeechManager _speechManager;
		private IHotKeyRegister _speakHotKey;
		private ICopyTextFromScreenService _copyTextFromScreenService = new CopyTextFromScreenService();
		#endregion

		#region PUBLIC PROPERTIES

		public ObservableCollection<string> Voices { get; set; }
		public SpeechSettings Settings { get; set; }
		public string SelectedText { get; set; }

		#endregion

		#region COMMANDS

		public RelayCommand<string> SpeechTestButtonCommand { get { return _speechTestButtonCommand; } }
		private RelayCommand<string> _speechTestButtonCommand;

		#endregion

		#region CONSTRUTORS
		public MainWindowViewModel(MainWindow mainWindow)
		{
			_speechManager = new SAPI();
			Voices = new ObservableCollection<string>(_speechManager.GetVoices());
			Settings = new SpeechSettings();
			Settings.Rate = 1.6;
			Settings.Voice = _speechManager.GetVoices().ToList()[0];
			SelectedText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
			_speechTestButtonCommand = new RelayCommand<string>(SpeechTestButtonCommandMethod);

			_speakHotKey = new HotKeyRegister(mainWindow, Key.NumPad9, Modifiers.Control);
			_speakHotKey.HotkeyTriggered += SpeakHotKeyMethod;
		}


		#endregion



		#region PUBLIC METHODS

		private void SpeakHotKeyMethod(object sender, EventArgs e)
		{
			string text = _copyTextFromScreenService.GetTextFromScreen();
			_speechManager.PlayAudio(text, Settings.Voice, Settings.Rate);
		}
		public void SpeechTestButtonCommandMethod(string text)
		{
			_speechManager.PlayAudio(text, Settings.Voice, Settings.Rate);
			int i = 1;
		}
		

		#endregion

		#region PRIVATE METHODS

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
