namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.Data;
    using Maticsoft.Accounts.IData;
    using Maticsoft.Accounts.MySqlData;
    using System;
    using System.Data;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using System.Text;

    [Serializable]
    public class SiteIdentity : IIdentity
    {
        private string _user_clang;
        private DateTime _user_dateapprove;
        private DateTime _user_datecreate;
        private DateTime _user_dateexpire;
        private DateTime _user_datevalid;
        private int _user_iapprover;
        private int _user_iapprovestate;
        private int _user_icreator;
        private bool activity;
        private IUser dataUser;
        private string departmentID;
        private string email;
        private int employeeID;
        private byte[] password;
        private string phone;
        private string sex;
        private int style;
        private string trueName;
        private int userID;
        private string userName;
        private string userType;

        public SiteIdentity(int currentUserID)
        {
            this.dataUser = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            this.departmentID = "-1";
            DataRow userRow = this.dataUser.Retrieve(currentUserID);
            if (userRow != null)
            {
                this.LoadFromDR(userRow);
            }
        }

        public SiteIdentity(string currentUserName)
        {
            this.dataUser = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            this.departmentID = "-1";
            DataRow userRow = this.dataUser.Retrieve(currentUserName);
            if (userRow != null)
            {
                this.LoadFromDR(userRow);
            }
        }

        private void LoadFromDR(DataRow userRow)
        {
            if (userRow != null)
            {
                this.UserID = (int) userRow["UserID"];
                this.userName = userRow["UserName"].ToString();
                this.trueName = userRow["TrueName"].ToString();
                this.activity = (bool) userRow["Activity"];
                this.userType = userRow["UserType"].ToString();
                this.password = (byte[]) userRow["Password"];
                if (!object.Equals(userRow["Sex"], null) && !object.Equals(userRow["Sex"], DBNull.Value))
                {
                    this.sex = userRow["Sex"].ToString();
                }
                if (!object.Equals(userRow["Phone"], null) && !object.Equals(userRow["Phone"], DBNull.Value))
                {
                    this.phone = userRow["Phone"].ToString();
                }
                if (!object.Equals(userRow["Email"], null) && !object.Equals(userRow["Email"], DBNull.Value))
                {
                    this.email = userRow["Email"].ToString();
                }
                if (!object.Equals(userRow["EmployeeID"], null) && !object.Equals(userRow["EmployeeID"], DBNull.Value))
                {
                    this.employeeID = Convert.ToInt32(userRow["EmployeeID"]);
                }
                if (!object.Equals(userRow["DepartmentID"], null) && !object.Equals(userRow["DepartmentID"], DBNull.Value))
                {
                    this.departmentID = userRow["DepartmentID"].ToString();
                }
                if (!object.Equals(userRow["Style"], null) && !object.Equals(userRow["Style"], DBNull.Value))
                {
                    this.style = Convert.ToInt32(userRow["Style"]);
                }
                if (!object.Equals(userRow["User_iCreator"], null) && !object.Equals(userRow["User_iCreator"], DBNull.Value))
                {
                    this._user_icreator = Convert.ToInt32(userRow["User_iCreator"]);
                }
                if (!object.Equals(userRow["User_dateCreate"], null) && !object.Equals(userRow["User_dateCreate"], DBNull.Value))
                {
                    this._user_datecreate = Convert.ToDateTime(userRow["User_dateCreate"]);
                }
                if (!object.Equals(userRow["User_dateValid"], null) && !object.Equals(userRow["User_dateValid"], DBNull.Value))
                {
                    this._user_datevalid = Convert.ToDateTime(userRow["User_dateValid"]);
                }
                if (!object.Equals(userRow["User_dateExpire"], null) && !object.Equals(userRow["User_dateExpire"], DBNull.Value))
                {
                    this._user_dateexpire = Convert.ToDateTime(userRow["User_dateExpire"]);
                }
                if (!object.Equals(userRow["User_iApprover"], null) && !object.Equals(userRow["User_iApprover"], DBNull.Value))
                {
                    this._user_iapprover = Convert.ToInt32(userRow["User_iApprover"]);
                }
                if (!object.Equals(userRow["User_dateApprove"], null) && !object.Equals(userRow["User_dateApprove"], DBNull.Value))
                {
                    this._user_dateapprove = Convert.ToDateTime(userRow["User_dateApprove"]);
                }
                if (!object.Equals(userRow["User_iApproveState"], null) && !object.Equals(userRow["User_iApproveState"], DBNull.Value))
                {
                    this._user_iapprovestate = Convert.ToInt32(userRow["User_iApproveState"]);
                }
                this._user_clang = userRow["User_cLang"].ToString();
            }
        }

        public int TestPassword(string password)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(password);
            byte[] encPassword = new SHA1CryptoServiceProvider().ComputeHash(bytes);
            return this.dataUser.TestPassword(this.userID, encPassword);
        }

        public bool Activity
        {
            get
            {
                return this.activity;
            }
            set
            {
                this.activity = value;
            }
        }

        public string AuthenticationType
        {
            get
            {
                return "Custom Authentication";
            }
            set
            {
            }
        }

        public string DepartmentID
        {
            get
            {
                return this.departmentID;
            }
            set
            {
                this.departmentID = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public int EmployeeID
        {
            get
            {
                return this.employeeID;
            }
            set
            {
                this.employeeID = value;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return this.userName;
            }
        }

        public byte[] Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string Phone
        {
            get
            {
                return this.phone;
            }
            set
            {
                this.phone = value;
            }
        }

        public string Sex
        {
            get
            {
                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }

        public int Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
            }
        }

        public string TrueName
        {
            get
            {
                return this.trueName;
            }
            set
            {
                this.trueName = value;
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

        public DateTime User_dateApprove
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

        public DateTime User_dateCreate
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

        public DateTime User_dateExpire
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

        public DateTime User_dateValid
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

        public int User_iApprover
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

        public int User_iApproveState
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

        public int User_iCreator
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
                return this.userID;
            }
            set
            {
                this.userID = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        public string UserType
        {
            get
            {
                return this.userType;
            }
            set
            {
                this.userType = value;
            }
        }
    }
}

