namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class UserRank
    {
        private int? _creatoruserid;
        private string _description;
        private bool _isdefault;
        private bool _ismembercreated;
        private string _name;
        private int? _numberofmemberranks;
        private int _pointmax;
        private int _pointmin;
        private string _priceoperations;
        private string _pricetype;
        private decimal _pricevalue;
        private int _rankid;
        private int _ranktype;

        public int? CreatorUserId
        {
            get
            {
                return this._creatoruserid;
            }
            set
            {
                this._creatoruserid = value;
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

        public bool IsMemberCreated
        {
            get
            {
                return this._ismembercreated;
            }
            set
            {
                this._ismembercreated = value;
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

        public int? NumberOfMemberRanks
        {
            get
            {
                return this._numberofmemberranks;
            }
            set
            {
                this._numberofmemberranks = value;
            }
        }

        public int PointMax
        {
            get
            {
                return this._pointmax;
            }
            set
            {
                this._pointmax = value;
            }
        }

        public int PointMin
        {
            get
            {
                return this._pointmin;
            }
            set
            {
                this._pointmin = value;
            }
        }

        public string PriceOperations
        {
            get
            {
                return this._priceoperations;
            }
            set
            {
                this._priceoperations = value;
            }
        }

        public string PriceType
        {
            get
            {
                return this._pricetype;
            }
            set
            {
                this._pricetype = value;
            }
        }

        public decimal PriceValue
        {
            get
            {
                return this._pricevalue;
            }
            set
            {
                this._pricevalue = value;
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

        public int RankType
        {
            get
            {
                return this._ranktype;
            }
            set
            {
                this._ranktype = value;
            }
        }
    }
}

