namespace VideoEncoder
{
    using System;
    using System.Runtime.CompilerServices;

    public class EncodedVideo
    {
        public string EncodedVideoPath { get; set; }

        public string EncodingLog { get; set; }

        public bool Success { get; set; }

        public string ThumbnailPath { get; set; }
    }
}

