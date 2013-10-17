namespace Maticsoft.OAuth
{
    using System;

    public class Provider
    {
        private string authUrl;
        private string bindUrl;
        private int mediaID;
        private string mediaName;

        public Provider(string authUrl, string bindUrl, int mediaID, string mediaName)
        {
            this.authUrl = authUrl;
            this.bindUrl = bindUrl;
            this.mediaID = mediaID;
            this.mediaName = mediaName;
        }

        public string AuthUrl
        {
            get
            {
                return this.authUrl;
            }
            set
            {
                this.authUrl = value;
            }
        }

        public string BindUrl
        {
            get
            {
                return this.bindUrl;
            }
            set
            {
                this.bindUrl = value;
            }
        }

        public int MediaID
        {
            get
            {
                return this.mediaID;
            }
            set
            {
                this.mediaID = value;
            }
        }

        public string MediaName
        {
            get
            {
                return this.mediaName;
            }
            set
            {
                this.mediaName = value;
            }
        }
    }
}

