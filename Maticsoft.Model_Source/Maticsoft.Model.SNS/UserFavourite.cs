namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class UserFavourite
    {
        private DateTime _createddate;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private int _favouriteid;
        private string _ownernickname;
        private int? _owneruserid;
        private string _tags;
        private int _targetid;
        private int _type;

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

        public int FavouriteID
        {
            get
            {
                return this._favouriteid;
            }
            set
            {
                this._favouriteid = value;
            }
        }

        public string OwnerNickName
        {
            get
            {
                return this._ownernickname;
            }
            set
            {
                this._ownernickname = value;
            }
        }

        public int? OwnerUserID
        {
            get
            {
                return this._owneruserid;
            }
            set
            {
                this._owneruserid = value;
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

        public int TargetID
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
    }
}

