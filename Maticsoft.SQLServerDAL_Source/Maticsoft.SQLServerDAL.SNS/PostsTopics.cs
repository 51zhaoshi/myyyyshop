namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PostsTopics : IPostsTopics
    {
        public bool Add(Maticsoft.Model.SNS.PostsTopics model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_PostsTopics(");
            builder.Append("Title,Description,CreatedUserID,CreatedNickName,CreatedDate,TopicsCount,IsRecommend,Sequence,Tags)");
            builder.Append(" values (");
            builder.Append("@Title,@Description,@CreatedUserID,@CreatedNickName,@CreatedDate,@TopicsCount,@IsRecommend,@Sequence,@Tags)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Title", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@TopicsCount", SqlDbType.Int, 4), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.Title;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.CreatedUserID;
            cmdParms[3].Value = model.CreatedNickName;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.TopicsCount;
            cmdParms[6].Value = model.IsRecommend;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.Tags;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.SNS.PostsTopics DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.PostsTopics topics = new Maticsoft.Model.SNS.PostsTopics();
            if (row != null)
            {
                if (row["Title"] != null)
                {
                    topics.Title = row["Title"].ToString();
                }
                if (row["Description"] != null)
                {
                    topics.Description = row["Description"].ToString();
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    topics.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    topics.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    topics.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["TopicsCount"] != null) && (row["TopicsCount"].ToString() != ""))
                {
                    topics.TopicsCount = int.Parse(row["TopicsCount"].ToString());
                }
                if ((row["IsRecommend"] != null) && (row["IsRecommend"].ToString() != ""))
                {
                    if ((row["IsRecommend"].ToString() == "1") || (row["IsRecommend"].ToString().ToLower() == "true"))
                    {
                        topics.IsRecommend = true;
                    }
                    else
                    {
                        topics.IsRecommend = false;
                    }
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    topics.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["Tags"] != null)
                {
                    topics.Tags = row["Tags"].ToString();
                }
            }
            return topics;
        }

        public bool Delete(string Title)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_PostsTopics ");
            builder.Append(" where Title=@Title ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Title", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = Title;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Titlelist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_PostsTopics ");
            builder.Append(" where Title in (" + Titlelist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Title)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_PostsTopics");
            builder.Append(" where Title=@Title ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Title", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = Title;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Title,Description,CreatedUserID,CreatedNickName,CreatedDate,TopicsCount,IsRecommend,Sequence,Tags ");
            builder.Append(" FROM SNS_PostsTopics ");
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
            builder.Append(" Title,Description,CreatedUserID,CreatedNickName,CreatedDate,TopicsCount,IsRecommend,Sequence,Tags ");
            builder.Append(" FROM SNS_PostsTopics ");
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
                builder.Append("order by T.Title desc");
            }
            builder.Append(")AS Row, T.*  from SNS_PostsTopics T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.PostsTopics GetModel(string Title)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Title,Description,CreatedUserID,CreatedNickName,CreatedDate,TopicsCount,IsRecommend,Sequence,Tags from SNS_PostsTopics ");
            builder.Append(" where Title=@Title ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Title", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = Title;
            new Maticsoft.Model.SNS.PostsTopics();
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
            builder.Append("select count(1) FROM SNS_PostsTopics ");
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

        public bool Update(Maticsoft.Model.SNS.PostsTopics model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_PostsTopics set ");
            builder.Append("Description=@Description,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("TopicsCount=@TopicsCount,");
            builder.Append("IsRecommend=@IsRecommend,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Tags=@Tags");
            builder.Append(" where Title=@Title ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@TopicsCount", SqlDbType.Int, 4), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@Title", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.Description;
            cmdParms[1].Value = model.CreatedUserID;
            cmdParms[2].Value = model.CreatedNickName;
            cmdParms[3].Value = model.CreatedDate;
            cmdParms[4].Value = model.TopicsCount;
            cmdParms[5].Value = model.IsRecommend;
            cmdParms[6].Value = model.Sequence;
            cmdParms[7].Value = model.Tags;
            cmdParms[8].Value = model.Title;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

