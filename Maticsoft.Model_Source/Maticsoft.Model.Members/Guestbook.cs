namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class Guestbook
    {
        private DateTime _createddate = DateTime.Now;
        private string _createnickname;
        private int? _createuserid;
        private string _creatorcompany;
        private string _creatoremail;
        private string _creatormsn;
        private string _creatorpagesite;
        private string _creatorphone;
        private string _creatorqq;
        private string _creatorregion;
        private bool _creatorsex;
        private string _creatoruserip;
        private string _description;
        private DateTime? _handlerdate;
        private string _handlernickname;
        private int? _handleruserid;
        private int _id;
        private int? _parentid = 0;
        private int? _privacy;
        private int _replycount;
        private string _replydescription;
        private int? _status;
        private string _title;
        private string _tonickname;
        private int? _touserid;

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

        public int? CreateUserID
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

        public string CreatorCompany
        {
            get
            {
                return this._creatorcompany;
            }
            set
            {
                this._creatorcompany = value;
            }
        }

        public string CreatorEmail
        {
            get
            {
                return this._creatoremail;
            }
            set
            {
                this._creatoremail = value;
            }
        }

        public string CreatorMsn
        {
            get
            {
                return this._creatormsn;
            }
            set
            {
                this._creatormsn = value;
            }
        }

        public string CreatorPageSite
        {
            get
            {
                return this._creatorpagesite;
            }
            set
            {
                this._creatorpagesite = value;
            }
        }

        public string CreatorPhone
        {
            get
            {
                return this._creatorphone;
            }
            set
            {
                this._creatorphone = value;
            }
        }

        public string CreatorQQ
        {
            get
            {
                return this._creatorqq;
            }
            set
            {
                this._creatorqq = value;
            }
        }

        public string CreatorRegion
        {
            get
            {
                return this._creatorregion;
            }
            set
            {
                this._creatorregion = value;
            }
        }

        public bool CreatorSex
        {
            get
            {
                return this._creatorsex;
            }
            set
            {
                this._creatorsex = value;
            }
        }

        public string CreatorUserIP
        {
            get
            {
                return this._creatoruserip;
            }
            set
            {
                this._creatoruserip = value;
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

        public DateTime? HandlerDate
        {
            get
            {
                return this._handlerdate;
            }
            set
            {
                this._handlerdate = value;
            }
        }

        public string HandlerNickName
        {
            get
            {
                return this._handlernickname;
            }
            set
            {
                this._handlernickname = value;
            }
        }

        public int? HandlerUserID
        {
            get
            {
                return this._handleruserid;
            }
            set
            {
                this._handleruserid = value;
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

        public string ReplyDescription
        {
            get
            {
                return this._replydescription;
            }
            set
            {
                this._replydescription = value;
            }
        }

        public int? Status
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

        public int? ToUserID
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
    }
}

