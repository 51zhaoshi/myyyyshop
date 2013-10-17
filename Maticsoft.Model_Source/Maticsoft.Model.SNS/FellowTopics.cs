namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class FellowTopics
    {
        private DateTime _createddate;
        private int _createduserid;
        private int _id;
        private int _status;
        private string _topictitle;

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

        public int CreatedUserId
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

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
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

        public string TopicTitle
        {
            get
            {
                return this._topictitle;
            }
            set
            {
                this._topictitle = value;
            }
        }
    }
}

