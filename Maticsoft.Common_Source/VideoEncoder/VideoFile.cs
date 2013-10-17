namespace VideoEncoder
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class VideoFile
    {
        private string _Path;

        public VideoFile(string path)
        {
            this._Path = path;
            this.Initialize();
        }

        private void Initialize()
        {
            this.infoGathered = false;
            if (string.IsNullOrEmpty(this._Path))
            {
                throw new Exception("Video file Path not set or empty.");
            }
            if (!File.Exists(this._Path))
            {
                throw new Exception("The video file " + this._Path + " does not exist.");
            }
        }

        public double AudioBitRate { get; set; }

        public string AudioFormat { get; set; }

        public double BitRate { get; set; }

        public TimeSpan Duration { get; set; }

        public double FrameRate { get; set; }

        public int Height { get; set; }

        public bool infoGathered { get; set; }

        public string Path
        {
            get
            {
                return this._Path;
            }
            set
            {
                this._Path = value;
            }
        }

        public string RawAudioFormat { get; set; }

        public string RawInfo { get; set; }

        public string RawVideoFormat { get; set; }

        public long TotalFrames { get; set; }

        public double VideoBitRate { get; set; }

        public string VideoFormat { get; set; }

        public int Width { get; set; }
    }
}

