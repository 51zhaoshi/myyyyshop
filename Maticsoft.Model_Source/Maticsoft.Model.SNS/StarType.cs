namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class StarType
    {
        private string _checkrule;
        private string _remark;
        private int? _status;
        private int _typeid;
        private string _typename;

        public string CheckRule
        {
            get
            {
                return this._checkrule;
            }
            set
            {
                this._checkrule = value;
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

        public int? Status
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

        public int TypeID
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this._typename;
            }
            set
            {
                this._typename = value;
            }
        }
    }
}

