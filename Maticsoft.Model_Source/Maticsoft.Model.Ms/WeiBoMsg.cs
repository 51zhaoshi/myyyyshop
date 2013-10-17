namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class WeiBoMsg
    {
        private DateTime _createdate;
        private string _imageurl;
        private DateTime? _publishdate;
        private int _weiboid;
        private string _weibomsg;

        public DateTime CreateDate
        {
            get
            {
                return this._createdate;
            }
            set
            {
                this._createdate = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return this._imageurl;
            }
            set
            {
                this._imageurl = value;
            }
        }

        public DateTime? PublishDate
        {
            get
            {
                return this._publishdate;
            }
            set
            {
                this._publishdate = value;
            }
        }

        public int WeiBoId
        {
            get
            {
                return this._weiboid;
            }
            set
            {
                this._weiboid = value;
            }
        }

        public string WeiboMsg
        {
            get
            {
                return this._weibomsg;
            }
            set
            {
                this._weibomsg = value;
            }
        }
    }
}

