using System;
using System.Collections.Generic;
using System.Text;

namespace GameBot_One.Services.Music.Exception
{
    public class NullUrlTrackException : System.Exception
    {
        public NullUrlTrackException() : base() { }

        public NullUrlTrackException(string message) : base(message) { }

        public NullUrlTrackException(string message, System.Exception inner) : base(message, inner) { }

        protected NullUrlTrackException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}