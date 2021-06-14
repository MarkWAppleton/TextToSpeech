using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace TextToSpeechV3.SpeechManager
{
	public class WindowsMediaSpeechSynthesis : ISpeechManager
	{
		public WindowsMediaSpeechSynthesis()
		{

		}

		public async Task PlayAudio(string text, string voiceName, double speechRate)
		{
			Windows.Media.Playback.MediaPlayer mediaPlayer = new Windows.Media.Playback.MediaPlayer();
			SpeechSynthesizer synth = new SpeechSynthesizer();

			synth.Voice = SpeechSynthesizer.AllVoices.Where(w => w.DisplayName == voiceName).FirstOrDefault();
			synth.Options.SpeakingRate = speechRate;
			SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);

			mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromStream(stream, stream.ContentType);
			mediaPlayer.AutoPlay = true;
		}
		public IEnumerable<string> GetVoices()
		{
			return SpeechSynthesizer.AllVoices.Select(s => s.DisplayName);
		}

	}
}
