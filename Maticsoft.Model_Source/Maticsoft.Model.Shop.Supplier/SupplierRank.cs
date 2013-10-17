namespace Maticsoft.Model.Shop.Supplier
{
    using System;

    [Serializable]
    public class SupplierRank
    {
        private string _description;
        private int _imagecount = -1;
        private bool _isapproval;
        private bool _isdefault;
        private string _name;
        private decimal _price;
        private int _productcount = -1;
        private int _rankid;
        private decimal _rankmax = -1M;
        private decimal _rankmin = -1M;

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

        public int ImageCount
        {
            get
            {
                return this._imagecount;
            }
            set
            {
                this._imagecount = value;
            }
        }

        public bool IsApproval
        {
            get
            {
                return this._isapproval;
            }
            set
            {
                this._isapproval = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return this._isdefault;
            }
            set
            {
                this._isdefault = value;
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

        public int ProductCount
        {
            get
            {
                return this._productcount;
            }
            set
            {
                this._productcount = value;
            }
        }

        public int RankId
        {
            get
            {
                return this._rankid;
            }
            set
            {
                this._rankid = value;
            }
        }

        public decimal RankMax
        {
            get
            {
                return this._rankmax;
            }
            set
            {
                this._rankmax = value;
            }
        }

        public decimal RankMin
        {
            get
            {
                return this._rankmin;
            }
            set
            {
                this._rankmin = value;
            }
        }
    }
}

