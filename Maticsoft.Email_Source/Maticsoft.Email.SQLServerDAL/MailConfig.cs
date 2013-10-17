namespace Maticsoft.Email.SQLServerDAL
{
    using Maticsoft.DBUtility;
    using Maticsoft.Email.IDAL;
    using Maticsoft.Email.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MailConfig : IMailConfig
    {
        public int Add(Maticsoft.Email.Model.MailConfig model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_MailConfig(");
            builder.Append("UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL)");
            builder.Append(" values (");
            builder.Append("@UserID,@Mailaddress,@Username,@Password,@SMTPServer,@SMTPPort,@SMTPSSL,@POPServer,@POPPort,@POPSSL)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Mailaddress", SqlDbType.NVarChar, 100), new SqlParameter("@Username", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.NVarChar, 50), new SqlParameter("@SMTPServer", SqlDbType.NVarChar, 50), new SqlParameter("@SMTPPort", SqlDbType.Int, 4), new SqlParameter("@SMTPSSL", SqlDbType.SmallInt, 2), new SqlParameter("@POPServer", SqlDbType.NVarChar, 50), new SqlParameter("@POPPort", SqlDbType.Int, 4), new SqlParameter("@POPSSL", SqlDbType.SmallInt, 2) };
            cmdParms[0].Value = model.UserID;
            cmdParms[1].Value = model.Mailaddress;
            cmdParms[2].Value = model.Username;
            cmdParms[3].Value = model.Password;
            cmdParms[4].Value = model.SMTPServer;
            cmdParms[5].Value = model.SMTPPort;
            cmdParms[6].Value = model.SMTPSSL ? 1 : 0;
            cmdParms[7].Value = model.POPServer;
            cmdParms[8].Value = model.POPPort;
            cmdParms[9].Value = model.POPSSL ? 1 : 0;
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
            builder.Append("delete from Accounts_MailConfig ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(int UserID, string Mailaddress)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_MailConfig");
            builder.Append(" where UserID=@UserID and  Mailaddress=@Mailaddress");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Mailaddress", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = Mailaddress;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL ");
            builder.Append(" FROM Accounts_MailConfig ");
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
            builder.Append(" ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL ");
            builder.Append(" FROM Accounts_MailConfig ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Email.Model.MailConfig GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL from Accounts_MailConfig ");
            Maticsoft.Email.Model.MailConfig config = new Maticsoft.Email.Model.MailConfig();
            DataSet set = DbHelperSQL.Query(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ID"].ToString() != "")
            {
                config.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            if (set.Tables[0].Rows[0]["UserID"].ToString() != "")
            {
                config.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            config.Mailaddress = set.Tables[0].Rows[0]["Mailaddress"].ToString();
            config.Username = set.Tables[0].Rows[0]["Username"].ToString();
            config.Password = set.Tables[0].Rows[0]["Password"].ToString();
            config.SMTPServer = set.Tables[0].Rows[0]["SMTPServer"].ToString();
            if (set.Tables[0].Rows[0]["SMTPPort"].ToString() != "")
            {
                config.SMTPPort = int.Parse(set.Tables[0].Rows[0]["SMTPPort"].ToString());
            }
            if (set.Tables[0].Rows[0]["SMTPSSL"].ToString() != "")
            {
                config.SMTPSSL = set.Tables[0].Rows[0]["SMTPSSL"].ToString() == "1";
            }
            config.POPServer = set.Tables[0].Rows[0]["POPServer"].ToString();
            if (set.Tables[0].Rows[0]["POPPort"].ToString() != "")
            {
                config.POPPort = int.Parse(set.Tables[0].Rows[0]["POPPort"].ToString());
            }
            if (set.Tables[0].Rows[0]["POPSSL"].ToString() != "")
            {
                config.POPSSL = set.Tables[0].Rows[0]["POPSSL"].ToString() == "1";
            }
            return config;
        }

        public Maticsoft.Email.Model.MailConfig GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL from Accounts_MailConfig ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Email.Model.MailConfig config = new Maticsoft.Email.Model.MailConfig();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ID"].ToString() != "")
            {
                config.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            if (set.Tables[0].Rows[0]["UserID"].ToString() != "")
            {
                config.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            config.Mailaddress = set.Tables[0].Rows[0]["Mailaddress"].ToString();
            config.Username = set.Tables[0].Rows[0]["Username"].ToString();
            config.Password = set.Tables[0].Rows[0]["Password"].ToString();
            config.SMTPServer = set.Tables[0].Rows[0]["SMTPServer"].ToString();
            if (set.Tables[0].Rows[0]["SMTPPort"].ToString() != "")
            {
                config.SMTPPort = int.Parse(set.Tables[0].Rows[0]["SMTPPort"].ToString());
            }
            if (set.Tables[0].Rows[0]["SMTPSSL"].ToString() != "")
            {
                config.SMTPSSL = set.Tables[0].Rows[0]["SMTPSSL"].ToString() == "1";
            }
            config.POPServer = set.Tables[0].Rows[0]["POPServer"].ToString();
            if (set.Tables[0].Rows[0]["POPPort"].ToString() != "")
            {
                config.POPPort = int.Parse(set.Tables[0].Rows[0]["POPPort"].ToString());
            }
            if (set.Tables[0].Rows[0]["POPSSL"].ToString() != "")
            {
                config.POPSSL = set.Tables[0].Rows[0]["POPSSL"].ToString() == "1";
            }
            return config;
        }

        public void Update(Maticsoft.Email.Model.MailConfig model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_MailConfig set ");
            builder.Append("UserID=@UserID,");
            builder.Append("Mailaddress=@Mailaddress,");
            builder.Append("Username=@Username,");
            builder.Append("Password=@Password,");
            builder.Append("SMTPServer=@SMTPServer,");
            builder.Append("SMTPPort=@SMTPPort,");
            builder.Append("SMTPSSL=@SMTPSSL,");
            builder.Append("POPServer=@POPServer,");
            builder.Append("POPPort=@POPPort,");
            builder.Append("POPSSL=@POPSSL");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Mailaddress", SqlDbType.NVarChar, 100), new SqlParameter("@Username", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.NVarChar, 50), new SqlParameter("@SMTPServer", SqlDbType.NVarChar, 50), new SqlParameter("@SMTPPort", SqlDbType.Int, 4), new SqlParameter("@SMTPSSL", SqlDbType.SmallInt, 2), new SqlParameter("@POPServer", SqlDbType.NVarChar, 50), new SqlParameter("@POPPort", SqlDbType.Int, 4), new SqlParameter("@POPSSL", SqlDbType.SmallInt, 2) };
            cmdParms[0].Value = model.ID;
            cmdParms[1].Value = model.UserID;
            cmdParms[2].Value = model.Mailaddress;
            cmdParms[3].Value = model.Username;
            cmdParms[4].Value = model.Password;
            cmdParms[5].Value = model.SMTPServer;
            cmdParms[6].Value = model.SMTPPort;
            cmdParms[7].Value = model.SMTPSSL ? 1 : 0;
            cmdParms[8].Value = model.POPServer;
            cmdParms[9].Value = model.POPPort;
            cmdParms[10].Value = model.POPSSL ? 1 : 0;
            DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

