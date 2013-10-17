namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class UserShipCategories
    {
        private int _categoryid;
        private string _categoryname;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private string _description = "";
        private DateTime? _lastupdateddate = new DateTime?(DateTime.Now);
        private int _privacy = 30;
        private int _sequence;

        public int CategoryID
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return this._categoryname;
            }
            set
            {
                this._categoryname = value;
            }
        }

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

        public DateTime? LastUpdatedDate
        {
            get
            {
                return this._lastupdateddate;
            }
            set
            {
                this._lastupdateddate = value;
            }
        }

        public int Privacy
        {
            get
            {
                return this._privacy;
            }
            set
            {
                this._privacy = value;
            }
        }

        public int Sequence
        {
            get
            {
                return this._sequence;
            }
            set
            {
                this._sequence = value;
            }
        }
    }
}

