using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TextToSpeech.Model;
using TextToSpeech.ViewModels;

namespace TextToSpeech.Views
{
	/// <summary>
	/// Interaction logic for SnippingTool.xaml
	/// </summary>
	public partial class SnippingTool : Window
	{
		private SnippingToolViewModel _vm;

		public ObjectPositionAndSize Result { get; set; }

		public SnippingTool()
		{
			InitializeComponent();
			_vm = new SnippingToolViewModel(this);
			this.DataContext = _vm;
			if (_vm.CloseAction == null)
			{
				_vm.CloseAction = new Action(this.Close);
			}
		}

		private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if(e.RightButton == MouseButtonState.Pressed)
			{
				_vm.MouseDownButtonMethod(MouseButton.Right);
			}
			else
			{
				_vm.MouseDownCommandMethod(e.GetPosition(this));
			}
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			_vm.MouseMoveCommandMethod(e.GetPosition(this));
		}

		private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			_vm.MouseUpCommandMethod(rect);
		}

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			_vm.KeyUpMethod(e.Key);
		}
	}
}
