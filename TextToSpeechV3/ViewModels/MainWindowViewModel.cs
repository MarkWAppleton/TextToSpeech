using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TextToSpeechV3.Model;
using TextToSpeechV3.SpeechManager;
using TextToSpeechV3.Utility;

namespace TextToSpeechV3.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		#region PRIVATE PROPERTIES
		private ISpeechManager _speechManager;
		#endregion

		#region PUBLIC PROPERTIES
		public SpeechSettings Settings { get; set; }
		public string SelectedText { get; set; }

		#endregion

		#region COMMANDS

		public RelayCommand<string> SpeechTestButtonCommand { get { return _speechTestButtonCommand; } }
		private RelayCommand<string> _speechTestButtonCommand;

		#endregion

		#region CONSTRUTORS
		public MainWindowViewModel()
		{
			Settings = new SpeechSettings();
			SelectedText = "This is a test message";
			_speechTestButtonCommand = new RelayCommand<string>(SpeechTestButtonCommandMethod);
		}
		#endregion

		#region PUBLIC METHODS
		public void SpeechTestButtonCommandMethod(string text)
		{
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
