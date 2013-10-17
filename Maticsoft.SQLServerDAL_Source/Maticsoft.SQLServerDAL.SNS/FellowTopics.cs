namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class FellowTopics : IFellowTopics
    {
        public int Add(Maticsoft.Model.SNS.FellowTopics model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_FellowTopics(");
            builder.Append("TopicTitle,CreatedUserId,CreatedDate,Status)");
            builder.Append(" values (");
            builder.Append("@TopicTitle,@CreatedUserId,@CreatedDate,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TopicTitle", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TopicTitle;
            cmdParms[1].Value = model.CreatedUserId;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.FellowTopics DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.FellowTopics topics = new Maticsoft.Model.SNS.FellowTopics();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    topics.ID = int.Parse(row["ID"].ToString());
                }
                if (row["TopicTitle"] != null)
                {
                    topics.TopicTitle = row["TopicTitle"].ToString();
                }
                if ((row["CreatedUserId"] != null) && (row["CreatedUserId"].ToString() != ""))
                {
                    topics.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    topics.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    topics.Status = int.Parse(row["Status"].ToString());
                }
            }
            return topics;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_FellowTopics ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(string TopicTitle, int CreatedUserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_FellowTopics ");
            builder.Append(" where TopicTitle=@TopicTitle and CreatedUserId=@CreatedUserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TopicTitle", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TopicTitle;
            cmdParms[1].Value = CreatedUserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_FellowTopics ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string TopicTitle, int CreatedUserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_FellowTopics");
            builder.Append(" where TopicTitle=@TopicTitle and CreatedUserId=@CreatedUserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TopicTitle", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TopicTitle;
            cmdParms[1].Value = CreatedUserId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TopicTitle,CreatedUserId,CreatedDate,Status ");
            builder.Append(" FROM SNS_FellowTopics ");
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
            builder.Append(" ID,TopicTitle,CreatedUserId,CreatedDate,Status ");
            builder.Append(" FROM SNS_FellowTopics ");
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
            builder.Append(")AS Row, T.*  from SNS_FellowTopics T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.FellowTopics GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,TopicTitle,CreatedUserId,CreatedDate,Status from SNS_FellowTopics ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.FellowTopics();
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
            builder.Append("select count(1) FROM SNS_FellowTopics ");
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

        public bool Update(Maticsoft.Model.SNS.FellowTopics model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_FellowTopics set ");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@TopicTitle", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.CreatedDate;
            cmdParms[1].Value = model.Status;
            cmdParms[2].Value = model.ID;
            cmdParms[3].Value = model.TopicTitle;
            cmdParms[4].Value = model.CreatedUserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

