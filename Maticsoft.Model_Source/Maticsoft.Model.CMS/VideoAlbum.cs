namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class VideoAlbum
    {
        private int _albumid;
        private string _albumname;
        private string _covervideo = "AlbumDefaultPictures.jpg";
        private DateTime _createddate;
        private int _createduserid;
        private string _createdusername;
        private string _description;
        private DateTime? _lastupdateddate;
        private int? _lastupdateuserid;
        private string _lastupdateusername;
        private int? _privacy;
        private int _pvcount;
        private int _sequence;
        private int _state;

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

        public string CoverVideo
        {
            get
            {
                return this._covervideo;
            }
            set
            {
                this._covervideo = value;
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

        public int? LastUpdateUserID
        {
            get
            {
                return this._lastupdateuserid;
            }
            set
            {
                this._lastupdateuserid = value;
            }
        }

        public string LastUpdateUserName
        {
            get
            {
                return this._lastupdateusername;
            }
            set
            {
                this._lastupdateusername = value;
            }
        }

        public int? Privacy
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

        public int PvCount
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
    }
}

