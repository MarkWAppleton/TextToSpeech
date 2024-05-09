using System.ComponentModel;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Views.UserControlls;

namespace TextToSpeech.ViewModels.UserControlls
{
	public class GaussianBlurImageProcessingConfigViewModel : INotifyPropertyChanged
	{
		#region PRIVATE PROPERTIES
		private readonly GaussianBlurImageProcessingConfigView _view;
		private GaussianBlurImageProcessingConfig _serviceConfig;

		#endregion

		#region PUBLIC PROPERTIES
		public GaussianBlurImageProcessingConfig ServiceConfig { get {  return _serviceConfig; } set { _serviceConfig = value; OnPropertyChanged(nameof(ServiceConfig)); } }

		#endregion

		#region COMMANDS

		#endregion

		#region CONSTRUTORS
		public GaussianBlurImageProcessingConfigViewModel(GaussianBlurImageProcessingConfigView view, GaussianBlurImageProcessingConfig serviceConfig)
        {
            _view = view;   
			_serviceConfig = serviceConfig;
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
