using System.Threading.Tasks;
using Discord.Commands;
using GameBot_One.Services.Games;
using GameBot_One.Services.Games.CardsAgainstTheHumanity;

namespace GameBot_One.Modules
{
    // Modules must be public and inherit from an IModuleBase
    public class GameModule : ModuleBase<SocketCommandContext>
    {
        [Command("start-cah")]
        public async Task StartCah()
        {
            Game game = CardsAgainstTheHumanityFactory.Game(Context.Guild);
            game.Start();
            await ReplyAsync("Game created in " + game.Channel.Id);
        }
    }
}