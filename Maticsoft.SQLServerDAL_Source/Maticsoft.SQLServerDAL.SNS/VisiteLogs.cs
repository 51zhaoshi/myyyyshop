namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class VisiteLogs : IVisiteLogs
    {
        public int Add(Maticsoft.Model.SNS.VisiteLogs model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_VisiteLogs(");
            builder.Append("FromUserID,FromNickName,ToUserID,ToNickName,VisitTimes,LastVisitTime)");
            builder.Append(" values (");
            builder.Append("@FromUserID,@FromNickName,@ToUserID,@ToNickName,@VisitTimes,@LastVisitTime)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FromUserID", SqlDbType.Int, 4), new SqlParameter("@FromNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ToUserID", SqlDbType.Int, 4), new SqlParameter("@ToNickName", SqlDbType.NVarChar, 50), new SqlParameter("@VisitTimes", SqlDbType.Int, 4), new SqlParameter("@LastVisitTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.FromUserID;
            cmdParms[1].Value = model.FromNickName;
            cmdParms[2].Value = model.ToUserID;
            cmdParms[3].Value = model.ToNickName;
            cmdParms[4].Value = model.VisitTimes;
            cmdParms[5].Value = model.LastVisitTime;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.VisiteLogs DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.VisiteLogs logs = new Maticsoft.Model.SNS.VisiteLogs();
            if (row != null)
            {
                if ((row["VisitID"] != null) && (row["VisitID"].ToString() != ""))
                {
                    logs.VisitID = int.Parse(row["VisitID"].ToString());
                }
                if ((row["FromUserID"] != null) && (row["FromUserID"].ToString() != ""))
                {
                    logs.FromUserID = int.Parse(row["FromUserID"].ToString());
                }
                if (row["FromNickName"] != null)
                {
                    logs.FromNickName = row["FromNickName"].ToString();
                }
                if ((row["ToUserID"] != null) && (row["ToUserID"].ToString() != ""))
                {
                    logs.ToUserID = int.Parse(row["ToUserID"].ToString());
                }
                if (row["ToNickName"] != null)
                {
                    logs.ToNickName = row["ToNickName"].ToString();
                }
                if ((row["VisitTimes"] != null) && (row["VisitTimes"].ToString() != ""))
                {
                    logs.VisitTimes = new int?(int.Parse(row["VisitTimes"].ToString()));
                }
                if ((row["LastVisitTime"] != null) && (row["LastVisitTime"].ToString() != ""))
                {
                    logs.LastVisitTime = DateTime.Parse(row["LastVisitTime"].ToString());
                }
            }
            return logs;
        }

        public bool Delete(int VisitID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_VisiteLogs ");
            builder.Append(" where VisitID=@VisitID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VisitID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VisitID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string VisitIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_VisiteLogs ");
            builder.Append(" where VisitID in (" + VisitIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select VisitID,FromUserID,FromNickName,ToUserID,ToNickName,VisitTimes,LastVisitTime ");
            builder.Append(" FROM SNS_VisiteLogs ");
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
            builder.Append(" VisitID,FromUserID,FromNickName,ToUserID,ToNickName,VisitTimes,LastVisitTime ");
            builder.Append(" FROM SNS_VisiteLogs ");
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
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.VisitID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_VisiteLogs T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.VisiteLogs GetModel(int VisitID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 VisitID,FromUserID,FromNickName,ToUserID,ToNickName,VisitTimes,LastVisitTime from SNS_VisiteLogs ");
            builder.Append(" where VisitID=@VisitID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VisitID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VisitID;
            new Maticsoft.Model.SNS.VisiteLogs();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_VisiteLogs ");
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

        public bool Update(Maticsoft.Model.SNS.VisiteLogs model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_VisiteLogs set ");
            builder.Append("FromUserID=@FromUserID,");
            builder.Append("FromNickName=@FromNickName,");
            builder.Append("ToUserID=@ToUserID,");
            builder.Append("ToNickName=@ToNickName,");
            builder.Append("VisitTimes=@VisitTimes,");
            builder.Append("LastVisitTime=@LastVisitTime");
            builder.Append(" where VisitID=@VisitID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FromUserID", SqlDbType.Int, 4), new SqlParameter("@FromNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ToUserID", SqlDbType.Int, 4), new SqlParameter("@ToNickName", SqlDbType.NVarChar, 50), new SqlParameter("@VisitTimes", SqlDbType.Int, 4), new SqlParameter("@LastVisitTime", SqlDbType.DateTime), new SqlParameter("@VisitID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.FromUserID;
            cmdParms[1].Value = model.FromNickName;
            cmdParms[2].Value = model.ToUserID;
            cmdParms[3].Value = model.ToNickName;
            cmdParms[4].Value = model.VisitTimes;
            cmdParms[5].Value = model.LastVisitTime;
            cmdParms[6].Value = model.VisitID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

