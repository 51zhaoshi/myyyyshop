namespace Maticsoft.Model.Shop.Sales
{
    using System;

    [Serializable]
    public class SalesRule
    {
        private DateTime _createddate;
        private int _createduserid;
        private int _ruleid;
        private int _rulemode;
        private string _rulename;
        private int _ruleunit;
        private int _status;

        public DateTime CreatedDate
        {
            get
            {
                return this._createddate;
            }
            set
            {
                this._createddate = value;
            }
        }

        public int CreatedUserID
        {
            get
            {
                return this._createduserid;
            }
            set
            {
                this._createduserid = value;
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

        public int RuleMode
        {
            get
            {
                return this._rulemode;
            }
            set
            {
                this._rulemode = value;
            }
        }

        public string RuleName
        {
            get
            {
                return this._rulename;
            }
            set
            {
                this._rulename = value;
            }
        }

        public int RuleUnit
        {
            get
            {
                return this._ruleunit;
            }
            set
            {
                this._ruleunit = value;
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
    }
}

