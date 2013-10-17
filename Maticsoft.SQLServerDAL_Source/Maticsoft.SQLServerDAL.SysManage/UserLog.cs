namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserLog : IUserLog
    {
        public int GetCount(string strWhere)
        {
            return Convert.ToInt32(DbHelperSQL.GetSingle("select count(*) from  SA_UserLog   where  " + strWhere));
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select [ID],[OPTime],[url],[OPInfo],[UserName],[UserType],[UserIp] ");
            builder.Append(" FROM SA_UserLog ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere + " Order By [OPTime] Desc ");
            }
            else
            {
                builder.Append(" Order By [OPTime] Desc ");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public void LogDelete(DateTime dtDateBefore)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@OPTime", SqlDbType.DateTime) };
            parameters[0].Value = dtDateBefore;
            DbHelperSQL.RunProcedure("sp_LogUser_delete", parameters);
        }

        public void LogUserAdd(Maticsoft.Model.SysManage.UserLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_UserLog(");
            builder.Append("OPTime,Url,OPInfo,UserName,UserType,UserIP)");
            builder.Append(" values (");
            builder.Append("@OPTime,@Url,@OPInfo,@UserName,@UserType,@UserIP)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OPTime", SqlDbType.DateTime), new SqlParameter("@Url", SqlDbType.NVarChar, 200), new SqlParameter("@OPInfo", SqlDbType.NVarChar), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@UserIP", SqlDbType.NVarChar, 20) };
            cmdParms[0].Value = DateTime.Now;
            cmdParms[1].Value = model.Url;
            cmdParms[2].Value = model.OPInfo;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.UserType;
            cmdParms[5].Value = model.UserIP;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void LogUserDelete(DateTime dtDateBefore)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SA_UserLog ");
            builder.Append(" where OPTime <= @OPTime");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OPTime", SqlDbType.DateTime) };
            cmdParms[0].Value = dtDateBefore;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void LogUserDelete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SA_UserLog ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void LogUserDelete(string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SA_UserLog ");
            builder.Append(" where ID in(" + IdList + ")");
            DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}

