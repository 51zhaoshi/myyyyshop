namespace VideoEncoder
{
    using System;
    using System.Runtime.CompilerServices;

    public class EncodeFinishedEventArgs : EventArgs
    {
        public EncodedVideo EncodedVideoInfo { get; set; }
    }
}

