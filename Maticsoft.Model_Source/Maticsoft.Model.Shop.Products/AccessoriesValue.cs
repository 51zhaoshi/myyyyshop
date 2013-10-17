namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class AccessoriesValue
    {
        private int _accessoriesvalueid;
        private int _productaccessoriesid;
        private string _productaccessoriessku;

        public int AccessoriesValueId
        {
            get
            {
                return this._accessoriesvalueid;
            }
            set
            {
                this._accessoriesvalueid = value;
            }
        }

        public int ProductAccessoriesId
        {
            get
            {
                return this._productaccessoriesid;
            }
            set
            {
                this._productaccessoriesid = value;
            }
        }

        public string ProductAccessoriesSKU
        {
            get
            {
                return this._productaccessoriessku;
            }
            set
            {
                this._productaccessoriessku = value;
            }
        }
    }
}

