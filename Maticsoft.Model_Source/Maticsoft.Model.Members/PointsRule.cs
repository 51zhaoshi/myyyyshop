namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class PointsRule
    {
        private string _description;
        private string _name;
        private int _pointslimitid;
        private string _ruleaction;
        private int _score;

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

        public string RuleAction
        {
            get
            {
                return this._ruleaction;
            }
            set
            {
                this._ruleaction = value;
            }
        }

        public int Score
        {
            get
            {
                return this._score;
            }
            set
            {
                this._score = value;
            }
        }
    }
}

