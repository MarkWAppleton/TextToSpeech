﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.Services.Interfaces
{
	public interface IOcrEngine
	{
		string RunOcr(Bitmap image);
	}
}
