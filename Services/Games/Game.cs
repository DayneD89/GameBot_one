using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Linq;

namespace GameBot_One.Services.Games
{
    abstract internal class Game
    {
        public RestTextChannel Channel { get; protected set; }
        public SocketGuild Guild { get; internal set; }
        public string ID { get; internal set; }
        public RestRole Role { get; internal set; }

        abstract internal void Start();
        abstract internal void Startup();



        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}