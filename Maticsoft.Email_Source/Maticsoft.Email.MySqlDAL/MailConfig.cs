namespace Maticsoft.Email.MySqlDAL
{
    using Maticsoft.DBUtility;
    using Maticsoft.Email.IDAL;
    using Maticsoft.Email.Model;
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
    using System.Text;

    public class MailConfig : IMailConfig
    {
        public int Add(Maticsoft.Email.Model.MailConfig model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_MailConfig(");
            builder.Append("UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL)");
            builder.Append(" values (");
            builder.Append("?UserID,?Mailaddress,?Username,?Password,?SMTPServer,?SMTPPort,?SMTPSSL,?POPServer,?POPPort,?POPSSL)");
            builder.Append(";select ??IDENTITY");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?Mailaddress", MySqlDbType.VarChar, 100), new MySqlParameter("?Username", MySqlDbType.VarChar, 50), new MySqlParameter("?Password", MySqlDbType.VarChar, 50), new MySqlParameter("?SMTPServer", MySqlDbType.VarChar, 50), new MySqlParameter("?SMTPPort", MySqlDbType.Int32, 4), new MySqlParameter("?SMTPSSL", MySqlDbType.Int16, 2), new MySqlParameter("?POPServer", MySqlDbType.VarChar, 50), new MySqlParameter("?POPPort", MySqlDbType.Int32, 4), new MySqlParameter("?POPSSL", MySqlDbType.Int16, 2) };
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
            object single = DbHelperMySQL.GetSingle(builder.ToString(), cmdParms);
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
            builder.Append(" where ID=?ID ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?ID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = ID;
            DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public bool Exists(int UserID, string Mailaddress)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_MailConfig");
            builder.Append(" where UserID=?UserID and  Mailaddress=?Mailaddress");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?Mailaddress", MySqlDbType.VarChar, 100) };
            cmdParms[0].Value = UserID;
            cmdParms[1].Value = Mailaddress;
            return DbHelperMySQL.Exists(builder.ToString(), cmdParms);
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
            return DbHelperMySQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(" ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL ");
            builder.Append(" FROM Accounts_MailConfig ");
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

        public Maticsoft.Email.Model.MailConfig GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL from Accounts_MailConfig  LIMIT 1");
            Maticsoft.Email.Model.MailConfig config = new Maticsoft.Email.Model.MailConfig();
            DataSet set = DbHelperMySQL.Query(builder.ToString());
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
            builder.Append("select   ID,UserID,Mailaddress,Username,Password,SMTPServer,SMTPPort,SMTPSSL,POPServer,POPPort,POPSSL from Accounts_MailConfig ");
            builder.Append(" where ID=?ID LIMIT 1");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?ID", MySqlDbType.Int32, 4) };
            cmdParms[0].Value = ID;
            Maticsoft.Email.Model.MailConfig config = new Maticsoft.Email.Model.MailConfig();
            DataSet set = DbHelperMySQL.Query(builder.ToString(), cmdParms);
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
            builder.Append("UserID=?UserID,");
            builder.Append("Mailaddress=?Mailaddress,");
            builder.Append("Username=?Username,");
            builder.Append("Password=?Password,");
            builder.Append("SMTPServer=?SMTPServer,");
            builder.Append("SMTPPort=?SMTPPort,");
            builder.Append("SMTPSSL=?SMTPSSL,");
            builder.Append("POPServer=?POPServer,");
            builder.Append("POPPort=?POPPort,");
            builder.Append("POPSSL=?POPSSL");
            builder.Append(" where ID=?ID ");
            MySqlParameter[] cmdParms = new MySqlParameter[] { new MySqlParameter("?ID", MySqlDbType.Int32, 4), new MySqlParameter("?UserID", MySqlDbType.Int32, 4), new MySqlParameter("?Mailaddress", MySqlDbType.VarChar, 100), new MySqlParameter("?Username", MySqlDbType.VarChar, 50), new MySqlParameter("?Password", MySqlDbType.VarChar, 50), new MySqlParameter("?SMTPServer", MySqlDbType.VarChar, 50), new MySqlParameter("?SMTPPort", MySqlDbType.Int32, 4), new MySqlParameter("?SMTPSSL", MySqlDbType.Int16, 2), new MySqlParameter("?POPServer", MySqlDbType.VarChar, 50), new MySqlParameter("?POPPort", MySqlDbType.Int32, 4), new MySqlParameter("?POPSSL", MySqlDbType.Int16, 2) };
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
            DbHelperMySQL.ExecuteSql(builder.ToString(), cmdParms);
        }
    }
}

