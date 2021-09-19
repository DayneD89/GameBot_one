using Discord;
using Discord.WebSocket;

namespace GameBot_One.Services.Games.CardsAgainstTheHumanity
{
    internal class CardsAgainstTheHumanityFactory
    {
        internal static CardsAgainstTheHumanity Game(SocketGuild Guild)
        {
            CardsAgainstTheHumanity game = new CardsAgainstTheHumanity();
            game.Guild = Guild;
            game.Startup();
            return game;
        }
    }
}