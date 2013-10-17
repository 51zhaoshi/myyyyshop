namespace Maticsoft.Model.Shop.Products
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class ProductType
    {
        private IList<int> _brandsTypes;
        private string _remark;
        private int _typeid;
        private string _typename;

        public IList<int> BrandsTypes
        {
            get
            {
                return this._brandsTypes;
            }
            set
            {
                this._brandsTypes = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public int TypeId
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this._typename;
            }
            set
            {
                this._typename = value;
            }
        }
    }
}

