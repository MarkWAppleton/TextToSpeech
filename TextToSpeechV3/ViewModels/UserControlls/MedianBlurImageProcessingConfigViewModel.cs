using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextToSpeech.Views.UserControlls;

namespace TextToSpeech.ViewModels.UserControlls
{
	public class MedianBlurImageProcessingConfigViewModel : INotifyPropertyChanged
	{
		#region PRIVATE PROPERTIES
		private readonly MedianBlurImageProcessingConfigView _view;

		#endregion

		#region PUBLIC PROPERTIES

		#endregion

		#region COMMANDS

		#endregion

		#region CONSTRUTORS
		public MedianBlurImageProcessingConfigViewModel(MedianBlurImageProcessingConfigView view)
		{
			_view = view;
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
