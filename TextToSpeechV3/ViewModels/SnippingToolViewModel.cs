using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;
using TextToSpeechV3.Utility;
using TextToSpeechV3.Views;

namespace TextToSpeechV3.ViewModels
{
	public class SnippingToolViewModel : INotifyPropertyChanged
	{

		#region PRIVATE PROPERTIES

		private SnippingTool _view;
		private Point _startPosition;

		#endregion

		#region PUBLIC PROPERTIES

		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public Visibility RectabgleVisibility { get; set; }
		public Point ScreenPosition { get; set; }

		#endregion

		#region COMMANDS

		private RelayCommand<object> _mouseMoveCommand;
		private RelayCommand<object> _mouseUpCommand;

		public RelayCommand<object> MouseMoveCommand { get { return _mouseMoveCommand; } }
		public RelayCommand<object> MouseUpCommand { get { return _mouseUpCommand; } }

		#endregion

		#region CONSTRUTORS

		public SnippingToolViewModel(SnippingTool view)
		{
			_view = view;
			SetStartWindowPositionAccrossAllMonitors();
			RectabgleVisibility = Visibility.Hidden;
			_mouseMoveCommand = new RelayCommand<object>(MouseMoveCommandMethod);
			_mouseUpCommand = new RelayCommand<object>(MouseUpCommandMethod);
			X = 10;
			Y = 10;
			Width = 100;
			Height = 100;
		}

		#endregion



		#region PUBLIC METHODS

		public void MouseDownCommandMethod(Point mousePosition)
		{
			RectabgleVisibility = Visibility.Visible;
			_startPosition = mousePosition;

			X = mousePosition.X;
			Y = mousePosition.Y;

			OnPropertyChanged(nameof(RectabgleVisibility));
			OnPropertyChanged(nameof(X));
			OnPropertyChanged(nameof(Y));
			OnPropertyChanged(nameof(Width));
			OnPropertyChanged(nameof(Height));

			int v = 1;
		}

		public void MouseMoveCommandMethod(object nothing)
		{

		}
		public void MouseUpCommandMethod(object nothing)
		{

		}

		#endregion

		#region PRIVATE METHODS

		private void SetStartWindowPositionAccrossAllMonitors()
		{
			var scaleRatio = Math.Max(Screen.PrimaryScreen.WorkingArea.Width / SystemParameters.PrimaryScreenWidth,
				Screen.PrimaryScreen.WorkingArea.Height / SystemParameters.PrimaryScreenHeight);
			var left = Screen.AllScreens.Min(m => m.WorkingArea.Left / scaleRatio);
			var top = Screen.AllScreens.Min(m => m.WorkingArea.Top / scaleRatio);
			ScreenPosition = new Point(left, top);
			OnPropertyChanged(nameof(ScreenPosition));
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
