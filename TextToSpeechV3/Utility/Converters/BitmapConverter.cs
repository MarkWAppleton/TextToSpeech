using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TextToSpeech.Utility.Converters
{
	[ValueConversion(typeof(Bitmap), typeof(BitmapImage))]
	public class BitmapConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(value == null) return new BitmapImage();

			Bitmap bitmap = (Bitmap)value;
			BitmapImage result = new BitmapImage();
			using (MemoryStream memory = new MemoryStream())
			{
				bitmap.Save(memory, ImageFormat.Bmp);
				memory.Seek(0, SeekOrigin.Begin);

				result.BeginInit();
				result.StreamSource = memory;
				result.CacheOption = BitmapCacheOption.OnLoad;
				result.EndInit();
				result.Freeze();
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
