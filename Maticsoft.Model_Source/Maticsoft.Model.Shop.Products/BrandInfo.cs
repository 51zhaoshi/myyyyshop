namespace Maticsoft.Model.Shop.Products
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class BrandInfo
    {
        private int _brandid;
        private string _brandname;
        private string _brandspell;
        private string _companyurl;
        private string _description;
        private int _displaysequence;
        private string _logo;
        private string _meta_description;
        private string _meta_keywords;
        private IList<int> _productTypeIdOrBrandsId;
        private string _theme;
        private IList<int> productTypes;

        public int BrandId
        {
            get
            {
                return this._brandid;
            }
            set
            {
                this._brandid = value;
            }
        }

        public string BrandName
        {
            get
            {
                return this._brandname;
            }
            set
            {
                this._brandname = value;
            }
        }

        public string BrandSpell
        {
            get
            {
                return this._brandspell;
            }
            set
            {
                this._brandspell = value;
            }
        }

        public string CompanyUrl
        {
            get
            {
                return this._companyurl;
            }
            set
            {
                this._companyurl = value;
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

        public int DisplaySequence
        {
            get
            {
                return this._displaysequence;
            }
            set
            {
                this._displaysequence = value;
            }
        }

        public string Logo
        {
            get
            {
                return this._logo;
            }
            set
            {
                this._logo = value;
            }
        }

        public string Meta_Description
        {
            get
            {
                return this._meta_description;
            }
            set
            {
                this._meta_description = value;
            }
        }

        public string Meta_Keywords
        {
            get
            {
                return this._meta_keywords;
            }
            set
            {
                this._meta_keywords = value;
            }
        }

        public IList<int> ProductTypeIdOrBrandsId
        {
            get
            {
                return this._productTypeIdOrBrandsId;
            }
            set
            {
                this._productTypeIdOrBrandsId = value;
            }
        }

        public IList<int> ProductTypes
        {
            get
            {
                if (this.productTypes == null)
                {
                    this.productTypes = new List<int>();
                }
                return this.productTypes;
            }
            set
            {
                this.productTypes = value;
            }
        }

        public string Theme
        {
            get
            {
                return this._theme;
            }
            set
            {
                this._theme = value;
            }
        }
    }
}

