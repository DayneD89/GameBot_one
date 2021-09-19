using System;
using System.Collections.Generic;
using System.Text;

namespace GameBot_One.Services.Music.Core
{
    public abstract class AudioLoadResultHandler
    {
        public abstract void OnLoadTrack(AudioTrack track);
        public abstract void OnLoadPlaylist(List<AudioTrack> tracks);
    }
}