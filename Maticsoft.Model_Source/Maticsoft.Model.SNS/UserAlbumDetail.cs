namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class UserAlbumDetail
    {
        private int _albumid;
        private int _albumuserid;
        private string _description;
        private int _id;
        private int _targetid;
        private int _type;

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

        public int AlbumUserId
        {
            get
            {
                return this._albumuserid;
            }
            set
            {
                this._albumuserid = value;
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

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
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

