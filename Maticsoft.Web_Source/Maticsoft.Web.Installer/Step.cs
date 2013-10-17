namespace Maticsoft.Web.Installer
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Model.Members;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    public class Step : Page
    {
        protected CheckBox chkSample;
        protected HtmlForm form1;
        protected Image HdImg;
        private const string IMAGEOK = "/Installer/images/ok.gif";
        protected Literal lbldblogin;
        protected Literal lbldbName;
        protected Literal lbldbpasword;
        protected Literal lbldbServer;
        protected Label lblErrMessage;
        protected Literal lblPwd;
        protected Literal lblPwdAgain;
        protected Literal lblUserName;
        protected Literal Literal1;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal literalPort;
        protected Literal litPassword;
        protected Label litSetpErrorMessage;
        protected TextBox txtdbLogin;
        protected TextBox txtdbName;
        protected TextBox txtdbPassWord;
        protected TextBox txtdbPort;
        protected TextBox txtdbServer;
        protected TextBox txtDesciption;
        protected TextBox txtEmail;
        protected TextBox txtPassword;
        protected TextBox txtPasswordCompare;
        protected TextBox txtSiteName;
        protected TextBox txtUserName;
        protected Wizard wizard1;
        protected WizardStep WizardStep1;
        protected WizardStep WizardStep2;

        public bool AddUser(Maticsoft.Model.Members.Users model)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand command = new SqlCommand {
                CommandType = CommandType.Text,
                Connection = connection
            };
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_Users(");
            builder.Append("UserName,Password,NickName,Email,EmployeeID,DepartmentID,Activity,UserType,Style,User_dateCreate)");
            builder.Append(" values (");
            builder.Append("@UserName,@Password,@NickName,@Email,@EmployeeID,@DepartmentID,@Activity,@UserType,@Style,@User_dateCreate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.VarChar, 50), new SqlParameter("@Password", SqlDbType.Binary, 20), new SqlParameter("@NickName", SqlDbType.VarChar, 50), new SqlParameter("@Email", SqlDbType.VarChar, 100), new SqlParameter("@EmployeeID", SqlDbType.Int, 4), new SqlParameter("@DepartmentID", SqlDbType.VarChar, 15), new SqlParameter("@Activity", SqlDbType.Bit, 1), new SqlParameter("@UserType", SqlDbType.Char, 2), new SqlParameter("@Style", SqlDbType.Int, 4), new SqlParameter("@User_dateCreate", SqlDbType.DateTime) };
            parameterArray[0].Value = model.UserName;
            parameterArray[1].Value = model.Password;
            parameterArray[2].Value = model.NickName;
            parameterArray[3].Value = model.Email;
            parameterArray[4].Value = model.EmployeeID;
            parameterArray[5].Value = model.DepartmentID;
            parameterArray[6].Value = model.Activity;
            parameterArray[7].Value = model.UserType;
            parameterArray[8].Value = model.Style;
            parameterArray[9].Value = model.User_dateCreate;
            command.CommandText = builder.ToString();
            foreach (SqlParameter parameter in parameterArray)
            {
                if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
                command.Parameters.Add(parameter);
            }
            connection.Open();
            int num = command.ExecuteNonQuery();
            connection.Close();
            return (num > 0);
        }

        public bool AddUserExp(UsersExpModel model)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand command = new SqlCommand {
                CommandType = CommandType.Text,
                Connection = connection
            };
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UsersExp(");
            builder.Append("UserID,Gravatar,Singature,TelPhone,QQ,MSN,HomePage,Birthday,BirthdayVisible,BirthdayIndexVisible,Constellation,ConstellationVisible,ConstellationIndexVisible,NativePlace,NativePlaceVisible,NativePlaceIndexVisible,RegionId,Address,AddressVisible,AddressIndexVisible,BodilyForm,BodilyFormVisible,BodilyFormIndexVisible,BloodType,BloodTypeVisible,BloodTypeIndexVisible,Marriaged,MarriagedVisible,MarriagedIndexVisible,PersonalStatus,PersonalStatusVisible,PersonalStatusIndexVisible,Grade,Balance,Points,TopicCount,ReplyTopicCount,FavTopicCount,PvCount,FansCount,FellowCount,AblumsCount,FavouritesCount,FavoritedCount,ShareCount,ProductsCount,PersonalDomain,LastAccessTime,LastAccessIP,LastPostTime,LastLoginTime,Remark,IsUserDPI,PayAccount)");
            builder.Append(" values (");
            builder.Append("@UserID,@Gravatar,@Singature,@TelPhone,@QQ,@MSN,@HomePage,@Birthday,@BirthdayVisible,@BirthdayIndexVisible,@Constellation,@ConstellationVisible,@ConstellationIndexVisible,@NativePlace,@NativePlaceVisible,@NativePlaceIndexVisible,@RegionId,@Address,@AddressVisible,@AddressIndexVisible,@BodilyForm,@BodilyFormVisible,@BodilyFormIndexVisible,@BloodType,@BloodTypeVisible,@BloodTypeIndexVisible,@Marriaged,@MarriagedVisible,@MarriagedIndexVisible,@PersonalStatus,@PersonalStatusVisible,@PersonalStatusIndexVisible,@Grade,@Balance,@Points,@TopicCount,@ReplyTopicCount,@FavTopicCount,@PvCount,@FansCount,@FellowCount,@AblumsCount,@FavouritesCount,@FavoritedCount,@ShareCount,@ProductsCount,@PersonalDomain,@LastAccessTime,@LastAccessIP,@LastPostTime,@LastLoginTime,@Remark,@IsUserDPI,@PayAccount)");
            SqlParameter[] parameterArray = new SqlParameter[] { 
                new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Gravatar", SqlDbType.VarChar, 200), new SqlParameter("@Singature", SqlDbType.NVarChar, 200), new SqlParameter("@TelPhone", SqlDbType.VarChar, 20), new SqlParameter("@QQ", SqlDbType.VarChar, 30), new SqlParameter("@MSN", SqlDbType.VarChar, 30), new SqlParameter("@HomePage", SqlDbType.VarChar, 50), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@BirthdayVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BirthdayIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Constellation", SqlDbType.VarChar, 10), new SqlParameter("@ConstellationVisible", SqlDbType.SmallInt, 2), new SqlParameter("@ConstellationIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@NativePlace", SqlDbType.NVarChar, 300), new SqlParameter("@NativePlaceVisible", SqlDbType.SmallInt, 2), new SqlParameter("@NativePlaceIndexVisible", SqlDbType.Bit, 1), 
                new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Address", SqlDbType.NVarChar, 300), new SqlParameter("@AddressVisible", SqlDbType.SmallInt, 2), new SqlParameter("@AddressIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@BodilyForm", SqlDbType.NVarChar, 10), new SqlParameter("@BodilyFormVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BodilyFormIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@BloodType", SqlDbType.NVarChar, 10), new SqlParameter("@BloodTypeVisible", SqlDbType.SmallInt, 2), new SqlParameter("@BloodTypeIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@Marriaged", SqlDbType.NVarChar, 10), new SqlParameter("@MarriagedVisible", SqlDbType.SmallInt, 2), new SqlParameter("@MarriagedIndexVisible", SqlDbType.Bit, 1), new SqlParameter("@PersonalStatus", SqlDbType.NVarChar, 10), new SqlParameter("@PersonalStatusVisible", SqlDbType.SmallInt, 2), new SqlParameter("@PersonalStatusIndexVisible", SqlDbType.Bit, 1), 
                new SqlParameter("@Grade", SqlDbType.Int, 4), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@TopicCount", SqlDbType.Int, 4), new SqlParameter("@ReplyTopicCount", SqlDbType.Int, 4), new SqlParameter("@FavTopicCount", SqlDbType.Int, 4), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@FansCount", SqlDbType.Int, 4), new SqlParameter("@FellowCount", SqlDbType.Int, 4), new SqlParameter("@AblumsCount", SqlDbType.Int, 4), new SqlParameter("@FavouritesCount", SqlDbType.Int, 4), new SqlParameter("@FavoritedCount", SqlDbType.Int, 4), new SqlParameter("@ShareCount", SqlDbType.Int, 4), new SqlParameter("@ProductsCount", SqlDbType.Int, 4), new SqlParameter("@PersonalDomain", SqlDbType.NVarChar, 50), new SqlParameter("@LastAccessTime", SqlDbType.DateTime), 
                new SqlParameter("@LastAccessIP", SqlDbType.VarChar, 50), new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@LastLoginTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.VarChar), new SqlParameter("@IsUserDPI", SqlDbType.Bit, 1), new SqlParameter("@PayAccount", SqlDbType.NVarChar, 200)
             };
            parameterArray[0].Value = model.UserID;
            parameterArray[1].Value = model.Gravatar;
            parameterArray[2].Value = model.Singature;
            parameterArray[3].Value = model.TelPhone;
            parameterArray[4].Value = model.QQ;
            parameterArray[5].Value = model.MSN;
            parameterArray[6].Value = model.HomePage;
            parameterArray[7].Value = model.Birthday;
            parameterArray[8].Value = model.BirthdayVisible;
            parameterArray[9].Value = model.BirthdayIndexVisible;
            parameterArray[10].Value = model.Constellation;
            parameterArray[11].Value = model.ConstellationVisible;
            parameterArray[12].Value = model.ConstellationIndexVisible;
            parameterArray[13].Value = model.NativePlace;
            parameterArray[14].Value = model.NativePlaceVisible;
            parameterArray[15].Value = model.NativePlaceIndexVisible;
            parameterArray[0x10].Value = model.RegionId;
            parameterArray[0x11].Value = model.Address;
            parameterArray[0x12].Value = model.AddressVisible;
            parameterArray[0x13].Value = model.AddressIndexVisible;
            parameterArray[20].Value = model.BodilyForm;
            parameterArray[0x15].Value = model.BodilyFormVisible;
            parameterArray[0x16].Value = model.BodilyFormIndexVisible;
            parameterArray[0x17].Value = model.BloodType;
            parameterArray[0x18].Value = model.BloodTypeVisible;
            parameterArray[0x19].Value = model.BloodTypeIndexVisible;
            parameterArray[0x1a].Value = model.Marriaged;
            parameterArray[0x1b].Value = model.MarriagedVisible;
            parameterArray[0x1c].Value = model.MarriagedIndexVisible;
            parameterArray[0x1d].Value = model.PersonalStatus;
            parameterArray[30].Value = model.PersonalStatusVisible;
            parameterArray[0x1f].Value = model.PersonalStatusIndexVisible;
            parameterArray[0x20].Value = model.Grade;
            parameterArray[0x21].Value = model.Balance;
            parameterArray[0x22].Value = model.Points;
            parameterArray[0x23].Value = model.TopicCount;
            parameterArray[0x24].Value = model.ReplyTopicCount;
            parameterArray[0x25].Value = model.FavTopicCount;
            parameterArray[0x26].Value = model.PvCount;
            parameterArray[0x27].Value = model.FansCount;
            parameterArray[40].Value = model.FellowCount;
            parameterArray[0x29].Value = model.AblumsCount;
            parameterArray[0x2a].Value = model.FavouritesCount;
            parameterArray[0x2b].Value = model.FavoritedCount;
            parameterArray[0x2c].Value = model.ShareCount;
            parameterArray[0x2d].Value = model.ProductsCount;
            parameterArray[0x2e].Value = model.PersonalDomain;
            parameterArray[0x2f].Value = model.LastAccessTime;
            parameterArray[0x30].Value = model.LastAccessIP;
            parameterArray[0x31].Value = model.LastPostTime;
            parameterArray[50].Value = model.LastLoginTime;
            parameterArray[0x33].Value = model.Remark;
            parameterArray[0x34].Value = model.IsUserDPI;
            parameterArray[0x35].Value = model.PayAccount;
            command.CommandText = builder.ToString();
            foreach (SqlParameter parameter in parameterArray)
            {
                if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
                command.Parameters.Add(parameter);
            }
            connection.Open();
            int num = command.ExecuteNonQuery();
            connection.Close();
            return (num > 0);
        }

        public bool AddUserRoles(int userId, int roleId)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand command = new SqlCommand {
                CommandType = CommandType.Text,
                Connection = connection
            };
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UserRoles(");
            builder.Append("UserID,RoleID)");
            builder.Append(" values (");
            builder.Append("@UserID,@RoleID)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] values = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RoleID", SqlDbType.Int, 4) };
            values[0].Value = userId;
            values[1].Value = roleId;
            command.CommandText = builder.ToString();
            command.Parameters.AddRange(values);
            connection.Open();
            int num = command.ExecuteNonQuery();
            connection.Close();
            return (num > 0);
        }

        protected void btnDownConfig_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration(base.Request.ApplicationPath);
            configuration.ConnectionStrings.ConnectionStrings.Clear();
            ConnectionStringSettings settings = new ConnectionStringSettings("MaticsoftSqlServer", this.ConnectionString, "System.Data.SqlClient");
            configuration.ConnectionStrings.ConnectionStrings.Add(settings);
            configuration.ConnectionStrings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            XmlDocument document = new XmlDocument();
            document.LoadXml(configuration.ConnectionStrings.SectionInformation.GetRawXml());
            XmlNode node = document.SelectSingleNode("//connectionStrings");
            node.ChildNodes[0].Attributes["name"].Value = "MaticsoftSqlServer";
            node.ChildNodes[0].Attributes["connectionString"].Value = this.ConnectionString;
            node.ChildNodes[0].Attributes["providerName"].Value = "System.Data.SqlClient";
            XmlNode encryptConnectionStringNode = configuration.ConnectionStrings.SectionInformation.ProtectionProvider.Encrypt(node);
            base.Response.Clear();
            base.Response.ClearHeaders();
            base.Response.Buffer = false;
            base.Response.ContentEncoding = Encoding.UTF8;
            base.Response.ContentType = "application/octet-stream";
            base.Response.AppendHeader("Content-Disposition", "attachment;filename=Web.config");
            base.Response.BinaryWrite(this.InitStream(encryptConnectionStringNode));
            base.Response.Flush();
            base.Response.End();
        }

        private bool CreateDatabase(out string message)
        {
            message = "";
            return true;
        }

        private bool CreateDemo(out string message)
        {
            string path = base.Request.MapPath("SqlScripts/SiteDemo.Sql");
            if (!File.Exists(path))
            {
                message = "";
                return false;
            }
            return this.ExecuteScriptFile(path, out message);
        }

        private bool CreateInitData(out string message)
        {
            string path = base.Request.MapPath("SqlScripts/SiteInitData.Sql");
            if (!File.Exists(path))
            {
                message = "初始化数据文件不存在！";
                return false;
            }
            return this.ExecuteScriptFile(path, out message);
        }

        private string CreateKey(int len)
        {
            byte[] data = new byte[len];
            new RNGCryptoServiceProvider().GetBytes(data);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(string.Format("{0:X2}", data[i]));
            }
            return builder.ToString();
        }

        private bool CreateSite(out string message)
        {
            message = string.Empty;
            string host = base.Request.Url.Host;
            if (!this.UpdateConfigFile(out message))
            {
                return false;
            }
            string errorMsg = string.Empty;
            if (!this.CreateUser(out errorMsg))
            {
                message = errorMsg;
                return false;
            }
            this.UpdateSiteDescription(host, Globals.HtmlEncode(this.txtSiteName.Text.Trim()), Globals.HtmlEncode(this.txtDesciption.Text.Trim()));
            message = "";
            return true;
        }

        private bool CreateUser(out string errorMsg)
        {
            try
            {
                Maticsoft.Model.Members.Users users;
                errorMsg = "";
                users = new Maticsoft.Model.Members.Users {
                    Activity = true,
                    DepartmentID = "",
                    EmployeeID = -1,
                    Email = this.txtEmail.Text.Trim(),
                    UserName = users.NickName = this.txtUserName.Text.Trim(),
                    Password = AccountsPrincipal.EncryptPassword(this.txtPassword.Text),
                    UserType = "AA",
                    Style = 1,
                    User_dateCreate = new DateTime?(DateTime.Now)
                };
                if (!this.AddUser(users))
                {
                    errorMsg = "创建管理员帐号失败";
                    return false;
                }
                UsersExp model = new UsersExp {
                    UserID = 1,
                    BirthdayVisible = 0,
                    BirthdayIndexVisible = false,
                    Gravatar = "/Upload/User/Gravatar/1",
                    ConstellationVisible = 0,
                    ConstellationIndexVisible = false,
                    NativePlaceVisible = 0,
                    NativePlaceIndexVisible = false,
                    RegionId = 0,
                    AddressVisible = 0,
                    AddressIndexVisible = false,
                    BodilyFormVisible = 0,
                    BodilyFormIndexVisible = false,
                    BloodTypeVisible = 0,
                    BloodTypeIndexVisible = false,
                    MarriagedVisible = 0,
                    MarriagedIndexVisible = false,
                    PersonalStatusVisible = 0,
                    PersonalStatusIndexVisible = false,
                    LastAccessIP = "",
                    LastAccessTime = new DateTime?(DateTime.Now),
                    LastLoginTime = DateTime.Now,
                    LastPostTime = new DateTime?(DateTime.Now)
                };
                if (!this.AddUserExp(model))
                {
                    errorMsg = "创建管理员帐号扩展数据添加失败";
                    return false;
                }
                if (!this.AddUserRoles(1, 1))
                {
                    errorMsg = "创建管理员角色数据失败";
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                errorMsg = exception.Message;
                return false;
            }
        }

        private bool ExecuteScriptFile(string pathToScriptFile, out string message)
        {
            bool flag;
            try
            {
                string applicationPath = Globals.ApplicationPath;
                using (StreamReader reader = new StreamReader(pathToScriptFile))
                {
                    SqlConnection connection = new SqlConnection(this.ConnectionString);
                    SqlCommand command = new SqlCommand {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    };
                    connection.Open();
                    while (!reader.EndOfStream)
                    {
                        string str2 = NextSqlFromStream(reader);
                        if (!string.IsNullOrWhiteSpace(str2))
                        {
                            command.CommandText = str2.Replace("$VirsualPath$", applicationPath);
                            command.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                    connection.Close();
                    message = "";
                    flag = true;
                }
            }
            catch (SqlException exception)
            {
                message = exception.Message;
                flag = false;
            }
            return flag;
        }

        private RijndaelManaged GetCryptographer()
        {
            RijndaelManaged managed = new RijndaelManaged {
                KeySize = 0x80
            };
            managed.GenerateIV();
            managed.GenerateKey();
            return managed;
        }

        private byte[] InitStream(XmlNode encryptConnectionStringNode)
        {
            string filename = base.Request.MapPath(Globals.ApplicationPath + "/web.config");
            XmlDocument document = new XmlDocument {
                PreserveWhitespace = true
            };
            document.Load(filename);
            XmlNode node = document.SelectSingleNode("configuration/connectionStrings");
            XmlNode node2 = document.SelectSingleNode("configuration/appSettings");
            node.RemoveAll();
            XmlAttribute attribute = document.CreateAttribute("configProtectionProvider");
            attribute.Value = "DataProtectionConfigurationProvider";
            node.Attributes.Append(attribute);
            XmlNode newChild = document.CreateElement("EncryptedData");
            newChild.InnerText = encryptConnectionStringNode.InnerText;
            newChild.InnerXml = encryptConnectionStringNode.InnerXml;
            node.AppendChild(newChild);
            XmlNode oldChild = node2.SelectSingleNode("add[@key='Installer']");
            node2.RemoveChild(oldChild);
            XmlNode node5 = node2.SelectSingleNode("add[@key='Key']");
            XmlNode node6 = node2.SelectSingleNode("add[@key='IV']");
            using (RijndaelManaged managed = this.GetCryptographer())
            {
                node5.Attributes["value"].Value = Convert.ToBase64String(managed.Key);
                node6.Attributes["value"].Value = Convert.ToBase64String(managed.IV);
            }
            XmlNode node7 = document.SelectSingleNode("//machineKey");
            node7.Attributes["validationKey"].Value = this.CreateKey(20);
            node7.Attributes["decryptionKey"].Value = this.CreateKey(0x18);
            return Encoding.UTF8.GetBytes(document.OuterXml);
        }

        private static string NextSqlFromStream(StreamReader reader)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                string strA = reader.ReadLine().Trim();
                while (!reader.EndOfStream && (string.Compare(strA, "GO", true, CultureInfo.InvariantCulture) != 0))
                {
                    builder.Append(strA + Environment.NewLine);
                    strA = reader.ReadLine();
                }
                if (string.Compare(strA, "GO", true, CultureInfo.InvariantCulture) != 0)
                {
                    builder.Append(strA + Environment.NewLine);
                }
                return builder.ToString();
            }
            catch
            {
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (MvcApplication.IsInstall)
                {
                    base.Response.Redirect("/", true);
                }
                else if ((this.Session["Install"] == null) || (this.Session["Install"].ToString() != "Checked"))
                {
                    base.Response.Redirect("/Installer/Check.aspx");
                }
            }
        }

        private void ShowMessage(Label lblMessage, string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }

        private bool TestConnection(out string message)
        {
            try
            {
                SqlConnection connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                connection.Close();
                message = null;
                return true;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
        }

        private bool UpdateConfigFile(out string message)
        {
            message = "";
            try
            {
                string str;
                System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration(base.Request.ApplicationPath);
                if (((str = MvcApplication.ProductInfo) != null) && (str == "Maticsoft Shop"))
                {
                    configuration.ConnectionStrings.ConnectionStrings["MaticsoftSqlServer"].ConnectionString = this.ConnectionString;
                }
                configuration.AppSettings.Settings["ConnectionString"].Value = this.ConnectionString;
                configuration.AppSettings.Settings["Installer"].Value = "True";
                MvcApplication.IsInstall = true;
                configuration.Save();
                ConfigurationManager.RefreshSection("AppSettings");
                ConfigSystem.UpdateConnectionString(this.ConnectionString);
                return true;
            }
            catch (NullReferenceException)
            {
                message = "web.config文件更新失败! 请还原web.config文件为安装包内的原版重试!";
                return false;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
        }

        private void UpdateSiteDescription(string siteUrl, string siteName, string siteDescription)
        {
            this.UpdateSiteInfo("WebName", siteName, 1, "站点信息");
            this.UpdateSiteInfo("BaseHost", siteUrl, 1, "站点域名");
            this.UpdateSiteInfo("Description", siteDescription, 1, "站点描述");
        }

        private bool UpdateSiteInfo(string key, string value, int type = 1, string Desc = "")
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            SqlCommand command = new SqlCommand {
                CommandType = CommandType.Text,
                Connection = connection
            };
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_Config_System set ");
            builder.Append("Value=@Value,KeyType=@KeyType,Description=@Description ");
            builder.Append(" where Keyname=@Keyname ");
            SqlParameter[] values = new SqlParameter[] { new SqlParameter("@Keyname", SqlDbType.VarChar, 50), new SqlParameter("@Value", SqlDbType.VarChar), new SqlParameter("@KeyType", SqlDbType.Int), new SqlParameter("@Description", SqlDbType.VarChar, 200) };
            values[0].Value = key;
            values[1].Value = value;
            values[2].Value = type;
            values[3].Value = Desc;
            command.CommandText = builder.ToString();
            command.Parameters.AddRange(values);
            connection.Open();
            int num = command.ExecuteNonQuery();
            connection.Close();
            return (num > 0);
        }

        private bool ValidateAdministrator(out string message)
        {
            message = string.Empty;
            bool flag = true;
            if (string.IsNullOrWhiteSpace(this.txtUserName.Text.Trim()))
            {
                message = "用户名不能为空<br/>";
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(this.txtEmail.Text))
            {
                message = message + "邮箱不能为空 <br/>";
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(this.txtPassword.Text.Trim()))
            {
                message = message + "密码不能为空 <br/>";
                flag = false;
            }
            if (this.txtPassword.Text != this.txtPasswordCompare.Text)
            {
                message = message + "确认密码不匹配 <br/>";
                flag = false;
            }
            return flag;
        }

        private bool ValidateConnectionString(out string message)
        {
            message = string.Empty;
            bool flag = true;
            if (string.IsNullOrWhiteSpace(this.txtdbServer.Text.Trim()))
            {
                flag = false;
                message = "数据库服务器不能为空 <br/>";
            }
            if (string.IsNullOrWhiteSpace(this.txtdbName.Text.Trim()))
            {
                flag = false;
                message = message + "数据库名不能为空 <br/>";
            }
            if (string.IsNullOrWhiteSpace(this.txtdbLogin.Text.Trim()))
            {
                flag = false;
                message = message + "数据库用户名不能为空<br/>";
            }
            if (string.IsNullOrWhiteSpace(this.txtdbPassWord.Text.Trim()))
            {
                flag = false;
                message = message + "数据库登录密码不能为空 <br/>";
            }
            return flag;
        }

        protected void wizardInstaller_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            this.Page.Response.Redirect(base.ResolveUrl("~/"), true);
        }

        protected void wizardInstaller_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            string str;
            if (e.CurrentStepIndex == 0)
            {
                if (!this.ValidateConnectionString(out str))
                {
                    this.ShowMessage(this.lblErrMessage, str);
                    e.Cancel = true;
                    return;
                }
                this.litPassword.Text = this.txtdbPassWord.Text;
                if (!this.TestConnection(out str))
                {
                    this.ShowMessage(this.lblErrMessage, str);
                    e.Cancel = true;
                    return;
                }
                this.HdImg.ImageUrl = "/Installer/images/i10.jpg";
            }
            if (e.CurrentStepIndex == 1)
            {
                if (!this.ValidateAdministrator(out str))
                {
                    this.ShowMessage(this.litSetpErrorMessage, str);
                    e.Cancel = true;
                }
                else if (!this.CreateInitData(out str))
                {
                    this.ShowMessage(this.litSetpErrorMessage, str);
                    e.Cancel = true;
                }
                else if (!this.CreateSite(out str))
                {
                    this.ShowMessage(this.litSetpErrorMessage, str);
                    e.Cancel = true;
                }
                else if ((this.chkSample.Checked && File.Exists(base.Server.MapPath("/Installer/SqlScripts/SiteDemo.Sql"))) && !this.CreateDemo(out str))
                {
                    this.ShowMessage(this.litSetpErrorMessage, str);
                    e.Cancel = true;
                }
                else
                {
                    GenerateHtml.GenImageJs();
                    base.Response.Redirect("Complete.aspx?type=complete");
                }
            }
        }

        private string ConnectionString
        {
            get
            {
                string str = this.txtdbServer.Text.Trim();
                string str2 = this.txtdbPort.Text.Trim();
                if (str2 == "1433")
                {
                    str2 = null;
                }
                string str3 = this.txtdbName.Text.Trim();
                string str4 = this.txtdbLogin.Text.Trim();
                string text = this.litPassword.Text;
                return string.Format(CultureInfo.InvariantCulture, "server={0};uid={1};pwd={2};database={3}", new object[] { string.IsNullOrWhiteSpace(str2) ? str : (str + "," + str2), str4, text, str3 });
            }
        }
    }
}

