namespace Maticsoft.Model.Shop.Shipping
{
    using System;

    [Serializable]
    public class ShippingType
    {
        private decimal? _addprice;
        private int? _addweight;
        private string _description;
        private int _displaysequence;
        private string _expresscompanyen;
        private string _expresscompanyname;
        private int _modeid;
        private string _name;
        private decimal _price;
        private int _weight;

        public decimal? AddPrice
        {
            get
            {
                return this._addprice;
            }
            set
            {
                this._addprice = value;
            }
        }

        public int? AddWeight
        {
            get
            {
                return this._addweight;
            }
            set
            {
                this._addweight = value;
            }
        }

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

        public int DisplaySequence
        {
            get
            {
                return this._displaysequence;
            }
            set
            {
                this._displaysequence = value;
            }
        }

        public string ExpressCompanyEn
        {
            get
            {
                return this._expresscompanyen;
            }
            set
            {
                this._expresscompanyen = value;
            }
        }

        public string ExpressCompanyName
        {
            get
            {
                return this._expresscompanyname;
            }
            set
            {
                this._expresscompanyname = value;
            }
        }

        public int ModeId
        {
            get
            {
                return this._modeid;
            }
            set
            {
                this._modeid = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public int Weight
        {
            get
            {
                return this._weight;
            }
            set
            {
                this._weight = value;
            }
        }
    }
}

