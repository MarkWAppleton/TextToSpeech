using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeech.Model.ImagePrcessing;
using TextToSpeech.Services.Interfaces;

namespace TextToSpeech.Services.ImagePrcessingStages
{
    public class SimpleResizeImageProcessingService : IImageProcessingService
    {
        public Bitmap ProcessImage(Bitmap original, IImageProcessingConfig? config = null)
        {
            return new Bitmap(original, new Size(original.Width * 2, original.Height * 2));
        }
    }
}
