namespace Maticsoft.Model.Shop.Inquiry
{
    using System;

    [Serializable]
    public class InquiryInfo
    {
        private string _address;
        private decimal _amount;
        private string _cellphone;
        private string _company;
        private DateTime _createddate;
        private string _email;
        private long _inquiryid;
        private string _leavemsg;
        private decimal? _marketprice;
        private long _parentid;
        private string _qq;
        private int _regionid;
        private string _remark;
        private string _replymsg;
        private int _status;
        private string _telephone;
        private DateTime? _updateddate;
        private int _updateduserid;
        private int _userid;
        private string _username;

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;
            }
        }

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

        public string CellPhone
        {
            get
            {
                return this._cellphone;
            }
            set
            {
                this._cellphone = value;
            }
        }

        public string Company
        {
            get
            {
                return this._company;
            }
            set
            {
                this._company = value;
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

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public long InquiryId
        {
            get
            {
                return this._inquiryid;
            }
            set
            {
                this._inquiryid = value;
            }
        }

        public string LeaveMsg
        {
            get
            {
                return this._leavemsg;
            }
            set
            {
                this._leavemsg = value;
            }
        }

        public decimal? MarketPrice
        {
            get
            {
                return this._marketprice;
            }
            set
            {
                this._marketprice = value;
            }
        }

        public long ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public string QQ
        {
            get
            {
                return this._qq;
            }
            set
            {
                this._qq = value;
            }
        }

        public int RegionId
        {
            get
            {
                return this._regionid;
            }
            set
            {
                this._regionid = value;
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

        public string ReplyMsg
        {
            get
            {
                return this._replymsg;
            }
            set
            {
                this._replymsg = value;
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

        public string Telephone
        {
            get
            {
                return this._telephone;
            }
            set
            {
                this._telephone = value;
            }
        }

        public DateTime? UpdatedDate
        {
            get
            {
                return this._updateddate;
            }
            set
            {
                this._updateddate = value;
            }
        }

        public int UpdatedUserId
        {
            get
            {
                return this._updateduserid;
            }
            set
            {
                this._updateduserid = value;
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

