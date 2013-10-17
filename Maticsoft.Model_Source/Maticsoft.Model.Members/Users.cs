namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class Users
    {
        private bool? _activity;
        private string _departmentid;
        private string _email;
        private int? _employeeid;
        private string _nickname;
        private byte[] _password;
        private string _phone;
        private string _sex;
        private int? _style;
        private string _truename;
        private string _user_clang;
        private DateTime? _user_dateapprove;
        private DateTime? _user_datecreate;
        private DateTime? _user_dateexpire;
        private DateTime? _user_datevalid;
        private int? _user_iapprover;
        private int? _user_iapprovestate;
        private int? _user_icreator;
        private int _userid;
        private string _username;
        private string _usertype;

        public bool? Activity
        {
            get
            {
                return this._activity;
            }
            set
            {
                this._activity = value;
            }
        }

        public string DepartmentID
        {
            get
            {
                return this._departmentid;
            }
            set
            {
                this._departmentid = value;
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

        public int? EmployeeID
        {
            get
            {
                return this._employeeid;
            }
            set
            {
                this._employeeid = value;
            }
        }

        public string NickName
        {
            get
            {
                return this._nickname;
            }
            set
            {
                this._nickname = value;
            }
        }

        public byte[] Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
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

        public string Sex
        {
            get
            {
                return this._sex;
            }
            set
            {
                this._sex = value;
            }
        }

        public int? Style
        {
            get
            {
                return this._style;
            }
            set
            {
                this._style = value;
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

        public string User_cLang
        {
            get
            {
                return this._user_clang;
            }
            set
            {
                this._user_clang = value;
            }
        }

        public DateTime? User_dateApprove
        {
            get
            {
                return this._user_dateapprove;
            }
            set
            {
                this._user_dateapprove = value;
            }
        }

        public DateTime? User_dateCreate
        {
            get
            {
                return this._user_datecreate;
            }
            set
            {
                this._user_datecreate = value;
            }
        }

        public DateTime? User_dateExpire
        {
            get
            {
                return this._user_dateexpire;
            }
            set
            {
                this._user_dateexpire = value;
            }
        }

        public DateTime? User_dateValid
        {
            get
            {
                return this._user_datevalid;
            }
            set
            {
                this._user_datevalid = value;
            }
        }

        public int? User_iApprover
        {
            get
            {
                return this._user_iapprover;
            }
            set
            {
                this._user_iapprover = value;
            }
        }

        public int? User_iApproveState
        {
            get
            {
                return this._user_iapprovestate;
            }
            set
            {
                this._user_iapprovestate = value;
            }
        }

        public int? User_iCreator
        {
            get
            {
                return this._user_icreator;
            }
            set
            {
                this._user_icreator = value;
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

        public string UserType
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

