using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TextToSpeechV3.Model;
using TextToSpeechV3.SpeechManager;
using TextToSpeechV3.Utility;

namespace TextToSpeechV3.ViewModels
{
	public class SettingsViewModel : INotifyPropertyChanged
	{
		#region PRIVATE PROPERTIES
		private string _speechTestText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
		private ISpeechManager _speechManager;
		#endregion

		#region PUBLIC PROPERTIES

		private ObservableCollection<string> Voices { get{ return new ObservableCollection<string>(_speechManager.GetVoices()); } }
		public SpeechSettings Settings { get; set; }

		#endregion

		#region COMMANDS

		private RelayCommand<string> _speechTestButtonCommand;
		public RelayCommand<string> SpeechTestButtonCommand { get { return _speechTestButtonCommand; } }
		#endregion

		#region CONSTRUTORS
		public SettingsViewModel(SpeechSettings settings, ISpeechManager speechManager)
		{
			Settings = settings.ShallowClone();
			_speechManager = speechManager;
			_speechTestButtonCommand = new RelayCommand<string>(SpeechTestButtonCommandMethod);
		}
		#endregion


		#region PUBLIC METHODS

		public void SpeechTestButtonCommandMethod(string nothing)
		{
			_speechManager.SpeakTextWithSettings(_speechTestText, Settings);
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
