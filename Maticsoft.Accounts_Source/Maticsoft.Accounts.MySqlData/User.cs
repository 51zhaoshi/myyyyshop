namespace Maticsoft.Accounts.MySqlData
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using MySql.Data.MySqlClient;
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
            builder.Append("?UserID,?RoleID)");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?RoleID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = RoleID;
            DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool AddRole(int userId, int roleId)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4), new MySqlParameter("?_RoleID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userId;
            parameters[1].Value = roleId;
            DbHelperMySQL.RunProcedure("sp_Accounts_AddUserToRole", parameters, out num);
            return (num == 1);
        }

        public bool AssignRoleExists(int UserID, int RoleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_UserAssignmentRoles");
            builder.Append(" where UserID= ?UserID and RoleID=?RoleID ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?RoleID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = RoleID;
            return DbHelperMySQL.Exists(builder.ToString(), cmdParms);
        }

        public int Create(string userName, byte[] password, string nickName, string trueName, string sex, string phone, string email, int employeeID, string departmentID, bool activity, string userType, int style, int User_iCreator, DateTime User_dateValid, string User_cLang)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { 
                new MySqlParameter("?_UserName", MySqlDbType.VarChar, 100), new MySqlParameter("?_Password", MySqlDbType.VarBinary, 20), new MySqlParameter("?_NickName", MySqlDbType.VarChar, 50), new MySqlParameter("?_TrueName", MySqlDbType.VarChar, 50), new MySqlParameter("?_Sex", MySqlDbType.VarChar, 2), new MySqlParameter("?_Phone", MySqlDbType.VarChar, 20), new MySqlParameter("?_Email", MySqlDbType.VarChar, 100), new MySqlParameter("?_EmployeeID", MySqlDbType.Int32, 4), new MySqlParameter("?_DepartmentID", MySqlDbType.VarChar, 15), new MySqlParameter("?_Activity", MySqlDbType.Bit, 1), new MySqlParameter("?_UserType", MySqlDbType.VarChar, 2), new MySqlParameter("?_UserID", MySqlDbType.Int32, 4), new MySqlParameter("?_Style", MySqlDbType.Int32, 4), new MySqlParameter("?_User_iCreator", MySqlDbType.Int32, 4), new MySqlParameter("?_User_dateCreate", MySqlDbType.DateTime), new MySqlParameter("?_User_dateValid", MySqlDbType.DateTime), 
                new MySqlParameter("?_User_cLang", MySqlDbType.VarChar, 10)
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
                DbHelperMySQL.RunProcedure("sp_Accounts_CreateUser", parameters, out num);
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
            MySqlParameter[] parameters = new MySqlParameter[] { 
                new MySqlParameter("?_UserName", MySqlDbType.VarChar, 100), new MySqlParameter("?_Password", MySqlDbType.VarBinary, 20), new MySqlParameter("?_NickName", MySqlDbType.VarChar, 50), new MySqlParameter("?_TrueName", MySqlDbType.VarChar, 50), new MySqlParameter("?_Sex", MySqlDbType.VarChar, 2), new MySqlParameter("?_Phone", MySqlDbType.VarChar, 20), new MySqlParameter("?_Email", MySqlDbType.VarChar, 100), new MySqlParameter("?_EmployeeID", MySqlDbType.Int32, 4), new MySqlParameter("?_DepartmentID", MySqlDbType.VarChar, 15), new MySqlParameter("?_Activity", MySqlDbType.Bit, 1), new MySqlParameter("?_UserType", MySqlDbType.VarChar, 2), new MySqlParameter("?_UserID", MySqlDbType.Int32, 4), new MySqlParameter("?_Style", MySqlDbType.Int32, 4), new MySqlParameter("?_User_iCreator", MySqlDbType.Int32, 4), new MySqlParameter("?_User_dateCreate", MySqlDbType.DateTime), new MySqlParameter("?_User_dateValid", MySqlDbType.DateTime), 
                new MySqlParameter("?_User_cLang", MySqlDbType.VarChar, 10)
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
                DbHelperMySQL.RunProcedure("sp_Accounts_CreateUser", parameters, out num);
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
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userID;
            DbHelperMySQL.RunProcedure("sp_Accounts_DeleteUser", parameters, out num);
            return (num == 1);
        }

        public void DeleteAssignRole(int UserID, int RoleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete Accounts_UserAssignmentRoles ");
            builder.Append(" where UserID= ?UserID and RoleID=?RoleID ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?RoleID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = RoleID;
            DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public DataSet GetAssignRolesByUser(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Roles where RoleID in ");
            builder.Append("(select RoleID from Accounts_UserAssignmentRoles ");
            builder.Append(" where UserID= ?UserID) ");
            builder.Append(" ORDER BY Description ASC ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = UserID;
            return DbHelperMySQL.Query(builder.ToString(), cmdParms);
        }

        public ArrayList GetEffectivePermissionList(int userID)
        {
            ArrayList list = new ArrayList();
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userID;
            MySqlDataReader reader = DbHelperMySQL.RunProcedure("sp_Accounts_GetEffectivePermissionList", parameters);
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
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userID;
            MySqlDataReader reader = DbHelperMySQL.RunProcedure("sp_Accounts_GetEffectivePermissionListID", parameters);
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }
            reader.Close();
            return list;
        }

        public DataSet GetEffectivePermissionLists(int userID)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userID;
            return DbHelperMySQL.RunProcedure("sp_Accounts_GetEffectivePermissionList", parameters, "PermissionList");
        }

        public DataSet GetNoAssignRolesByUser(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Roles where RoleID not in ");
            builder.Append("(select RoleID from Accounts_UserAssignmentRoles ");
            builder.Append(" where UserID= ?UserID) ");
            builder.Append(" ORDER BY Description ASC ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = UserID;
            return DbHelperMySQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetUserList(string key)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_key", MySqlDbType.VarChar, 50) };
            parameters[0].Value = key;
            return DbHelperMySQL.RunProcedure("sp_Accounts_GetUsers", parameters, "Users");
        }

        public DataSet GetUserList(string UserType, string DepartmentID, string Key)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where (1=1)");
            builder.Append(" and UserType in ( ?UserType )");
            builder.Append(" and DepartmentID= ?DepartmentID ");
            builder.Append(" and (UserName like ('%'+?Key+'%') or TrueName like ('%'+?Key+'%'))  ");
            builder.Append(" order by UserName ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserType", MySqlDbType.VarChar, 50), new MySqlParameter("?DepartmentID", MySqlDbType.VarChar, 30), new MySqlParameter("?Key", MySqlDbType.VarChar, 50) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = DepartmentID;
            cmdParms[2].Value = Key;
            return DbHelperMySQL.Query(builder.ToString(), cmdParms);
        }

        [Obsolete]
        public ArrayList GetUserRoles(int userID)
        {
            ArrayList list = new ArrayList();
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userID;
            MySqlDataReader reader = DbHelperMySQL.RunProcedure("sp_Accounts_GetUserRoles", parameters);
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
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userID;
            MySqlDataReader reader = DbHelperMySQL.RunProcedure("sp_Accounts_GetUserRoles", parameters);
            while (reader.Read())
            {
                dictionary.Add(reader.GetInt32(0), reader.GetString(1));
            }
            reader.Close();
            return dictionary;
        }

        public DataSet GetUsersByDepart(string DepartmentID, string Key)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_DepartmentID", MySqlDbType.VarChar, 30), new MySqlParameter("?_key", MySqlDbType.VarChar, 50) };
            parameters[0].Value = DepartmentID;
            parameters[1].Value = Key;
            return DbHelperMySQL.RunProcedure("sp_Accounts_GetUsersByDepart", parameters, "Users");
        }

        public DataSet GetUsersByEmp(int EmployeeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where EmployeeID= ?EmployeeID ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?EmployeeID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = EmployeeID;
            return DbHelperMySQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetUsersByRole(int RoleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users where UserID in ");
            builder.Append("(select UserID from Accounts_UserRoles ");
            builder.Append(" where RoleID= ?RoleID) ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?RoleID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = RoleID;
            return DbHelperMySQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetUsersByType(string UserType, string Key)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where ");
            if (UserType.Length > 0)
            {
                builder.Append(" UserType = ?UserType ");
            }
            if (Key.Length > 0)
            {
                if (UserType.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" (UserName like ('%'+?Key+'%') or TrueName like ('%'+?Key+'%'))  ");
            }
            builder.Append(" order by UserName ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserType", MySqlDbType.VarChar, 50), new MySqlParameter("?Key", MySqlDbType.VarChar, 50) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = Key;
            return DbHelperMySQL.Query(builder.ToString(), cmdParms);
        }

        [Obsolete]
        public bool HasUser(string userName)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserName", MySqlDbType.VarChar, 100) };
            parameters[0].Value = userName;
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetUserDetailsByUserName", parameters, "Users"))
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
            builder.Append("where Email = ?Email ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?Email", MySqlDbType.VarChar, 100) };
            cmdParms[0].Value = email;
            return (DbHelperMySQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool HasUserByNickName(string nickName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where NickName = ?NickName ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?NickName", MySqlDbType.VarChar, 50) };
            cmdParms[0].Value = nickName;
            return (DbHelperMySQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool HasUserByPhone(string phone)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where Phone = ?Phone ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?Phone", MySqlDbType.VarChar, 20) };
            cmdParms[0].Value = phone;
            return (DbHelperMySQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool HasUserByPhone(string phone, string userType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where Phone = ?Phone and UserType = ?UserType");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?Phone", MySqlDbType.VarChar, 20), new MySqlParameter("?UserType", MySqlDbType.VarChar, 2) };
            cmdParms[0].Value = phone;
            cmdParms[1].Value = userType;
            return (DbHelperMySQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool HasUserByUserName(string userName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users ");
            builder.Append("where UserName = ?UserName ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserName", MySqlDbType.VarChar, 100) };
            cmdParms[0].Value = userName;
            return (DbHelperMySQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool RemoveRole(int userId, int roleId)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4), new MySqlParameter("?_RoleID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userId;
            parameters[1].Value = roleId;
            DbHelperMySQL.RunProcedure("sp_Accounts_RemoveUserFromRole", parameters, out num);
            return (num == 1);
        }

        public DataRow Retrieve(int userID)
        {
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserID", MySqlDbType.Int32, 4) };
            parameters[0].Value = userID;
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetUserDetails", parameters, "Users"))
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
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserName", MySqlDbType.VarChar, 100) };
            parameters[0].Value = userName;
            using (DataSet set = DbHelperMySQL.RunProcedure("sp_Accounts_GetUserDetailsByUserName", parameters, "Users"))
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
            builder.Append("select from Accounts_Users ");
            builder.Append("where NickName = ?NickName LIMIT 1");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?NickName", MySqlDbType.VarChar, 50) };
            cmdParms[0].Value = nickName;
            DataSet set = DbHelperMySQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0];
            }
            return null;
        }

        public bool SetPassword(string UserName, byte[] encPassword)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserName", MySqlDbType.VarChar), new MySqlParameter("?_EncryptedPassword", MySqlDbType.VarBinary, 20) };
            parameters[0].Value = UserName;
            parameters[1].Value = encPassword;
            DbHelperMySQL.RunProcedure("sp_Accounts_SetPassword", parameters, out num);
            return (num == 1);
        }

        public int TestPassword(int userID, byte[] encPassword)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT UserID FROM Accounts_Users WHERE UserID = ?UserID AND Password = UNHEX(?EncryptedPassword)");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?EncryptedPassword", MySqlDbType.VarString, 100) };
            cmdParms[0].Value = userID;
            cmdParms[1].Value = BitConverter.ToString(encPassword).Replace("-", "");
            object single = DbHelperMySQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return int.Parse(single.ToString());
        }

        public bool Update(int UserID, int User_iApprover, int User_iApproveState)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Users set ");
            builder.Append("User_iApprover=?User_iApprover,");
            builder.Append("User_dateApprove=?User_dateApprove,");
            builder.Append("User_iApproveState=?User_iApproveState");
            builder.Append(" where UserID=?UserID");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?User_iApprover", MySqlDbType.Int32, 4), new MySqlParameter("?User_dateApprove", MySqlDbType.DateTime), new MySqlParameter("?User_iApproveState", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = User_iApprover;
            cmdParms[2].Value = DateTime.Now;
            cmdParms[3].Value = User_iApproveState;
            return (DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(int UserID, int EmployeeID, string DepartmentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Users set ");
            builder.Append("EmployeeID=?EmployeeID,");
            builder.Append("DepartmentID=?DepartmentID ");
            builder.Append(" where UserID=?UserID");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?EmployeeID", MySqlDbType.Int32, 4), new MySqlParameter("?DepartmentID", MySqlDbType.VarChar, 15) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = EmployeeID;
            cmdParms[2].Value = DepartmentID;
            return (DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(int userID, string userName, byte[] password, string nickName, string trueName, string sex, string phone, string email, int employeeID, string departmentID, bool activity, string userType, int style)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserName", MySqlDbType.VarChar, 100), new MySqlParameter("?_Password", MySqlDbType.VarBinary, 20), new MySqlParameter("?_NickName", MySqlDbType.VarChar, 50), new MySqlParameter("?_TrueName", MySqlDbType.VarChar, 50), new MySqlParameter("?_Sex", MySqlDbType.VarChar, 2), new MySqlParameter("?_Phone", MySqlDbType.VarChar, 20), new MySqlParameter("?_Email", MySqlDbType.VarChar, 100), new MySqlParameter("?_EmployeeID", MySqlDbType.Int32, 4), new MySqlParameter("?_DepartmentID", MySqlDbType.VarChar, 15), new MySqlParameter("?_Activity", MySqlDbType.Bit, 1), new MySqlParameter("?_UserType", MySqlDbType.VarChar, 2), new MySqlParameter("?_UserID", MySqlDbType.Int32, 4), new MySqlParameter("?_Style", MySqlDbType.Int32, 4) };
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
            DbHelperMySQL.RunProcedure("sp_Accounts_UpdateUser", parameters, out num);
            return (num == 1);
        }

        public bool UpdateActivity(int userId, bool activity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_Users ");
            builder.Append("SET Activity=?Activity ");
            builder.Append("WHERE UserID=?UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("?_UserID", SqlDbType.Int, 4), new SqlParameter("?_Activity", SqlDbType.Bit, 1) };
            cmdParms[0].Value = userId;
            cmdParms[1].Value = activity;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int ValidateLogin(string userName, byte[] encPassword)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT UserID FROM Accounts_Users WHERE UserName = ?UserName AND Password = UNHEX(?EncryptedPassword)");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserName", MySqlDbType.VarChar, 100), new MySqlParameter("?EncryptedPassword", MySqlDbType.VarString, 100) };
            cmdParms[0].Value = userName;
            cmdParms[1].Value = BitConverter.ToString(encPassword).Replace("-", "");
            object single = DbHelperMySQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return int.Parse(single.ToString());
        }

        [Obsolete]
        public int ValidateLogin4Email(string email, byte[] encPassword)
        {
            int num;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserName", MySqlDbType.VarChar, 100), new MySqlParameter("?_Email", MySqlDbType.VarChar, 50), new MySqlParameter("?_EncryptedPassword", MySqlDbType.VarBinary, 20) };
            parameters[0].Value = null;
            parameters[1].Value = email;
            parameters[2].Value = encPassword;
            return DbHelperMySQL.RunProcedure("sp_Accounts_ValidateLogin", parameters, out num);
        }
    }
}

