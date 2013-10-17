namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductCategories
    {
        private int _categoryid;
        private string _categoryPath;
        private long _productid;

        public int CategoryId
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
            }
        }

        public string CategoryPath
        {
            get
            {
                return this._categoryPath;
            }
            set
            {
                this._categoryPath = value;
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
    }
}

