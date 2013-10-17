namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class VideoClass
    {
        private int _depth;
        private int? _parentid;
        private string _path;
        private int _sequence;
        private int _videoclassid;
        private string _videoclassname;

        public int Depth
        {
            get
            {
                return this._depth;
            }
            set
            {
                this._depth = value;
            }
        }

        public int? ParentID
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
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

        public int VideoClassID
        {
            get
            {
                return this._videoclassid;
            }
            set
            {
                this._videoclassid = value;
            }
        }

        public string VideoClassName
        {
            get
            {
                return this._videoclassname;
            }
            set
            {
                this._videoclassname = value;
            }
        }
    }
}

