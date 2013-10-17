namespace Maticsoft.Model.Shop.Inquiry
{
    using System;

    [Serializable]
    public class InquiryItem
    {
        private decimal _adjustedprice;
        private string _attribute;
        private decimal _costprice;
        private decimal? _deduct;
        private string _description;
        private long _inquiryid;
        private long _itemid;
        private string _name;
        private int _points;
        private string _productcode;
        private int? _productlineid;
        private int _quantity;
        private string _remark;
        private decimal _sellprice;
        private string _sku;
        private int? _supplierid;
        private string _suppliername;
        private long _targetid;
        private string _thumbnailsurl;
        private int _type;
        private int _weight;

        public decimal AdjustedPrice
        {
            get
            {
                return this._adjustedprice;
            }
            set
            {
                this._adjustedprice = value;
            }
        }

        public string Attribute
        {
            get
            {
                return this._attribute;
            }
            set
            {
                this._attribute = value;
            }
        }

        public decimal CostPrice
        {
            get
            {
                return this._costprice;
            }
            set
            {
                this._costprice = value;
            }
        }

        public decimal? Deduct
        {
            get
            {
                return this._deduct;
            }
            set
            {
                this._deduct = value;
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

        public long ItemId
        {
            get
            {
                return this._itemid;
            }
            set
            {
                this._itemid = value;
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

        public int Points
        {
            get
            {
                return this._points;
            }
            set
            {
                this._points = value;
            }
        }

        public string ProductCode
        {
            get
            {
                return this._productcode;
            }
            set
            {
                this._productcode = value;
            }
        }

        public int? ProductLineId
        {
            get
            {
                return this._productlineid;
            }
            set
            {
                this._productlineid = value;
            }
        }

        public int Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
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

        public decimal SellPrice
        {
            get
            {
                return this._sellprice;
            }
            set
            {
                this._sellprice = value;
            }
        }

        public string SKU
        {
            get
            {
                return this._sku;
            }
            set
            {
                this._sku = value;
            }
        }

        public int? SupplierId
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

        public string SupplierName
        {
            get
            {
                return this._suppliername;
            }
            set
            {
                this._suppliername = value;
            }
        }

        public long TargetId
        {
            get
            {
                return this._targetid;
            }
            set
            {
                this._targetid = value;
            }
        }

        public string ThumbnailsUrl
        {
            get
            {
                return this._thumbnailsurl;
            }
            set
            {
                this._thumbnailsurl = value;
            }
        }

        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public int Weight
        {
            get
            {
                return this._weight;
            }
            set
            {
                this._weight = value;
            }
        }
    }
}

