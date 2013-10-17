namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class GroupTopicFav
    {
        private DateTime _createddate;
        private int _createduserid;
        private int _id;
        private string _remark;
        private string _tags;
        private int _topicid;

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

        public string Tags
        {
            get
            {
                return this._tags;
            }
            set
            {
                this._tags = value;
            }
        }

        public int TopicID
        {
            get
            {
                return this._topicid;
            }
            set
            {
                this._topicid = value;
            }
        }
    }
}

