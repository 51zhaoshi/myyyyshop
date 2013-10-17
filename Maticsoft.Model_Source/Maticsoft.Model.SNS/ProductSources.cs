namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class ProductSources
    {
        private string _categorytags;
        private int _id;
        private string _imagestag;
        private string _pricetags;
        private int _status;
        private string _websitelogo;
        private string _websitename;
        private string _websiteurl;

        public string CategoryTags
        {
            get
            {
                return this._categorytags;
            }
            set
            {
                this._categorytags = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string ImagesTag
        {
            get
            {
                return this._imagestag;
            }
            set
            {
                this._imagestag = value;
            }
        }

        public string PriceTags
        {
            get
            {
                return this._pricetags;
            }
            set
            {
                this._pricetags = value;
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

        public string WebSiteLogo
        {
            get
            {
                return this._websitelogo;
            }
            set
            {
                this._websitelogo = value;
            }
        }

        public string WebSiteName
        {
            get
            {
                return this._websitename;
            }
            set
            {
                this._websitename = value;
            }
        }

        public string WebSiteUrl
        {
            get
            {
                return this._websiteurl;
            }
            set
            {
                this._websiteurl = value;
            }
        }
    }
}

