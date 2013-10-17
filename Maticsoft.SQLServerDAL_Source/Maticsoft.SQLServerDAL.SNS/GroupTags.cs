namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class GroupTags : IGroupTags
    {
        public int Add(Maticsoft.Model.SNS.GroupTags model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO SNS_GroupTags(");
            builder.Append("TagName,IsRecommand,Status)");
            builder.Append(" VALUES (");
            builder.Append("@TagName,@IsRecommand,@Status)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagName", SqlDbType.NVarChar, 50), new SqlParameter("@IsRecommand", SqlDbType.SmallInt, 2), new SqlParameter("@Status", SqlDbType.SmallInt, 2) };
            cmdParms[0].Value = model.TagName;
            cmdParms[1].Value = model.IsRecommand;
            cmdParms[2].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM SNS_GroupTags ");
            builder.Append(" WHERE TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TagIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM SNS_GroupTags ");
            builder.Append(" WHERE TagID in (" + TagIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SNS_GroupTags");
            builder.Append(" WHERE TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string TagName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SNS_GroupTags");
            builder.Append(" WHERE TagName=@TagName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TagName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(int TagID, string TagName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SNS_GroupTags");
            builder.Append(" WHERE TagID<>@TagID AND TagName=@TagName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4), new SqlParameter("@TagName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = TagID;
            cmdParms[1].Value = TagName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TagID,TagName,IsRecommand,Status ");
            builder.Append(" FROM SNS_GroupTags ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
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
            builder.Append(" TagID,TagName,IsRecommand,Status ");
            builder.Append(" FROM SNS_GroupTags ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrEmpty(filedOrder.Trim()))
            {
                builder.Append(" ORDER BY " + filedOrder);
            }
            else
            {
                builder.Append(" ORDER BY TagID DESC");
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
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.TagID desc");
            }
            builder.Append(")AS Row, T.*  FROM SNS_GroupTags T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("TagID", "SNS_GroupTags");
        }

        public Maticsoft.Model.SNS.GroupTags GetModel(int TagID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 TagID,TagName,IsRecommand,Status FROM SNS_GroupTags ");
            builder.Append(" WHERE TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TagID;
            Maticsoft.Model.SNS.GroupTags tags = new Maticsoft.Model.SNS.GroupTags();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["TagID"] != null) && (set.Tables[0].Rows[0]["TagID"].ToString() != ""))
            {
                tags.TagID = int.Parse(set.Tables[0].Rows[0]["TagID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["TagName"] != null) && (set.Tables[0].Rows[0]["TagName"].ToString() != ""))
            {
                tags.TagName = set.Tables[0].Rows[0]["TagName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IsRecommand"] != null) && (set.Tables[0].Rows[0]["IsRecommand"].ToString() != ""))
            {
                tags.IsRecommand = int.Parse(set.Tables[0].Rows[0]["IsRecommand"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Status"] != null) && (set.Tables[0].Rows[0]["Status"].ToString() != ""))
            {
                tags.Status = int.Parse(set.Tables[0].Rows[0]["Status"].ToString());
            }
            return tags;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM SNS_GroupTags ");
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

        public bool Update(Maticsoft.Model.SNS.GroupTags model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SNS_GroupTags SET ");
            builder.Append("TagName=@TagName,");
            builder.Append("IsRecommand=@IsRecommand,");
            builder.Append("Status=@Status");
            builder.Append(" WHERE TagID=@TagID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TagName", SqlDbType.NVarChar, 50), new SqlParameter("@IsRecommand", SqlDbType.SmallInt, 2), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@TagID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TagName;
            cmdParms[1].Value = model.IsRecommand;
            cmdParms[2].Value = model.Status;
            cmdParms[3].Value = model.TagID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTags set ");
            builder.AppendFormat(" IsRecommand={0} ", IsRecommand);
            builder.AppendFormat(" where TagID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTags set ");
            builder.AppendFormat(" Status={0} ", Status);
            builder.AppendFormat(" where TagID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

