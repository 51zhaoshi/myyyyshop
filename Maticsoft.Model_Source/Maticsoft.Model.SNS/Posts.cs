namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class Posts
    {
        private string _audiourl;
        private int _commentcount;
        private DateTime _createddate = DateTime.Now;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private int? _favcount;
        private int _forwardcount;
        private int? _forwardedid = 0;
        private bool _hasreferusers;
        private string _imageurl;
        private bool _isrecommend;
        private int _originalid;
        private string _postexurl;
        private int _postid;
        private decimal? _price;
        private string _productlinkurl;
        private string _productname;
        private int _sequence;
        private int _status = 1;
        private string _tags;
        private int _targetid;
        private string _topictitle = "0";
        private int? _type = 0;
        private string _userip;
        private string _videourl;

        public string AudioUrl
        {
            get
            {
                return this._audiourl;
            }
            set
            {
                this._audiourl = value;
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

        public int? FavCount
        {
            get
            {
                return this._favcount;
            }
            set
            {
                this._favcount = value;
            }
        }

        public int ForwardCount
        {
            get
            {
                return this._forwardcount;
            }
            set
            {
                this._forwardcount = value;
            }
        }

        public int? ForwardedID
        {
            get
            {
                return this._forwardedid;
            }
            set
            {
                this._forwardedid = value;
            }
        }

        public bool HasReferUsers
        {
            get
            {
                return this._hasreferusers;
            }
            set
            {
                this._hasreferusers = value;
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

        public bool IsRecommend
        {
            get
            {
                return this._isrecommend;
            }
            set
            {
                this._isrecommend = value;
            }
        }

        public int OriginalID
        {
            get
            {
                return this._originalid;
            }
            set
            {
                this._originalid = value;
            }
        }

        public string PostExUrl
        {
            get
            {
                return this._postexurl;
            }
            set
            {
                this._postexurl = value;
            }
        }

        public int PostID
        {
            get
            {
                return this._postid;
            }
            set
            {
                this._postid = value;
            }
        }

        public decimal? Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public string ProductLinkUrl
        {
            get
            {
                return this._productlinkurl;
            }
            set
            {
                this._productlinkurl = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this._productname;
            }
            set
            {
                this._productname = value;
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

        public int TargetId
        {
            get
            {
                return this._targetid;
            }
            set
            {
                this._targetid = value;
            }
        }

        public string TopicTitle
        {
            get
            {
                return this._topictitle;
            }
            set
            {
                this._topictitle = value;
            }
        }

        public int? Type
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

        public string UserIP
        {
            get
            {
                return this._userip;
            }
            set
            {
                this._userip = value;
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

