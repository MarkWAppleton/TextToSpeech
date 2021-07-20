using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media.Imaging;
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
		private ISnippingScreenshot _snippingScreenshot = new SnippingScreenshot();
		private IOcrEngine _ocrEngine = new TesseractOcrEngine();
		private IImageProcessingService _imageProcessingService = new SimpleResizeImageProcessingService();
		private Dictionary<EnumFeature, IHotKeyRegister> _activeHotkeys = new Dictionary<EnumFeature, IHotKeyRegister>();
		private MainWindow _mainWindow;
		#endregion

		#region PUBLIC PROPERTIES

		public Action CloseAction { get; set; }
		public SpeechSettings Settings { get; set; }
		public Dictionary<EnumFeature, Hotkey> Hotkeys { get { return Settings.Hotkeys; } }
		public BitmapImage Image { get; set; }
		public ObservableCollection<BitmapImage> Images { get; set; }

		#endregion

		#region COMMANDS

		private RelayCommand<object> _settingsButtonCommand;
		private RelayCommand<object> _clostCommand;
		public RelayCommand<object> SettingsButtonCommand { get { return _settingsButtonCommand; } }
		public RelayCommand<object> ClostCommand { get { return _clostCommand; } }

		#endregion

		#region CONSTRUTORS
		public MainWindowViewModel(MainWindow mainWindow)
		{
			_mainWindow = mainWindow;
			Images = new ObservableCollection<BitmapImage>();
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
			_clostCommand = new RelayCommand<object>(CloseCommandMethod);
		}

		#endregion

		#region PUBLIC METHODS

		public void SpeakHotKeyMethod(object sender, EventArgs e)
		{
			string text = _copyTextFromScreenService.GetTextFromScreen();
			_speechManager.SpeakText(text);
		}
		
		public void InstantScreenshotHotkeyMethod(object sender, EventArgs e)
		{
			if (_speechManager.IsSpeaking)
			{
				_speechManager.StopSpeaking();
				return;
			}

			Bitmap snippingResult = _snippingScreenshot.TakeSnippingScreenshot();

			if (snippingResult == null)
				return;

			//Images.Add(BitmapConverter.ToBitmapImage(snippingResult));

			List<Bitmap> imageProcessing;
			Bitmap processed = _imageProcessingService.ProcessImage(snippingResult, out imageProcessing);
			//imageProcessing.ForEach(f => Images.Add(BitmapConverter.ToBitmapImage(f)));
			//OnPropertyChanged(nameof(Image));

			string orcResult = _ocrEngine.RunOcr(processed);
			string unescapedText = Regex.Unescape(orcResult);
			string processedText = unescapedText.Replace("\n", " ");
			_speechManager.SpeakText(processedText);
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
			OnPropertyChanged(nameof(Settings));
		}

		public void CloseCommandMethod(object nothing)
		{
			CloseAction();
		}

		#endregion

		#region PRIVATE METHODS

		private void AddSupportedHotkeys(SpeechSettings settings)
		{
			CheckAndAddHotKey(settings, EnumFeature.Speak, new Hotkey(Keys.NumPad9, Modifiers.Control));
			CheckAndAddHotKey(settings, EnumFeature.InstantScreenshot, new Hotkey(Keys.NumPad8, Modifiers.Control));
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
			Hotkey speakHotkey = Settings.Hotkeys[EnumFeature.Speak];
			IHotKeyRegister speakHotkeyRegister = new HotKeyRegister(_mainWindow, speakHotkey);
			speakHotkeyRegister.HotkeyTriggered += SpeakHotKeyMethod;
			_activeHotkeys.Add(EnumFeature.Speak, speakHotkeyRegister);

			Hotkey instanceScreenshotHotkey = Settings.Hotkeys[EnumFeature.InstantScreenshot];
			IHotKeyRegister instanceScreenshotHotkeyRegister = new HotKeyRegister(_mainWindow, instanceScreenshotHotkey);
			instanceScreenshotHotkeyRegister.HotkeyTriggered += InstantScreenshotHotkeyMethod;
			_activeHotkeys.Add(EnumFeature.InstantScreenshot, instanceScreenshotHotkeyRegister);
		}

		private void UnregisterHotkeys()
		{
			IHotKeyRegister speakHotkey = _activeHotkeys[EnumFeature.Speak];
			speakHotkey.UnregisterHotkey();
			speakHotkey.HotkeyTriggered -= SpeakHotKeyMethod;
			_activeHotkeys.Remove(EnumFeature.Speak);

			IHotKeyRegister instanceScreenshotHotkey = _activeHotkeys[EnumFeature.InstantScreenshot];
			instanceScreenshotHotkey.UnregisterHotkey();
			instanceScreenshotHotkey.HotkeyTriggered -= InstantScreenshotHotkeyMethod;
			_activeHotkeys.Remove(EnumFeature.InstantScreenshot);
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
