using System;
using System.Collections.Generic;
using System.Text;

namespace TextToSpeechV3.Model
{
	public class SpeechSettings
	{
		public string Voice { get; set; }
		public double Rate { get; set; }
		public double Volume { get; set; }

		public SpeechSettings ShallowClone()
		{
			return (SpeechSettings)this.MemberwiseClone();
		}
	}
}
