namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class AlbumType
    {
        private int? _albumscount = 0;
        private int _id;
        private bool _ismenu;
        private bool _menuisshow;
        private int _menusequence;
        private string _remark;
        private int _status;
        private string _typename;

        public int? AlbumsCount
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

        public bool IsMenu
        {
            get
            {
                return this._ismenu;
            }
            set
            {
                this._ismenu = value;
            }
        }

        public bool MenuIsShow
        {
            get
            {
                return this._menuisshow;
            }
            set
            {
                this._menuisshow = value;
            }
        }

        public int MenuSequence
        {
            get
            {
                return this._menusequence;
            }
            set
            {
                this._menusequence = value;
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

        public string TypeName
        {
            get
            {
                return this._typename;
            }
            set
            {
                this._typename = value;
            }
        }
    }
}

