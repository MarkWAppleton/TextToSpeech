using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechV3.Model;

namespace TextToSpeechV3.SpeechManager
{
	public interface ISpeechManager
	{
		EnumSpeechEngine EngineType { get; }
		Task SpeakText(string text);

		IEnumerable<string> GetVoices();

		void SetVoice(string name);

		void SetRate(double rate);

		void setVolume(double volume);

		void setAllSettings(SpeechSettings settings);
	}
}
