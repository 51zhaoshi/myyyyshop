namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class UserFavAlbum
    {
        private int _albumid;
        private DateTime _createddate;
        private int _id;
        private string _tags;
        private int _userid;

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

        public int UserID
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

