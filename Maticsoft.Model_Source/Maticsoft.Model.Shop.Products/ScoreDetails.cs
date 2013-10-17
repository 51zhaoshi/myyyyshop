namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ScoreDetails
    {
        private DateTime? _createddate;
        private long? _productid;
        private int _reviewid;
        private int? _score;
        private int _scoreid;
        private int? _userid;

        public DateTime? CreatedDate
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

        public long? ProductId
        {
            get
            {
                return this._productid;
            }
            set
            {
                this._productid = value;
            }
        }

        public int ReviewId
        {
            get
            {
                return this._reviewid;
            }
            set
            {
                this._reviewid = value;
            }
        }

        public int? Score
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

        public int ScoreId
        {
            get
            {
                return this._scoreid;
            }
            set
            {
                this._scoreid = value;
            }
        }

        public int? UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }
    }
}

