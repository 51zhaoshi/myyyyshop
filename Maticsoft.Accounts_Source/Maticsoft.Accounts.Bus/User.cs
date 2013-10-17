namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.Data;
    using Maticsoft.Accounts.IData;
    using Maticsoft.Accounts.MySqlData;
    using System;
    using System.Data;

    [Serializable]
    public class User
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
        private string nickName;
        private byte[] password;
        private string phone;
        private string sex;
        private int style;
        private string trueName;
        private int userID;
        private string userName;
        private string userType;

        public User()
        {
            this.dataUser = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            this.departmentID = "-1";
        }

        public User(AccountsPrincipal existingPrincipal)
        {
            this.dataUser = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            this.departmentID = "-1";
            this.userID = ((SiteIdentity) existingPrincipal.Identity).UserID;
            DataRow userRow = this.dataUser.Retrieve(this.userID);
            this.LoadFromDR(userRow);
        }

        public User(int existingUserID)
        {
            this.dataUser = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            this.departmentID = "-1";
            this.userID = existingUserID;
            DataRow userRow = this.dataUser.Retrieve(this.userID);
            this.LoadFromDR(userRow);
        }

        public User(string UserName)
        {
            this.dataUser = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            this.departmentID = "-1";
            DataRow userRow = this.dataUser.Retrieve(UserName);
            this.LoadFromDR(userRow);
        }

        public void AddAssignRole(int UserID, int RoleID)
        {
            if (!this.AssignRoleExists(UserID, RoleID))
            {
                this.dataUser.AddAssignRole(UserID, RoleID);
            }
        }

        public bool AddToRole(int roleId)
        {
            return this.dataUser.AddRole(this.userID, roleId);
        }

        public bool AssignRoleExists(int UserID, int RoleID)
        {
            return this.dataUser.AssignRoleExists(UserID, RoleID);
        }

        public int Create()
        {
            this.userID = this.dataUser.Create(this.userName, this.password, this.nickName, this.trueName, this.sex, this.phone, this.email, this.employeeID, this.departmentID, this.activity, this.userType, this.style, this.User_iCreator, this.User_dateValid, this.User_cLang);
            return this.userID;
        }

        public int Create4CreateDate()
        {
            this.userID = this.dataUser.Create(this.userName, this.password, this.nickName, this.trueName, this.sex, this.phone, this.email, this.employeeID, this.departmentID, this.activity, this.userType, this.style, this.User_iCreator, this.User_dateCreate, this.User_dateValid, this.User_cLang);
            return this.userID;
        }

        public bool Delete()
        {
            return this.dataUser.Delete(this.userID);
        }

        public void DeleteAssignRole(int UserID, int RoleID)
        {
            this.dataUser.DeleteAssignRole(UserID, RoleID);
        }

        public DataSet GetAssignRolesByUser(int UserID)
        {
            return this.dataUser.GetAssignRolesByUser(UserID);
        }

        public DataSet GetNoAssignRolesByUser(int UserID)
        {
            return this.dataUser.GetNoAssignRolesByUser(UserID);
        }

        public string GetTrueNameByCache(int userID)
        {
            string cacheKey = "TrueName-" + userID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = new Maticsoft.Accounts.Bus.User(userID).TrueName;
                    if (cache == null)
                    {
                        return "";
                    }
                    int configInt = ConfigHelper.GetConfigInt("CacheTime");
                    DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((configInt > 0) ? ((double) configInt) : ((double) 180)), TimeSpan.Zero);
                }
                catch
                {
                    return "";
                }
            }
            return cache.ToString();
        }

        public Maticsoft.Accounts.Bus.User GetUserByNickName(string NickName)
        {
            DataRow userRow = this.dataUser.RetrieveByNickName(NickName);
            this.LoadFromDR(userRow);
            return this;
        }

        public DataSet GetUserList(string key)
        {
            return this.dataUser.GetUserList(key);
        }

        public DataSet GetUserList(string UserType, string DepartmentID, string Key)
        {
            return this.dataUser.GetUserList(UserType, DepartmentID, Key);
        }

        public string GetUserNameByCache(int userID)
        {
            string cacheKey = "UserName-" + userID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = new Maticsoft.Accounts.Bus.User(userID).UserName;
                    if (cache == null)
                    {
                        return "";
                    }
                    int configInt = ConfigHelper.GetConfigInt("CacheTime");
                    DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((configInt > 0) ? ((double) configInt) : ((double) 180)), TimeSpan.Zero);
                }
                catch
                {
                    return "";
                }
            }
            return cache.ToString();
        }

        public DataSet GetUsersByDepart(string DepartmentID, string Key)
        {
            return this.dataUser.GetUsersByDepart(DepartmentID, Key);
        }

        public DataSet GetUsersByEmp(int EmployeeID)
        {
            return this.dataUser.GetUsersByEmp(EmployeeID);
        }

        public DataSet GetUsersByRole(int RoleID)
        {
            return this.dataUser.GetUsersByRole(RoleID);
        }

        public DataSet GetUsersByType(string usertype, string key)
        {
            return this.dataUser.GetUsersByType(usertype, key);
        }

        [Obsolete]
        public bool HasUser(string userName)
        {
            return this.dataUser.HasUser(userName);
        }

        public bool HasUserByEmail(string email)
        {
            return this.dataUser.HasUserByEmail(email);
        }

        public bool HasUserByNickName(string nickName)
        {
            return this.dataUser.HasUserByNickName(nickName);
        }

        public bool HasUserByPhone(string phone)
        {
            return this.dataUser.HasUserByPhone(phone);
        }

        public bool HasUserByPhone(string phone, string userType)
        {
            return this.dataUser.HasUserByPhone(phone, userType);
        }

        public bool HasUserByUserName(string userName)
        {
            return this.dataUser.HasUserByUserName(userName);
        }

        private void LoadFromDR(DataRow userRow)
        {
            if (userRow != null)
            {
                this.UserID = (int) userRow["UserID"];
                this.userName = userRow["UserName"].ToString();
                if (!object.Equals(userRow["NickName"], null) && !object.Equals(userRow["NickName"], DBNull.Value))
                {
                    this.nickName = userRow["NickName"].ToString();
                }
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

        public bool RemoveRole(int roleId)
        {
            return this.dataUser.RemoveRole(this.userID, roleId);
        }

        public bool RemoveRole(int userID, int roleId)
        {
            return this.dataUser.RemoveRole(userID, roleId);
        }

        public bool SetPassword(string UserName, string password)
        {
            byte[] encPassword = AccountsPrincipal.EncryptPassword(password);
            return this.dataUser.SetPassword(UserName, encPassword);
        }

        public bool Update()
        {
            return this.dataUser.Update(this.userID, this.userName, this.password, this.nickName, this.trueName, this.sex, this.phone, this.email, this.employeeID, this.departmentID, this.activity, this.userType, this.style);
        }

        public bool UpdateActivity(int userId, bool activity)
        {
            return this.dataUser.UpdateActivity(userId, activity);
        }

        public bool UpdateApprover(int UserID, int User_iApprover, int User_iApproveState)
        {
            return this.dataUser.Update(UserID, User_iApprover, User_iApproveState);
        }

        public bool UpdateEmployee(int UserID, int employeeID, string departmentID)
        {
            return this.dataUser.Update(this.userID, employeeID, departmentID);
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

        public string NickName
        {
            get
            {
                return this.nickName;
            }
            set
            {
                this.nickName = value;
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

