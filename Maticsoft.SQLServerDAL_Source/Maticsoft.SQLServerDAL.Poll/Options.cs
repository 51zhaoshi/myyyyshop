namespace Maticsoft.SQLServerDAL.Poll
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Options : IOptions
    {
        public int Add(Maticsoft.Model.Poll.Options model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Poll_Options(");
            builder.Append("Name,TopicID,isChecked,SubmitNum)");
            builder.Append(" values (");
            builder.Append("@Name,@TopicID,@isChecked,@SubmitNum)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 150), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@isChecked", SqlDbType.SmallInt, 2), new SqlParameter("@SubmitNum", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.TopicID;
            cmdParms[2].Value = model.isChecked;
            cmdParms[3].Value = model.SubmitNum;
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
            builder.Append("delete from Poll_Options ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool DeleteList(string ClassIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Poll_Options ");
            builder.Append(" where ID in (" + ClassIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TopicID, string Name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Poll_Options");
            builder.Append(" where TopicID=@TopicID and Name=@Name ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar) };
            cmdParms[0].Value = TopicID;
            cmdParms[1].Value = Name;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetCountList(int FormID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Name,SubmitNum,TopicID from Poll_Options ");
            builder.Append(" where TopicID in  (");
            builder.Append(" select ID from Poll_Topics where FormID=" + FormID);
            builder.Append(")");
            builder.Append("order by ID");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetCountList(string strwhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Name,SubmitNum,TopicID from Poll_Options ");
            builder.Append(" where TopicID in  (");
            builder.Append(" select ID from Poll_Topics ");
            if (strwhere.Length > 0)
            {
                builder.AppendFormat(" where {0} ", strwhere);
            }
            builder.Append(")");
            builder.Append("order by ID");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Name,TopicID,isChecked,SubmitNum ");
            builder.Append(" FROM Poll_Options ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Poll.Options GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,Name,TopicID,isChecked,SubmitNum from Poll_Options ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Model.Poll.Options options = new Maticsoft.Model.Poll.Options();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ID"].ToString() != "")
            {
                options.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            options.Name = set.Tables[0].Rows[0]["Name"].ToString();
            if (set.Tables[0].Rows[0]["TopicID"].ToString() != "")
            {
                options.TopicID = new int?(int.Parse(set.Tables[0].Rows[0]["TopicID"].ToString()));
            }
            if (set.Tables[0].Rows[0]["isChecked"].ToString() != "")
            {
                options.isChecked = new int?(int.Parse(set.Tables[0].Rows[0]["isChecked"].ToString()));
            }
            if (set.Tables[0].Rows[0]["SubmitNum"].ToString() != "")
            {
                options.SubmitNum = new int?(int.Parse(set.Tables[0].Rows[0]["SubmitNum"].ToString()));
            }
            return options;
        }

        public void Update(Maticsoft.Model.Poll.Options model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Poll_Options set ");
            builder.Append("Name=@Name,");
            builder.Append("TopicID=@TopicID,");
            builder.Append("isChecked=@isChecked,");
            builder.Append("SubmitNum=@SubmitNum");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 150), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@isChecked", SqlDbType.SmallInt, 2), new SqlParameter("@SubmitNum", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ID;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.TopicID;
            cmdParms[3].Value = model.isChecked;
            cmdParms[4].Value = model.SubmitNum;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

