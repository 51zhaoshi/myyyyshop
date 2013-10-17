namespace Maticsoft.SQLServerDAL.Poll
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Reply : IReply
    {
        public int Add(Maticsoft.Model.Poll.Reply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Poll_Reply(");
            builder.Append("TopicID,ReContent,ReTime)");
            builder.Append(" values (");
            builder.Append("@TopicID,@ReContent,@ReTime)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@ReContent", SqlDbType.NVarChar, 300), new SqlParameter("@ReTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.TopicID;
            cmdParms[1].Value = model.ReContent;
            cmdParms[2].Value = model.ReTime;
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
            builder.Append("delete from Poll_Reply ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Poll_Reply");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,TopicID,ReContent,ReTime ");
            builder.Append(" FROM Poll_Reply ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Poll_Reply");
        }

        public Maticsoft.Model.Poll.Reply GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,TopicID,ReContent,ReTime from Poll_Reply ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Model.Poll.Reply reply = new Maticsoft.Model.Poll.Reply();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ID"].ToString() != "")
            {
                reply.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            if (set.Tables[0].Rows[0]["TopicID"].ToString() != "")
            {
                reply.TopicID = new int?(int.Parse(set.Tables[0].Rows[0]["TopicID"].ToString()));
            }
            reply.ReContent = set.Tables[0].Rows[0]["ReContent"].ToString();
            if (set.Tables[0].Rows[0]["ReTime"].ToString() != "")
            {
                reply.ReTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["ReTime"].ToString()));
            }
            return reply;
        }

        public void Update(Maticsoft.Model.Poll.Reply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Poll_Reply set ");
            builder.Append("TopicID=@TopicID,");
            builder.Append("ReContent=@ReContent,");
            builder.Append("ReTime=@ReTime");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@ReContent", SqlDbType.NVarChar, 300), new SqlParameter("@ReTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ID;
            cmdParms[1].Value = model.TopicID;
            cmdParms[2].Value = model.ReContent;
            cmdParms[3].Value = model.ReTime;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

