namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Tags : ITags
    {
        public int Add(Maticsoft.Model.SNS.Tags model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Tags(");
            builder.Append("TagName,TypeId,IsRecommand,Status)");
            builder.Append(" values (");
            builder.Append("@TagName,@TypeId,@IsRecommand,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagName", SqlDbType.NVarChar, 50), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@IsRecommand", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TagName;
            cmdParms[1].Value = model.TypeId;
            cmdParms[2].Value = model.IsRecommand;
            cmdParms[3].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.Tags DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Tags tags = new Maticsoft.Model.SNS.Tags();
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
                if ((row["TypeId"] != null) && (row["TypeId"].ToString() != ""))
                {
                    tags.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if ((row["IsRecommand"] != null) && (row["IsRecommand"].ToString() != ""))
                {
                    tags.IsRecommand = int.Parse(row["IsRecommand"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    tags.Status = int.Parse(row["Status"].ToString());
                }
            }
            return tags;
        }

        public bool Delete(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Tags ");
            builder.Append(" where TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TagIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Tags ");
            builder.Append(" where TagID in (" + TagIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Tags");
            builder.Append(" where TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(int TypeId, string TagName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Tags");
            builder.Append(" where TypeId=@TypeId and TagName=@TagName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@TagName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TypeId;
            cmdParms[1].Value = TagName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetHotTags(int top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT   ");
            if (top > 10)
            {
                builder.Append("top " + top + "  * from  SNS_Tags order by newid()");
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
            builder.Append("select TagID,TagName,TypeId,IsRecommand,Status ");
            builder.Append(" FROM SNS_Tags ");
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
            builder.Append(" TagID,TagName,TypeId,IsRecommand,Status ");
            builder.Append(" FROM SNS_Tags ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            else
            {
                builder.Append(" order by TagID desc");
            }
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
            builder.Append(")AS Row, T.*  from SNS_Tags T ");
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
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" SNST.*,SNSTT.TypeName ");
            builder.Append("  FROM SNS_Tags SNST LEFT JOIN SNS_TagType SNSTT ON SNSTT.ID=SNST.TypeId ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            else
            {
                builder.Append(" order by TagID desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.Tags GetModel(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TagID,TagName,TypeId,IsRecommand,Status from SNS_Tags ");
            builder.Append(" where TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            new Maticsoft.Model.SNS.Tags();
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
            builder.Append("select count(1) FROM SNS_Tags ");
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

        public bool Update(Maticsoft.Model.SNS.Tags model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Tags set ");
            builder.Append("TagName=@TagName,");
            builder.Append("TypeId=@TypeId,");
            builder.Append("IsRecommand=@IsRecommand,");
            builder.Append("Status=@Status");
            builder.Append(" where TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagName", SqlDbType.NVarChar, 50), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@IsRecommand", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TagName;
            cmdParms[1].Value = model.TypeId;
            cmdParms[2].Value = model.IsRecommand;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.TagID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Tags set ");
            builder.AppendFormat(" IsRecommand={0} ", IsRecommand);
            builder.AppendFormat(" where TagID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Tags set ");
            builder.AppendFormat(" Status={0} ", Status);
            builder.AppendFormat(" where TagID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

