namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductAttribute
    {
        private long _attributeid;
        private long _productid;
        private int _valueid;

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

        public int ValueId
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
    }
}

