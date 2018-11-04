using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
namespace TwitchTextToSpeechApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IReadOnlyCollection<InstalledVoice> _InstalledVoices;

        public MainWindow()
        {
            InitializeComponent();

            _InstalledVoices = TextToSpeech.GetInstalledVoices();

            ValidateInstalledVoices();
            GetTextToSpeechOptions();
            SetDefaultTextToSpeechValues();
        }

        private void ValidateInstalledVoices()
        {
            if (_InstalledVoices == null || _InstalledVoices.Count == 0)
            {
                MessageBox.Show("Could not find any installed text to speech voices.", "Error!");
            }
        }

        private void GetTextToSpeechOptions()
        {
            Combobox_Volume.ItemsSource = Enumerable.Range(0, 101).Where(x => x % 5 == 0);
            Combobox_Speed.ItemsSource = Enumerable.Range(-10, 21);
            Combobox_Voice.ItemsSource = _InstalledVoices.Select(x => x.VoiceInfo.Name);
        }

        private void SetDefaultTextToSpeechValues()
        {
            Combobox_Volume.SelectedIndex = 15;
            Combobox_Speed.SelectedIndex = 10;
            Combobox_Voice.SelectedIndex = 0;
        }

        private TextToSpeechSettings GetSelectedSpeechSettingsValues(VoiceInfo voiceInfo)
        {
            return new TextToSpeechSettings
            {
                Volume = (int)Combobox_Volume.SelectedValue,
                Speed = (int)Combobox_Speed.SelectedValue,
                Gender = voiceInfo.Gender,
                Age = voiceInfo.Age,
                Culture = voiceInfo.Culture
            };
        }

        private void Button_Click_ApplyTextToSpeechSettings(object sender, RoutedEventArgs e)
        {
            var selectedVoice = _InstalledVoices.FirstOrDefault(x => x.VoiceInfo.Name.Equals(Combobox_Voice.SelectedValue.ToString()));
            TextToSpeech.SetSettings(GetSelectedSpeechSettingsValues(selectedVoice.VoiceInfo));
        }

        private void Button_Click_TestTextToSpeech(object sender, RoutedEventArgs e)
        {
            TextToSpeech.Speak(SpeechText_Textbox.Text);
        }

        private void Button_Click_ConnectTwitchBot(object sender, RoutedEventArgs e)
        {
            var channelName = TextBox_ChannelName.Text;
            var oAuthCode = PasswordBox_OAuthCode.Password;
            var username = TextBox_Username.Text;
            TwitchBot.Connect(username, oAuthCode, channelName);
        }
    }
}
