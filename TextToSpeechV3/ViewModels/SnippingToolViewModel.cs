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
using TextToSpeechV3.Utility;
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
		public Visibility RectabgleVisibility { get; set; }
		public Point ScreenPosition { get; set; }
		public Point RectStartPoint { get; set; }
		public Point RectSize { get; set; }

		public ScreenshotPositionAndSize Result { get; private set; }

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
			RectSize = new Point(0, 0);
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

			RectStartPoint = new Point(mousePosition.X, mousePosition.Y);
			RectSize = new Point(0, 0);

			OnPropertyChanged(nameof(RectabgleVisibility));
			OnPropertyChanged(nameof(RectStartPoint));
			OnPropertyChanged(nameof(RectSize));
		}

		public void MouseMoveCommandMethod(Point mousePosition)
		{
			if (!_snipping)
				return;

			Point newSize = new Point();

			newSize.X = mousePosition.X - RectStartPoint.X;
			newSize.Y = mousePosition.Y - RectStartPoint.Y;


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

			RectSize = newSize;
			OnPropertyChanged(nameof(RectSize));
			OnPropertyChanged(nameof(RectStartPoint));
		}

		public void MouseUpCommandMethod(Rectangle rectangle)
		{
			Point coodinate = rectangle.TranslatePoint(new Point(0, 0), VisualTreeHelper.GetParent(rectangle) as UIElement);
			Result = new ScreenshotPositionAndSize(
				Convert.ToInt32(coodinate.X),
				Convert.ToInt32(coodinate.Y),
				Convert.ToInt32(rectangle.Width),
				Convert.ToInt32(rectangle.Height));
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

			}
		}

		#endregion

		#region PRIVATE METHODS

		private void CancelCurrentSnip()
		{

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
