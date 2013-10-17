namespace Maticsoft.SQLServerDAL.Poll
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PollUsers : IPollUsers
    {
        public int Add(Maticsoft.Model.Poll.PollUsers model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Poll_Users(");
            builder.Append("UserName,Password,TrueName,Age,Sex,Phone,Email,UserType)");
            builder.Append(" values (");
            builder.Append("@UserName,@Password,@TrueName,@Age,@Sex,@Phone,@Email,@UserType)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.Binary, 50), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@Age", SqlDbType.Int, 4), new SqlParameter("@Sex", SqlDbType.NVarChar, 5), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 50), new SqlParameter("@UserType", SqlDbType.Char, 2) };
            cmdParms[0].Value = model.UserName;
            cmdParms[1].Value = model.Password;
            cmdParms[2].Value = model.TrueName;
            cmdParms[3].Value = model.Age;
            cmdParms[4].Value = model.Sex;
            cmdParms[5].Value = model.Phone;
            cmdParms[6].Value = model.Email;
            cmdParms[7].Value = model.UserType;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public void Delete(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Poll_Users ");
            builder.Append(" where UserID=" + UserID);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Poll_Options set SubmitNum=SubmitNum-1  ");
            builder2.Append(" where ID in (select OptionID from Poll_UserPoll where UserID=" + UserID + ")");
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from Poll_UserPoll ");
            builder3.Append(" where UserID=" + UserID);
            DbHelperSQL.ExecuteSqlTran(new List<string> { builder2.ToString(), builder3.ToString(), builder.ToString() });
        }

        public bool DeleteList(string ClassIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Poll_Users ");
            builder.Append(" where UserID in (" + ClassIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Poll_Users");
            builder.Append(" where UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserID,UserName,Password,TrueName,Age,Sex,Phone,Email,UserType ");
            builder.Append(" FROM Poll_Users ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by UserID desc ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("UserID", "Poll_Users");
        }

        public Maticsoft.Model.Poll.PollUsers GetModel(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserID,UserName,Password,TrueName,Age,Sex,Phone,Email,UserType from Poll_Users ");
            builder.Append(" where UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            Maticsoft.Model.Poll.PollUsers users = new Maticsoft.Model.Poll.PollUsers();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["UserID"].ToString() != "")
            {
                users.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            users.UserName = set.Tables[0].Rows[0]["UserName"].ToString();
            if (set.Tables[0].Rows[0]["Password"].ToString() != "")
            {
                users.Password = (byte[]) set.Tables[0].Rows[0]["Password"];
            }
            users.TrueName = set.Tables[0].Rows[0]["TrueName"].ToString();
            if (set.Tables[0].Rows[0]["Age"].ToString() != "")
            {
                users.Age = new int?(int.Parse(set.Tables[0].Rows[0]["Age"].ToString()));
            }
            users.Sex = set.Tables[0].Rows[0]["Sex"].ToString();
            users.Phone = set.Tables[0].Rows[0]["Phone"].ToString();
            users.Email = set.Tables[0].Rows[0]["Email"].ToString();
            users.UserType = set.Tables[0].Rows[0]["UserType"].ToString();
            return users;
        }

        public int GetUserCount()
        {
            string sQLString = "select count(1) from Poll_Users";
            object single = DbHelperSQL.GetSingle(sQLString);
            if (!object.Equals(single, null) && !object.Equals(single, DBNull.Value))
            {
                return int.Parse(single.ToString());
            }
            return 0;
        }

        public bool Update(Maticsoft.Model.Poll.PollUsers model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Poll_Users set ");
            builder.Append("UserName=@UserName,");
            builder.Append("Password=@Password,");
            builder.Append("TrueName=@TrueName,");
            builder.Append("Age=@Age,");
            builder.Append("Sex=@Sex,");
            builder.Append("Phone=@Phone,");
            builder.Append("Email=@Email,");
            builder.Append("UserType=@UserType");
            builder.Append(" where UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.Binary, 50), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@Age", SqlDbType.Int, 4), new SqlParameter("@Sex", SqlDbType.NVarChar, 5), new SqlParameter("@Phone", SqlDbType.NVarChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 50), new SqlParameter("@UserType", SqlDbType.Char, 2) };
            cmdParms[0].Value = model.UserID;
            cmdParms[1].Value = model.UserName;
            cmdParms[2].Value = model.Password;
            cmdParms[3].Value = model.TrueName;
            cmdParms[4].Value = model.Age;
            cmdParms[5].Value = model.Sex;
            cmdParms[6].Value = model.Phone;
            cmdParms[7].Value = model.Email;
            cmdParms[8].Value = model.UserType;
            if (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) == 0)
            {
                return false;
            }
            return true;
        }
    }
}

