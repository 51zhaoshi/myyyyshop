namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class Comment
    {
        private int? _contentid;
        private DateTime _createddate = DateTime.Now;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private int _id;
        private bool _isread;
        private int _parentid;
        private int _replycount;
        private bool _state;
        private string _title;
        private int _typeid;

        public int? ContentId
        {
            get
            {
                return this._contentid;
            }
            set
            {
                this._contentid = value;
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

        public int ParentID
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

        public bool State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
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

        public int TypeID
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }
    }
}

