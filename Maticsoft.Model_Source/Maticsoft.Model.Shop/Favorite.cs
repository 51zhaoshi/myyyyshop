namespace Maticsoft.Model.Shop
{
    using System;

    [Serializable]
    public class Favorite
    {
        private DateTime _createddate = DateTime.Now;
        private int _favoriteid;
        private string _remark;
        private string _tags;
        private long _targetid;
        private int _type;
        private int _userid;

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

        public int FavoriteId
        {
            get
            {
                return this._favoriteid;
            }
            set
            {
                this._favoriteid = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
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

        public long TargetId
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

        public int UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }
    }
}

