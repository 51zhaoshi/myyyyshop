namespace Maticsoft.Model.Poll
{
    using System;

    [Serializable]
    public class Options
    {
        private int _id;
        private int? _ischecked;
        private string _name;
        private int? _submitnum;
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

        public int? isChecked
        {
            get
            {
                return this._ischecked;
            }
            set
            {
                this._ischecked = value;
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

        public int? SubmitNum
        {
            get
            {
                return this._submitnum;
            }
            set
            {
                this._submitnum = value;
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

