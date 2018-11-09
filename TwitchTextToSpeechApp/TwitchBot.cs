using System;
using System.Windows;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchTextToSpeechApp
{
    public static class TwitchBot
    {
        private static TwitchClient client;

        public static void Connect(TwitchConnectSettings twitchSettings)
        {
            client = new TwitchClient();
            var credentials = new ConnectionCredentials(twitchSettings.Username, twitchSettings.OAuthCode);
            client.Initialize(credentials, twitchSettings.ChannelName);
            client.Connect();

            client.OnJoinedChannel += OnJoinedChannel;
            client.OnMessageReceived += OnMessageReceived;
            client.OnConnected += OnConnected;

            if (!client.IsConnected)
            {
                MessageBox.Show("Could not connect to Twitch.", "Error!");
            }
        }

        private static void OnConnected(object sender, OnConnectedArgs e)
        {
            var message = $"Connected to Twitch by user '{e.BotUsername}' to channel '{e.AutoJoinChannel}'.";
            MessageBox.Show(message, "Success!");
        }

        private static void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            client.SendMessage(e.Channel, "Text to speech bot joined the channel.");
            MessageBox.Show($"Connected to channel '{e.Channel}'.", "Success!");
        }

        private static void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            TextToSpeech.Speak($"{e.ChatMessage.Username} said, {e.ChatMessage.Message}");
        }
    }
}
