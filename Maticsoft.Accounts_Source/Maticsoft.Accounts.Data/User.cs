namespace Maticsoft.Accounts.Data
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Security.Principal;
    using System.Text;

    [Serializable]
    public class User : IUser
    {
        public void AddAssignRole(int UserID, int RoleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UserAssignmentRoles(");
            builder.Append("UserID,RoleID)");
            builder.Append(" values (");
            builder.Append("@UserID,@RoleID)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = RoleID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool AddRole(int userId, int roleId)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            parameters[0].Value = userId;
            parameters[1].Value = roleId;
            DbHelperSQL.RunProcedure("sp_Accounts_AddUserToRole", parameters, out num);
            return (num == 1);
        }

        public bool AssignRoleExists(int UserID, int RoleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_UserAssignmentRoles");
            builder.Append(" where UserID= @UserID and RoleID=@RoleID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = RoleID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public int Create(string userName, byte[] password, string nickName, string trueName, string sex, string phone, string email, int employeeID, string departmentID, bool activity, string userType, int style, int User_iCreator, DateTime User_dateValid, string User_cLang)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@Password", SqlDbType.Binary, 20), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@Sex", SqlDbType.Char, 2), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@EmployeeID", SqlDbType.Int, 4), new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15), new SqlParameter("@Activity", SqlDbType.Bit, 1), new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Style", SqlDbType.Int, 4), new SqlParameter("@User_iCreator", SqlDbType.Int, 4), new SqlParameter("@User_dateCreate", SqlDbType.DateTime), new SqlParameter("@User_dateValid", SqlDbType.DateTime), 
                new SqlParameter("@User_cLang", SqlDbType.NVarChar, 10)
             };
            parameters[0].Value = userName;
            parameters[1].Value = password;
            parameters[2].Value = nickName;
            parameters[3].Value = trueName;
            parameters[4].Value = sex;
            parameters[5].Value = phone;
            parameters[6].Value = email;
            parameters[7].Value = employeeID;
            parameters[8].Value = departmentID;
            parameters[9].Value = activity ? 1 : 0;
            parameters[10].Value = userType;
            parameters[11].Direction = ParameterDirection.Output;
            parameters[12].Value = style;
            parameters[13].Value = User_iCreator;
            parameters[14].Value = DateTime.Now;
            parameters[15].Value = DateTime.Now;
            parameters[0x10].Value = User_cLang;
            try
            {
                int num;
                DbHelperSQL.RunProcedure("sp_Accounts_CreateUser", parameters, out num);
            }
            catch (SqlException exception)
            {
                if (exception.Number == 0xa29)
                {
                    return -100;
                }
            }
            return (int) parameters[11].Value;
        }

        public int Create(string userName, byte[] password, string nickName, string trueName, string sex, string phone, string email, int employeeID, string departmentID, bool activity, string userType, int style, int User_iCreator, DateTime User_dateCreate, DateTime User_dateValid, string User_cLang)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@Password", SqlDbType.Binary, 20), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@Sex", SqlDbType.Char, 2), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@EmployeeID", SqlDbType.Int, 4), new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15), new SqlParameter("@Activity", SqlDbType.Bit, 1), new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Style", SqlDbType.Int, 4), new SqlParameter("@User_iCreator", SqlDbType.Int, 4), new SqlParameter("@User_dateCreate", SqlDbType.DateTime), new SqlParameter("@User_dateValid", SqlDbType.DateTime), 
                new SqlParameter("@User_cLang", SqlDbType.NVarChar, 10)
             };
            parameters[0].Value = userName;
            parameters[1].Value = password;
            parameters[2].Value = nickName;
            parameters[3].Value = trueName;
            parameters[4].Value = sex;
            parameters[5].Value = phone;
            parameters[6].Value = email;
            parameters[7].Value = employeeID;
            parameters[8].Value = departmentID;
            parameters[9].Value = activity ? 1 : 0;
            parameters[10].Value = userType;
            parameters[11].Direction = ParameterDirection.Output;
            parameters[12].Value = style;
            parameters[13].Value = User_iCreator;
            parameters[14].Value = (User_dateCreate == DateTime.MinValue) ? DateTime.Now : User_dateCreate;
            parameters[15].Value = DateTime.Now;
            parameters[0x10].Value = User_cLang;
            try
            {
                int num;
                DbHelperSQL.RunProcedure("sp_Accounts_CreateUser", parameters, out num);
            }
            catch (SqlException exception)
            {
                if (exception.Number == 0xa29)
                {
                    return -100;
                }
            }
            return (int) parameters[11].Value;
        }

        public bool Delete(int userID)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;
            DbHelperSQL.RunProcedure("sp_Accounts_DeleteUser", parameters, out num);
            return (num == 1);
        }

        public void DeleteAssignRole(int UserID, int RoleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete Accounts_UserAssignmentRoles ");
            builder.Append(" where UserID= @UserID and RoleID=@RoleID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = RoleID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public DataSet GetAssignRolesByUser(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Roles where RoleID in ");
            builder.Append("(select RoleID from Accounts_UserAssignmentRoles ");
            builder.Append(" where UserID= @UserID) ");
            builder.Append(" ORDER BY Description ASC ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public ArrayList GetEffectivePermissionList(int userID)
        {
            ArrayList list = new ArrayList();
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;
            SqlDataReader reader = DbHelperSQL.RunProcedure("sp_Accounts_GetEffectivePermissionList", parameters);
            while (reader.Read())
            {
                list.Add(reader.GetString(1));
            }
            reader.Close();
            return list;
        }

        public ArrayList GetEffectivePermissionListID(int userID)
        {
            ArrayList list = new ArrayList();
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;
            SqlDataReader reader = DbHelperSQL.RunProcedure("sp_Accounts_GetEffectivePermissionListID", parameters);
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }
            reader.Close();
            return list;
        }

        public DataSet GetEffectivePermissionLists(int userID)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;
            return DbHelperSQL.RunProcedure("sp_Accounts_GetEffectivePermissionList", parameters, "PermissionList");
        }

        public DataSet GetNoAssignRolesByUser(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Roles where RoleID not in ");
            builder.Append("(select RoleID from Accounts_UserAssignmentRoles ");
            builder.Append(" where UserID= @UserID) ");
            builder.Append(" ORDER BY Description ASC ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetUserList(string key)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@key", SqlDbType.NVarChar, 50) };
            parameters[0].Value = key;
            return DbHelperSQL.RunProcedure("sp_Accounts_GetUsers", parameters, "Users");
        }

        public DataSet GetUserList(string UserType, string DepartmentID, string Key)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where (1=1)");
            builder.Append(" and UserType in ( @UserType )");
            builder.Append(" and DepartmentID= @DepartmentID ");
            builder.Append(" and (UserName like '%'+@Key+'%' or TrueName like '%'+@Key+'%')  ");
            builder.Append(" order by UserName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserType", SqlDbType.NVarChar, 50), new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 30), new SqlParameter("@Key", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = DepartmentID;
            cmdParms[2].Value = Key;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        [Obsolete]
        public ArrayList GetUserRoles(int userID)
        {
            ArrayList list = new ArrayList();
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;
            SqlDataReader reader = DbHelperSQL.RunProcedure("sp_Accounts_GetUserRoles", parameters);
            while (reader.Read())
            {
                list.Add(reader.GetString(1));
            }
            reader.Close();
            return list;
        }

        public Dictionary<int, string> GetUserRoles4KeyValues(int userID)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;
            SqlDataReader reader = DbHelperSQL.RunProcedure("sp_Accounts_GetUserRoles", parameters);
            while (reader.Read())
            {
                dictionary.Add(reader.GetInt32(0), reader.GetString(1));
            }
            reader.Close();
            return dictionary;
        }

        public DataSet GetUsersByDepart(string DepartmentID, string Key)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 30), new SqlParameter("@key", SqlDbType.NVarChar, 50) };
            parameters[0].Value = DepartmentID;
            parameters[1].Value = Key;
            return DbHelperSQL.RunProcedure("sp_Accounts_GetUsersByDepart", parameters, "Users");
        }

        public DataSet GetUsersByEmp(int EmployeeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where EmployeeID= @EmployeeID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@EmployeeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = EmployeeID;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetUsersByRole(int RoleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users where UserID in ");
            builder.Append("(select UserID from Accounts_UserRoles ");
            builder.Append(" where RoleID= @RoleID) ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            cmdParms[0].Value = RoleID;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetUsersByType(string UserType, string Key)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where ");
            if (UserType.Length > 0)
            {
                builder.Append(" UserType = @UserType ");
            }
            if (Key.Length > 0)
            {
                if (UserType.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" (UserName like '%'+@Key+'%' or TrueName like '%'+@Key+'%')  ");
            }
            builder.Append(" order by UserName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserType", SqlDbType.NVarChar, 50), new SqlParameter("@Key", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = Key;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        [Obsolete]
        public bool HasUser(string userName)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 100) };
            parameters[0].Value = userName;
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetUserDetailsByUserName", parameters, "Users"))
            {
                if (set.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool HasUserByEmail(string email)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where Email = @Email ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = email;
            return (DbHelperSQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool HasUserByNickName(string nickName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where NickName = @NickName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = nickName;
            return (DbHelperSQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool HasUserByPhone(string phone)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where Phone = @Phone ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Phone", SqlDbType.NVarChar, 20) };
            cmdParms[0].Value = phone;
            return (DbHelperSQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool HasUserByPhone(string phone, string userType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where Phone = @Phone and UserType = @UserType");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@UserType", SqlDbType.Char, 2) };
            cmdParms[0].Value = phone;
            cmdParms[1].Value = userType;
            return (DbHelperSQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool HasUserByUserName(string userName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where UserName = @UserName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = userName;
            return (DbHelperSQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool RemoveRole(int userId, int roleId)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            parameters[0].Value = userId;
            parameters[1].Value = roleId;
            DbHelperSQL.RunProcedure("sp_Accounts_RemoveUserFromRole", parameters, out num);
            return (num == 1);
        }

        public DataRow Retrieve(int userID)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetUserDetails", parameters, "Users"))
            {
                if (set.Tables[0].Rows.Count > 0)
                {
                    return set.Tables[0].Rows[0];
                }
                return null;
            }
        }

        public DataRow Retrieve(string userName)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 100) };
            parameters[0].Value = userName;
            using (DataSet set = DbHelperSQL.RunProcedure("sp_Accounts_GetUserDetailsByUserName", parameters, "Users"))
            {
                if (set.Tables[0].Rows.Count == 0)
                {
                    throw new IdentityNotMappedException("无此用户或用户已过期：" + userName);
                }
                return set.Tables[0].Rows[0];
            }
        }

        public DataRow RetrieveByNickName(string nickName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top(1) from Accounts_Users ");
            builder.Append("where NickName = @NickName ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = nickName;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0];
            }
            return null;
        }

        public bool SetPassword(string UserName, byte[] encPassword)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar), new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20) };
            parameters[0].Value = UserName;
            parameters[1].Value = encPassword;
            DbHelperSQL.RunProcedure("sp_Accounts_SetPassword", parameters, out num);
            return (num == 1);
        }

        public int TestPassword(int userID, byte[] encPassword)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20) };
            parameters[0].Value = userID;
            parameters[1].Value = encPassword;
            return DbHelperSQL.RunProcedure("sp_Accounts_TestPassword", parameters, out num);
        }

        public bool Update(int UserID, int User_iApprover, int User_iApproveState)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Users set ");
            builder.Append("User_iApprover=@User_iApprover,");
            builder.Append("User_dateApprove=@User_dateApprove,");
            builder.Append("User_iApproveState=@User_iApproveState");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@User_iApprover", SqlDbType.Int, 4), new SqlParameter("@User_dateApprove", SqlDbType.DateTime), new SqlParameter("@User_iApproveState", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = User_iApprover;
            cmdParms[2].Value = DateTime.Now;
            cmdParms[3].Value = User_iApproveState;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(int UserID, int EmployeeID, string DepartmentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Users set ");
            builder.Append("EmployeeID=@EmployeeID,");
            builder.Append("DepartmentID=@DepartmentID ");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@EmployeeID", SqlDbType.Int, 4), new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = EmployeeID;
            cmdParms[2].Value = DepartmentID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(int userID, string userName, byte[] password, string nickName, string trueName, string sex, string phone, string email, int employeeID, string departmentID, bool activity, string userType, int style)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@Password", SqlDbType.Binary, 20), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@Sex", SqlDbType.Char, 2), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@EmployeeID", SqlDbType.Int, 4), new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15), new SqlParameter("@Activity", SqlDbType.Bit, 1), new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Style", SqlDbType.Int, 4) };
            parameters[0].Value = userName;
            parameters[1].Value = password;
            parameters[2].Value = nickName;
            parameters[3].Value = trueName;
            parameters[4].Value = sex;
            parameters[5].Value = phone;
            parameters[6].Value = email;
            parameters[7].Value = employeeID;
            parameters[8].Value = departmentID;
            parameters[9].Value = activity;
            parameters[10].Value = userType;
            parameters[11].Value = userID;
            parameters[12].Value = style;
            DbHelperSQL.RunProcedure("sp_Accounts_UpdateUser", parameters, out num);
            return (num == 1);
        }

        public bool UpdateActivity(int userId, bool activity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_Users ");
            builder.Append("SET Activity=@Activity ");
            builder.Append("WHERE UserID=@UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Activity", SqlDbType.Bit, 1) };
            cmdParms[0].Value = userId;
            cmdParms[1].Value = activity;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int ValidateLogin(string userName, byte[] encPassword)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20) };
            parameters[0].Value = userName;
            parameters[1].Value = encPassword;
            return DbHelperSQL.RunProcedure("sp_Accounts_ValidateLogin", parameters, out num);
        }

        public int ValidateLogin4Email(string email, byte[] encPassword)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NVarChar, 50), new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20) };
            parameters[0].Value = email;
            parameters[1].Value = encPassword;
            return DbHelperSQL.RunProcedure("sp_Accounts_ValidateLogin", parameters, out num);
        }
    }
}

