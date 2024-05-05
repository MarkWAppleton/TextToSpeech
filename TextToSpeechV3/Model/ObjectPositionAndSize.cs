using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.Model
{
	public class ObjectPositionAndSize
	{
		public double XCoordinate { get; set; }
		public double YCoordinate { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }

		public ObjectPositionAndSize(
			double xCoordinate, 
			double yCoordinate, 
			double width, 
			double height)
		{
			XCoordinate = xCoordinate;
			YCoordinate = yCoordinate;
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
