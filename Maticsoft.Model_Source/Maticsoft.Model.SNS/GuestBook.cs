namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class GuestBook
    {
        private DateTime _createddate;
        private string _createnickname;
        private int _createuserid;
        private int? _depth;
        private string _description;
        private string _email;
        private int _guestbookid;
        private int? _parentid = 0;
        private string _path;
        private int? _privacy;
        private string _tonickname;
        private int _touserid;
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

        public string CreateNickName
        {
            get
            {
                return this._createnickname;
            }
            set
            {
                this._createnickname = value;
            }
        }

        public int CreateUserID
        {
            get
            {
                return this._createuserid;
            }
            set
            {
                this._createuserid = value;
            }
        }

        public int? Depth
        {
            get
            {
                return this._depth;
            }
            set
            {
                this._depth = value;
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

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public int GuestBookID
        {
            get
            {
                return this._guestbookid;
            }
            set
            {
                this._guestbookid = value;
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

        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }

        public int? Privacy
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

        public string ToNickName
        {
            get
            {
                return this._tonickname;
            }
            set
            {
                this._tonickname = value;
            }
        }

        public int ToUserID
        {
            get
            {
                return this._touserid;
            }
            set
            {
                this._touserid = value;
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

