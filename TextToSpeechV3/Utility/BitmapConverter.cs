using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace TextToSpeech.Utility
{
	public static class BitmapConverter
	{
		public static BitmapImage ToBitmapImage(Bitmap bitmap)
		{
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

	}
}
