namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ErrorLog : IErrorLog
    {
        public int Add(Maticsoft.Model.SysManage.ErrorLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_ErrorLog(");
            builder.Append("OPTime,Url,Loginfo,StackTrace)");
            builder.Append(" values (");
            builder.Append("@OPTime,@Url,@Loginfo,@StackTrace)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OPTime", SqlDbType.DateTime), new SqlParameter("@Url", SqlDbType.NVarChar, 200), new SqlParameter("@Loginfo", SqlDbType.NVarChar), new SqlParameter("@StackTrace", SqlDbType.NVarChar) };
            cmdParms[0].Value = DateTime.Now.ToString();
            cmdParms[1].Value = model.Url;
            cmdParms[2].Value = model.Loginfo;
            cmdParms[3].Value = model.StackTrace;
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
            builder.Append("delete from SA_ErrorLog ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public void Delete(string IDList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_ErrorLog ");
            builder.Append(" where ID in (" + IDList + ") ");
            DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public void DeleteByDate(DateTime dtDateBefore)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SA_ErrorLog ");
            builder.Append(" where OPTime <= @OPTime");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OPTime", SqlDbType.DateTime) };
            cmdParms[0].Value = dtDateBefore;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,OPTime,Url,Loginfo,StackTrace ");
            builder.Append(" FROM SA_ErrorLog ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
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
            builder.Append(" ID,OPTime,Url,Loginfo,StackTrace ");
            builder.Append(" FROM SA_ErrorLog ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SysManage.ErrorLog GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,OPTime,Url,Loginfo,StackTrace from SA_ErrorLog ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Model.SysManage.ErrorLog log = new Maticsoft.Model.SysManage.ErrorLog();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ID"].ToString() != "")
            {
                log.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            if (set.Tables[0].Rows[0]["OPTime"].ToString() != "")
            {
                log.OPTime = DateTime.Parse(set.Tables[0].Rows[0]["OPTime"].ToString());
            }
            log.Url = set.Tables[0].Rows[0]["Url"].ToString();
            log.Loginfo = set.Tables[0].Rows[0]["Loginfo"].ToString();
            log.StackTrace = set.Tables[0].Rows[0]["StackTrace"].ToString();
            return log;
        }

        public void Update(Maticsoft.Model.SysManage.ErrorLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_ErrorLog set ");
            builder.Append("OPTime=@OPTime,");
            builder.Append("Url=@Url,");
            builder.Append("Loginfo=@Loginfo,");
            builder.Append("StackTrace=@StackTrace");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@OPTime", SqlDbType.DateTime), new SqlParameter("@Url", SqlDbType.NVarChar, 200), new SqlParameter("@Loginfo", SqlDbType.NVarChar), new SqlParameter("@StackTrace", SqlDbType.NVarChar) };
            cmdParms[0].Value = model.ID;
            cmdParms[1].Value = DateTime.Now.ToString();
            cmdParms[2].Value = model.Url;
            cmdParms[3].Value = model.Loginfo;
            cmdParms[4].Value = model.StackTrace;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

