namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Users : IUsers
    {
        public int Add(Maticsoft.Model.Members.Users model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_Users(");
            builder.Append("UserName,Password,TrueName,Sex,Phone,Email,EmployeeID,DepartmentID,Activity,UserType,Style,User_iCreator,User_dateCreate,User_dateValid,User_dateExpire,User_iApprover,User_dateApprove,User_iApproveState,User_cLang)");
            builder.Append(" values (");
            builder.Append("@UserName,@Password,@TrueName,@Sex,@Phone,@Email,@EmployeeID,@DepartmentID,@Activity,@UserType,@Style,@User_iCreator,@User_dateCreate,@User_dateValid,@User_dateExpire,@User_iApprover,@User_dateApprove,@User_iApproveState,@User_cLang)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.Binary, 20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@Sex", SqlDbType.Char, 10), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@EmployeeID", SqlDbType.Int, 4), new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15), new SqlParameter("@Activity", SqlDbType.Bit, 1), new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@Style", SqlDbType.Int, 4), new SqlParameter("@User_iCreator", SqlDbType.Int, 4), new SqlParameter("@User_dateCreate", SqlDbType.DateTime), new SqlParameter("@User_dateValid", SqlDbType.DateTime), new SqlParameter("@User_dateExpire", SqlDbType.DateTime), new SqlParameter("@User_iApprover", SqlDbType.Int, 4), 
                new SqlParameter("@User_dateApprove", SqlDbType.DateTime), new SqlParameter("@User_iApproveState", SqlDbType.Int, 4), new SqlParameter("@User_cLang", SqlDbType.NVarChar, 10)
             };
            cmdParms[0].Value = model.UserName;
            cmdParms[1].Value = model.Password;
            cmdParms[2].Value = model.TrueName;
            cmdParms[3].Value = model.Sex;
            cmdParms[4].Value = model.Phone;
            cmdParms[5].Value = model.Email;
            cmdParms[6].Value = model.EmployeeID;
            cmdParms[7].Value = model.DepartmentID;
            cmdParms[8].Value = model.Activity;
            cmdParms[9].Value = model.UserType;
            cmdParms[10].Value = model.Style;
            cmdParms[11].Value = model.User_iCreator;
            cmdParms[12].Value = model.User_dateCreate;
            cmdParms[13].Value = model.User_dateValid;
            cmdParms[14].Value = model.User_dateExpire;
            cmdParms[15].Value = model.User_iApprover;
            cmdParms[0x10].Value = model.User_dateApprove;
            cmdParms[0x11].Value = model.User_iApproveState;
            cmdParms[0x12].Value = model.User_cLang;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_Users ");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteByDepartmentID(int DepartmentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_Users ");
            builder.Append(" where DepartmentID=@DepartmentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DepartmentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = DepartmentID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int userId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_Users ");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            para[0].Value = userId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from Accounts_UsersExp ");
            builder2.Append(" where UserID=@UserID  ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = userId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteList(string UserIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_Users ");
            builder.Append(" where UserID in (" + UserIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteListByDepartmentID(string DepartmentIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_Users ");
            builder.Append(" where DepartmentID in (" + DepartmentIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool ExistByPhone(string Phone)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from Accounts_Users ");
            builder.Append(" where Phone=@Phone ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Phone", SqlDbType.NVarChar) };
            cmdParms[0].Value = Phone;
            return (DbHelperSQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool Exists(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_Users");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExistsByEmail(string Email)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 *  from Accounts_Users ");
            builder.Append(" where Email=@Email");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NVarChar) };
            cmdParms[0].Value = Email;
            return (DbHelperSQL.Query(builder.ToString(), cmdParms).Tables[0].Rows.Count > 0);
        }

        public bool ExistsNickName(string nickname)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_Users");
            builder.Append(" where NickName=@NickName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = nickname;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExistsNickName(int userid, string nickname)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_Users");
            builder.Append(" where UserID<>@UserID AND NickName=@NickName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = userid;
            cmdParms[1].Value = nickname;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public int GetDefaultUserId()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MIN(UserID) from Accounts_Users   where  Activity=1 and UserType='UU'");
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Accounts_Users ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string type, string keyWord)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Accounts_Users ");
            new StringBuilder();
            builder.Append(" WHERE Activity=1 ");
            if (!string.IsNullOrWhiteSpace(type))
            {
                builder.Append(" AND UserType=" + type);
            }
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                builder.AppendFormat(" AND UserName LIKE '%{0}%' ", keyWord);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" UserID,UserName,Password,TrueName,Sex,Phone,Email,EmployeeID,DepartmentID,Activity,UserType,Style,User_iCreator,User_dateCreate,User_dateValid,User_dateExpire,User_iApprover,User_dateApprove,User_iApproveState,User_cLang ");
            builder.Append(" FROM Accounts_Users ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.UserID desc");
            }
            builder.Append(")AS Row, T.*  from Accounts_Users T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEX(string keyWord = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users inner join Accounts_UsersExp on Accounts_UsersExp.UserID=Accounts_Users.UserID");
            builder.Append(" WHERE UserType='UU'");
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                builder.AppendFormat(" AND UserName LIKE '%{0}%' ", keyWord);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEXByType(string type, string keyWord = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users inner join Accounts_UsersExp on Accounts_UsersExp.UserID=Accounts_Users.UserID");
            StringBuilder builder2 = new StringBuilder();
            if (!string.IsNullOrEmpty(type))
            {
                if (!string.IsNullOrWhiteSpace(builder2.ToString()))
                {
                    builder2.Append(" AND ");
                }
                builder2.Append("  UserType='" + type + "'");
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                if (!string.IsNullOrWhiteSpace(builder2.ToString()))
                {
                    builder2.Append(" AND ");
                }
                builder2.AppendFormat("  UserName LIKE '%{0}%' ", keyWord);
            }
            builder.Append(" WHERE   " + builder2.ToString());
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("UserID", "Accounts_Users");
        }

        public Maticsoft.Model.Members.Users GetModel(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserID,UserName,NickName,Password,TrueName,Sex,Phone,Email,EmployeeID,DepartmentID,Activity,UserType,Style,User_iCreator,User_dateCreate,User_dateValid,User_dateExpire,User_iApprover,User_dateApprove,User_iApproveState,User_cLang from Accounts_Users ");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            Maticsoft.Model.Members.Users users = new Maticsoft.Model.Members.Users();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["UserID"] != null) && (set.Tables[0].Rows[0]["UserID"].ToString() != ""))
            {
                users.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserName"] != null) && (set.Tables[0].Rows[0]["UserName"].ToString() != ""))
            {
                users.UserName = set.Tables[0].Rows[0]["UserName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Password"] != null) && (set.Tables[0].Rows[0]["Password"].ToString() != ""))
            {
                users.Password = (byte[]) set.Tables[0].Rows[0]["Password"];
            }
            if ((set.Tables[0].Rows[0]["TrueName"] != null) && (set.Tables[0].Rows[0]["TrueName"].ToString() != ""))
            {
                users.TrueName = set.Tables[0].Rows[0]["TrueName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["NickName"] != null) && (set.Tables[0].Rows[0]["NickName"].ToString() != ""))
            {
                users.NickName = set.Tables[0].Rows[0]["NickName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Sex"] != null) && (set.Tables[0].Rows[0]["Sex"].ToString() != ""))
            {
                users.Sex = set.Tables[0].Rows[0]["Sex"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Phone"] != null) && (set.Tables[0].Rows[0]["Phone"].ToString() != ""))
            {
                users.Phone = set.Tables[0].Rows[0]["Phone"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Email"] != null) && (set.Tables[0].Rows[0]["Email"].ToString() != ""))
            {
                users.Email = set.Tables[0].Rows[0]["Email"].ToString();
            }
            if ((set.Tables[0].Rows[0]["EmployeeID"] != null) && (set.Tables[0].Rows[0]["EmployeeID"].ToString() != ""))
            {
                users.EmployeeID = new int?(int.Parse(set.Tables[0].Rows[0]["EmployeeID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["DepartmentID"] != null) && (set.Tables[0].Rows[0]["DepartmentID"].ToString() != ""))
            {
                users.DepartmentID = set.Tables[0].Rows[0]["DepartmentID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Activity"] != null) && (set.Tables[0].Rows[0]["Activity"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["Activity"].ToString() == "1") || (set.Tables[0].Rows[0]["Activity"].ToString().ToLower() == "true"))
                {
                    users.Activity = true;
                }
                else
                {
                    users.Activity = false;
                }
            }
            if ((set.Tables[0].Rows[0]["UserType"] != null) && (set.Tables[0].Rows[0]["UserType"].ToString() != ""))
            {
                users.UserType = set.Tables[0].Rows[0]["UserType"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Style"] != null) && (set.Tables[0].Rows[0]["Style"].ToString() != ""))
            {
                users.Style = new int?(int.Parse(set.Tables[0].Rows[0]["Style"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["User_iCreator"] != null) && (set.Tables[0].Rows[0]["User_iCreator"].ToString() != ""))
            {
                users.User_iCreator = new int?(int.Parse(set.Tables[0].Rows[0]["User_iCreator"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["User_dateCreate"] != null) && (set.Tables[0].Rows[0]["User_dateCreate"].ToString() != ""))
            {
                users.User_dateCreate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["User_dateCreate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["User_dateValid"] != null) && (set.Tables[0].Rows[0]["User_dateValid"].ToString() != ""))
            {
                users.User_dateValid = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["User_dateValid"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["User_dateExpire"] != null) && (set.Tables[0].Rows[0]["User_dateExpire"].ToString() != ""))
            {
                users.User_dateExpire = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["User_dateExpire"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["User_iApprover"] != null) && (set.Tables[0].Rows[0]["User_iApprover"].ToString() != ""))
            {
                users.User_iApprover = new int?(int.Parse(set.Tables[0].Rows[0]["User_iApprover"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["User_dateApprove"] != null) && (set.Tables[0].Rows[0]["User_dateApprove"].ToString() != ""))
            {
                users.User_dateApprove = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["User_dateApprove"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["User_iApproveState"] != null) && (set.Tables[0].Rows[0]["User_iApproveState"].ToString() != ""))
            {
                users.User_iApproveState = new int?(int.Parse(set.Tables[0].Rows[0]["User_iApproveState"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["User_cLang"] != null) && (set.Tables[0].Rows[0]["User_cLang"].ToString() != ""))
            {
                users.User_cLang = set.Tables[0].Rows[0]["User_cLang"].ToString();
            }
            return users;
        }

        public string GetNickName(int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select NickName FROM Accounts_Users ");
            builder.Append(" where UserId=" + UserId);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return "";
            }
            return single.ToString();
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Accounts_Users ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetSearchList(string type, string StrWhere = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Accounts_Users inner join Accounts_UsersExp on Accounts_UsersExp.UserID=Accounts_Users.UserID");
            StringBuilder builder2 = new StringBuilder();
            if (!string.IsNullOrEmpty(type))
            {
                if (!string.IsNullOrWhiteSpace(builder2.ToString()))
                {
                    builder2.Append(" AND ");
                }
                builder2.Append("  UserType='" + type + "'");
            }
            if (!string.IsNullOrEmpty(StrWhere))
            {
                if (!string.IsNullOrWhiteSpace(builder2.ToString()))
                {
                    builder2.Append(" AND ");
                }
                builder2.Append(StrWhere);
            }
            builder.Append(" WHERE   " + builder2.ToString());
            builder.Append(" order by  User_dateCreate desc");
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Members.Users GetUserIdByDepartmentID(string DepartmentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserID FROM Accounts_Users ");
            builder.Append(" where DepartmentID=@DepartmentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15) };
            cmdParms[0].Value = DepartmentID;
            Maticsoft.Model.Members.Users users = new Maticsoft.Model.Members.Users();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["UserID"] != null) && (set.Tables[0].Rows[0]["UserID"].ToString() != ""))
            {
                users.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            return users;
        }

        public int GetUserIdByNickName(string NickName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserID FROM Accounts_Users ");
            if (NickName.Trim() != "")
            {
                builder.Append(" where NickName=@NickName");
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = NickName;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public string GetUserName(int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserName FROM Accounts_Users ");
            builder.Append(" where UserId=" + UserId);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return "";
            }
            return single.ToString();
        }

        public bool Update(Maticsoft.Model.Members.Users model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Users set ");
            builder.Append("UserName=@UserName,");
            builder.Append("Password=@Password,");
            builder.Append("TrueName=@TrueName,");
            builder.Append("Sex=@Sex,");
            builder.Append("Phone=@Phone,");
            builder.Append("Email=@Email,");
            builder.Append("EmployeeID=@EmployeeID,");
            builder.Append("DepartmentID=@DepartmentID,");
            builder.Append("Activity=@Activity,");
            builder.Append("UserType=@UserType,");
            builder.Append("Style=@Style,");
            builder.Append("User_iCreator=@User_iCreator,");
            builder.Append("User_dateCreate=@User_dateCreate,");
            builder.Append("User_dateValid=@User_dateValid,");
            builder.Append("User_dateExpire=@User_dateExpire,");
            builder.Append("User_iApprover=@User_iApprover,");
            builder.Append("User_dateApprove=@User_dateApprove,");
            builder.Append("User_iApproveState=@User_iApproveState,");
            builder.Append("User_cLang=@User_cLang");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.Binary, 20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@Sex", SqlDbType.Char, 10), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@EmployeeID", SqlDbType.Int, 4), new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15), new SqlParameter("@Activity", SqlDbType.Bit, 1), new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@Style", SqlDbType.Int, 4), new SqlParameter("@User_iCreator", SqlDbType.Int, 4), new SqlParameter("@User_dateCreate", SqlDbType.DateTime), new SqlParameter("@User_dateValid", SqlDbType.DateTime), new SqlParameter("@User_dateExpire", SqlDbType.DateTime), new SqlParameter("@User_iApprover", SqlDbType.Int, 4), 
                new SqlParameter("@User_dateApprove", SqlDbType.DateTime), new SqlParameter("@User_iApproveState", SqlDbType.Int, 4), new SqlParameter("@User_cLang", SqlDbType.NVarChar, 10), new SqlParameter("@UserID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.UserName;
            cmdParms[1].Value = model.Password;
            cmdParms[2].Value = model.TrueName;
            cmdParms[3].Value = model.Sex;
            cmdParms[4].Value = model.Phone;
            cmdParms[5].Value = model.Email;
            cmdParms[6].Value = model.EmployeeID;
            cmdParms[7].Value = model.DepartmentID;
            cmdParms[8].Value = model.Activity;
            cmdParms[9].Value = model.UserType;
            cmdParms[10].Value = model.Style;
            cmdParms[11].Value = model.User_iCreator;
            cmdParms[12].Value = model.User_dateCreate;
            cmdParms[13].Value = model.User_dateValid;
            cmdParms[14].Value = model.User_dateExpire;
            cmdParms[15].Value = model.User_iApprover;
            cmdParms[0x10].Value = model.User_dateApprove;
            cmdParms[0x11].Value = model.User_iApproveState;
            cmdParms[0x12].Value = model.User_cLang;
            cmdParms[0x13].Value = model.UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateActiveStatus(string Ids, int ActiveType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "UPDATE Accounts_Users SET Activity=", ActiveType, " Where UserID in(", Ids, ")" }));
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateFansAndFellowCount()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_UsersExp SET FansCount=(SELECT COUNT(1) FROM SNS_UserShip us WHERE Accounts_UsersExp.UserID=us.PassiveUserID),FellowCount=(SELECT COUNT(1) FROM SNS_UserShip us WHERE Accounts_UsersExp.UserID=us.ActiveUserID)");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

