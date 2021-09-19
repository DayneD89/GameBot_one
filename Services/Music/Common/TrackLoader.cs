﻿using GameBot_One.Services.Music.Provider;
using System;
using System.Threading.Tasks;
using GameBot_One.Services.Music.Core;
using System.IO;
using System.Diagnostics;
using GameBot_One.Services.Music.Exception;
using GameBot_One.Services.Music.Objects;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web;

namespace GameBot_One.Services.Music.Common
{
    public class TrackLoader
    {
        public static async Task<List<AudioTrack>> LoadAudioTrack(string query, bool fromUrl = true)
        {
            if (!fromUrl)
            {
                query = HttpUtility.UrlEncode(query);
            }

            JObject ytdlResponseJson = await YoutubeDLInfoProvider.ExtractInfo(query, fromUrl);

            List<AudioTrack> songs = new List<AudioTrack>();

            // Check if playlist
            if (ytdlResponseJson.ContainsKey("entries"))
            {
                if (fromUrl)
                {
                    foreach (JObject ytdlVideoJson in ytdlResponseJson["entries"].Value<JArray>())
                    {
                        SongInfo songInfo = SongInfo.ParseYtdlResponse(ytdlVideoJson);
                        songs.Add(new AudioTrack(LoadFFmpegProcess(songInfo.Url))
                        {
                            Url = songInfo.Url,
                            TrackInfo = songInfo
                        });
                    }
                }
                else
                {
                    JObject ytdlVideoJson = ytdlResponseJson["entries"].Value<JArray>()[0].Value<JObject>();
                    SongInfo firstEntrySong = SongInfo.ParseYtdlResponse(ytdlVideoJson);
                    songs.Add(new AudioTrack(LoadFFmpegProcess(firstEntrySong.Url))
                    {
                        Url = firstEntrySong.Url,
                        TrackInfo = firstEntrySong
                    });
                }
            }
            else
            {
                SongInfo songInfo = SongInfo.ParseYtdlResponse(ytdlResponseJson);
                songs.Add(new AudioTrack(LoadFFmpegProcess(songInfo.Url))
                {
                    Url = songInfo.Url,
                    TrackInfo = songInfo
                });
            }
            return songs;
        }

        public static async Task LoadAudioTrack(string query, AudioLoadResultHandler handler, bool fromUrl = true)
        {
            JObject ytdlResponseJson = await YoutubeDLInfoProvider.ExtractInfo(query, fromUrl);

            List<AudioTrack> songs = new List<AudioTrack>();

            // Check if playlist
            if (ytdlResponseJson.ContainsKey("entries"))
            {
                if (fromUrl)
                {
                    foreach (JObject ytdlVideoJson in ytdlResponseJson["entries"].Value<JArray>())
                    {
                        SongInfo songInfo = SongInfo.ParseYtdlResponse(ytdlVideoJson);
                        songs.Add(new AudioTrack(LoadFFmpegProcess(songInfo.Url))
                        {
                            Url = songInfo.Url,
                            TrackInfo = songInfo
                        });
                    }
                    handler.OnLoadPlaylist(songs);
                }
                else
                {
                    JObject ytdlVideoJson = ytdlResponseJson["entries"].Value<JArray>()[0].Value<JObject>();
                    SongInfo firstEntrySong = SongInfo.ParseYtdlResponse(ytdlVideoJson);
                    handler.OnLoadTrack(new AudioTrack(LoadFFmpegProcess(firstEntrySong.Url))
                    {
                        Url = firstEntrySong.Url,
                        TrackInfo = firstEntrySong
                    });
                }
            }
            else
            {
                SongInfo songInfo = SongInfo.ParseYtdlResponse(ytdlResponseJson);
                handler.OnLoadTrack(new AudioTrack(LoadFFmpegProcess(songInfo.Url))
                {
                    Url = songInfo.Url,
                    TrackInfo = songInfo
                });
            }
        }

        public static Process LoadFFmpegProcess(string url)
        {
            var args = string.Format("youtube-dl --format bestaudio -o - {0} | ffmpeg -loglevel panic -i pipe:0 -ac 2 -f s16le -ar 48000 pipe:1", url);
            return Process.Start(new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{args}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }
    }
}