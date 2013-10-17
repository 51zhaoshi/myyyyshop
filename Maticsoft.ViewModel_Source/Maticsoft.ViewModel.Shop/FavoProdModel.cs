namespace Maticsoft.ViewModel.Shop
{
    using System;

    public class FavoProdModel
    {
        private DateTime _createddate;
        private int _favoriteid;
        private long _productId;
        private string _productName;
        private int _saleStatus;
        private string _thumbnailUrl1;

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

        public int FavoriteId
        {
            get
            {
                return this._favoriteid;
            }
            set
            {
                this._favoriteid = value;
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

        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }

        public int SaleStatus
        {
            get
            {
                return this._saleStatus;
            }
            set
            {
                this._saleStatus = value;
            }
        }

        public string ThumbnailUrl1
        {
            get
            {
                return this._thumbnailUrl1;
            }
            set
            {
                this._thumbnailUrl1 = value;
            }
        }
    }
}

