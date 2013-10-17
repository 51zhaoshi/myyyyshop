namespace Maticsoft.Model.Shop.Order
{
    using System;

    [Serializable]
    public class OrderLookupItems
    {
        private decimal? _appendmoney;
        private int? _calculatemode;
        private string _inputtitle;
        private bool _isinputrequired;
        private int _lookupitemid;
        private int _lookuplistid;
        private string _name;
        private string _remark;

        public decimal? AppendMoney
        {
            get
            {
                return this._appendmoney;
            }
            set
            {
                this._appendmoney = value;
            }
        }

        public int? CalculateMode
        {
            get
            {
                return this._calculatemode;
            }
            set
            {
                this._calculatemode = value;
            }
        }

        public string InputTitle
        {
            get
            {
                return this._inputtitle;
            }
            set
            {
                this._inputtitle = value;
            }
        }

        public bool IsInputRequired
        {
            get
            {
                return this._isinputrequired;
            }
            set
            {
                this._isinputrequired = value;
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

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
    }
}

