using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;

namespace TwitchTextToSpeechApp
{
    public static class TextToSpeech
    {
        private static readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        public static void SetSettings(TextToSpeechSettings textToSpeechSettings)
        {
            _speechSynthesizer.Volume = textToSpeechSettings.Volume;
            _speechSynthesizer.Rate = textToSpeechSettings.Speed;
            _speechSynthesizer.SelectVoiceByHints(textToSpeechSettings.Gender, textToSpeechSettings.Age, 0, textToSpeechSettings.Culture);
        }

        public static void Speak(string textToSpeech)
        {
            _speechSynthesizer.SpeakAsync(textToSpeech);
        }

        public static void Stop()
        {
            _speechSynthesizer.SpeakAsyncCancelAll();
            _speechSynthesizer.Pause();
        }

        public static void Start()
        {
            _speechSynthesizer.Resume();
        }

        public static IReadOnlyCollection<InstalledVoice> GetInstalledVoices()
        {
            return _speechSynthesizer.GetInstalledVoices();
        }
    }
}
