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
			Settings = settings;
			_speechManager = speechManager;
		}
		#endregion


		#region PUBLIC METHODS

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
