namespace Maticsoft.Model.Shop.Coupon
{
    using System;

    [Serializable]
    public class CouponHistory
    {
        private int _categoryid;
        private int _classid;
        private string _couponcode;
        private string _couponname;
        private decimal _couponprice;
        private string _couponpwd;
        private DateTime _enddate;
        private DateTime _generatetime;
        private int _ispwd;
        private int _isreuse;
        private decimal _limitprice;
        private int _needpoint;
        private int _ruleid;
        private DateTime _startdate;
        private int _status;
        private int _supplierid;
        private DateTime? _useddate;
        private string _useremail;
        private int _userid;

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

        public string CouponCode
        {
            get
            {
                return this._couponcode;
            }
            set
            {
                this._couponcode = value;
            }
        }

        public string CouponName
        {
            get
            {
                return this._couponname;
            }
            set
            {
                this._couponname = value;
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

        public string CouponPwd
        {
            get
            {
                return this._couponpwd;
            }
            set
            {
                this._couponpwd = value;
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

        public DateTime GenerateTime
        {
            get
            {
                return this._generatetime;
            }
            set
            {
                this._generatetime = value;
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

        public DateTime? UsedDate
        {
            get
            {
                return this._useddate;
            }
            set
            {
                this._useddate = value;
            }
        }

        public string UserEmail
        {
            get
            {
                return this._useremail;
            }
            set
            {
                this._useremail = value;
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

