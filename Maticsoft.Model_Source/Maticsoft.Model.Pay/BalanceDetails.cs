namespace Maticsoft.Model.Pay
{
    using System;

    [Serializable]
    public class BalanceDetails
    {
        private decimal _balance;
        private decimal? _expenses;
        private decimal? _income;
        private long _journalnumber;
        private int? _payee;
        private int? _payer;
        private string _remark;
        private DateTime _tradedate;
        private int _tradetype;
        private int _userid;

        public decimal Balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                this._balance = value;
            }
        }

        public decimal? Expenses
        {
            get
            {
                return this._expenses;
            }
            set
            {
                this._expenses = value;
            }
        }

        public decimal? Income
        {
            get
            {
                return this._income;
            }
            set
            {
                this._income = value;
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

        public int? Payee
        {
            get
            {
                return this._payee;
            }
            set
            {
                this._payee = value;
            }
        }

        public int? Payer
        {
            get
            {
                return this._payer;
            }
            set
            {
                this._payer = value;
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

        public int TradeType
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

