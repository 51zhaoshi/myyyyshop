namespace Maticsoft.Model.Shop.Supplier
{
    using System;

    [Serializable]
    public class SuppProductCategories
    {
        private int _categoryid;
        private string _categorypath;
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
                return this._categorypath;
            }
            set
            {
                this._categorypath = value;
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

