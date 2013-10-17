namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class Videos
    {
        private int? _categoryid;
        private int _commentcount;
        private DateTime _createddate = DateTime.Now;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private int _favouritecount;
        private int _forwardedcount;
        private int _isrecomend;
        private string _normalimageurl;
        private int? _ownervideoid;
        private int _pvcount;
        private int _sequence;
        private int _status = 1;
        private string _tags;
        private string _thumbimageurl;
        private int _type;
        private int _videoid;
        private string _videoname;
        private string _videourl;

        public int? CategoryId
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
            }
        }

        public int CommentCount
        {
            get
            {
                return this._commentcount;
            }
            set
            {
                this._commentcount = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return this._createddate;
            }
            set
            {
                this._createddate = value;
            }
        }

        public string CreatedNickName
        {
            get
            {
                return this._creatednickname;
            }
            set
            {
                this._creatednickname = value;
            }
        }

        public int CreatedUserID
        {
            get
            {
                return this._createduserid;
            }
            set
            {
                this._createduserid = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public int FavouriteCount
        {
            get
            {
                return this._favouritecount;
            }
            set
            {
                this._favouritecount = value;
            }
        }

        public int ForwardedCount
        {
            get
            {
                return this._forwardedcount;
            }
            set
            {
                this._forwardedcount = value;
            }
        }

        public int IsRecomend
        {
            get
            {
                return this._isrecomend;
            }
            set
            {
                this._isrecomend = value;
            }
        }

        public string NormalImageUrl
        {
            get
            {
                return this._normalimageurl;
            }
            set
            {
                this._normalimageurl = value;
            }
        }

        public int? OwnerVideoId
        {
            get
            {
                return this._ownervideoid;
            }
            set
            {
                this._ownervideoid = value;
            }
        }

        public int PVCount
        {
            get
            {
                return this._pvcount;
            }
            set
            {
                this._pvcount = value;
            }
        }

        public int Sequence
        {
            get
            {
                return this._sequence;
            }
            set
            {
                this._sequence = value;
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public string Tags
        {
            get
            {
                return this._tags;
            }
            set
            {
                this._tags = value;
            }
        }

        public string ThumbImageUrl
        {
            get
            {
                return this._thumbimageurl;
            }
            set
            {
                this._thumbimageurl = value;
            }
        }

        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public int VideoID
        {
            get
            {
                return this._videoid;
            }
            set
            {
                this._videoid = value;
            }
        }

        public string VideoName
        {
            get
            {
                return this._videoname;
            }
            set
            {
                this._videoname = value;
            }
        }

        public string VideoUrl
        {
            get
            {
                return this._videourl;
            }
            set
            {
                this._videourl = value;
            }
        }
    }
}

