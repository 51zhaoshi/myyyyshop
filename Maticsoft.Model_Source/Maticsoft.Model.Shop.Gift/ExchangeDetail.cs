namespace Maticsoft.Model.Shop.Gift
{
    using System;

    [Serializable]
    public class ExchangeDetail
    {
        private int _costscore;
        private DateTime _createddate;
        private string _description;
        private int _exchangedetailid;
        private int _giftid;
        private string _giftname;
        private int? _orderid;
        private int _status;
        private int _userid;

        public int CostScore
        {
            get
            {
                return this._costscore;
            }
            set
            {
                this._costscore = value;
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

        public int ExchangeDetailID
        {
            get
            {
                return this._exchangedetailid;
            }
            set
            {
                this._exchangedetailid = value;
            }
        }

        public int GiftID
        {
            get
            {
                return this._giftid;
            }
            set
            {
                this._giftid = value;
            }
        }

        public string GiftName
        {
            get
            {
                return this._giftname;
            }
            set
            {
                this._giftname = value;
            }
        }

        public int? OrderID
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
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

        public int UserID
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

