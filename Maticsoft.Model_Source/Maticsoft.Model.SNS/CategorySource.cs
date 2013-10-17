namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class CategorySource
    {
        private int _categoryid;
        private DateTime _createddate;
        private int _createduserid;
        private int _depth;
        private string _description;
        private bool _haschildren;
        private bool _ismenu;
        private bool _menuisshow;
        private int _menusequence;
        private string _name;
        private int _parentid;
        private string _path;
        private int _sequence;
        private int? _snscategoryid;
        private int _sourceid;
        private int _status;
        private int _type;

        public int CategoryId
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
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

        public bool HasChildren
        {
            get
            {
                return this._haschildren;
            }
            set
            {
                this._haschildren = value;
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

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public int ParentID
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

        public int? SnsCategoryId
        {
            get
            {
                return this._snscategoryid;
            }
            set
            {
                this._snscategoryid = value;
            }
        }

        public int SourceId
        {
            get
            {
                return this._sourceid;
            }
            set
            {
                this._sourceid = value;
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

