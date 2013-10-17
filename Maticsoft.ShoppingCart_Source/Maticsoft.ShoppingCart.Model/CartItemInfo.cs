namespace Maticsoft.ShoppingCart.Model
{
    using System;

    [Serializable]
    public class CartItemInfo
    {
        private decimal _costprice;
        private int _itemid;
        private CartItemType _itemtype = CartItemType.None;
        private decimal _marketPrice;
        private string _name;
        private long _productId;
        private int _quantity;
        private decimal _sellprice;
        private string _sku;
        private string _thumbnailsurl;
        private int _userid;

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

        public int ItemId
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

        public CartItemType ItemType
        {
            get
            {
                return this._itemtype;
            }
            set
            {
                this._itemtype = value;
            }
        }

        public decimal MarketPrice
        {
            get
            {
                return this._marketPrice;
            }
            set
            {
                this._marketPrice = value;
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

        public long ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
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

