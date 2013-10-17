namespace Maticsoft.Model.Pay
{
    using System;

    [Serializable]
    public class RechargeRequest
    {
        private string _paymentgateway;
        private int _paymenttypeid;
        private decimal _rechargeblance;
        private long _rechargeid;
        private int? _sellerid;
        private int _status;
        private DateTime _tradedate;
        private int? _tradetype;
        private int _userid;

        public string PaymentGateway
        {
            get
            {
                return this._paymentgateway;
            }
            set
            {
                this._paymentgateway = value;
            }
        }

        public int PaymentTypeId
        {
            get
            {
                return this._paymenttypeid;
            }
            set
            {
                this._paymenttypeid = value;
            }
        }

        public decimal RechargeBlance
        {
            get
            {
                return this._rechargeblance;
            }
            set
            {
                this._rechargeblance = value;
            }
        }

        public long RechargeId
        {
            get
            {
                return this._rechargeid;
            }
            set
            {
                this._rechargeid = value;
            }
        }

        public int? SellerId
        {
            get
            {
                return this._sellerid;
            }
            set
            {
                this._sellerid = value;
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

        public DateTime TradeDate
        {
            get
            {
                return this._tradedate;
            }
            set
            {
                this._tradedate = value;
            }
        }

        public int? Tradetype
        {
            get
            {
                return this._tradetype;
            }
            set
            {
                this._tradetype = value;
            }
        }

        public int UserId
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

