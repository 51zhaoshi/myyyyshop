namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class Feedback
    {
        private DateTime _createddate;
        private string _description;
        private string _extdata;
        private int _feedbackid;
        private bool _issolved;
        private string _phone;
        private string _remark;
        private string _result;
        private int? _status;
        private string _telphone;
        private int _typeid;
        private string _usercompany;
        private string _useremail;
        private string _userip;
        private string _username;
        private string _usersex;

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

        public string ExtData
        {
            get
            {
                return this._extdata;
            }
            set
            {
                this._extdata = value;
            }
        }

        public int FeedbackId
        {
            get
            {
                return this._feedbackid;
            }
            set
            {
                this._feedbackid = value;
            }
        }

        public bool IsSolved
        {
            get
            {
                return this._issolved;
            }
            set
            {
                this._issolved = value;
            }
        }

        public string Phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                this._phone = value;
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

        public string Result
        {
            get
            {
                return this._result;
            }
            set
            {
                this._result = value;
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

        public string TelPhone
        {
            get
            {
                return this._telphone;
            }
            set
            {
                this._telphone = value;
            }
        }

        public int TypeId
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

        public string UserCompany
        {
            get
            {
                return this._usercompany;
            }
            set
            {
                this._usercompany = value;
            }
        }

        public string UserEmail
        {
            get
            {
                return this._useremail;
            }
            set
            {
                this._useremail = value;
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

        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public string UserSex
        {
            get
            {
                return this._usersex;
            }
            set
            {
                this._usersex = value;
            }
        }
    }
}

