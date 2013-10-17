namespace Maticsoft.Model.Shop.Supplier
{
    using System;

    [Serializable]
    public class SupplierConfig
    {
        private string _description;
        private int _id;
        private string _keyname;
        private int? _keytype;
        private int _supplierid;
        private string _value;

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

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string KeyName
        {
            get
            {
                return this._keyname;
            }
            set
            {
                this._keyname = value;
            }
        }

        public int? KeyType
        {
            get
            {
                return this._keytype;
            }
            set
            {
                this._keytype = value;
            }
        }

        public int SupplierId
        {
            get
            {
                return this._supplierid;
            }
            set
            {
                this._supplierid = value;
            }
        }

        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }
}

