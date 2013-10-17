namespace Maticsoft.SQLServerDAL.Poll
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Topics : ITopics
    {
        public int Add(Maticsoft.Model.Poll.Topics model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Poll_Topics(");
            builder.Append("Title,Type,FormID)");
            builder.Append(" values (");
            builder.Append("@Title,@Type,@FormID)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Title", SqlDbType.NVarChar), new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@FormID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Title;
            cmdParms[1].Value = model.Type;
            cmdParms[2].Value = model.FormID;
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
            builder.Append("delete from Poll_Topics ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool DeleteList(string ClassIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Poll_Topics ");
            builder.Append(" where ID in (" + ClassIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int FormID, string Title)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Poll_Topics");
            builder.Append(" where FormID=@FormID and Title=@Title");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FormID", SqlDbType.Int, 4), new SqlParameter("@Title", SqlDbType.NVarChar) };
            cmdParms[0].Value = FormID;
            cmdParms[1].Value = Title;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ROW_NUMBER() OVER(ORDER BY ID ASC) AS RowNum, ID,Title,Type,FormID ");
            builder.Append(" FROM Poll_Topics ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ID,Title,Type,FormID ");
            builder.Append(" FROM Poll_Topics ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (strWhere.Trim() != "")
            {
                builder.Append(filedOrder);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Poll.Topics GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,Title,Type,FormID from Poll_Topics ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Model.Poll.Topics topics = new Maticsoft.Model.Poll.Topics();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ID"].ToString() != "")
            {
                topics.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            topics.Title = set.Tables[0].Rows[0]["Title"].ToString();
            if (set.Tables[0].Rows[0]["Type"].ToString() != "")
            {
                topics.Type = new int?(int.Parse(set.Tables[0].Rows[0]["Type"].ToString()));
            }
            if (set.Tables[0].Rows[0]["FormID"].ToString() != "")
            {
                topics.FormID = new int?(int.Parse(set.Tables[0].Rows[0]["FormID"].ToString()));
            }
            return topics;
        }

        public void Update(Maticsoft.Model.Poll.Topics model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Poll_Topics set ");
            builder.Append("Title=@Title,");
            builder.Append("Type=@Type,");
            builder.Append("FormID=@FormID");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Title", SqlDbType.NVarChar), new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@FormID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ID;
            cmdParms[1].Value = model.Title;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.FormID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

