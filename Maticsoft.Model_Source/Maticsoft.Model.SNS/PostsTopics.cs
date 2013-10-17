namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class PostsTopics
    {
        private DateTime _createddate;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private bool _isrecommend;
        private int _sequence;
        private string _tags;
        private string _title;
        private int _topicscount;

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

        public string CreatedNickName
        {
            get
            {
                return this._creatednickname;
            }
            set
            {
                this._creatednickname = value;
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

        public bool IsRecommend
        {
            get
            {
                return this._isrecommend;
            }
            set
            {
                this._isrecommend = value;
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

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public int TopicsCount
        {
            get
            {
                return this._topicscount;
            }
            set
            {
                this._topicscount = value;
            }
        }
    }
}

