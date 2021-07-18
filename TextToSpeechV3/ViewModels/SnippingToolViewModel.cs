using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TextToSpeechV3.Model;
using TextToSpeechV3.Views;

namespace TextToSpeechV3.ViewModels
{
	public class SnippingToolViewModel : INotifyPropertyChanged
	{

		#region PRIVATE PROPERTIES

		private SnippingTool _view;
		private Point _startPosition;
		private bool _snipping;

		#endregion

		#region PUBLIC PROPERTIES
		public Action CloseAction { get; set; }

		public Visibility RectabgleVisibility { get; set; }
		public Point ScreenPosition { get; set; }
		public ObjectPositionAndSize SnippingWindow { get; set; }


		#endregion

		#region COMMANDS

		#endregion

		#region CONSTRUTORS

		public SnippingToolViewModel(SnippingTool view)
		{
			_view = view;
			SetStartWindowPositionAccrossAllMonitors();
			RectabgleVisibility = Visibility.Hidden;
			_snipping = false;
			SnippingWindow = new ObjectPositionAndSize(0,0,0,0);
			//RectSize = new Point(0, 0);
		}

		#endregion



		#region PUBLIC METHODS

		public void MouseDownCommandMethod(Point mousePosition)
		{
			if (_snipping)
				return;

			_snipping = true;
			RectabgleVisibility = Visibility.Visible;
			_startPosition = mousePosition;

			SnippingWindow = new ObjectPositionAndSize(mousePosition.X, mousePosition.Y, 0, 0);

			OnPropertyChanged(nameof(RectabgleVisibility));
			OnPropertyChanged(nameof(SnippingWindow));
		}

		public void MouseMoveCommandMethod(Point mousePosition)
		{
			if (!_snipping)
				return;

			Point newSize = new Point();

			newSize.X = mousePosition.X - SnippingWindow.XCoordinate;
			newSize.Y = mousePosition.Y - SnippingWindow.YCoordinate;


			if(newSize.X < 0)
			{
				//RectStartPoint = new Point(mousePosition.X, RectStartPoint.Y);
				//newSize = new Point(_startPosition.X - mousePosition.X, newSize.Y);
				newSize = new Point(0, newSize.Y);
			}
			if (newSize.Y < 0)
			{
				newSize = new Point(newSize.X, 0);
			}

			//System.Diagnostics.Debug.WriteLine("newSize.X = " + newSize.X);

			SnippingWindow.Width = newSize.X;
			SnippingWindow.Height = newSize.Y;
			OnPropertyChanged(nameof(SnippingWindow));
		}

		public void MouseUpCommandMethod(Rectangle rectangle)
		{
			if (!_snipping)
				return;

			Point coodinate = rectangle.TranslatePoint(new Point(0, 0), VisualTreeHelper.GetParent(rectangle) as UIElement);
			_view.Result = new ObjectPositionAndSize(coodinate.X,coodinate.Y,rectangle.Width,rectangle.Height);
			CancelCurrentSnip();
			CloseAction();
		}

		public void MouseDownButtonMethod(MouseButton mouseButton)
		{
			RectabgleVisibility = Visibility.Visible;
			_snipping = false;
			OnPropertyChanged(nameof(RectabgleVisibility));
		}

		public void KeyUpMethod(Key key)
		{
			if(key == Key.Escape)
			{
				if (_snipping)
				{
					CancelCurrentSnip();
				}
				else
				{
					_view.Result = null;
					CloseAction();
				}
			}
		}

		#endregion

		#region PRIVATE METHODS

		private void CancelCurrentSnip()
		{
			RectabgleVisibility = Visibility.Hidden;
			SnippingWindow = new ObjectPositionAndSize(0, 0, 0, 0);
			_snipping = false;
			OnPropertyChanged(nameof(RectabgleVisibility));
			OnPropertyChanged(nameof(SnippingWindow));
		}

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
