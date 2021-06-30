using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using TextToSpeechV3.Utility;

namespace TextToSpeechV3.ViewModels
{
	public class SnippingToolViewModel : INotifyPropertyChanged
	{

		#region PRIVATE PROPERTIES

		#endregion

		#region PUBLIC PROPERTIES

		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public Visibility RectabgleVisibility { get; set; }

		#endregion

		#region COMMANDS

		private RelayCommand<object> _mouseDownCommand;
		private RelayCommand<object> _mouseMoveCommand;
		private RelayCommand<object> _mouseUpCommand;

		public RelayCommand<object> MouseDownCommand { get { return _mouseDownCommand; } }
		public RelayCommand<object> MouseMoveCommand { get { return _mouseMoveCommand; } }
		public RelayCommand<object> MouseUpCommand { get { return _mouseUpCommand; } }

		#endregion

		#region CONSTRUTORS

		public SnippingToolViewModel()
		{
			RectabgleVisibility = Visibility.Hidden;
			_mouseDownCommand = new RelayCommand<object>(MouseDownCommandMethod);
			_mouseMoveCommand = new RelayCommand<object>(MouseMoveCommandMethod);
			_mouseUpCommand = new RelayCommand<object>(MouseUpCommandMethod);
		}

		#endregion



		#region PUBLIC METHODS

		public void MouseDownCommandMethod(object nothing)
		{
			


		}

		public void MouseMoveCommandMethod(object nothing)
		{

		}
		public void MouseUpCommandMethod(object nothing)
		{

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
