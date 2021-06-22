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
		ISpeechManager _speechManager;
		SpeechSettings _settings;
		#endregion

		#region PUBLIC PROPERTIES

		public ObservableCollection<string> Voices { get{ return new ObservableCollection<string>(SpeechManagerFactory.CreateSpeechManager(Settings.Engine).GetVoices()); } }
		public SpeechSettings Settings
		{
			get { return _settings; }
			set
			{
				_settings = value;
				OnPropertyChanged(nameof(Settings));
			}
		}
		public Dictionary<EnumSpeechEngine, string> Engines { get { return EnumExtensions<EnumSpeechEngine>.ToDictionary(); } }

		#endregion

		#region COMMANDS

		private RelayCommand<string> _speechTestButtonCommand;
		public RelayCommand<string> SpeechTestButtonCommand { get { return _speechTestButtonCommand; } }
		#endregion

		#region CONSTRUTORS
		public SettingsViewModel(SpeechSettings settings)
		{
			Settings = settings.ShallowClone();
			_speechTestButtonCommand = new RelayCommand<string>(SpeechTestButtonCommandMethod);
		}
		#endregion


		#region PUBLIC METHODS

		public void SpeechTestButtonCommandMethod(string nothing)
		{
			if (_speechManager!= null && _speechManager.IsSpeaking)
			{
				_speechManager?.StopSpeaking();
			} 
			else
			{
				_speechManager = SpeechManagerFactory.CreateSpeechManager(Settings.Engine);
				_speechManager.setAllSettings(Settings);
				_speechManager.SpeakText(_speechTestText);
			}
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
