namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class WeiBoTaskMsg
    {
        private DateTime _createdate;
        private string _imageurl;
        private DateTime? _publishdate;
        private string _weibomsg;
        private int _weibotaskid;

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

        public int WeiBoTaskId
        {
            get
            {
                return this._weibotaskid;
            }
            set
            {
                this._weibotaskid = value;
            }
        }
    }
}

