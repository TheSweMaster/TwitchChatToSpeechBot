using System.Globalization;
using System.Speech.Synthesis;

namespace TwitchTextToSpeechApp
{
    public class TextToSpeechSettings
    {
        public int Volume { get; set; }
        public int Speed { get; set; }
        public CultureInfo Culture { get; set; }
        public VoiceGender Gender { get; set; }
        public VoiceAge Age { get; set; }
    }
}
