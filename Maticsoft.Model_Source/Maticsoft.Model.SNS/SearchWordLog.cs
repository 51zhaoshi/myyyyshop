namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class SearchWordLog
    {
        private DateTime _createddate;
        private string _creatednickname;
        private int _createduserid;
        private int _id;
        private string _searchword;
        private int _status;

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

        public string CreatedNickName
        {
            get
            {
                return this._creatednickname;
            }
            set
            {
                this._creatednickname = value;
            }
        }

        public int CreatedUserId
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

        public string SearchWord
        {
            get
            {
                return this._searchword;
            }
            set
            {
                this._searchword = value;
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
    }
}

