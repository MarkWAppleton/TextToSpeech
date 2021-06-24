using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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
		private Dictionary<string, IHotKeyRegister> _activeHotkeys;
		private MainWindow _mainWindow;
		#endregion

		#region PUBLIC PROPERTIES

		public SpeechSettings Settings { get; set; }

		#endregion

		#region COMMANDS

		private RelayCommand<string> _speechTestButtonCommand;
		public RelayCommand<string> SpeechTestButtonCommand { get { return _speechTestButtonCommand; } }

		private RelayCommand<object> _settingsButtonCommand;
		public RelayCommand<object> SettingsButtonCommand { get { return _settingsButtonCommand; } }

		#endregion

		#region CONSTRUTORS
		public MainWindowViewModel(MainWindow mainWindow)
		{
			_mainWindow = mainWindow;
			string speechSettingsJson = Properties.Settings.Default.SpeechSettings;
			if (string.IsNullOrWhiteSpace(speechSettingsJson))
			{
				Settings = new SpeechSettings();
				Settings.Rate = 1.6;
				Settings.Volume = 1;
				Settings.Voice = "";
				Settings.Engine = EnumSpeechEngine.Legacy;
			}
			else
			{
				Settings = JsonSerializer.Deserialize<SpeechSettings>(speechSettingsJson);
			}
			
			_speechManager = SpeechManagerFactory.CreateSpeechManager(Settings);

			//SelectedText = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
			_speechTestButtonCommand = new RelayCommand<string>(SpeechTestButtonCommandMethod);
			_settingsButtonCommand = new RelayCommand<object>(SettingsButtonCommandMethod);

			_speakHotKey = new HotKeyRegister(mainWindow, new Hotkey(Key.NumPad9, Modifiers.Control));
			_speakHotKey.HotkeyTriggered += SpeakHotKeyMethod;
		}

		#endregion



		#region PUBLIC METHODS

		public void SpeakHotKeyMethod(object sender, EventArgs e)
		{
			string text = _copyTextFromScreenService.GetTextFromScreen();
			_speechManager.SpeakText(text);//, Settings.Voice, Settings.Rate);
		}

		public void SpeechTestButtonCommandMethod(string text)
		{

			_speechManager.SetAllSettings(Settings);
			_speechManager.SpeakText(text);//, Settings.Voice, Settings.Rate);
		}
		
		public void SettingsButtonCommandMethod(object nothing)
		{
			new SettingsView(Settings).Show();
		}

		#endregion

		#region PRIVATE METHODS

		private void AddSupportedHotkeys(SpeechSettings settings)
		{
			CheckAndAddHotKey(settings, "Speak", new Hotkey(Key.NumPad9, Modifiers.Control));
		}

		private void CheckAndAddHotKey(SpeechSettings settings, string name, Hotkey hotkey)
		{
			if (!settings.Hotkeys.ContainsKey(name))
			{
				settings.Hotkeys.Add(name, hotkey);
			}
		}

		private void ActivateHotkeys()
		{
			string name = "Speak";
			Hotkey hotkey = Settings.Hotkeys[name];

			IHotKeyRegister hotKeyRegister = new HotKeyRegister(_mainWindow, hotkey);
			hotKeyRegister.HotkeyTriggered += SpeakHotKeyMethod;
			_activeHotkeys.Add(name, hotKeyRegister);

		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
