using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

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
