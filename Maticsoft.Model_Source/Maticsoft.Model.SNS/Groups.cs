namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class Groups
    {
        private string _applygroupreason;
        private DateTime _createddate = DateTime.Now;
        private string _creatednickname;
        private int _createduserid;
        private string _groupbackground;
        private string _groupdescription;
        private int _groupid;
        private string _grouplogo;
        private string _grouplogothumb;
        private string _groupname;
        private int _groupusercount;
        private int _isrecommand;
        private int _privacy;
        private int _sequence;
        private int _status;
        private string _tags;
        private int _topiccount;
        private int _topicreplycount;

        public string ApplyGroupReason
        {
            get
            {
                return this._applygroupreason;
            }
            set
            {
                this._applygroupreason = value;
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

        public string GroupBackground
        {
            get
            {
                return this._groupbackground;
            }
            set
            {
                this._groupbackground = value;
            }
        }

        public string GroupDescription
        {
            get
            {
                return this._groupdescription;
            }
            set
            {
                this._groupdescription = value;
            }
        }

        public int GroupID
        {
            get
            {
                return this._groupid;
            }
            set
            {
                this._groupid = value;
            }
        }

        public string GroupLogo
        {
            get
            {
                return this._grouplogo;
            }
            set
            {
                this._grouplogo = value;
            }
        }

        public string GroupLogoThumb
        {
            get
            {
                return this._grouplogothumb;
            }
            set
            {
                this._grouplogothumb = value;
            }
        }

        public string GroupName
        {
            get
            {
                return this._groupname;
            }
            set
            {
                this._groupname = value;
            }
        }

        public int GroupUserCount
        {
            get
            {
                return this._groupusercount;
            }
            set
            {
                this._groupusercount = value;
            }
        }

        public int IsRecommand
        {
            get
            {
                return this._isrecommand;
            }
            set
            {
                this._isrecommand = value;
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

        public int TopicCount
        {
            get
            {
                return this._topiccount;
            }
            set
            {
                this._topiccount = value;
            }
        }

        public int TopicReplyCount
        {
            get
            {
                return this._topicreplycount;
            }
            set
            {
                this._topicreplycount = value;
            }
        }
    }
}

