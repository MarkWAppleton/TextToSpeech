using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechV3.Utility
{
	public class ObjectPositionAndSize
	{
		public double XCoordinate { get; set; }
		public double YCoordinate { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }

		public ObjectPositionAndSize(double xCoord, double yCoord, double width, double height)
		{
			XCoordinate = xCoord;
			YCoordinate = yCoord;
			Width = width;
			Height = height;
		}

		public Rectangle ToRectangle()
		{
			return new Rectangle(
				Convert.ToInt32(XCoordinate),
				Convert.ToInt32(YCoordinate), 
				Convert.ToInt32(Width),
				Convert.ToInt32(Height));
		}
	}
}
