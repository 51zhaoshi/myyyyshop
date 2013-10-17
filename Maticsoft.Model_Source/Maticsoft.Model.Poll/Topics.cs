namespace Maticsoft.Model.Poll
{
    using System;

    [Serializable]
    public class Topics
    {
        private int? _formid;
        private int _id;
        private int _rowNum;
        private string _title;
        private int? _type;

        public int? FormID
        {
            get
            {
                return this._formid;
            }
            set
            {
                this._formid = value;
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

        public int RowNum
        {
            get
            {
                return this._rowNum;
            }
            set
            {
                this._rowNum = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public int? Type
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

