using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechV3.Services.Interfaces
{
	public interface IOcrEngine
	{
		string RunOcr(Bitmap image);
	}
}
