namespace Maticsoft.Model.Shop.Order
{
    using System;

    [Serializable]
    public class OrderAction
    {
        private string _actioncode;
        private DateTime _actiondate;
        private long _actionid;
        private string _ordercode;
        private long _orderid;
        private string _remark;
        private int _userid;
        private string _username;

        public string ActionCode
        {
            get
            {
                return this._actioncode;
            }
            set
            {
                this._actioncode = value;
            }
        }

        public DateTime ActionDate
        {
            get
            {
                return this._actiondate;
            }
            set
            {
                this._actiondate = value;
            }
        }

        public long ActionId
        {
            get
            {
                return this._actionid;
            }
            set
            {
                this._actionid = value;
            }
        }

        public string OrderCode
        {
            get
            {
                return this._ordercode;
            }
            set
            {
                this._ordercode = value;
            }
        }

        public long OrderId
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

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
    }
}

