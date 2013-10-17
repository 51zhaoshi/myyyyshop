namespace Maticsoft.Model.Shop.Shipping
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ShippingAddress
    {
        private string _address;
        private string _celphone;
        private string _emailaddress;
        private int _regionid;
        private string _shipname;
        private int _shippingid;
        private string _telphone;
        private int _userid;
        private string _zipcode;

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;
            }
        }

        public string CelPhone
        {
            get
            {
                return this._celphone;
            }
            set
            {
                this._celphone = value;
            }
        }

        public string EmailAddress
        {
            get
            {
                return this._emailaddress;
            }
            set
            {
                this._emailaddress = value;
            }
        }

        public string RegionFullName { get; set; }

        public int RegionId
        {
            get
            {
                return this._regionid;
            }
            set
            {
                this._regionid = value;
            }
        }

        public string ShipName
        {
            get
            {
                return this._shipname;
            }
            set
            {
                this._shipname = value;
            }
        }

        public int ShippingId
        {
            get
            {
                return this._shippingid;
            }
            set
            {
                this._shippingid = value;
            }
        }

        public string TelPhone
        {
            get
            {
                return this._telphone;
            }
            set
            {
                this._telphone = value;
            }
        }

        public int UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public string Zipcode
        {
            get
            {
                return this._zipcode;
            }
            set
            {
                this._zipcode = value;
            }
        }
    }
}

