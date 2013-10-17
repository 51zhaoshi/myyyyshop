namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Report : IReport
    {
        public int Add(Maticsoft.Model.SNS.Report model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Report(");
            builder.Append("ReportTypeID,TargetType,TargetID,CreatedUserID,CreatedNickName,Description,CreatedDate,Status)");
            builder.Append(" values (");
            builder.Append("@ReportTypeID,@TargetType,@TargetID,@CreatedUserID,@CreatedNickName,@Description,@CreatedDate,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReportTypeID", SqlDbType.Int, 4), new SqlParameter("@TargetType", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ReportTypeID;
            cmdParms[1].Value = model.TargetType;
            cmdParms[2].Value = model.TargetID;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.CreatedNickName;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.Report DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Report report = new Maticsoft.Model.SNS.Report();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    report.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["ReportTypeID"] != null) && (row["ReportTypeID"].ToString() != ""))
                {
                    report.ReportTypeID = int.Parse(row["ReportTypeID"].ToString());
                }
                if ((row["TargetType"] != null) && (row["TargetType"].ToString() != ""))
                {
                    report.TargetType = int.Parse(row["TargetType"].ToString());
                }
                if ((row["TargetID"] != null) && (row["TargetID"].ToString() != ""))
                {
                    report.TargetID = int.Parse(row["TargetID"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    report.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    report.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if (row["Description"] != null)
                {
                    report.Description = row["Description"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    report.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    report.Status = new int?(int.Parse(row["Status"].ToString()));
                }
            }
            return report;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Report ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Report ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Report");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ReportTypeID,TargetType,TargetID,CreatedUserID,CreatedNickName,Description,CreatedDate,Status ");
            builder.Append(" FROM SNS_Report ");
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
            builder.Append(" ID,ReportTypeID,TargetType,TargetID,CreatedUserID,CreatedNickName,Description,CreatedDate,Status ");
            builder.Append(" FROM SNS_Report ");
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
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Report T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" SNSR.*,SNSRT.* ");
            builder.Append(" FROM SNS_Report SNSR LEFT JOIN SNS_ReportType SNSRT ON SNSRT.ID=SNSR.ReportTypeID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" ORDER BY " + filedOrder);
            }
            else
            {
                builder.Append(" ORDER BY SNSR.ID DESC");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.Report GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,ReportTypeID,TargetType,TargetID,CreatedUserID,CreatedNickName,Description,CreatedDate,Status from SNS_Report ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.Report();
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
            builder.Append("select count(1) FROM SNS_Report ");
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

        public bool Update(Maticsoft.Model.SNS.Report model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Report set ");
            builder.Append("ReportTypeID=@ReportTypeID,");
            builder.Append("TargetType=@TargetType,");
            builder.Append("TargetID=@TargetID,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("Description=@Description,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReportTypeID", SqlDbType.Int, 4), new SqlParameter("@TargetType", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ReportTypeID;
            cmdParms[1].Value = model.TargetType;
            cmdParms[2].Value = model.TargetID;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.CreatedNickName;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.Status;
            cmdParms[8].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateReportStatus(int status, int reportId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SNS_Report ");
            builder.Append("SET Status=@Status ");
            builder.Append("WHERE ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int), new SqlParameter("@ID", SqlDbType.Int) };
            cmdParms[0].Value = status;
            cmdParms[1].Value = reportId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateReportStatus(int status, string reportIds)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SNS_Report ");
            builder.Append("SET Status=" + status + " ");
            builder.Append("WHERE ID in(" + reportIds + ") ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

