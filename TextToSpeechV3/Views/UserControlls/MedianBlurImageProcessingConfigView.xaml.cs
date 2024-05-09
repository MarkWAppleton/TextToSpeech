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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextToSpeech.ViewModels.UserControlls;

namespace TextToSpeech.Views.UserControlls
{
	/// <summary>
	/// Interaction logic for MedianBlurImageProcessingConfigView.xaml
	/// </summary>
	public partial class MedianBlurImageProcessingConfigView : UserControl
	{
		public MedianBlurImageProcessingConfigView()
		{
			MedianBlurImageProcessingConfigViewModel vm = new MedianBlurImageProcessingConfigViewModel(this);
			DataContext = vm;
			InitializeComponent();
		}
	}
}
