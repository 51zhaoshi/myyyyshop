namespace Maticsoft.Accounts.Data
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    [Serializable]
    public class UserType : IUserType
    {
        public void Add(string UserType, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UserType(");
            builder.Append("UserType,Description)");
            builder.Append(" values (");
            builder.Append("@UserType,@Description)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@Description", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = Description;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void Delete(string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete Accounts_UserType ");
            builder.Append(" where UserType=@UserType ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserType", SqlDbType.Char, 2) };
            cmdParms[0].Value = UserType;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(string UserType, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_UserType");
            builder.Append(" where UserType=@UserType ");
            builder.Append(" and Description=@Description ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@Description", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = Description;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public string GetDescription(string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 Description from Accounts_UserType ");
            builder.Append(" where UserType=@UserType ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserType", SqlDbType.Char, 2) };
            cmdParms[0].Value = UserType;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0]["Description"].ToString();
            }
            return "";
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserType,Description ");
            builder.Append(" FROM Accounts_UserType ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public void Update(string UserType, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UserType set ");
            builder.Append("Description=@Description");
            builder.Append(" where UserType=@UserType ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@Description", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = Description;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

