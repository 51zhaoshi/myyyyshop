namespace Maticsoft.Model.Shop.Package
{
    using System;

    [Serializable]
    public class ProductPackage
    {
        private int _packageid;
        private long _productid;

        public int PackageId
        {
            get
            {
                return this._packageid;
            }
            set
            {
                this._packageid = value;
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

