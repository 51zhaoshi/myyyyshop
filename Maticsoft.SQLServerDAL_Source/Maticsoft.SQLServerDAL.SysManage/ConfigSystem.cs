namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ConfigSystem : IConfigSystem
    {
        public int Add(string Keyname, string Value, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_Config_System(");
            builder.Append("Keyname,Value,Description)");
            builder.Append(" values (");
            builder.Append("@Keyname,@Value,@Description)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keyname", SqlDbType.NVarChar, 50), new SqlParameter("@Value", SqlDbType.NVarChar, -1), new SqlParameter("@Description", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = Keyname;
            cmdParms[1].Value = Value;
            cmdParms[2].Value = Description;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public int Add(string Keyname, string Value, string Description, ApplicationKeyType KeyType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_Config_System(");
            builder.Append("Keyname,Value,Description,KeyType)");
            builder.Append(" values (");
            builder.Append("@Keyname,@Value,@Description,@KeyType)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keyname", SqlDbType.NVarChar, 50), new SqlParameter("@Value", SqlDbType.NVarChar, -1), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@KeyType", SqlDbType.Int) };
            cmdParms[0].Value = Keyname;
            cmdParms[1].Value = Value;
            cmdParms[2].Value = Description;
            cmdParms[3].Value = (int) KeyType;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public void Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_Config_System ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(string Keyname)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_Config_System");
            builder.Append(" where Keyname=@Keyname ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keyname", SqlDbType.NVarChar) };
            cmdParms[0].Value = Keyname;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Keyname,Value,Description ");
            builder.Append(" FROM SA_Config_System ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public string GetValue(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  Value from SA_Config_System ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (!object.Equals(single, null) && !object.Equals(single, DBNull.Value))
            {
                return single.ToString();
            }
            return "";
        }

        public string GetValue(string Keyname)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  Value from SA_Config_System ");
            builder.Append(" where Keyname=@Keyname ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keyname", SqlDbType.NVarChar) };
            cmdParms[0].Value = Keyname;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (!object.Equals(single, null) && !object.Equals(single, DBNull.Value))
            {
                return single.ToString();
            }
            return "";
        }

        public bool Update(string Keyname, string Value)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SA_Config_System SET ");
            builder.Append("Value=@Value");
            builder.Append(" WHERE Keyname=@Keyname");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keyname", SqlDbType.NVarChar, 50), new SqlParameter("@Value", SqlDbType.NVarChar, -1), new SqlParameter("@KeyType", SqlDbType.Int) };
            cmdParms[0].Value = Keyname;
            cmdParms[1].Value = Value;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(string Keyname, string Value, ApplicationKeyType KeyType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SA_Config_System SET ");
            builder.Append("Value=@Value");
            builder.Append(" WHERE Keyname=@Keyname AND KeyType=@KeyType");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keyname", SqlDbType.NVarChar, 50), new SqlParameter("@Value", SqlDbType.NVarChar, -1), new SqlParameter("@KeyType", SqlDbType.Int) };
            cmdParms[0].Value = Keyname;
            cmdParms[1].Value = Value;
            cmdParms[2].Value = (int) KeyType;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(string Keyname, string Value, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Config_System set ");
            builder.Append("Value=@Value,");
            builder.Append("Description=@Description");
            builder.Append(" where Keyname=@Keyname ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keyname", SqlDbType.NVarChar, 50), new SqlParameter("@Value", SqlDbType.NVarChar, -1), new SqlParameter("@Description", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = Keyname;
            cmdParms[1].Value = Value;
            cmdParms[2].Value = Description;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public void Update(int ID, string Keyname, string Value, string Description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Config_System set ");
            builder.Append("Keyname=@Keyname,");
            builder.Append("Value=@Value,");
            builder.Append("Description=@Description");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Keyname", SqlDbType.NVarChar, 50), new SqlParameter("@Value", SqlDbType.NVarChar, -1), new SqlParameter("@Description", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = ID;
            cmdParms[1].Value = Keyname;
            cmdParms[2].Value = Value;
            cmdParms[3].Value = Description;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void UpdateConnectionString(string connectionString)
        {
            DbHelperSQL.connectionString = connectionString;
        }
    }
}

