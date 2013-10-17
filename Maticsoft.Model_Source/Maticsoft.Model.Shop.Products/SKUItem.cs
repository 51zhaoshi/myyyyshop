namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class SKUItem
    {
        private int _abDisplaysequence;
        private long _attributeid;
        private string _attributename;
        private int _AV_displaysequence;
        private string _AV_imageurl;
        private string _AV_valuestr;
        private string _imageUrl;
        private long _productId;
        private long _skuid;
        private long _specId;
        private int _usagemode;
        private bool _useattributeimage;
        private bool _userDefinedPic;
        private long _valueid;
        private string _valueStr;

        public int AB_DisplaySequence
        {
            get
            {
                return this._abDisplaysequence;
            }
            set
            {
                this._abDisplaysequence = value;
            }
        }

        public long AttributeId
        {
            get
            {
                return this._attributeid;
            }
            set
            {
                this._attributeid = value;
            }
        }

        public string AttributeName
        {
            get
            {
                return this._attributename;
            }
            set
            {
                this._attributename = value;
            }
        }

        public int AV_DisplaySequence
        {
            get
            {
                return this._AV_displaysequence;
            }
            set
            {
                this._AV_displaysequence = value;
            }
        }

        public string AV_ImageUrl
        {
            get
            {
                return this._AV_imageurl;
            }
            set
            {
                this._AV_imageurl = value;
            }
        }

        public string AV_ValueStr
        {
            get
            {
                return this._AV_valuestr;
            }
            set
            {
                this._AV_valuestr = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return this._imageUrl;
            }
            set
            {
                this._imageUrl = value;
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

        public int UsageMode
        {
            get
            {
                return this._usagemode;
            }
            set
            {
                this._usagemode = value;
            }
        }

        public bool UseAttributeImage
        {
            get
            {
                return this._useattributeimage;
            }
            set
            {
                this._useattributeimage = value;
            }
        }

        public bool UserDefinedPic
        {
            get
            {
                return this._userDefinedPic;
            }
            set
            {
                this._userDefinedPic = value;
            }
        }

        public long ValueId
        {
            get
            {
                return this._valueid;
            }
            set
            {
                this._valueid = value;
            }
        }

        public string ValueStr
        {
            get
            {
                return this._valueStr;
            }
            set
            {
                this._valueStr = value;
            }
        }
    }
}

