namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class PointsLimit
    {
        private int _cycle;
        private string _cycleunit;
        private int _maxtimes;
        private string _name;
        private int _pointslimitid;

        public int Cycle
        {
            get
            {
                return this._cycle;
            }
            set
            {
                this._cycle = value;
            }
        }

        public string CycleUnit
        {
            get
            {
                return this._cycleunit;
            }
            set
            {
                this._cycleunit = value;
            }
        }

        public int MaxTimes
        {
            get
            {
                return this._maxtimes;
            }
            set
            {
                this._maxtimes = value;
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

        public int PointsLimitID
        {
            get
            {
                return this._pointslimitid;
            }
            set
            {
                this._pointslimitid = value;
            }
        }
    }
}

