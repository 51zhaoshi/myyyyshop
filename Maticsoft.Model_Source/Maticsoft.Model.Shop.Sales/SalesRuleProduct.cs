namespace Maticsoft.Model.Shop.Sales
{
    using System;

    [Serializable]
    public class SalesRuleProduct
    {
        private long _productid;
        private string _productname;
        private int _ruleid;

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

        public string ProductName
        {
            get
            {
                return this._productname;
            }
            set
            {
                this._productname = value;
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
    }
}

