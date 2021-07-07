using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SpeechLib;
using System.Linq;
using System.Threading;
using TextToSpeechV3.Model;

namespace TextToSpeechV3.SpeechManager
{
	public class SAPI : ISpeechManager
	{
		private SpVoice _spVoice;
		private bool _isSpeaking;
		private CancellationTokenSource _cancellationTokenSource;

		public EnumSpeechEngine EngineType => EnumSpeechEngine.Legacy;
		public bool IsSpeaking => _isSpeaking;

		public SAPI()
		{
			_spVoice = new SpVoice();
			_spVoice.EndStream += SPVoiceEndSpeaking;
			_spVoice.AlertBoundary = SpeechVoiceEvents.SVEPhoneme;
			_isSpeaking = false;
		}

		public IEnumerable<string> GetVoices()
		{
			List<string> results = new List<string>();
			ISpeechObjectTokens voices = new SpVoice().GetVoices();

			for(int i = 0; i < voices.Count; i++)
			{
				SpObjectToken voice = voices.Item(i);
				string name = voice.GetDescription();
				results.Add(name);
			}

			return results;
		}

		public Task SpeakText(string text)
		{
			if (_isSpeaking)
			{
				StopSpeaking();
			}
			else
			{
				_isSpeaking = true;
				int queueID = _spVoice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
			}
			return null;
		}
		public void StopSpeaking()
		{
			if (_isSpeaking)
			{
				_spVoice.Speak("", SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
				_isSpeaking = false;
			}
		}

		public void SetVoice(string name)
		{
			ISpeechObjectTokens voices = new SpVoice().GetVoices();

			for (int i = 0; i < voices.Count; i++)
			{
				SpObjectToken voice = voices.Item(i);
				if (string.Equals(voice.GetDescription(), name))
				{
					_spVoice.Voice = voice;
					return;
				}
			}
			_spVoice.Voice = voices.Item(0);
		}
		public void SetRate(double rate)
		{
			_spVoice.Rate = ConvertDoubleToInt(rate);
		}

		public void SetVolume(double volume)
		{
			_spVoice.Volume = ConvertDoubleToInt(volume) * 100;
		}

		/// <summary>
		/// Converts 0-2 double to a whole number (1.6 becomes 6)
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private int ConvertDoubleToInt(double value)
		{
			if(value >= 2)
			{
				return 2;
			}
			else if(value == 1)
			{
				return 1;
			}
			var decimalPart = value - Math.Truncate(value);
			return (int)(decimalPart * 10);
		}

		private void SPVoiceEndSpeaking(int StreamNumber, object StreamPosition)
		{
			_isSpeaking = false;
		}

		public void SetAllSettings(SpeechSettings settings)
		{
			SetVoice(settings.Voice);
			SetRate(settings.Rate);
			SetVolume(settings.Volume);
		}

	}
}
