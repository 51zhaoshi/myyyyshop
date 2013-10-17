namespace Maticsoft.Model.Shop.Gift
{
    using System;

    [Serializable]
    public class GiftsCategory
    {
        private int _categoryid;
        private int _depth;
        private string _description;
        private int _displaysequence;
        private bool _haschildren;
        private string _name;
        private int? _parentcategoryid;
        private string _path;
        private string _theme;

        public int CategoryID
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

        public int DisplaySequence
        {
            get
            {
                return this._displaysequence;
            }
            set
            {
                this._displaysequence = value;
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

        public int? ParentCategoryId
        {
            get
            {
                return this._parentcategoryid;
            }
            set
            {
                this._parentcategoryid = value;
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
    }
}

