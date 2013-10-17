namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ConfigType : IConfigType
    {
        public int Add(string TypeName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_Config_Type(");
            builder.Append("TypeName)");
            builder.Append(" values (");
            builder.Append("@TypeName)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TypeName;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int KeyType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_Config_Type ");
            builder.Append(" where KeyType=@KeyType");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyType", SqlDbType.Int, 4) };
            cmdParms[0].Value = KeyType;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string KeyTypelist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_Config_Type ");
            builder.Append(" where KeyType in (" + KeyTypelist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TypeName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_Config_Type");
            builder.Append(" where TypeName=@TypeName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TypeName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select KeyType,TypeName ");
            builder.Append(" FROM SA_Config_Type ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public string GetTypeName(int KeyType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeName from SA_Config_Type ");
            builder.Append(" where KeyType=@KeyType");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyType", SqlDbType.Int, 4) };
            cmdParms[0].Value = KeyType;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0]["TypeName"].ToString();
            }
            return "";
        }

        public bool Update(int KeyType, string TypeName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Config_Type set ");
            builder.Append("TypeName=@TypeName");
            builder.Append(" where KeyType=@KeyType");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@KeyType", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeName;
            cmdParms[1].Value = KeyType;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

