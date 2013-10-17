namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductReviews
    {
        private string _attribute;
        private DateTime _createddate;
        private string _imagesnames;
        private string _imagespath;
        private long _orderid;
        private int _parentid;
        private long _productid;
        private int _reviewid;
        private string _reviewtext;
        private string _sku;
        private int _status;
        private string _useremail;
        private int _userid;
        private string _username;

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

        public string ImagesNames
        {
            get
            {
                return this._imagesnames;
            }
            set
            {
                this._imagesnames = value;
            }
        }

        public string ImagesPath
        {
            get
            {
                return this._imagespath;
            }
            set
            {
                this._imagespath = value;
            }
        }

        public long OrderId
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }

        public int ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
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

        public int ReviewId
        {
            get
            {
                return this._reviewid;
            }
            set
            {
                this._reviewid = value;
            }
        }

        public string ReviewText
        {
            get
            {
                return this._reviewtext;
            }
            set
            {
                this._reviewtext = value;
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

        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
    }
}

