namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class RelatedProduct
    {
        private long _productid;
        private long _relatedid;

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

        public long RelatedId
        {
            get
            {
                return this._relatedid;
            }
            set
            {
                this._relatedid = value;
            }
        }
    }
}

