using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TextToSpeechV3.Model;
using TextToSpeechV3.SpeechManager;
using TextToSpeechV3.Utility;
using System.Text.Json;

namespace TextToSpeechV3.ViewModels
{
	public class SettingsViewModel : INotifyPropertyChanged
	{
		#region PRIVATE PROPERTIES
		private string _speechTestText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
		private ISpeechManager _speechManager;
		#endregion

		#region PUBLIC PROPERTIES

		public Action CloseAction { get; set; }

		public ObservableCollection<string> Voices { get{ return new ObservableCollection<string>(SpeechManagerFactory.CreateSpeechManager(Settings.Engine).GetVoices()); } }
		public Collection<string> VoicesCollection;
		public SpeechSettings Settings { get; set; }
		public Dictionary<EnumSpeechEngine, string> Engines { get { return EnumExtensions<EnumSpeechEngine>.ToDictionary(); } }

		public EnumSpeechEngine SelectedEngine
		{
			get
			{
				return Settings.Engine;
			}
			set
			{
				Settings.Engine = value;
				Settings.Voice = Voices[0];
				OnPropertyChanged(nameof(Voices));
			}
		}
		#endregion

		#region COMMANDS

		private RelayCommand<string> _speechTestButtonCommand;
		private RelayCommand<string> _exitButtonCommand;
		private RelayCommand<string> _saveButtonCommand;
		public RelayCommand<string> SpeechTestButtonCommand { get { return _speechTestButtonCommand; } }
		public RelayCommand<string> ExitButtonCommand { get { return _exitButtonCommand; } }
		public RelayCommand<string> SaveButtonCommand { get { return _saveButtonCommand; } }
		#endregion

		#region CONSTRUTORS
		public SettingsViewModel(SpeechSettings settings)
		{
			Settings = settings.ShallowClone();
			_speechTestButtonCommand = new RelayCommand<string>(SpeechTestButtonCommandMethod);
			_exitButtonCommand = new RelayCommand<string>(ExitButtonCommandMethod);
			_saveButtonCommand = new RelayCommand<string>(SaveButtonCommandMethod);
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
				_speechManager.SetAllSettings(Settings);
				_speechManager.SpeakText(_speechTestText);
			}
		}

		public void ExitButtonCommandMethod(string nothing)
		{
			CloseAction();
		}

		public void SaveButtonCommandMethod(string nothing)
		{
			Properties.Settings.Default.SpeechSettings = JsonSerializer.Serialize(Settings);
			Properties.Settings.Default.Save();
			CloseAction();
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
