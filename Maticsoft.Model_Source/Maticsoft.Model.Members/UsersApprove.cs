namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class UsersApprove
    {
        private DateTime? _approvedate;
        private int _approveid;
        private int _approveuserid;
        private DateTime _createddate = DateTime.Now;
        private DateTime? _duedate;
        private string _frontview;
        private string _idcardnum;
        private string _rearview;
        private int _status;
        private string _truename;
        private int _userid;
        private int? _usertype;

        public DateTime? ApproveDate
        {
            get
            {
                return this._approvedate;
            }
            set
            {
                this._approvedate = value;
            }
        }

        public int ApproveID
        {
            get
            {
                return this._approveid;
            }
            set
            {
                this._approveid = value;
            }
        }

        public int ApproveUserID
        {
            get
            {
                return this._approveuserid;
            }
            set
            {
                this._approveuserid = value;
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

        public DateTime? DueDate
        {
            get
            {
                return this._duedate;
            }
            set
            {
                this._duedate = value;
            }
        }

        public string FrontView
        {
            get
            {
                return this._frontview;
            }
            set
            {
                this._frontview = value;
            }
        }

        public string IDCardNum
        {
            get
            {
                return this._idcardnum;
            }
            set
            {
                this._idcardnum = value;
            }
        }

        public string RearView
        {
            get
            {
                return this._rearview;
            }
            set
            {
                this._rearview = value;
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

        public string TrueName
        {
            get
            {
                return this._truename;
            }
            set
            {
                this._truename = value;
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

        public int? UserType
        {
            get
            {
                return this._usertype;
            }
            set
            {
                this._usertype = value;
            }
        }
    }
}

