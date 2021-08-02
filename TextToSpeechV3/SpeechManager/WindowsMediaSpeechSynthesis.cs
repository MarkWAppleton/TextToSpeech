using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextToSpeech.Model;
using Windows.Media.Playback;
using Windows.Media.SpeechSynthesis;

namespace TextToSpeech.SpeechManager
{
	public class WindowsMediaSpeechSynthesis : ISpeechManager
	{
		private Windows.Media.Playback.MediaPlayer _mediaPlayer;
		private SpeechSynthesizer _synth;
		private bool _isSpeaking;

		public EnumSpeechEngine EngineType => EnumSpeechEngine.Windows10;
		public bool IsSpeaking => _isSpeaking;

		public WindowsMediaSpeechSynthesis()
		{
			_mediaPlayer = new Windows.Media.Playback.MediaPlayer();
			_synth = new SpeechSynthesizer();
			_mediaPlayer.MediaEnded += EndSpeaking;
		}

		public async Task SpeakText(string text)
		{
			_isSpeaking = true;
			SpeechSynthesisStream stream = await _synth.SynthesizeTextToStreamAsync(text);
			_mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromStream(stream, stream.ContentType);
			_mediaPlayer.AutoPlay = true;
		}

		public void StopSpeaking()
		{
			if (_isSpeaking)
			{
				_mediaPlayer.Pause();
				_isSpeaking = false;
			}
		}

		public IEnumerable<string> GetVoices()
		{
			return SpeechSynthesizer.AllVoices.Select(s => s.DisplayName);
		}

		public void SetVoice(string name)
		{
			VoiceInformation selectedVoice = SpeechSynthesizer.AllVoices.Where(w => w.DisplayName == name).FirstOrDefault();
			if(selectedVoice == null)
			{
				_synth.Voice = SpeechSynthesizer.AllVoices.FirstOrDefault();
			}
			else
			{
				_synth.Voice = selectedVoice;
			}
		}

		public void SetRate(double rate)
		{
			_synth.Options.SpeakingRate = rate;
		}

		public void SetVolume(double volume)
		{
			throw new NotImplementedException();
		}

		public void SetAllSettings(SpeechSettings settings)
		{
			SetVoice(settings.Voice);
			SetRate(settings.Rate);
		}

		private void EndSpeaking(MediaPlayer sender, object args)
		{
			_isSpeaking = false;
		}

	}
}
