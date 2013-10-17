namespace Maticsoft.Model.SNS
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class UserAlbums
    {
        private int _albumid;
        private string _albumname;
        private int _channelsequence;
        private int? _commentscount = 0;
        private string _coverphotourl;
        private int? _covertargetid;
        private int? _covertargettype;
        private DateTime _createddate = DateTime.Now;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private int _favouritecount;
        private int _isrecommend;
        private DateTime? _lastupdateddate = new DateTime?(DateTime.Now);
        private int _photocount;
        private int _privacy;
        private int _pvcount;
        private int _sequence;
        private int _status = 1;
        private string _tags;

        public int AlbumID
        {
            get
            {
                return this._albumid;
            }
            set
            {
                this._albumid = value;
            }
        }

        public string AlbumName
        {
            get
            {
                return this._albumname;
            }
            set
            {
                this._albumname = value;
            }
        }

        public int ChannelSequence
        {
            get
            {
                return this._channelsequence;
            }
            set
            {
                this._channelsequence = value;
            }
        }

        public int? CommentsCount
        {
            get
            {
                return this._commentscount;
            }
            set
            {
                this._commentscount = value;
            }
        }

        public string CoverPhotoUrl
        {
            get
            {
                return this._coverphotourl;
            }
            set
            {
                this._coverphotourl = value;
            }
        }

        public int? CoverTargetID
        {
            get
            {
                return this._covertargetid;
            }
            set
            {
                this._covertargetid = value;
            }
        }

        public int? CoverTargetType
        {
            get
            {
                return this._covertargettype;
            }
            set
            {
                this._covertargettype = value;
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

        public int IsRecommend
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

        public DateTime? LastUpdatedDate
        {
            get
            {
                return this._lastupdateddate;
            }
            set
            {
                this._lastupdateddate = value;
            }
        }

        public int PhotoCount
        {
            get
            {
                return this._photocount;
            }
            set
            {
                this._photocount = value;
            }
        }

        public int Privacy
        {
            get
            {
                return this._privacy;
            }
            set
            {
                this._privacy = value;
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

        public int TypeId { get; set; }
    }
}

