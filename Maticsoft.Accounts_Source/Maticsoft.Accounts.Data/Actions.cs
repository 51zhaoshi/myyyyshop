namespace Maticsoft.Accounts.Data
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Actions : IActions
    {
        public int Add(string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_Actions_Permission(");
            builder.Append("Description)");
            builder.Append(" values (");
            builder.Append("@Description)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Description", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = Description;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public int Add(string Description, int PermissionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_Actions_Permission(");
            builder.Append("Description,PermissionID)");
            builder.Append(" values (");
            builder.Append("@Description,@PermissionID)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Description;
            cmdParms[1].Value = PermissionID;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public void AddPermission(int ActionID, int PermissionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Actions_Permission set ");
            builder.Append("PermissionID=@PermissionID");
            builder.Append(" where ActionID=@ActionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionID", SqlDbType.Int, 4), new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ActionID;
            cmdParms[1].Value = PermissionID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void AddPermission(string ActionIDs, int PermissionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Actions_Permission set ");
            builder.Append("PermissionID=" + PermissionID);
            builder.Append(" where ActionID in (" + ActionIDs + ")");
            DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public void ClearPermissions(int ActionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Actions_Permission set ");
            builder.Append("PermissionID=@PermissionID");
            builder.Append(" where ActionID=@ActionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionID", SqlDbType.Int, 4), new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ActionID;
            cmdParms[1].Value = 0;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void Delete(int ActionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_Actions_Permission ");
            builder.Append(" where ActionID=@ActionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ActionID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_Actions_Permission");
            builder.Append(" where Description=@Description ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Description", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = Description;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public string GetDescription(int ActionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 Description from Accounts_Actions_Permission ");
            builder.Append(" where ActionID=@ActionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ActionID;
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
            builder.Append("select ActionID,Description,PermissionID ");
            builder.Append(" FROM Accounts_Actions_Permission ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public void Update(int ActionID, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Actions_Permission set ");
            builder.Append("Description=@Description");
            builder.Append(" where ActionID=@ActionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = ActionID;
            cmdParms[1].Value = Description;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void Update(int ActionID, string Description, int PermissionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_Actions_Permission set ");
            builder.Append("Description=@Description,");
            builder.Append("PermissionID=@PermissionID");
            builder.Append(" where ActionID=@ActionID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ActionID;
            cmdParms[1].Value = Description;
            cmdParms[2].Value = PermissionID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

