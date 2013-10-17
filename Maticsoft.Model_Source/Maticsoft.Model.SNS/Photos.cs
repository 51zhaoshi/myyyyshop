namespace Maticsoft.Model.SNS
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Photos
    {
        private int _categoryid = -1;
        private int _commentcount;
        private DateTime _createddate = DateTime.Now;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private int _favouritecount;
        private int _forwardedcount;
        private int _isrecomend;
        private string _maplat;
        private string _maplng;
        private string _normalimageurl;
        private int? _ownerphotoid;
        private string _photoaddress;
        private int _photoid;
        private string _photoname;
        private string _photourl;
        private int _pvcount;
        private int _sequence;
        private string _staticurl;
        private int _status = 1;
        private string _tags;
        private string _thumbimageurl;
        private string _topcommentsid;
        private int _type;
        public List<Comments> commmentList = new List<Comments>();

        public int CategoryId
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

        public string MapLat
        {
            get
            {
                return this._maplat;
            }
            set
            {
                this._maplat = value;
            }
        }

        public string MapLng
        {
            get
            {
                return this._maplng;
            }
            set
            {
                this._maplng = value;
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

        public int? OwnerPhotoId
        {
            get
            {
                return this._ownerphotoid;
            }
            set
            {
                this._ownerphotoid = value;
            }
        }

        public string PhotoAddress
        {
            get
            {
                return this._photoaddress;
            }
            set
            {
                this._photoaddress = value;
            }
        }

        public int PhotoID
        {
            get
            {
                return this._photoid;
            }
            set
            {
                this._photoid = value;
            }
        }

        public string PhotoName
        {
            get
            {
                return this._photoname;
            }
            set
            {
                this._photoname = value;
            }
        }

        public string PhotoUrl
        {
            get
            {
                return this._photourl;
            }
            set
            {
                this._photourl = value;
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

        public string StaticUrl
        {
            get
            {
                return this._staticurl;
            }
            set
            {
                this._staticurl = value;
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

        public string TopCommentsId
        {
            get
            {
                return this._topcommentsid;
            }
            set
            {
                this._topcommentsid = value;
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
    }
}

