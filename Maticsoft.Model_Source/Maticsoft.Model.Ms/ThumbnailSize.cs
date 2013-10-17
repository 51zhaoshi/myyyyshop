namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class ThumbnailSize
    {
        private string _cloudsizename;
        private int _cloudtype;
        private bool _iswatermark;
        private string _remark;
        private string _theme;
        private int _thumheight;
        private int _thummode;
        private string _thumname;
        private int _thumwidth;
        private int _type;

        public string CloudSizeName
        {
            get
            {
                return this._cloudsizename;
            }
            set
            {
                this._cloudsizename = value;
            }
        }

        public int CloudType
        {
            get
            {
                return this._cloudtype;
            }
            set
            {
                this._cloudtype = value;
            }
        }

        public bool IsWatermark
        {
            get
            {
                return this._iswatermark;
            }
            set
            {
                this._iswatermark = value;
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

        public string Theme
        {
            get
            {
                return this._theme;
            }
            set
            {
                this._theme = value;
            }
        }

        public int ThumHeight
        {
            get
            {
                return this._thumheight;
            }
            set
            {
                this._thumheight = value;
            }
        }

        public int ThumMode
        {
            get
            {
                return this._thummode;
            }
            set
            {
                this._thummode = value;
            }
        }

        public string ThumName
        {
            get
            {
                return this._thumname;
            }
            set
            {
                this._thumname = value;
            }
        }

        public int ThumWidth
        {
            get
            {
                return this._thumwidth;
            }
            set
            {
                this._thumwidth = value;
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

