namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class HotWords
    {
        private DateTime _createddate;
        private int _id;
        private bool _isrecommend;
        private string _keyword;
        private int _sequence;
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

        public bool IsRecommend
        {
            get
            {
                return this._isrecommend;
            }
            set
            {
                this._isrecommend = value;
            }
        }

        public string KeyWord
        {
            get
            {
                return this._keyword;
            }
            set
            {
                this._keyword = value;
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

