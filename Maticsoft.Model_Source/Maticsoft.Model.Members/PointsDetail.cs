namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class PointsDetail
    {
        private DateTime _createddate;
        private int _currentpoints;
        private string _description;
        private string _extdata;
        private int _pointsdetailid;
        private string _ruleaction;
        private int _score;
        private int _type;
        private int _userid;

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

        public int CurrentPoints
        {
            get
            {
                return this._currentpoints;
            }
            set
            {
                this._currentpoints = value;
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

        public string ExtData
        {
            get
            {
                return this._extdata;
            }
            set
            {
                this._extdata = value;
            }
        }

        public int PointsDetailID
        {
            get
            {
                return this._pointsdetailid;
            }
            set
            {
                this._pointsdetailid = value;
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

        public int UserID
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }
    }
}

