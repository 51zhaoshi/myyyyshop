namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class PhotoClass
    {
        private int _classid;
        private string _classname;
        private int? _depth;
        private int? _parentid = 0;
        private string _path;
        private int? _sequence;

        public int ClassID
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string ClassName
        {
            get
            {
                return this._classname;
            }
            set
            {
                this._classname = value;
            }
        }

        public int? Depth
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

        public int? ParentId
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

        public int? Sequence
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
    }
}

