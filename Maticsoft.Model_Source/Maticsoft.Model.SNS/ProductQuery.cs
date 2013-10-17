namespace Maticsoft.Model.SNS
{
    using System;

    public class ProductQuery
    {
        private int? _categoryid;
        private string _color;
        private int? _createuserid;
        private int? _isrecomend;
        private bool _istopcategory;
        private decimal? _maxprice;
        private decimal? _minprice;
        private string _order;
        private int? _ownerproductid;
        private int? _productid;
        private int? _productsourceid;
        private int _querytype;
        private string _sharedescription;
        private int? _status;
        private string _tags;
        private string keywords;

        public int? CategoryID
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

        public string Color
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
            }
        }

        public int? CreateUserID
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

        public int? IsRecomend
        {
            get
            {
                return this._isrecomend;
            }
            set
            {
                this._isrecomend = value;
            }
        }

        public bool IsTopCategory
        {
            get
            {
                return this._istopcategory;
            }
            set
            {
                this._istopcategory = value;
            }
        }

        public string Keywords
        {
            get
            {
                return this.keywords;
            }
            set
            {
                this.keywords = value;
            }
        }

        public decimal? MaxPrice
        {
            get
            {
                return this._maxprice;
            }
            set
            {
                this._maxprice = value;
            }
        }

        public decimal? MinPrice
        {
            get
            {
                return this._minprice;
            }
            set
            {
                this._minprice = value;
            }
        }

        public string Order
        {
            get
            {
                return this._order;
            }
            set
            {
                this._order = value;
            }
        }

        public int? OwnerProductId
        {
            get
            {
                return this._ownerproductid;
            }
            set
            {
                this._ownerproductid = value;
            }
        }

        public int? ProductID
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

        public int? ProductSourceID
        {
            get
            {
                return this._productsourceid;
            }
            set
            {
                this._productsourceid = value;
            }
        }

        public int QueryType
        {
            get
            {
                return this._querytype;
            }
            set
            {
                this._querytype = value;
            }
        }

        public string ShareDescription
        {
            get
            {
                return this._sharedescription;
            }
            set
            {
                this._sharedescription = value;
            }
        }

        public int? Status
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

        public string Tags
        {
            get
            {
                return this._tags;
            }
            set
            {
                this._tags = value;
            }
        }
    }
}

