namespace Maticsoft.Model.Poll
{
    using System;

    [Serializable]
    public class Reply
    {
        private int _id;
        private string _recontent;
        private DateTime? _retime;
        private int? _topicid;

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

        public string ReContent
        {
            get
            {
                return this._recontent;
            }
            set
            {
                this._recontent = value;
            }
        }

        public DateTime? ReTime
        {
            get
            {
                return this._retime;
            }
            set
            {
                this._retime = value;
            }
        }

        public int? TopicID
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

