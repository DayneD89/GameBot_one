using System;
using System.Collections.Generic;
using System.Text;

namespace GameBot_One.Services.Music.Exception
{
    public class TrackStuckException : System.Exception
    {
        public TrackStuckException() : base() { }

        public TrackStuckException(string message) : base(message) { }

        public TrackStuckException(string message, System.Exception inner) : base(message, inner) { }

        protected TrackStuckException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}