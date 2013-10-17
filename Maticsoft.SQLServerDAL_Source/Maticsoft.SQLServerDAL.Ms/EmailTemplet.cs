namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class EmailTemplet : IEmailTemplet
    {
        public int Add(Maticsoft.Model.Ms.EmailTemplet model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_EmailTemplet(");
            builder.Append("EmailType,EmailPriority,TagDescription,EmailDescription,EmailSubject,EmailBody)");
            builder.Append(" values (");
            builder.Append("@EmailType,@EmailPriority,@TagDescription,@EmailDescription,@EmailSubject,@EmailBody)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@EmailType", SqlDbType.NVarChar, 100), new SqlParameter("@EmailPriority", SqlDbType.Int, 4), new SqlParameter("@TagDescription", SqlDbType.NVarChar, 500), new SqlParameter("@EmailDescription", SqlDbType.NVarChar, 500), new SqlParameter("@EmailSubject", SqlDbType.NVarChar, 0x400), new SqlParameter("@EmailBody", SqlDbType.NText) };
            cmdParms[0].Value = model.EmailType;
            cmdParms[1].Value = model.EmailPriority;
            cmdParms[2].Value = model.TagDescription;
            cmdParms[3].Value = model.EmailDescription;
            cmdParms[4].Value = model.EmailSubject;
            cmdParms[5].Value = model.EmailBody;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Ms.EmailTemplet DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Ms.EmailTemplet templet = new Maticsoft.Model.Ms.EmailTemplet();
            if (row != null)
            {
                if ((row["TempletId"] != null) && (row["TempletId"].ToString() != ""))
                {
                    templet.TempletId = int.Parse(row["TempletId"].ToString());
                }
                if (row["EmailType"] != null)
                {
                    templet.EmailType = row["EmailType"].ToString();
                }
                if ((row["EmailPriority"] != null) && (row["EmailPriority"].ToString() != ""))
                {
                    templet.EmailPriority = int.Parse(row["EmailPriority"].ToString());
                }
                if (row["TagDescription"] != null)
                {
                    templet.TagDescription = row["TagDescription"].ToString();
                }
                if (row["EmailDescription"] != null)
                {
                    templet.EmailDescription = row["EmailDescription"].ToString();
                }
                if (row["EmailSubject"] != null)
                {
                    templet.EmailSubject = row["EmailSubject"].ToString();
                }
                if (row["EmailBody"] != null)
                {
                    templet.EmailBody = row["EmailBody"].ToString();
                }
            }
            return templet;
        }

        public bool Delete(int TempletId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_EmailTemplet ");
            builder.Append(" where TempletId=@TempletId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TempletId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TempletId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TempletIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_EmailTemplet ");
            builder.Append(" where TempletId in (" + TempletIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TempletId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_EmailTemplet");
            builder.Append(" where TempletId=@TempletId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TempletId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TempletId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TempletId,EmailType,EmailPriority,TagDescription,EmailDescription,EmailSubject,EmailBody ");
            builder.Append(" FROM Ms_EmailTemplet ");
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
            builder.Append(" TempletId,EmailType,EmailPriority,TagDescription,EmailDescription,EmailSubject,EmailBody ");
            builder.Append(" FROM Ms_EmailTemplet ");
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
                builder.Append("order by T.TempletId desc");
            }
            builder.Append(")AS Row, T.*  from Ms_EmailTemplet T ");
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
            return DbHelperSQL.GetMaxID("TempletId", "Ms_EmailTemplet");
        }

        public Maticsoft.Model.Ms.EmailTemplet GetModel(int TempletId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TempletId,EmailType,EmailPriority,TagDescription,EmailDescription,EmailSubject,EmailBody from Ms_EmailTemplet ");
            builder.Append(" where TempletId=@TempletId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TempletId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TempletId;
            new Maticsoft.Model.Ms.EmailTemplet();
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
            builder.Append("select count(1) FROM Ms_EmailTemplet ");
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

        public bool Update(Maticsoft.Model.Ms.EmailTemplet model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_EmailTemplet set ");
            builder.Append("EmailType=@EmailType,");
            builder.Append("EmailPriority=@EmailPriority,");
            builder.Append("TagDescription=@TagDescription,");
            builder.Append("EmailDescription=@EmailDescription,");
            builder.Append("EmailSubject=@EmailSubject,");
            builder.Append("EmailBody=@EmailBody");
            builder.Append(" where TempletId=@TempletId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@EmailType", SqlDbType.NVarChar, 100), new SqlParameter("@EmailPriority", SqlDbType.Int, 4), new SqlParameter("@TagDescription", SqlDbType.NVarChar, 500), new SqlParameter("@EmailDescription", SqlDbType.NVarChar, 500), new SqlParameter("@EmailSubject", SqlDbType.NVarChar, 0x400), new SqlParameter("@EmailBody", SqlDbType.NText), new SqlParameter("@TempletId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.EmailType;
            cmdParms[1].Value = model.EmailPriority;
            cmdParms[2].Value = model.TagDescription;
            cmdParms[3].Value = model.EmailDescription;
            cmdParms[4].Value = model.EmailSubject;
            cmdParms[5].Value = model.EmailBody;
            cmdParms[6].Value = model.TempletId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

