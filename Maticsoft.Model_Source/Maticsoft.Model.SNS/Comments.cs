namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class Comments
    {
        private int _commentid;
        private DateTime _createddate = DateTime.Now;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private bool _hasreferuser;
        private bool _isread;
        private int? _parentid = 0;
        private int _replycount;
        private int _status = 1;
        private int _targetid;
        private int _type;
        private string _userip;
        public Maticsoft.Model.SNS.UserBlog UserBlog = new Maticsoft.Model.SNS.UserBlog();

        public int CommentID
        {
            get
            {
                return this._commentid;
            }
            set
            {
                this._commentid = value;
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

        public bool HasReferUser
        {
            get
            {
                return this._hasreferuser;
            }
            set
            {
                this._hasreferuser = value;
            }
        }

        public bool IsRead
        {
            get
            {
                return this._isread;
            }
            set
            {
                this._isread = value;
            }
        }

        public int? ParentID
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public int ReplyCount
        {
            get
            {
                return this._replycount;
            }
            set
            {
                this._replycount = value;
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

        public int TargetId
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

