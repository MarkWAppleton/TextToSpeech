using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace TextToSpeechV3.SpeechManager
{
	public class WindowsMediaSpeechSynthesis : ISpeechManager
	{
		private SpeechSynthesizer _synth;
		public WindowsMediaSpeechSynthesis()
		{
			_synth = new SpeechSynthesizer();
		}

		public async Task PlayAudio(string text, string voiceName, double speechRate)
		{
			Windows.Media.Playback.MediaPlayer mediaPlayer = new Windows.Media.Playback.MediaPlayer();
			
			//synth.Voice = SpeechSynthesizer.AllVoices.Where(w => w.DisplayName == voiceName).FirstOrDefault();
			//_synth.Options.SpeakingRate = speechRate;
			SpeechSynthesisStream stream = await _synth.SynthesizeTextToStreamAsync(text);

			mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromStream(stream, stream.ContentType);
			mediaPlayer.AutoPlay = true;
		}
		public IEnumerable<string> GetVoices()
		{
			return SpeechSynthesizer.AllVoices.Select(s => s.DisplayName);
		}

		public void SetVoice(string name)
		{
			_synth.Voice = SpeechSynthesizer.AllVoices.Where(w => w.DisplayName == name).FirstOrDefault();
		}

		public void SetRate(double rate)
		{
			_synth.Options.SpeakingRate = rate;
		}

		public void setVolume(double volume)
		{
			throw new NotImplementedException();
		}
	}
}
