namespace Maticsoft.Model.Pay
{
    using System;

    [Serializable]
    public class BalanceDrawRequest
    {
        private decimal _amount;
        private string _bankcard;
        private string _bankname;
        private int _cardtypeid;
        private long _journalnumber;
        private string _remark;
        private int _requeststatus = 1;
        private DateTime _requesttime;
        private string _truename;
        private int _userid;

        public decimal Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public string BankCard
        {
            get
            {
                return this._bankcard;
            }
            set
            {
                this._bankcard = value;
            }
        }

        public string BankName
        {
            get
            {
                return this._bankname;
            }
            set
            {
                this._bankname = value;
            }
        }

        public int CardTypeID
        {
            get
            {
                return this._cardtypeid;
            }
            set
            {
                this._cardtypeid = value;
            }
        }

        public long JournalNumber
        {
            get
            {
                return this._journalnumber;
            }
            set
            {
                this._journalnumber = value;
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

        public int RequestStatus
        {
            get
            {
                return this._requeststatus;
            }
            set
            {
                this._requeststatus = value;
            }
        }

        public DateTime RequestTime
        {
            get
            {
                return this._requesttime;
            }
            set
            {
                this._requesttime = value;
            }
        }

        public string TrueName
        {
            get
            {
                return this._truename;
            }
            set
            {
                this._truename = value;
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

