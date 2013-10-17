namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class GradeConfig
    {
        private int _gradeid;
        private string _gradename;
        private int? _maxrange;
        private int? _minrange;
        private string _remark;

        public int GradeID
        {
            get
            {
                return this._gradeid;
            }
            set
            {
                this._gradeid = value;
            }
        }

        public string GradeName
        {
            get
            {
                return this._gradename;
            }
            set
            {
                this._gradename = value;
            }
        }

        public int? MaxRange
        {
            get
            {
                return this._maxrange;
            }
            set
            {
                this._maxrange = value;
            }
        }

        public int? MinRange
        {
            get
            {
                return this._minrange;
            }
            set
            {
                this._minrange = value;
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

