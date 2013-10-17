namespace Maticsoft.SQLServerDAL.Poll
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Poll;
    using Maticsoft.Model.Poll;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Forms : IForms
    {
        public int Add(Maticsoft.Model.Poll.Forms model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Poll_Forms(");
            builder.Append("Name,IsActive,Description)");
            builder.Append(" values (");
            builder.Append("@Name,@IsActive,@Description)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@IsActive", SqlDbType.Bit), new SqlParameter("@Description", SqlDbType.NVarChar, 300) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.IsActive;
            cmdParms[2].Value = model.Description;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

        public void Delete(int FormID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Poll_Forms ");
            builder.Append(" where FormID= " + FormID.ToString());
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from Poll_Topics where FormID= " + FormID.ToString());
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from Poll_UserPoll ");
            builder3.Append(" where TopicID in (select ID from Poll_Topics where FormID=" + FormID.ToString() + ") ");
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete from Poll_Options ");
            builder4.Append(" where TopicID in (select ID from Poll_Topics where FormID=" + FormID.ToString() + ") ");
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("delete from Poll_Reply ");
            builder5.Append(" where TopicID in (select ID from Poll_Topics where FormID=" + FormID.ToString() + ") ");
            DbHelperSQL.ExecuteSqlTran(new List<string> { builder3.ToString(), builder4.ToString(), builder5.ToString(), builder2.ToString(), builder.ToString() });
        }

        public bool DeleteList(string FormIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Poll_Forms ");
            builder.Append(" where FormID in (" + FormIDlist + ")  ");
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from Poll_Topics where FormID in (" + FormIDlist + ")  ");
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from Poll_UserPoll ");
            builder3.Append(" where TopicID in (select ID from Poll_Topics where FormID in(" + FormIDlist + ")) ");
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete from Poll_Options ");
            builder4.Append(" where TopicID in (select ID from Poll_Topics where FormID in(" + FormIDlist + ")) ");
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("delete from Poll_Reply ");
            builder5.Append(" where TopicID in (select ID from Poll_Topics where FormID in(" + FormIDlist + ")) ");
            return (DbHelperSQL.ExecuteSqlTran(new List<string> { builder3.ToString(), builder4.ToString(), builder5.ToString(), builder2.ToString(), builder.ToString() }) > 0);
        }

        public bool Exists(int FormID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Poll_Forms");
            builder.Append(" where FormID=@FormID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FormID", SqlDbType.Int, 4) };
            cmdParms[0].Value = FormID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Poll_Forms ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("FormID", "Poll_Forms");
        }

        public Maticsoft.Model.Poll.Forms GetModel(int FormID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from Poll_Forms ");
            builder.Append(" where FormID=@FormID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FormID", SqlDbType.Int, 4) };
            cmdParms[0].Value = FormID;
            Maticsoft.Model.Poll.Forms forms = new Maticsoft.Model.Poll.Forms();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["FormID"].ToString() != "")
            {
                forms.FormID = int.Parse(set.Tables[0].Rows[0]["FormID"].ToString());
            }
            forms.Name = set.Tables[0].Rows[0]["Name"].ToString();
            forms.Description = set.Tables[0].Rows[0]["Description"].ToString();
            if ((set.Tables[0].Rows[0]["IsActive"] != null) && (set.Tables[0].Rows[0]["IsActive"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsActive"].ToString() == "1") || (set.Tables[0].Rows[0]["IsActive"].ToString().ToLower() == "true"))
                {
                    forms.IsActive = true;
                    return forms;
                }
                forms.IsActive = false;
            }
            return forms;
        }

        public int Update(Maticsoft.Model.Poll.Forms model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Poll_Forms set ");
            builder.Append("Name=@Name,");
            builder.Append("IsActive=@IsActive,");
            builder.Append("Description=@Description");
            builder.Append(" where FormID=@FormID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FormID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@IsActive", SqlDbType.Bit), new SqlParameter("@Description", SqlDbType.NVarChar, 300) };
            cmdParms[0].Value = model.FormID;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.IsActive;
            cmdParms[3].Value = model.Description;
            return DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

