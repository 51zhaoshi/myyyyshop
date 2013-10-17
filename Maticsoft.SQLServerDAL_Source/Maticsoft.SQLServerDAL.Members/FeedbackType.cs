namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class FeedbackType : IFeedbackType
    {
        public int Add(Maticsoft.Model.Members.FeedbackType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_FeedbackType(");
            builder.Append("TypeName,Description)");
            builder.Append(" values (");
            builder.Append("@TypeName,@Description)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.Description;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Members.FeedbackType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.FeedbackType type = new Maticsoft.Model.Members.FeedbackType();
            if (row != null)
            {
                if ((row["TypeId"] != null) && (row["TypeId"].ToString() != ""))
                {
                    type.TypeId = int.Parse(row["TypeId"].ToString());
                }
                if (row["TypeName"] != null)
                {
                    type.TypeName = row["TypeName"].ToString();
                }
                if (row["Description"] != null)
                {
                    type.Description = row["Description"].ToString();
                }
            }
            return type;
        }

        public bool Delete(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_FeedbackType ");
            builder.Append(" where TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TypeIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_FeedbackType ");
            builder.Append(" where TypeId in (" + TypeIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_FeedbackType");
            builder.Append(" where TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeId,TypeName,Description ");
            builder.Append(" FROM SA_FeedbackType ");
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
            builder.Append(" TypeId,TypeName,Description ");
            builder.Append(" FROM SA_FeedbackType ");
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
                builder.Append("order by T.TypeId desc");
            }
            builder.Append(")AS Row, T.*  from SA_FeedbackType T ");
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
            return DbHelperSQL.GetMaxID("TypeId", "SA_FeedbackType");
        }

        public Maticsoft.Model.Members.FeedbackType GetModel(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TypeId,TypeName,Description from SA_FeedbackType ");
            builder.Append(" where TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            new Maticsoft.Model.Members.FeedbackType();
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
            builder.Append("select count(1) FROM SA_FeedbackType ");
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

        public bool Update(Maticsoft.Model.Members.FeedbackType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_FeedbackType set ");
            builder.Append("TypeName=@TypeName,");
            builder.Append("Description=@Description");
            builder.Append(" where TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.TypeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

