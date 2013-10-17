namespace Maticsoft.Email.MySqlDAL
{
    using Maticsoft.DBUtility;
    using Maticsoft.Email.IDAL;
    using Maticsoft.Email.Model;
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
    using System.Text;

    public class EmailQueue : IEmailQueue
    {
        public bool Add(Maticsoft.Email.Model.EmailQueue model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_EmailQueue(");
            builder.Append("EmailPriority,IsBodyHtml,EmailTo,EmailCc,EmailBcc,EmailFrom,EmailSubject,EmailBody,NextTryTime,NumberOfTries)");
            builder.Append(" values (");
            builder.Append("?EmailPriority,?IsBodyHtml,?EmailTo,?EmailCc,?EmailBcc,?EmailFrom,?EmailSubject,?EmailBody,?NextTryTime,?NumberOfTries)");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?EmailPriority", MySqlDbType.Int32, 4), new MySqlParameter("?IsBodyHtml", MySqlDbType.Bit, 1), new MySqlParameter("?EmailTo", MySqlDbType.VarChar, 0x7d0), new MySqlParameter("?EmailCc", MySqlDbType.Text), new MySqlParameter("?EmailBcc", MySqlDbType.Text), new MySqlParameter("?EmailFrom", MySqlDbType.VarChar, 0x100), new MySqlParameter("?EmailSubject", MySqlDbType.VarChar, 0x400), new MySqlParameter("?EmailBody", MySqlDbType.Text), new MySqlParameter("?NextTryTime", MySqlDbType.DateTime), new MySqlParameter("?NumberOfTries", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = model.EmailPriority;
            cmdParms[1].Value = model.IsBodyHtml;
            cmdParms[2].Value = model.EmailTo;
            cmdParms[3].Value = model.EmailCc;
            cmdParms[4].Value = model.EmailBcc;
            cmdParms[5].Value = model.EmailFrom;
            cmdParms[6].Value = model.EmailSubject;
            cmdParms[7].Value = model.EmailBody;
            cmdParms[8].Value = model.NextTryTime;
            cmdParms[9].Value = model.NumberOfTries;
            return (DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_EmailQueue ");
            builder.Append(" where ");
            MySqlParameter[] cmdParms = new MySqlParameter[0];
            return (DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select EmailId,EmailPriority,IsBodyHtml,EmailTo,EmailCc,EmailBcc,EmailFrom,EmailSubject,EmailBody,NextTryTime,NumberOfTries ");
            builder.Append(" FROM Accounts_EmailQueue ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(" EmailId,EmailPriority,IsBodyHtml,EmailTo,EmailCc,EmailBcc,EmailFrom,EmailSubject,EmailBody,NextTryTime,NumberOfTries ");
            builder.Append(" FROM Accounts_EmailQueue ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            if (Top > 0)
            {
                builder.Append(" LIMIT " + Top.ToString());
            }
            return DbHelperMySQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Accounts_EmailQueue ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append(" order by " + orderby);
            }
            else
            {
                builder.Append(" order by ContentID desc");
            }
            builder.AppendFormat(" LIMIT {0} , {1}", startIndex - 1, (endIndex - startIndex) + 1);
            return DbHelperMySQL.Query(builder.ToString());
        }

        public Maticsoft.Email.Model.EmailQueue GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  EmailId,EmailPriority,IsBodyHtml,EmailTo,EmailCc,EmailBcc,EmailFrom,EmailSubject,EmailBody,NextTryTime,NumberOfTries from Accounts_EmailQueue ");
            builder.Append(" where ");
            MySqlParameter[] cmdParms = new MySqlParameter[0];
            Maticsoft.Email.Model.EmailQueue queue = new Maticsoft.Email.Model.EmailQueue();
            DataSet set = DbHelperMySQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["EmailId"] != null) && (set.Tables[0].Rows[0]["EmailId"].ToString() != ""))
            {
                queue.EmailId = int.Parse(set.Tables[0].Rows[0]["EmailId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["EmailPriority"] != null) && (set.Tables[0].Rows[0]["EmailPriority"].ToString() != ""))
            {
                queue.EmailPriority = int.Parse(set.Tables[0].Rows[0]["EmailPriority"].ToString());
            }
            if ((set.Tables[0].Rows[0]["IsBodyHtml"] != null) && (set.Tables[0].Rows[0]["IsBodyHtml"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsBodyHtml"].ToString() == "1") || (set.Tables[0].Rows[0]["IsBodyHtml"].ToString().ToLower() == "true"))
                {
                    queue.IsBodyHtml = true;
                }
                else
                {
                    queue.IsBodyHtml = false;
                }
            }
            if ((set.Tables[0].Rows[0]["EmailTo"] != null) && (set.Tables[0].Rows[0]["EmailTo"].ToString() != ""))
            {
                queue.EmailTo = set.Tables[0].Rows[0]["EmailTo"].ToString();
            }
            if ((set.Tables[0].Rows[0]["EmailCc"] != null) && (set.Tables[0].Rows[0]["EmailCc"].ToString() != ""))
            {
                queue.EmailCc = set.Tables[0].Rows[0]["EmailCc"].ToString();
            }
            if ((set.Tables[0].Rows[0]["EmailBcc"] != null) && (set.Tables[0].Rows[0]["EmailBcc"].ToString() != ""))
            {
                queue.EmailBcc = set.Tables[0].Rows[0]["EmailBcc"].ToString();
            }
            if ((set.Tables[0].Rows[0]["EmailFrom"] != null) && (set.Tables[0].Rows[0]["EmailFrom"].ToString() != ""))
            {
                queue.EmailFrom = set.Tables[0].Rows[0]["EmailFrom"].ToString();
            }
            if ((set.Tables[0].Rows[0]["EmailSubject"] != null) && (set.Tables[0].Rows[0]["EmailSubject"].ToString() != ""))
            {
                queue.EmailSubject = set.Tables[0].Rows[0]["EmailSubject"].ToString();
            }
            if ((set.Tables[0].Rows[0]["EmailBody"] != null) && (set.Tables[0].Rows[0]["EmailBody"].ToString() != ""))
            {
                queue.EmailBody = set.Tables[0].Rows[0]["EmailBody"].ToString();
            }
            if ((set.Tables[0].Rows[0]["NextTryTime"] != null) && (set.Tables[0].Rows[0]["NextTryTime"].ToString() != ""))
            {
                queue.NextTryTime = DateTime.Parse(set.Tables[0].Rows[0]["NextTryTime"].ToString());
            }
            if ((set.Tables[0].Rows[0]["NumberOfTries"] != null) && (set.Tables[0].Rows[0]["NumberOfTries"].ToString() != ""))
            {
                queue.NumberOfTries = int.Parse(set.Tables[0].Rows[0]["NumberOfTries"].ToString());
            }
            return queue;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Accounts_EmailQueue ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperMySQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool PushEmailQueur(string uType, string uName, string EmailSubject, string EmailBody, string EmailFrom)
        {
            int rowsAffected = 0;
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("?_UserType", MySqlDbType.VarChar), new MySqlParameter("?_UserName", MySqlDbType.VarChar), new MySqlParameter("?_EmailSubject", MySqlDbType.VarChar), new MySqlParameter("?_EmailBody", MySqlDbType.VarChar), new MySqlParameter("?_EmailFrom", MySqlDbType.VarChar) };
            parameters[0].Value = uType;
            parameters[1].Value = uName;
            parameters[2].Value = EmailSubject;
            parameters[3].Value = EmailBody;
            parameters[4].Value = EmailFrom;
            DbHelperMySQL.RunProcedure("sp_Accounts_SendEmail", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Update(Maticsoft.Email.Model.EmailQueue model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_EmailQueue set ");
            builder.Append("EmailId=?EmailId,");
            builder.Append("EmailPriority=?EmailPriority,");
            builder.Append("IsBodyHtml=?IsBodyHtml,");
            builder.Append("EmailTo=?EmailTo,");
            builder.Append("EmailCc=?EmailCc,");
            builder.Append("EmailBcc=?EmailBcc,");
            builder.Append("EmailFrom=?EmailFrom,");
            builder.Append("EmailSubject=?EmailSubject,");
            builder.Append("EmailBody=?EmailBody,");
            builder.Append("NextTryTime=?NextTryTime,");
            builder.Append("NumberOfTries=?NumberOfTries");
            builder.Append(" where ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?EmailId", MySqlDbType.Int32, 4), new MySqlParameter("?EmailPriority", MySqlDbType.Int32, 4), new MySqlParameter("?IsBodyHtml", MySqlDbType.Bit, 1), new MySqlParameter("?EmailTo", MySqlDbType.VarChar, 0x7d0), new MySqlParameter("?EmailCc", MySqlDbType.Text), new MySqlParameter("?EmailBcc", MySqlDbType.Text), new MySqlParameter("?EmailFrom", MySqlDbType.VarChar, 0x100), new MySqlParameter("?EmailSubject", MySqlDbType.VarChar, 0x400), new MySqlParameter("?EmailBody", MySqlDbType.Text), new MySqlParameter("?NextTryTime", MySqlDbType.DateTime), new MySqlParameter("?NumberOfTries", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = model.EmailId;
            cmdParms[1].Value = model.EmailPriority;
            cmdParms[2].Value = model.IsBodyHtml;
            cmdParms[3].Value = model.EmailTo;
            cmdParms[4].Value = model.EmailCc;
            cmdParms[5].Value = model.EmailBcc;
            cmdParms[6].Value = model.EmailFrom;
            cmdParms[7].Value = model.EmailSubject;
            cmdParms[8].Value = model.EmailBody;
            cmdParms[9].Value = model.NextTryTime;
            cmdParms[10].Value = model.NumberOfTries;
            return (DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

