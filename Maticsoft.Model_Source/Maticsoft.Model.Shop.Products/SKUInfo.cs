namespace Maticsoft.Model.Shop.Products
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class SKUInfo
    {
        private int _alertstock;
        private long _attributeId;
        private decimal? _costprice = 0;
        private long _productid;
        private decimal _saleprice;
        private string _sku;
        private long _skuid;
        private long _specId;
        private int _stock;
        private bool _upselling;
        private long _valueId;
        private string _valuesStr;
        private int? _weight;
        private string productImageUrl;
        private string productName;
        private string productThumbnailUrl;
        private List<SKUItem> skuItemList = new List<SKUItem>();

        public int AlertStock
        {
            get
            {
                return this._alertstock;
            }
            set
            {
                this._alertstock = value;
            }
        }

        public long AttributeId
        {
            get
            {
                return this._attributeId;
            }
            set
            {
                this._attributeId = value;
            }
        }

        public decimal? CostPrice
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

        public string ProductImageUrl
        {
            get
            {
                return this.productImageUrl;
            }
            set
            {
                this.productImageUrl = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set
            {
                this.productName = value;
            }
        }

        public string ProductThumbnailUrl
        {
            get
            {
                return this.productThumbnailUrl;
            }
            set
            {
                this.productThumbnailUrl = value;
            }
        }

        public decimal SalePrice
        {
            get
            {
                return this._saleprice;
            }
            set
            {
                this._saleprice = value;
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

        public long SkuId
        {
            get
            {
                return this._skuid;
            }
            set
            {
                this._skuid = value;
            }
        }

        public List<SKUItem> SkuItems
        {
            get
            {
                return this.skuItemList;
            }
            set
            {
                this.skuItemList = value;
            }
        }

        public long SpecId
        {
            get
            {
                return this._specId;
            }
            set
            {
                this._specId = value;
            }
        }

        public int Stock
        {
            get
            {
                return this._stock;
            }
            set
            {
                this._stock = value;
            }
        }

        public bool Upselling
        {
            get
            {
                return this._upselling;
            }
            set
            {
                this._upselling = value;
            }
        }

        public long ValueId
        {
            get
            {
                return this._valueId;
            }
            set
            {
                this._valueId = value;
            }
        }

        public string ValuesStr
        {
            get
            {
                return this._valuesStr;
            }
            set
            {
                this._valuesStr = value;
            }
        }

        public int? Weight
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

