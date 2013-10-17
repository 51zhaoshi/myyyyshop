namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class UserInvite
    {
        private DateTime _createddate;
        private int _inviteid;
        private string _invitenick;
        private int _inviteuserid;
        private bool _isnew;
        private bool _isrebate;
        private string _rebatedesc;
        private string _remark;
        private int _userid;
        private string _usernick;

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

        public int InviteId
        {
            get
            {
                return this._inviteid;
            }
            set
            {
                this._inviteid = value;
            }
        }

        public string InviteNick
        {
            get
            {
                return this._invitenick;
            }
            set
            {
                this._invitenick = value;
            }
        }

        public int InviteUserId
        {
            get
            {
                return this._inviteuserid;
            }
            set
            {
                this._inviteuserid = value;
            }
        }

        public bool IsNew
        {
            get
            {
                return this._isnew;
            }
            set
            {
                this._isnew = value;
            }
        }

        public bool IsRebate
        {
            get
            {
                return this._isrebate;
            }
            set
            {
                this._isrebate = value;
            }
        }

        public string RebateDesc
        {
            get
            {
                return this._rebatedesc;
            }
            set
            {
                this._rebatedesc = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public int UserId
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

        public string UserNick
        {
            get
            {
                return this._usernick;
            }
            set
            {
                this._usernick = value;
            }
        }
    }
}

