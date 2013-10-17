namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PhotoTags : IPhotoTags
    {
        public int Add(Maticsoft.Model.SNS.PhotoTags model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_PhotoTags(");
            builder.Append("TagName,IsRecommand,Status,CreatedDate,Remark)");
            builder.Append(" values (");
            builder.Append("@TagName,@IsRecommand,@Status,@CreatedDate,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagName", SqlDbType.NVarChar, 100), new SqlParameter("@IsRecommand", SqlDbType.SmallInt, 2), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.TagName;
            cmdParms[1].Value = model.IsRecommand;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.CreatedDate;
            cmdParms[4].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.PhotoTags DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.PhotoTags tags = new Maticsoft.Model.SNS.PhotoTags();
            if (row != null)
            {
                if ((row["TagID"] != null) && (row["TagID"].ToString() != ""))
                {
                    tags.TagID = int.Parse(row["TagID"].ToString());
                }
                if ((row["TagName"] != null) && (row["TagName"].ToString() != ""))
                {
                    tags.TagName = row["TagName"].ToString();
                }
                if ((row["IsRecommand"] != null) && (row["IsRecommand"].ToString() != ""))
                {
                    tags.IsRecommand = new int?(int.Parse(row["IsRecommand"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    tags.Status = new int?(int.Parse(row["Status"].ToString()));
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    tags.CreatedDate = new DateTime?(DateTime.Parse(row["CreatedDate"].ToString()));
                }
                if ((row["Remark"] != null) && (row["Remark"].ToString() != ""))
                {
                    tags.Remark = row["Remark"].ToString();
                }
            }
            return tags;
        }

        public bool Delete(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_PhotoTags ");
            builder.Append(" where TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TagIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_PhotoTags ");
            builder.Append(" where TagID in (" + TagIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_PhotoTags");
            builder.Append(" where TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string TagName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SNS_PhotoTags");
            builder.Append(" WHERE TagName=@TagName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TagName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(int TagID, string TagName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SNS_PhotoTags");
            builder.Append(" WHERE TagID<>@TagID AND TagName=@TagName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4), new SqlParameter("@TagName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TagID;
            cmdParms[1].Value = TagName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetHotTags(int top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT   ");
            if (top > 10)
            {
                builder.Append("top " + top + "  * from  SNS_PhotoTags order by newid()");
            }
            else
            {
                builder.Append("top 10  * from  SNS_Tags order by newid()");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TagID,TagName,IsRecommand,Status,CreatedDate,Remark ");
            builder.Append(" FROM SNS_PhotoTags ");
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
            builder.Append(" TagID,TagName,IsRecommand,Status,CreatedDate,Remark ");
            builder.Append(" FROM SNS_PhotoTags ");
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
                builder.Append("order by T.TagID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_PhotoTags T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("TagID", "SNS_PhotoTags");
        }

        public Maticsoft.Model.SNS.PhotoTags GetModel(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TagID,TagName,IsRecommand,Status,CreatedDate,Remark from SNS_PhotoTags ");
            builder.Append(" where TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            new Maticsoft.Model.SNS.PhotoTags();
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
            builder.Append("select count(1) FROM SNS_PhotoTags ");
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

        public bool Update(Maticsoft.Model.SNS.PhotoTags model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_PhotoTags set ");
            builder.Append("TagName=@TagName,");
            builder.Append("IsRecommand=@IsRecommand,");
            builder.Append("Status=@Status,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Remark=@Remark");
            builder.Append(" where TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagName", SqlDbType.NVarChar, 100), new SqlParameter("@IsRecommand", SqlDbType.SmallInt, 2), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TagName;
            cmdParms[1].Value = model.IsRecommand;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.CreatedDate;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.TagID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_PhotoTags set ");
            builder.AppendFormat(" Status={0} ", Status);
            builder.AppendFormat(" where TagID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

