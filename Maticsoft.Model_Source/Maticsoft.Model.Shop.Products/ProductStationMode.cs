namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductStationMode
    {
        private int _displaysequence;
        private long _productid;
        private int _stationid;
        private int _type;

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

        public int StationId
        {
            get
            {
                return this._stationid;
            }
            set
            {
                this._stationid = value;
            }
        }

        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

