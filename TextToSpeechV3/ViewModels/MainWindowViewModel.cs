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
using TextToSpeechV3.Services.Interfaces;
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
		private Dictionary<EnumFeature, IHotKeyRegister> _activeHotkeys = new Dictionary<EnumFeature, IHotKeyRegister>();
		private MainWindow _mainWindow;
		#endregion

		#region PUBLIC PROPERTIES

		public SpeechSettings Settings { get; set; }

		#endregion

		#region COMMANDS

		private RelayCommand<string> _speechTestButtonCommand;

		private RelayCommand<object> _settingsButtonCommand;

		private RelayCommand<object> _snippingButtonCommand;
		public RelayCommand<string> SpeechTestButtonCommand { get { return _speechTestButtonCommand; } }
		public RelayCommand<object> SettingsButtonCommand { get { return _settingsButtonCommand; } }
		public RelayCommand<object> SnippingButtonCommand { get { return _snippingButtonCommand; } }

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

			AddSupportedHotkeys(Settings);
			RegisterHotkeys();

			_settingsButtonCommand = new RelayCommand<object>(SettingsButtonCommandMethod);
			_snippingButtonCommand = new RelayCommand<object>(SnippingButtonCommandMethod);

			//_speakHotKey = new HotKeyRegister(mainWindow, new Hotkey(Key.NumPad9, Modifiers.Control));
			//_speakHotKey.HotkeyTriggered += SpeakHotKeyMethod;
		}

		#endregion



		#region PUBLIC METHODS

		public void SpeakHotKeyMethod(object sender, EventArgs e)
		{
			string text = _copyTextFromScreenService.GetTextFromScreen();
			_speechManager.SpeakText(text);//, Settings.Voice, Settings.Rate);
		}
		
		public void InstantScreenshotHotkeyMethod(object sender, EventArgs e)
		{

		}

		public void SettingsButtonCommandMethod(object nothing)
		{
			SettingsView settingsView = new SettingsView(Settings);
			settingsView.ShowDialog();
			if(settingsView.SpeechSettings == null)
			{
				return;
			}
			Settings = settingsView.SpeechSettings;

			UnregisterHotkeys();
			_speechManager.SetAllSettings(Settings);
			RegisterHotkeys();
		}

		public void SnippingButtonCommandMethod(object nothing)
		{
			SnippingTool snippingTool = new SnippingTool();
			snippingTool.ShowDialog();
		}

		#endregion

		#region PRIVATE METHODS

		private void AddSupportedHotkeys(SpeechSettings settings)
		{
			CheckAndAddHotKey(settings, EnumFeature.Speak, new Hotkey(Keys.NumPad9, Modifiers.Control));
		}

		private void CheckAndAddHotKey(SpeechSettings settings, EnumFeature feature, Hotkey hotkey)
		{
			if (!settings.Hotkeys.ContainsKey(feature))
			{
				settings.Hotkeys.Add(feature, hotkey);
			}
		}

		private void RegisterHotkeys()
		{

			Hotkey hotkey = Settings.Hotkeys[(int)EnumFeature.Speak];
			IHotKeyRegister speakHotkey = new HotKeyRegister(_mainWindow, hotkey);
			speakHotkey.HotkeyTriggered += SpeakHotKeyMethod;
			_activeHotkeys.Add(EnumFeature.Speak, speakHotkey);

		}

		private void UnregisterHotkeys()
		{
			IHotKeyRegister speakHotkey = _activeHotkeys[EnumFeature.Speak];
			speakHotkey.UnregisterHotkey();
			speakHotkey.HotkeyTriggered -= SpeakHotKeyMethod;
			_activeHotkeys.Remove(EnumFeature.Speak);

		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
