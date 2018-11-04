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

        public static void Connect(string username, string oAuthcode, string channel)
        {
            client = new TwitchClient();
            var credentials = new ConnectionCredentials(username, oAuthcode);
            client.Initialize(credentials, channel);
            client.Connect();

            client.OnJoinedChannel += OnJoinedChannel;
            client.OnMessageReceived += OnMessageReceived;
            client.OnConnected += OnConnected;

            if (!client.IsConnected)
            {
                var message = $"Could not connect to Twitch.";
                MessageBox.Show(message, "Error!");
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
            var message = $"Connected to channel '{e.Channel}'.";
            MessageBox.Show(message, "Success!");
        }

        private static void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            TextToSpeech.Speak($"{e.ChatMessage.Username} said, {e.ChatMessage.Message}");
        }
    }
}
