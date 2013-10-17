namespace VideoEncoder
{
    using System;
    using System.Runtime.CompilerServices;

    public class EncodeProgressEventArgs : EventArgs
    {
        public long CurrentFrame { get; set; }

        public short FPS { get; set; }

        public short Percentage { get; set; }

        public string RawOutputLine { get; set; }

        public long TotalFrames { get; set; }
    }
}

