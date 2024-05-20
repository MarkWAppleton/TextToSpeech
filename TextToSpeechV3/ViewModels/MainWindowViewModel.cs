using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using TextToSpeech.Hotkeys;
using TextToSpeech.Model;
using TextToSpeech.Services;
using TextToSpeech.Services.ImagePrcessingStages;
using TextToSpeech.Services.Interfaces;
using TextToSpeech.SpeechManager;
using TextToSpeech.Utility;
using TextToSpeech.Views;

namespace TextToSpeech.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		#region PRIVATE PROPERTIES
		private SpeechSettings _speechSettings;
		private ISpeechManager _speechManager;
		private ICopyTextFromScreenService _copyTextFromScreenService = new CopyTextFromScreenService();
		private ISnippingScreenshot _snippingScreenshot = new SnippingScreenshot();
		private IOcrEngine _ocrEngine = new TesseractOcrEngine();
		private IImageProcessingService _imageProcessingService = new ResizeImageProcessingService();
		private ISetScreenshotLocation _setScreenshotLocation = new SetScreenshotLocation();
		private ICreateBitmapService _createBitmapService = new CreateBitmapService();
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
		private RelayCommand<object> _imageProcessingSettingsButtonCommand;
		private RelayCommand<object> _aboutCommand;
		private RelayCommand<object> _clostCommand;
		public RelayCommand<object> SettingsButtonCommand { get { return _settingsButtonCommand; } }
		public RelayCommand<object> ImageProcessingSettingsButtonCommand { get { return _imageProcessingSettingsButtonCommand; } }
		public RelayCommand<object> AboutCommand { get { return _aboutCommand; } }
		public RelayCommand<object> ClostCommand { get { return _clostCommand; } }

		#endregion

		#region CONSTRUTORS
		public MainWindowViewModel(MainWindow mainWindow)
		{
			_mainWindow = mainWindow;
			Images = new ObservableCollection<BitmapImage>();
			string speechSettingsJson = Properties.Settings.Default.SpeechSettings;
			Settings = JsonUtility.DeserializeOrDefault(speechSettingsJson,
				new SpeechSettings()
				{
					Rate = 1.6,
					Volume = 1,
					Voice = "",
					Engine = EnumSpeechEngine.Legacy,
				});
			_speechSettings = Settings;

			_speechManager = SpeechManagerFactory.CreateSpeechManager(_speechSettings);

			AddSupportedHotkeys(_speechSettings);
			RegisterAllHotkeys();

			_settingsButtonCommand = new RelayCommand<object>(SettingsButtonCommandMethod);
			_imageProcessingSettingsButtonCommand = new RelayCommand<object>(ImageProcessingSettingsButtonCommandMethod);
			_aboutCommand = new RelayCommand<object>(AboutCommandMethod);
			_clostCommand = new RelayCommand<object>(CloseCommandMethod);
		}

		#endregion

		#region PUBLIC METHODS

		public void SpeakHotKeyMethod(object sender, EventArgs e)
		{
			try
			{
				string text = _copyTextFromScreenService.GetTextFromScreen();
				_speechManager.SpeakText(TextProcessing.ProcessTextForSpeech(text));
			}
			catch (Exception ex)
			{
				_mainWindow.ShowError(ex.Message);
			}
		}
		
		public void InstantScreenshotHotkeyMethod(object sender, EventArgs e)
		{
			try
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

				//List<Bitmap> imageProcessing;
				Bitmap processed = _imageProcessingService.ProcessImage(snippingResult);
				//imageProcessing.ForEach(f => Images.Add(BitmapConverter.ToBitmapImage(f)));
				//OnPropertyChanged(nameof(Image));
				//string orcResult = _ocrEngine.RunOcr(processed);

				string orcResult = _ocrEngine.RunOcr(snippingResult);
				_speechManager.SpeakText(TextProcessing.ProcessTextForSpeech(orcResult));
			} 
			catch(Exception ex)
			{
				_mainWindow.ShowError(ex.Message);
			}
		}

		public void SetScreenshotLocationHotkeyMethod(object sender, EventArgs e)
		{
			_setScreenshotLocation.SetScreenshotLocation();
		}

		public void ReadScreenshotHotkeyMethod(object sender, EventArgs e)
		{
			string screenshotLocationJson = Properties.Settings.Default.ScreenshotSettings;
			if(JsonUtility.TryDeserialize(screenshotLocationJson, out ObjectPositionAndSize screenshotLocation))
			{
				Bitmap image = _createBitmapService.CreateBitmap(screenshotLocation.ToRectangle());
				string orcResult = _ocrEngine.RunOcr(image);
				_speechManager.SpeakText(TextProcessing.ProcessTextForSpeech(orcResult));
			}
		}

		public void SettingsButtonCommandMethod(object _)
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
			RegisterAllHotkeys();
			OnPropertyChanged(nameof(Settings));
		}

		public void ImageProcessingSettingsButtonCommandMethod(object _)
		{
			ImageProcessingSettingsView imageProcessingSettingsView = new ImageProcessingSettingsView(_speechSettings);
			imageProcessingSettingsView.ShowDialog();
		}

		public void AboutCommandMethod(object nothing)
		{
			new AboutView().ShowDialog();
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
			CheckAndAddHotKey(settings, EnumFeature.SetScreenshotLocation, new Hotkey(Keys.NumPad5, Modifiers.Control));
			CheckAndAddHotKey(settings, EnumFeature.SpeakScreenshot, new Hotkey(Keys.NumPad6, Modifiers.Control));
		}

		private void CheckAndAddHotKey(SpeechSettings settings, EnumFeature feature, Hotkey hotkey)
		{
			if (!settings.Hotkeys.ContainsKey(feature))
			{
				settings.Hotkeys.Add(feature, hotkey);
			}
		}

		private void RegisterAllHotkeys()
		{
			RegisterHotkey(EnumFeature.Speak, SpeakHotKeyMethod);
			RegisterHotkey(EnumFeature.InstantScreenshot, InstantScreenshotHotkeyMethod);
			RegisterHotkey(EnumFeature.SetScreenshotLocation, SetScreenshotLocationHotkeyMethod);
			RegisterHotkey(EnumFeature.SpeakScreenshot, ReadScreenshotHotkeyMethod);
		}

		private void RegisterHotkey(EnumFeature feature, EventHandler eventHandler)
		{
			Hotkey hotkey = Settings.Hotkeys[feature];
			IHotKeyRegister hotkeyRegister = new HotKeyRegister(_mainWindow, hotkey);
			hotkeyRegister.HotkeyTriggered += eventHandler;
			_activeHotkeys.Add(feature, hotkeyRegister);
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
