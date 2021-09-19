namespace GameBot_One.Services.Games.CardsAgainstTheHumanity
{
    internal class CardsAgainstTheHumanity : Game
    {

        internal override void Startup()
        {
            ID = RandomString(8);
            Role = Guild.CreateRoleAsync("CAH-r" + ID,null, null,false,null).Result;
            Channel = Guild.CreateTextChannelAsync("CAH-c" + ID).Result;
        }
        internal override void Start()
        {
            throw new System.NotImplementedException();
        }
    }
}