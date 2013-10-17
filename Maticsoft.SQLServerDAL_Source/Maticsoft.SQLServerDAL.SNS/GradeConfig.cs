namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class GradeConfig : IGradeConfig
    {
        public int Add(Maticsoft.Model.SNS.GradeConfig model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO SNS_GradeConfig(");
            builder.Append("GradeName,MinRange,MaxRange,Remark)");
            builder.Append(" VALUES (");
            builder.Append("@GradeName,@MinRange,@MaxRange,@Remark)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GradeName", SqlDbType.NVarChar, 50), new SqlParameter("@MinRange", SqlDbType.Int, 4), new SqlParameter("@MaxRange", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.GradeName;
            cmdParms[1].Value = model.MinRange;
            cmdParms[2].Value = model.MaxRange;
            cmdParms[3].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int GradeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM SNS_GradeConfig ");
            builder.Append(" WHERE GradeID=@GradeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GradeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GradeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string GradeIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM SNS_GradeConfig ");
            builder.Append(" WHERE GradeID in (" + GradeIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int GradeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SNS_GradeConfig");
            builder.Append(" WHERE GradeID=@GradeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GradeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GradeID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT GradeID,GradeName,MinRange,MaxRange,Remark ");
            builder.Append(" FROM SNS_GradeConfig ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY MinRange ASC");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" GradeID,GradeName,MinRange,MaxRange,Remark ");
            builder.Append(" FROM SNS_GradeConfig ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.GradeConfig GetModel(int GradeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 GradeID,GradeName,MinRange,MaxRange,Remark FROM SNS_GradeConfig ");
            builder.Append(" WHERE GradeID=@GradeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GradeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GradeID;
            Maticsoft.Model.SNS.GradeConfig config = new Maticsoft.Model.SNS.GradeConfig();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["GradeID"] != null) && (set.Tables[0].Rows[0]["GradeID"].ToString() != ""))
            {
                config.GradeID = int.Parse(set.Tables[0].Rows[0]["GradeID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["GradeName"] != null) && (set.Tables[0].Rows[0]["GradeName"].ToString() != ""))
            {
                config.GradeName = set.Tables[0].Rows[0]["GradeName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MinRange"] != null) && (set.Tables[0].Rows[0]["MinRange"].ToString() != ""))
            {
                config.MinRange = new int?(int.Parse(set.Tables[0].Rows[0]["MinRange"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["MaxRange"] != null) && (set.Tables[0].Rows[0]["MaxRange"].ToString() != ""))
            {
                config.MaxRange = new int?(int.Parse(set.Tables[0].Rows[0]["MaxRange"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Remark"] != null) && (set.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                config.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            }
            return config;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SNS_GradeConfig ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.GradeConfig GetUserLevel(int? grades)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1.* FROM SNS_GradeConfig   ");
            builder.Append("WHERE @Score BETWEEN MinRange AND MaxRange ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Score", SqlDbType.Int, 4) };
            cmdParms[0].Value = grades;
            Maticsoft.Model.SNS.GradeConfig config = new Maticsoft.Model.SNS.GradeConfig();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["GradeID"] != null) && (set.Tables[0].Rows[0]["GradeID"].ToString() != ""))
            {
                config.GradeID = int.Parse(set.Tables[0].Rows[0]["GradeID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["GradeName"] != null) && (set.Tables[0].Rows[0]["GradeName"].ToString() != ""))
            {
                config.GradeName = set.Tables[0].Rows[0]["GradeName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MinRange"] != null) && (set.Tables[0].Rows[0]["MinRange"].ToString() != ""))
            {
                config.MinRange = new int?(int.Parse(set.Tables[0].Rows[0]["MinRange"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["MaxRange"] != null) && (set.Tables[0].Rows[0]["MaxRange"].ToString() != ""))
            {
                config.MaxRange = new int?(int.Parse(set.Tables[0].Rows[0]["MaxRange"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Remark"] != null) && (set.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                config.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            }
            return config;
        }

        public bool Update(Maticsoft.Model.SNS.GradeConfig model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SNS_GradeConfig SET ");
            builder.Append("GradeName=@GradeName,");
            builder.Append("MinRange=@MinRange,");
            builder.Append("MaxRange=@MaxRange,");
            builder.Append("Remark=@Remark");
            builder.Append(" WHERE GradeID=@GradeID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GradeName", SqlDbType.NVarChar, 50), new SqlParameter("@MinRange", SqlDbType.Int, 4), new SqlParameter("@MaxRange", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@GradeID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.GradeName;
            cmdParms[1].Value = model.MinRange;
            cmdParms[2].Value = model.MaxRange;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.GradeID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

