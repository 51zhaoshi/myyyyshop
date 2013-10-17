namespace Maticsoft.Model.Shop
{
    using System;

    [Serializable]
    public class Constant
    {
        private DateTime _datadate;
        private int _maxvalue;
        private string _remark;
        private int _type;

        public DateTime DataDate
        {
            get
            {
                return this._datadate;
            }
            set
            {
                this._datadate = value;
            }
        }

        public int MaxValue
        {
            get
            {
                return this._maxvalue;
            }
            set
            {
                this._maxvalue = value;
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

        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

