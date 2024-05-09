using System.Windows;
using System.Windows.Controls;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Utility.View;
using TextToSpeech.ViewModels.UserControlls;

namespace TextToSpeech.Views.UserControlls
{
	/// <summary>
	/// Interaction logic for GaussianBlurImageProcessingConfigView.xaml
	/// </summary>
	public partial class GaussianBlurImageProcessingConfigView : UserControl
	{
		public static readonly DependencyProperty ServiceConfigProperty =
			DependencyProperty.Register("ServiceConfig", typeof(GaussianBlurImageProcessingConfig), typeof(GaussianBlurImageProcessingConfigView), new PropertyMetadata(null));

		public GaussianBlurImageProcessingConfig ServiceConfig
		{
			get { return (GaussianBlurImageProcessingConfig)GetValue(ServiceConfigProperty); }
			set { SetValue(ServiceConfigProperty, value); }
		}


		//public static readonly DependencyProperty UserControlPropertyProperty =
		//	DependencyProperty.Register("UserControlProperty", typeof(string), typeof(GaussianBlurImageProcessingConfigView), new PropertyMetadata(null));

		//public string UserControlProperty
		//{
		//	get { return (string)GetValue(UserControlPropertyProperty); }
		//	set { SetValue(UserControlPropertyProperty, value); }
		//}

		public GaussianBlurImageProcessingConfigView()
		{
			GaussianBlurImageProcessingConfigViewModel vm = new GaussianBlurImageProcessingConfigViewModel(this, null);
			DataContext = vm;
			InitializeComponent();
		}


		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			var x = ServiceConfig;
			txtWidth.PreviewTextInput += TextBoxEvents.TextBox_PreviewTextInput_Numeric_Int;
			txtHeight.PreviewTextInput += TextBoxEvents.TextBox_PreviewTextInput_Numeric_Int;
			txtSigmaX.PreviewTextInput += TextBoxEvents.TextBox_PreviewTextInput_Numeric_Double;
			txtSigmaY.PreviewTextInput += TextBoxEvents.TextBox_PreviewTextInput_Numeric_Double;
		}
		private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
		{
			txtWidth.PreviewTextInput -= TextBoxEvents.TextBox_PreviewTextInput_Numeric_Int;
			txtHeight.PreviewTextInput -= TextBoxEvents.TextBox_PreviewTextInput_Numeric_Int;
			txtSigmaX.PreviewTextInput -= TextBoxEvents.TextBox_PreviewTextInput_Numeric_Double;
			txtSigmaY.PreviewTextInput -= TextBoxEvents.TextBox_PreviewTextInput_Numeric_Double;
		}

	}
}
