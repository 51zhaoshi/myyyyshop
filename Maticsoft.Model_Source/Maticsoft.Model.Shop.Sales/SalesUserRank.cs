namespace Maticsoft.Model.Shop.Sales
{
    using System;

    [Serializable]
    public class SalesUserRank
    {
        private int _rankid;
        private string _remark;
        private int _ruleid;

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

