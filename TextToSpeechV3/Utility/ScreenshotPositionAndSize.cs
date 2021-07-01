using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechV3.Utility
{
	public class ScreenshotPositionAndSize
	{
		public int XCoordinate { get; set; }
		public int YCoordinate { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public ScreenshotPositionAndSize(int xCoord, int yCoord, int width, int height)
		{
			XCoordinate = xCoord;
			YCoordinate = yCoord;
			Width = width;
			Height = height;
		}
	}
}
