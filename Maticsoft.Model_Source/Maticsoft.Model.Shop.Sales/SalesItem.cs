namespace Maticsoft.Model.Shop.Sales
{
    using System;

    [Serializable]
    public class SalesItem
    {
        private int _itemid;
        private int _itemtype;
        private int _ratevalue;
        private int _ruleid;
        private int _unitvalue;

        public int ItemId
        {
            get
            {
                return this._itemid;
            }
            set
            {
                this._itemid = value;
            }
        }

        public int ItemType
        {
            get
            {
                return this._itemtype;
            }
            set
            {
                this._itemtype = value;
            }
        }

        public int RateValue
        {
            get
            {
                return this._ratevalue;
            }
            set
            {
                this._ratevalue = value;
            }
        }

        public int RuleId
        {
            get
            {
                return this._ruleid;
            }
            set
            {
                this._ruleid = value;
            }
        }

        public int UnitValue
        {
            get
            {
                return this._unitvalue;
            }
            set
            {
                this._unitvalue = value;
            }
        }
    }
}

