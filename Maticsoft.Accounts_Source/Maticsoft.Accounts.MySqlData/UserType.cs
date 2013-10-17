namespace Maticsoft.Accounts.MySqlData
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
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
            builder.Append("?UserType,?Description)");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserType", MySqlDbType.VarChar, 2), new MySqlParameter("?Description", MySqlDbType.VarChar, 100) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = Description;
            DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void Delete(string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete Accounts_UserType ");
            builder.Append(" where UserType=?UserType ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserType", MySqlDbType.VarChar, 2) };
            cmdParms[0].Value = UserType;
            DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(string UserType, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_UserType");
            builder.Append(" where UserType=?UserType ");
            builder.Append(" and Description=?Description ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserType", MySqlDbType.VarChar, 2), new MySqlParameter("?Description", MySqlDbType.VarChar, 50) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = Description;
            return DbHelperMySQL.Exists(builder.ToString(), cmdParms);
        }

        public string GetDescription(string UserType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Description from Accounts_UserType ");
            builder.Append(" where UserType=?UserType LIMIT 1");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserType", MySqlDbType.VarChar, 2) };
            cmdParms[0].Value = UserType;
            DataSet set = DbHelperMySQL.Query(builder.ToString(), cmdParms);
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
            return DbHelperMySQL.Query(builder.ToString());
        }

        public void Update(string UserType, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UserType set ");
            builder.Append("Description=?Description");
            builder.Append(" where UserType=?UserType ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserType", MySqlDbType.VarChar, 2), new MySqlParameter("?Description", MySqlDbType.VarChar, 100) };
            cmdParms[0].Value = UserType;
            cmdParms[1].Value = Description;
            DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

