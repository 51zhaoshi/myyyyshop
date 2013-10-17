namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductImage
    {
        private string _imageurl;
        private long _productid;
        private int _productimageid;
        private string _thumbnailurl1;
        private string _thumbnailurl2;
        private string _thumbnailurl3;
        private string _thumbnailurl4;
        private string _thumbnailurl5;
        private string _thumbnailurl6;
        private string _thumbnailurl7;
        private string _thumbnailurl8;

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

        public int ProductImageId
        {
            get
            {
                return this._productimageid;
            }
            set
            {
                this._productimageid = value;
            }
        }

        public string ThumbnailUrl1
        {
            get
            {
                return this._thumbnailurl1;
            }
            set
            {
                this._thumbnailurl1 = value;
            }
        }

        public string ThumbnailUrl2
        {
            get
            {
                return this._thumbnailurl2;
            }
            set
            {
                this._thumbnailurl2 = value;
            }
        }

        public string ThumbnailUrl3
        {
            get
            {
                return this._thumbnailurl3;
            }
            set
            {
                this._thumbnailurl3 = value;
            }
        }

        public string ThumbnailUrl4
        {
            get
            {
                return this._thumbnailurl4;
            }
            set
            {
                this._thumbnailurl4 = value;
            }
        }

        public string ThumbnailUrl5
        {
            get
            {
                return this._thumbnailurl5;
            }
            set
            {
                this._thumbnailurl5 = value;
            }
        }

        public string ThumbnailUrl6
        {
            get
            {
                return this._thumbnailurl6;
            }
            set
            {
                this._thumbnailurl6 = value;
            }
        }

        public string ThumbnailUrl7
        {
            get
            {
                return this._thumbnailurl7;
            }
            set
            {
                this._thumbnailurl7 = value;
            }
        }

        public string ThumbnailUrl8
        {
            get
            {
                return this._thumbnailurl8;
            }
            set
            {
                this._thumbnailurl8 = value;
            }
        }
    }
}

