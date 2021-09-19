using GameBot_One.Services.Music.Core;

namespace GameBot_One.Services.Music
{
    public class GuildVoiceState
    {
        public AudioPlayer Player { get; set; }
        public TrackScheduler Scheduler { get; set; }

        public GuildVoiceState()
        {
            Player = new AudioPlayer();
            Scheduler = new TrackScheduler(Player);

            Player.RegisterEventAdapter(Scheduler);
        }
    }
}