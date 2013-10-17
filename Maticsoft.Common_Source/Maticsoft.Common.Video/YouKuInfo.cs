namespace Maticsoft.Common.Video
{
    using System;

    public class YouKuInfo
    {
        private string logo;
        private string title;
        private string vidEncoded;
        private string videoid;

        public string Logo
        {
            get
            {
                return this.logo;
            }
            set
            {
                this.logo = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public string VidEncoded
        {
            get
            {
                return this.vidEncoded;
            }
            set
            {
                this.vidEncoded = value;
            }
        }

        public string VideoID
        {
            get
            {
                return this.videoid;
            }
            set
            {
                this.videoid = value;
            }
        }
    }
}

