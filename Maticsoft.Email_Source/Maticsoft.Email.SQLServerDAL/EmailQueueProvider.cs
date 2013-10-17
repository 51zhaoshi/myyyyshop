namespace Maticsoft.Email.SQLServerDAL
{
    using Maticsoft.Common.Mail;
    using Maticsoft.DBUtility;
    using Maticsoft.Email.IDAL;
    using Maticsoft.Email.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Net.Mail;

    public class EmailQueueProvider : IEmailQueueProvider
    {
        public int DeleteQueuedEmail(int emailId)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@EmailId", SqlDbType.Int, 4) };
            parameters[0].Value = emailId;
            DbHelperSQL.RunProcedure("sp_EmailQueue_Delete", parameters, out rowsAffected);
            return rowsAffected;
        }

        public IList<EmailTemplate> DequeueEmail()
        {
            IList<EmailTemplate> list = new List<EmailTemplate>();
            SqlParameter[] parameters = new SqlParameter[0];
            DataSet set = DbHelperSQL.RunProcedure("sp_Emails_Dequeue", parameters, "ds");
            if (set.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    EmailTemplate model = new EmailTemplate();
                    this.LoadEntityData(ref model, row);
                    list.Add(model);
                }
            }
            return list;
        }

        private void LoadEntityData(ref EmailTemplate model, DataRow dr)
        {
            if (dr["EmailId"].ToString() != "")
            {
                model.EmailID = int.Parse(dr["EmailId"].ToString());
            }
            if ((dr["EmailTo"] != null) && (dr["EmailTo"].ToString() != ""))
            {
                model.EmailTo = dr["EmailTo"].ToString();
            }
            if ((dr["EmailSubject"] != null) && (dr["EmailSubject"].ToString() != ""))
            {
                model.Subject = dr["EmailSubject"].ToString();
            }
            if ((dr["EmailBody"] != null) && (dr["EmailBody"].ToString() != ""))
            {
                model.Body = dr["EmailBody"].ToString();
            }
        }

        public void QueueEmail(MailMessage message)
        {
            if (message != null)
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@EmailPriority", SqlDbType.Int, 4), new SqlParameter("@IsBodyHtml", SqlDbType.Bit, 1), new SqlParameter("@EmailTo", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@EmailCc", SqlDbType.NText), new SqlParameter("@EmailBcc", SqlDbType.NText), new SqlParameter("@EmailFrom", SqlDbType.NVarChar, 0x100), new SqlParameter("@EmailSubject", SqlDbType.NVarChar, 0x400), new SqlParameter("@EmailBody", SqlDbType.NText) };
                parameters[0].Value = message.Priority;
                parameters[1].Value = message.IsBodyHtml;
                parameters[2].Value = EmailConvert.ToDelimitedString(message.To, ",");
                parameters[3].Value = EmailConvert.ToDelimitedString(message.CC, ",");
                parameters[4].Value = EmailConvert.ToDelimitedString(message.Bcc, ",");
                parameters[5].Value = message.From.Address;
                parameters[6].Value = message.Subject;
                parameters[7].Value = message.Body;
                DbHelperSQL.RunProcedure("sp_Emails_Enqueue", parameters, out rowsAffected);
            }
        }

        public int QueueSendingFailure(IList<int> list, int failureInterval, int maxNumberOfTries)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@EmailId", SqlDbType.Int, 4), new SqlParameter("@FailureInterval", SqlDbType.Int), new SqlParameter("@MaxNumberOfTries", SqlDbType.Int) };
            foreach (int num2 in list)
            {
                parameters[0].Value = num2;
                parameters[1].Value = failureInterval;
                parameters[2].Value = maxNumberOfTries;
                DbHelperSQL.RunProcedure("sp_EmailQueue_Failure", parameters, out rowsAffected);
            }
            return rowsAffected;
        }
    }
}

