using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using GameBot_One.Services;

namespace GameBot_One.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        private string Ending => Context.User.Id switch
        {
            583356866944565258 => ", Sir.",
            889202419035156510 => ", Ma'am.",
            _ => "."
        };

        public PictureService PictureService { get; set; }

        [Command("ping")]
        [Alias("pong")]
        public Task PingAsync()
            => ReplyAsync("Pong"+Ending);

        [Command("hello")]
        public Task HelloAsync()
            => ReplyAsync("Hello " + Context.User.Username + Ending);

        [Command("cat")]
        public async Task CatAsync()
        {
            var stream = await PictureService.GetCatPictureAsync();
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
            await ReplyAsync("Your cat" + Ending);
        }

        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;

            await ReplyAsync(user.ToString());
        }

        [Command("echo")]
        public Task EchoAsync([Remainder] string text)
            => ReplyAsync('\u200B' + text);
    }
}