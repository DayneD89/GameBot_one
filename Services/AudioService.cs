using Discord;
using Discord.Commands;
using GameBot_One.Services.Music.Core;
using GameBot_One.Services.Music.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameBot_One.Services.Music;

namespace GameBot_One.Services
{
    public class AudioService
    {
        public GuildMusicManager MusicManager { get; set; }

        public AudioService()
        {
            MusicManager = new GuildMusicManager();
        }

        public async Task JoinChannel(IVoiceChannel channel, IGuild guild)
        {

            var audioClient = await channel.ConnectAsync();

            GuildVoiceState voiceState = MusicManager.GetGuildVoiceState(guild);
            voiceState.Player.SetAudioClient(audioClient);
        }

        public async Task LeaveChannel(SocketCommandContext Context)
        {
            if (MusicManager.VoiceStates.TryGetValue(Context.Guild.Id, out GuildVoiceState voiceState))
            {
                try
                {
                    await voiceState.Player.AudioClient.StopAsync();
                    MusicManager.VoiceStates.TryRemove(Context.Guild.Id, out voiceState);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + ", cannot disconnect");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("I'm not connected");
            }
        }

        public async Task loadAndPlay(string query, IGuild guild)
        {
            List<AudioTrack> tracks;

            // If query is Url
            if (Uri.IsWellFormedUriString(query, UriKind.Absolute))
            {
                Console.WriteLine(query + " is url");
                tracks = await TrackLoader.LoadAudioTrack(query, fromUrl: true);
            }
            else
            {
                Console.WriteLine(query + " is not url");
                tracks = await TrackLoader.LoadAudioTrack(query, fromUrl: false);
            }

            if (tracks.Count == 0)
            {
                return;
            }

            Console.WriteLine("Loaded " + tracks.Count + " entri(es)");

            GuildVoiceState voiceState = MusicManager.GetGuildVoiceState(guild);

            foreach (AudioTrack track in tracks)
            {
                //await voiceState.Scheduler.EnqueueAsync(track);
                voiceState.Scheduler.Enqueue(track);
                Console.WriteLine("Enqueued " + track.TrackInfo.Title);
            }
        }
    }
}