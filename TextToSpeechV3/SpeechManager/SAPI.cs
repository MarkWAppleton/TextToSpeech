using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SpeechLib;
using System.Linq;

namespace TextToSpeechV3.SpeechManager
{
	public class SAPI : ISpeechManager
	{
		private SpVoice _spVoice;

		public SAPI()
		{
			_spVoice = new SpVoice();
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

		public Task PlayAudio(string text, string voiceName, double speechRate)
		{
			_spVoice.Speak(text, SpeechVoiceSpeakFlags.SVSFDefault);
			return null;
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
		}
		public void SetRate(double rate)
		{
			_spVoice.Rate = ConvertDoubleToInt(rate);
		}

		public void setVolume(double volume)
		{
			_spVoice.Volume = ConvertDoubleToInt(volume);
		}

		private int ConvertDoubleToInt(double value)
		{
			var decimalPart = value - Math.Truncate(value);
			return (int)decimalPart * 10;
		}
	}
}
