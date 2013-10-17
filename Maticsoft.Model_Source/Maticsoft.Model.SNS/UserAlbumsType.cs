namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class UserAlbumsType
    {
        private int _albumsid;
        private int? _albumsuserid;
        private int _typeid;

        public int AlbumsID
        {
            get
            {
                return this._albumsid;
            }
            set
            {
                this._albumsid = value;
            }
        }

        public int? AlbumsUserID
        {
            get
            {
                return this._albumsuserid;
            }
            set
            {
                this._albumsuserid = value;
            }
        }

        public int TypeID
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }
    }
}

