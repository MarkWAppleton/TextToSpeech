using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechV3.Model;

namespace TextToSpeechV3.SpeechManager
{
	public interface ISpeechManager
	{
		Task PlayAudio(string text, string voiceName, double speechRate);

		IEnumerable<string> GetVoices();

		void SetVoice(string name);

		void SetRate(double rate);

		void setVolume(double volume);

		void setAllSettings(SpeechSettings settings);
	}
}
