namespace Maticsoft.Model.Shop.Order
{
    using System;

    [Serializable]
    public class OrderRemark
    {
        private DateTime _createddate;
        private string _ordercode;
        private long _orderid;
        private string _remark;
        private long _remarkid;
        private int _userid;
        private string _username;

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

        public long RemarkId
        {
            get
            {
                return this._remarkid;
            }
            set
            {
                this._remarkid = value;
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

        public string UserName
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

