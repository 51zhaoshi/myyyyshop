namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class Photo
    {
        private int _albumid;
        private int _classid;
        private int? _commentcount = 0;
        private DateTime _createddate;
        private int _createduserid;
        private string _createdusername;
        private string _description;
        private int _favouritecount;
        private string _imageurl;
        private bool? _isrecomend = false;
        private string _normalimageurl;
        private int _photoid;
        private string _photoname;
        private int _pvcount;
        private int? _sequence;
        private int _state;
        private string _tags;
        private string _thumbimageurl;

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

        public int ClassID
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public int? CommentCount
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

        public string CreatedUserName
        {
            get
            {
                return this._createdusername;
            }
            set
            {
                this._createdusername = value;
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

        public bool? IsRecomend
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

        public int? Sequence
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

        public int State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
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
    }
}

