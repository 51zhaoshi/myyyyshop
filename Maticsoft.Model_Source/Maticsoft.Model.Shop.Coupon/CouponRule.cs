namespace Maticsoft.Model.Shop.Coupon
{
    using System;

    [Serializable]
    public class CouponRule
    {
        private int _categoryid;
        private int _classid;
        private string _coupondesc;
        private decimal _couponprice;
        private DateTime _createdate;
        private int _createuserid;
        private DateTime _enddate;
        private string _imageurl;
        private int _ispwd;
        private int _isreuse;
        private decimal _limitprice;
        private string _name;
        private int _needpoint;
        private string _prename;
        private int _recommend;
        private int _ruleid;
        private int _sendcount;
        private DateTime _startdate;
        private int _status;
        private int _supplierid;

        public int CategoryId
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
            }
        }

        public int ClassId
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string CouponDesc
        {
            get
            {
                return this._coupondesc;
            }
            set
            {
                this._coupondesc = value;
            }
        }

        public decimal CouponPrice
        {
            get
            {
                return this._couponprice;
            }
            set
            {
                this._couponprice = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return this._createdate;
            }
            set
            {
                this._createdate = value;
            }
        }

        public int CreateUserId
        {
            get
            {
                return this._createuserid;
            }
            set
            {
                this._createuserid = value;
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

        public string ImageUrl
        {
            get
            {
                return this._imageurl;
            }
            set
            {
                this._imageurl = value;
            }
        }

        public int IsPwd
        {
            get
            {
                return this._ispwd;
            }
            set
            {
                this._ispwd = value;
            }
        }

        public int IsReuse
        {
            get
            {
                return this._isreuse;
            }
            set
            {
                this._isreuse = value;
            }
        }

        public decimal LimitPrice
        {
            get
            {
                return this._limitprice;
            }
            set
            {
                this._limitprice = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public int NeedPoint
        {
            get
            {
                return this._needpoint;
            }
            set
            {
                this._needpoint = value;
            }
        }

        public string PreName
        {
            get
            {
                return this._prename;
            }
            set
            {
                this._prename = value;
            }
        }

        public int Recommend
        {
            get
            {
                return this._recommend;
            }
            set
            {
                this._recommend = value;
            }
        }

        public int RuleId
        {
            get
            {
                return this._ruleid;
            }
            set
            {
                this._ruleid = value;
            }
        }

        public int SendCount
        {
            get
            {
                return this._sendcount;
            }
            set
            {
                this._sendcount = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this._startdate;
            }
            set
            {
                this._startdate = value;
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

        public int SupplierId
        {
            get
            {
                return this._supplierid;
            }
            set
            {
                this._supplierid = value;
            }
        }
    }
}

