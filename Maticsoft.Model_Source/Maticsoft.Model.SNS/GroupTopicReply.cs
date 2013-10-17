namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class GroupTopicReply
    {
        private DateTime _createddate;
        private string _description;
        private int _favcount;
        private int _groupid;
        private bool _hasreferusers;
        private string _orgianlnickname;
        private string _orginaldes;
        private int _orginaluserid;
        private int _originalid;
        private string _photourl;
        private decimal? _price;
        private string _productlinkurl;
        private string _productname;
        private string _producturl;
        private string _replyexurl;
        private int _replyid;
        private string _replynickname;
        private int _replytype;
        private int _replyuserid;
        private int _status;
        private int? _targetid;
        private int _topicid;
        private int? _type;
        private string _userip;

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

        public int FavCount
        {
            get
            {
                return this._favcount;
            }
            set
            {
                this._favcount = value;
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

        public bool HasReferUsers
        {
            get
            {
                return this._hasreferusers;
            }
            set
            {
                this._hasreferusers = value;
            }
        }

        public string OrgianlNickName
        {
            get
            {
                return this._orgianlnickname;
            }
            set
            {
                this._orgianlnickname = value;
            }
        }

        public string OrginalDes
        {
            get
            {
                return this._orginaldes;
            }
            set
            {
                this._orginaldes = value;
            }
        }

        public int OrginalUserID
        {
            get
            {
                return this._orginaluserid;
            }
            set
            {
                this._orginaluserid = value;
            }
        }

        public int OriginalID
        {
            get
            {
                return this._originalid;
            }
            set
            {
                this._originalid = value;
            }
        }

        public string PhotoUrl
        {
            get
            {
                return this._photourl;
            }
            set
            {
                this._photourl = value;
            }
        }

        public decimal? Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public string ProductLinkUrl
        {
            get
            {
                return this._productlinkurl;
            }
            set
            {
                this._productlinkurl = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this._productname;
            }
            set
            {
                this._productname = value;
            }
        }

        public string ProductUrl
        {
            get
            {
                return this._producturl;
            }
            set
            {
                this._producturl = value;
            }
        }

        public string ReplyExUrl
        {
            get
            {
                return this._replyexurl;
            }
            set
            {
                this._replyexurl = value;
            }
        }

        public int ReplyID
        {
            get
            {
                return this._replyid;
            }
            set
            {
                this._replyid = value;
            }
        }

        public string ReplyNickName
        {
            get
            {
                return this._replynickname;
            }
            set
            {
                this._replynickname = value;
            }
        }

        public int ReplyType
        {
            get
            {
                return this._replytype;
            }
            set
            {
                this._replytype = value;
            }
        }

        public int ReplyUserID
        {
            get
            {
                return this._replyuserid;
            }
            set
            {
                this._replyuserid = value;
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

        public int? TargetId
        {
            get
            {
                return this._targetid;
            }
            set
            {
                this._targetid = value;
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

        public int? Type
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

