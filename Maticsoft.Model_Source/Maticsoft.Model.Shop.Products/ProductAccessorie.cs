namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductAccessorie
    {
        private string _accessoriesname;
        private int _accessoriesvalueid;
        private decimal? _discountamount;
        private int? _discounttype;
        private int? _maxquantity;
        private int? _minquantity;
        private long _productid;
        private string _skuId;

        public string AccessoriesName
        {
            get
            {
                return this._accessoriesname;
            }
            set
            {
                this._accessoriesname = value;
            }
        }

        public int AccessoriesValueId
        {
            get
            {
                return this._accessoriesvalueid;
            }
            set
            {
                this._accessoriesvalueid = value;
            }
        }

        public decimal? DiscountAmount
        {
            get
            {
                return this._discountamount;
            }
            set
            {
                this._discountamount = value;
            }
        }

        public int? DiscountType
        {
            get
            {
                return this._discounttype;
            }
            set
            {
                this._discounttype = value;
            }
        }

        public int? MaxQuantity
        {
            get
            {
                return this._maxquantity;
            }
            set
            {
                this._maxquantity = value;
            }
        }

        public int? MinQuantity
        {
            get
            {
                return this._minquantity;
            }
            set
            {
                this._minquantity = value;
            }
        }

        public long ProductId
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

        public string SkuId
        {
            get
            {
                return this._skuId;
            }
            set
            {
                this._skuId = value;
            }
        }
    }
}

