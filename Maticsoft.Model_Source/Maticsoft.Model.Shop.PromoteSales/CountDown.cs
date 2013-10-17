namespace Maticsoft.Model.Shop.PromoteSales
{
    using System;

    [Serializable]
    public class CountDown
    {
        private int _countdownid;
        private string _description;
        private DateTime _enddate;
        private decimal _price;
        private long _productid;
        private int _sequence;
        private int _status;

        public int CountDownId
        {
            get
            {
                return this._countdownid;
            }
            set
            {
                this._countdownid = value;
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

        public DateTime EndDate
        {
            get
            {
                return this._enddate;
            }
            set
            {
                this._enddate = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public long ProductId
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

