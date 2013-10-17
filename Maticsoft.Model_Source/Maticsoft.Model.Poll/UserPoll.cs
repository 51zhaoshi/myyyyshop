namespace Maticsoft.Model.Poll
{
    using System;

    [Serializable]
    public class UserPoll
    {
        private DateTime? _creattime;
        private int? _optionid;
        private string _optionidlist;
        private int? _topicid;
        private int _userid;
        private string _userip;

        public DateTime? CreatTime
        {
            get
            {
                return this._creattime;
            }
            set
            {
                this._creattime = value;
            }
        }

        public int? OptionID
        {
            get
            {
                return this._optionid;
            }
            set
            {
                this._optionid = value;
            }
        }

        public string OptionIDList
        {
            get
            {
                return this._optionidlist;
            }
            set
            {
                this._optionidlist = value;
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

        public string UserIP
        {
            get
            {
                return this._userip;
            }
            set
            {
                this._userip = value;
            }
        }
    }
}

