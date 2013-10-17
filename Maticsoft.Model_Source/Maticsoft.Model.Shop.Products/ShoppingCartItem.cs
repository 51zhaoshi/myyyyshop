namespace Maticsoft.Model.Shop.Products
{
    using Maticsoft.ShoppingCart.Model;
    using System;

    [Serializable]
    public class ShoppingCartItem : CartItemInfo
    {
        private decimal _adjustedprice;
        private string _attributes;
        private decimal? _deduct;
        private string _description;
        private int _points;
        private int? _productlineid;
        private string _saleDes;
        private string _skuImageUrl;
        private string[] _skuValues;
        private int? _supplierid;
        private string _suppliername;
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

        public string Attributes
        {
            get
            {
                return this._attributes;
            }
            set
            {
                this._attributes = value;
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

        public string SaleDes
        {
            get
            {
                return this._saleDes;
            }
            set
            {
                this._saleDes = value;
            }
        }

        public string SkuImageUrl
        {
            get
            {
                return this._skuImageUrl;
            }
            set
            {
                this._skuImageUrl = value;
            }
        }

        public string[] SkuValues
        {
            get
            {
                return this._skuValues;
            }
            set
            {
                this._skuValues = value;
            }
        }

        public decimal SubTotal
        {
            get
            {
                return (base.Quantity * base.SellPrice);
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

