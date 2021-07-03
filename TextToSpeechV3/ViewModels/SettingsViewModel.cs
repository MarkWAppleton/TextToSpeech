using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TextToSpeechV3.Model;
using TextToSpeechV3.SpeechManager;
using TextToSpeechV3.Utility;
using System.Text.Json;
using TextToSpeechV3.Views;
using TextToSpeechV3.Hotkeys;

namespace TextToSpeechV3.ViewModels
{
	public class SettingsViewModel : INotifyPropertyChanged
	{
		#region PRIVATE PROPERTIES
		private string _speechTestText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
		private ISpeechManager _speechManager;
		private SettingsView _view;
		#endregion

		#region PUBLIC PROPERTIES

		public Action CloseAction { get; set; }

		public ObservableCollection<string> Voices { get { return new ObservableCollection<string>(SpeechManagerFactory.CreateSpeechManager(Settings.Engine).GetVoices()); } }
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

		public Dictionary<Modifiers, string> Modifiers { get { return EnumExtensions<Modifiers>.ToDictionary(); } }
		public Dictionary<Keys, string> Keys { get { return EnumExtensions<Keys>.ToDictionary(); } }

		public Modifiers SpeakModifier 
		{ 
			get
			{
				return Settings.Hotkeys[EnumFeature.Speak].Modifier;
			}
			set
			{
				Settings.Hotkeys[EnumFeature.Speak].Modifier = value;
			}
		}
		public Keys SpeakKey
		{
			get
			{
				return Settings.Hotkeys[EnumFeature.Speak].Key;
			}
			set
			{
				Settings.Hotkeys[EnumFeature.Speak].Key = value;
			}
		}

		public Modifiers InstanctScreenshotModifier
		{
			get
			{
				return Settings.Hotkeys[EnumFeature.InstantScreenshot].Modifier;
			}
			set
			{
				Settings.Hotkeys[EnumFeature.InstantScreenshot].Modifier = value;
			}
		}
		public Keys InstanctScreenshotKey
		{
			get
			{
				return Settings.Hotkeys[EnumFeature.InstantScreenshot].Key;
			}
			set
			{
				Settings.Hotkeys[EnumFeature.InstantScreenshot].Key = value;
			}
		}


		#endregion

		#region COMMANDS

		private RelayCommand<string> _speechTestButtonCommand;
		private RelayCommand<string> _cancelButtonCommand;
		private RelayCommand<string> _okButtonCommand;
		private RelayCommand<string> _applyButtonCommand;
		public RelayCommand<string> SpeechTestButtonCommand { get { return _speechTestButtonCommand; } }
		public RelayCommand<string> CancelButtonCommand { get { return _cancelButtonCommand; } }
		public RelayCommand<string> OkButtonCommand { get { return _okButtonCommand; } }
		public RelayCommand<string> ApplyButtonCommand { get { return _applyButtonCommand; } }
		#endregion

		#region CONSTRUTORS
		public SettingsViewModel(SettingsView view, SpeechSettings settings)
		{
			_view = view;
			Settings = settings.ShallowClone();
			_speechTestButtonCommand = new RelayCommand<string>(SpeechTestButtonCommandMethod);
			_cancelButtonCommand = new RelayCommand<string>(CancelButtonCommandMethod);
			_okButtonCommand = new RelayCommand<string>(OkButtonCommandMethod);
			_applyButtonCommand = new RelayCommand<string>(ApplyButtonCommandMethod);
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

		public void CancelButtonCommandMethod(string nothing)
		{
			CloseAction();
		}

		public void OkButtonCommandMethod(string nothing)
		{
			if (!Validate())
			{
				return;
			}

			Properties.Settings.Default.SpeechSettings = JsonSerializer.Serialize(Settings);
			Properties.Settings.Default.Save();
			_view.SpeechSettings = Settings.ShallowClone();
			CloseAction();
		}

		public void ApplyButtonCommandMethod(string nothing)
		{
			if (!Validate())
			{
				return;
			}

			Properties.Settings.Default.SpeechSettings = JsonSerializer.Serialize(Settings);
			Properties.Settings.Default.Save();
			_view.SpeechSettings = Settings.ShallowClone();
		}

		#endregion

		#region PRIVATE METHODS

		private bool Validate()
		{
			List<string> validationResults;
			bool value = Settings.IsValid(out validationResults);
			if (!value)
			{

			}
			return value;
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
