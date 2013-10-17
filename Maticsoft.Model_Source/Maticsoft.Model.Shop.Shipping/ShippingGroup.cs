namespace Maticsoft.Model.Shop.Shipping
{
    using System;

    [Serializable]
    public class ShippingGroup
    {
        private decimal? _addprice;
        private int _groupid;
        private int _modeid;
        private decimal _price;

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

        public int GroupId
        {
            get
            {
                return this._groupid;
            }
            set
            {
                this._groupid = value;
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
    }
}

