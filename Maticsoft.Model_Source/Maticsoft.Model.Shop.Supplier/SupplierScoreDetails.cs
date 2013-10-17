namespace Maticsoft.Model.Shop.Supplier
{
    using System;

    [Serializable]
    public class SupplierScoreDetails
    {
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private int _id;
        private string _remark;
        private decimal _score;
        private int _scoretype;
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

        public decimal Score
        {
            get
            {
                return this._score;
            }
            set
            {
                this._score = value;
            }
        }

        public int ScoreType
        {
            get
            {
                return this._scoretype;
            }
            set
            {
                this._scoretype = value;
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

