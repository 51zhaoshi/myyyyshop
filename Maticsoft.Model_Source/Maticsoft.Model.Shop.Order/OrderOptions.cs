namespace Maticsoft.Model.Shop.Order
{
    using System;

    [Serializable]
    public class OrderOptions
    {
        private decimal _adjustedprice;
        private string _customerdescription;
        private string _customertitle;
        private string _itemdescription;
        private string _listdescription;
        private int _lookupitemid;
        private int _lookuplistid;
        private string _ordercode;
        private long _orderid;

        public decimal AdjustedPrice
        {
            get
            {
                return this._adjustedprice;
            }
            set
            {
                this._adjustedprice = value;
            }
        }

        public string CustomerDescription
        {
            get
            {
                return this._customerdescription;
            }
            set
            {
                this._customerdescription = value;
            }
        }

        public string CustomerTitle
        {
            get
            {
                return this._customertitle;
            }
            set
            {
                this._customertitle = value;
            }
        }

        public string ItemDescription
        {
            get
            {
                return this._itemdescription;
            }
            set
            {
                this._itemdescription = value;
            }
        }

        public string ListDescription
        {
            get
            {
                return this._listdescription;
            }
            set
            {
                this._listdescription = value;
            }
        }

        public int LookupItemId
        {
            get
            {
                return this._lookupitemid;
            }
            set
            {
                this._lookupitemid = value;
            }
        }

        public int LookupListId
        {
            get
            {
                return this._lookuplistid;
            }
            set
            {
                this._lookuplistid = value;
            }
        }

        public string OrderCode
        {
            get
            {
                return this._ordercode;
            }
            set
            {
                this._ordercode = value;
            }
        }

        public long OrderId
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }
    }
}

