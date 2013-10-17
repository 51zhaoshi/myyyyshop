namespace Maticsoft.Model.Shop.Shipping
{
    using System;

    [Serializable]
    public class ShippingRegions
    {
        private int _groupid;
        private int _modeid;
        private int _regionid;

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
    }
}

