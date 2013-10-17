namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class SKURelation
    {
        private long _productid;
        private long _skuid;
        private long _specid;

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
                return this._specid;
            }
            set
            {
                this._specid = value;
            }
        }
    }
}

