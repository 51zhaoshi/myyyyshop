namespace Maticsoft.Common.Video
{
    using System;

    public class VideoModel
    {
        private string _imgPath;
        private string _savePath;
        private int _videoSpan;

        public string ImgPath
        {
            get
            {
                return this._imgPath;
            }
            set
            {
                this._imgPath = value;
            }
        }

        public string SavePath
        {
            get
            {
                return this._savePath;
            }
            set
            {
                this._savePath = value;
            }
        }

        public int VideoSpan
        {
            get
            {
                return this._videoSpan;
            }
            set
            {
                this._videoSpan = value;
            }
        }
    }
}

