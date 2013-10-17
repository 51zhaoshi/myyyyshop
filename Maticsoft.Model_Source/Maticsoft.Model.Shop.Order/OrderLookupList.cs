namespace Maticsoft.Model.Shop.Order
{
    using System;

    [Serializable]
    public class OrderLookupList
    {
        private string _description;
        private int _lookuplistid;
        private string _name;
        private int _selectmode;

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

        public int SelectMode
        {
            get
            {
                return this._selectmode;
            }
            set
            {
                this._selectmode = value;
            }
        }
    }
}

