namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductConsultsType
    {
        private DateTime? _createddate;
        private bool _isactive = true;
        private int _typeid;
        private string _typename;

        public DateTime? CreatedDate
        {
            get
            {
                return this._createddate;
            }
            set
            {
                this._createddate = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this._isactive;
            }
            set
            {
                this._isactive = value;
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

