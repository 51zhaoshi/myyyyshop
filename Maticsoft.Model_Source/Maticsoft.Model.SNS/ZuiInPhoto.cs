namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class ZuiInPhoto
    {
        private int _albumscount;
        private int _fanscount;
        private string _nickname;
        private int _photoid;
        private string _photourl;
        private int _userid;
        public string StaticUrl = "";

        public int AlbumsCount
        {
            get
            {
                return this._albumscount;
            }
            set
            {
                this._albumscount = value;
            }
        }

        public int FansCount
        {
            get
            {
                return this._fanscount;
            }
            set
            {
                this._fanscount = value;
            }
        }

        public string NickName
        {
            get
            {
                return this._nickname;
            }
            set
            {
                this._nickname = value;
            }
        }

        public int PhotoId
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

