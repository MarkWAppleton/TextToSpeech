using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TextToSpeech.Model;

namespace TextToSpeech.SpeechManager
{
	public interface ISpeechManager
	{
		public bool IsSpeaking { get; }

		EnumSpeechEngine EngineType { get; }
		Task SpeakText(string text);
		void StopSpeaking();

		IEnumerable<string> GetVoices();

		void SetVoice(string name);

		void SetRate(double rate);

		void SetVolume(double volume);

		void SetAllSettings(SpeechSettings settings);
	}
}
