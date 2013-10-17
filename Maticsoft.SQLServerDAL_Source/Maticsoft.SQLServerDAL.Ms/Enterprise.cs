namespace Maticsoft.SQLServerDAL.Ms
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Enterprise : IEnterprise
    {
        public int Add(Maticsoft.Model.Ms.Enterprise model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Ms_Enterprise(");
            builder.Append("Name,Introduction,RegisteredCapital,TelPhone,CellPhone,ContactMail,RegionID,Address,Remark,Contact,UserName,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,EnteRank,EnteClassID,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID,Balance,AgentID)");
            builder.Append(" VALUES (");
            builder.Append("@Name,@Introduction,@RegisteredCapital,@TelPhone,@CellPhone,@ContactMail,@RegionID,@Address,@Remark,@Contact,@UserName,@EstablishedDate,@EstablishedCity,@LOGO,@Fax,@PostCode,@HomePage,@ArtiPerson,@EnteRank,@EnteClassID,@CompanyType,@BusinessLicense,@TaxNumber,@AccountBank,@AccountInfo,@ServicePhone,@QQ,@MSN,@Status,@CreatedDate,@CreatedUserID,@UpdatedDate,@UpdatedUserID,@Balance,@AgentID)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Introduction", SqlDbType.NVarChar), new SqlParameter("@RegisteredCapital", SqlDbType.Int, 4), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ContactMail", SqlDbType.NVarChar, 50), new SqlParameter("@RegionID", SqlDbType.Int, 4), new SqlParameter("@Address", SqlDbType.NVarChar, 500), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Contact", SqlDbType.NVarChar, 50), new SqlParameter("@UserName", SqlDbType.NVarChar, 30), new SqlParameter("@EstablishedDate", SqlDbType.DateTime), new SqlParameter("@EstablishedCity", SqlDbType.Int, 4), new SqlParameter("@LOGO", SqlDbType.NVarChar, 300), new SqlParameter("@Fax", SqlDbType.NVarChar, 30), new SqlParameter("@PostCode", SqlDbType.NVarChar, 10), 
                new SqlParameter("@HomePage", SqlDbType.NVarChar, 50), new SqlParameter("@ArtiPerson", SqlDbType.NVarChar, 50), new SqlParameter("@EnteRank", SqlDbType.Int, 4), new SqlParameter("@EnteClassID", SqlDbType.Int, 4), new SqlParameter("@CompanyType", SqlDbType.SmallInt, 2), new SqlParameter("@BusinessLicense", SqlDbType.NVarChar, 300), new SqlParameter("@TaxNumber", SqlDbType.NVarChar, 300), new SqlParameter("@AccountBank", SqlDbType.NVarChar, 300), new SqlParameter("@AccountInfo", SqlDbType.NVarChar, 300), new SqlParameter("@ServicePhone", SqlDbType.NVarChar, 300), new SqlParameter("@QQ", SqlDbType.NVarChar, 30), new SqlParameter("@MSN", SqlDbType.NVarChar, 30), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), 
                new SqlParameter("@UpdatedUserID", SqlDbType.Int, 4), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@AgentID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Introduction;
            cmdParms[2].Value = model.RegisteredCapital;
            cmdParms[3].Value = model.TelPhone;
            cmdParms[4].Value = model.CellPhone;
            cmdParms[5].Value = model.ContactMail;
            cmdParms[6].Value = model.RegionID;
            cmdParms[7].Value = model.Address;
            cmdParms[8].Value = model.Remark;
            cmdParms[9].Value = model.Contact;
            cmdParms[10].Value = model.UserName;
            cmdParms[11].Value = model.EstablishedDate;
            cmdParms[12].Value = model.EstablishedCity;
            cmdParms[13].Value = model.LOGO;
            cmdParms[14].Value = model.Fax;
            cmdParms[15].Value = model.PostCode;
            cmdParms[0x10].Value = model.HomePage;
            cmdParms[0x11].Value = model.ArtiPerson;
            cmdParms[0x12].Value = model.EnteRank;
            cmdParms[0x13].Value = model.EnteClassID;
            cmdParms[20].Value = model.CompanyType;
            cmdParms[0x15].Value = model.BusinessLicense;
            cmdParms[0x16].Value = model.TaxNumber;
            cmdParms[0x17].Value = model.AccountBank;
            cmdParms[0x18].Value = model.AccountInfo;
            cmdParms[0x19].Value = model.ServicePhone;
            cmdParms[0x1a].Value = model.QQ;
            cmdParms[0x1b].Value = model.MSN;
            cmdParms[0x1c].Value = model.Status;
            cmdParms[0x1d].Value = model.CreatedDate;
            cmdParms[30].Value = model.CreatedUserID;
            cmdParms[0x1f].Value = model.UpdatedDate;
            cmdParms[0x20].Value = model.UpdatedUserID;
            cmdParms[0x21].Value = model.Balance;
            cmdParms[0x22].Value = model.AgentID;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int EnterpriseID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_Enterprise ");
            builder.Append(" where EnterpriseID=@EnterpriseID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@EnterpriseID", SqlDbType.Int, 4) };
            cmdParms[0].Value = EnterpriseID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string EnterpriseIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_Enterprise ");
            builder.Append(" where EnterpriseID in (" + EnterpriseIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int EnterpriseID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_Enterprise");
            builder.Append(" where EnterpriseID=@EnterpriseID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@EnterpriseID", SqlDbType.Int, 4) };
            cmdParms[0].Value = EnterpriseID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string Name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_Enterprise");
            builder.Append(" where Name=@Name");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = Name;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string Name, int EnterpriseID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_Enterprise");
            builder.Append(" where Name=@Name AND EnterpriseID<>@EnterpriseID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@EnterpriseID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Name;
            cmdParms[1].Value = EnterpriseID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select EnterpriseID,Name,Introduction,RegisteredCapital,TelPhone,CellPhone,ContactMail,RegionID,Address,Remark,Contact,UserName,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,EnteRank,EnteClassID,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID,AgentID ");
            builder.Append(" FROM Ms_Enterprise ");
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
            builder.Append(" EnterpriseID,Name,Introduction,RegisteredCapital,TelPhone,CellPhone,ContactMail,RegionID,Address,Remark,Contact,UserName,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,EnteRank,EnteClassID,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID,AgentID ");
            builder.Append(" FROM Ms_Enterprise ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.EnterpriseID desc");
            }
            builder.Append(")AS Row, T.*  from Ms_Enterprise T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("EnterpriseID", "Ms_Enterprise");
        }

        public Maticsoft.Model.Ms.Enterprise GetModel(int EnterpriseID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT MsEn.*,AU.UserName AS CreatedUserName,AUser.UserName AS UpdatedUserName FROM Ms_Enterprise MsEn  ");
            builder.Append(" LEFT JOIN Accounts_Users AU ON MsEn.CreatedUserID=AU.UserID ");
            builder.Append(" LEFT JOIN Accounts_Users AUser ON MsEn.UpdatedUserID=AUser.UserID ");
            builder.Append(" where EnterpriseID=@EnterpriseID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@EnterpriseID", SqlDbType.Int, 4) };
            cmdParms[0].Value = EnterpriseID;
            Maticsoft.Model.Ms.Enterprise enterprise = new Maticsoft.Model.Ms.Enterprise();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["EnterpriseID"] != null) && (set.Tables[0].Rows[0]["EnterpriseID"].ToString() != ""))
            {
                enterprise.EnterpriseID = int.Parse(set.Tables[0].Rows[0]["EnterpriseID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Name"] != null) && (set.Tables[0].Rows[0]["Name"].ToString() != ""))
            {
                enterprise.Name = set.Tables[0].Rows[0]["Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Introduction"] != null) && (set.Tables[0].Rows[0]["Introduction"].ToString() != ""))
            {
                enterprise.Introduction = set.Tables[0].Rows[0]["Introduction"].ToString();
            }
            if ((set.Tables[0].Rows[0]["RegisteredCapital"] != null) && (set.Tables[0].Rows[0]["RegisteredCapital"].ToString() != ""))
            {
                enterprise.RegisteredCapital = new int?(int.Parse(set.Tables[0].Rows[0]["RegisteredCapital"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["TelPhone"] != null) && (set.Tables[0].Rows[0]["TelPhone"].ToString() != ""))
            {
                enterprise.TelPhone = set.Tables[0].Rows[0]["TelPhone"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CellPhone"] != null) && (set.Tables[0].Rows[0]["CellPhone"].ToString() != ""))
            {
                enterprise.CellPhone = set.Tables[0].Rows[0]["CellPhone"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ContactMail"] != null) && (set.Tables[0].Rows[0]["ContactMail"].ToString() != ""))
            {
                enterprise.ContactMail = set.Tables[0].Rows[0]["ContactMail"].ToString();
            }
            if ((set.Tables[0].Rows[0]["RegionID"] != null) && (set.Tables[0].Rows[0]["RegionID"].ToString() != ""))
            {
                enterprise.RegionID = new int?(int.Parse(set.Tables[0].Rows[0]["RegionID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Address"] != null) && (set.Tables[0].Rows[0]["Address"].ToString() != ""))
            {
                enterprise.Address = set.Tables[0].Rows[0]["Address"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Remark"] != null) && (set.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                enterprise.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Contact"] != null) && (set.Tables[0].Rows[0]["Contact"].ToString() != ""))
            {
                enterprise.Contact = set.Tables[0].Rows[0]["Contact"].ToString();
            }
            if ((set.Tables[0].Rows[0]["UserName"] != null) && (set.Tables[0].Rows[0]["UserName"].ToString() != ""))
            {
                enterprise.UserName = set.Tables[0].Rows[0]["UserName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["EstablishedDate"] != null) && (set.Tables[0].Rows[0]["EstablishedDate"].ToString() != ""))
            {
                enterprise.EstablishedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["EstablishedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["EstablishedCity"] != null) && (set.Tables[0].Rows[0]["EstablishedCity"].ToString() != ""))
            {
                enterprise.EstablishedCity = new int?(int.Parse(set.Tables[0].Rows[0]["EstablishedCity"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["LOGO"] != null) && (set.Tables[0].Rows[0]["LOGO"].ToString() != ""))
            {
                enterprise.LOGO = set.Tables[0].Rows[0]["LOGO"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Fax"] != null) && (set.Tables[0].Rows[0]["Fax"].ToString() != ""))
            {
                enterprise.Fax = set.Tables[0].Rows[0]["Fax"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PostCode"] != null) && (set.Tables[0].Rows[0]["PostCode"].ToString() != ""))
            {
                enterprise.PostCode = set.Tables[0].Rows[0]["PostCode"].ToString();
            }
            if ((set.Tables[0].Rows[0]["HomePage"] != null) && (set.Tables[0].Rows[0]["HomePage"].ToString() != ""))
            {
                enterprise.HomePage = set.Tables[0].Rows[0]["HomePage"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ArtiPerson"] != null) && (set.Tables[0].Rows[0]["ArtiPerson"].ToString() != ""))
            {
                enterprise.ArtiPerson = set.Tables[0].Rows[0]["ArtiPerson"].ToString();
            }
            if ((set.Tables[0].Rows[0]["EnteRank"] != null) && (set.Tables[0].Rows[0]["EnteRank"].ToString() != ""))
            {
                enterprise.EnteRank = new int?(int.Parse(set.Tables[0].Rows[0]["EnteRank"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["EnteClassID"] != null) && (set.Tables[0].Rows[0]["EnteClassID"].ToString() != ""))
            {
                enterprise.EnteClassID = new int?(int.Parse(set.Tables[0].Rows[0]["EnteClassID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CompanyType"] != null) && (set.Tables[0].Rows[0]["CompanyType"].ToString() != ""))
            {
                enterprise.CompanyType = new int?(int.Parse(set.Tables[0].Rows[0]["CompanyType"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["BusinessLicense"] != null) && (set.Tables[0].Rows[0]["BusinessLicense"].ToString() != ""))
            {
                enterprise.BusinessLicense = set.Tables[0].Rows[0]["BusinessLicense"].ToString();
            }
            if ((set.Tables[0].Rows[0]["TaxNumber"] != null) && (set.Tables[0].Rows[0]["TaxNumber"].ToString() != ""))
            {
                enterprise.TaxNumber = set.Tables[0].Rows[0]["TaxNumber"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AccountBank"] != null) && (set.Tables[0].Rows[0]["AccountBank"].ToString() != ""))
            {
                enterprise.AccountBank = set.Tables[0].Rows[0]["AccountBank"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AccountInfo"] != null) && (set.Tables[0].Rows[0]["AccountInfo"].ToString() != ""))
            {
                enterprise.AccountInfo = set.Tables[0].Rows[0]["AccountInfo"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ServicePhone"] != null) && (set.Tables[0].Rows[0]["ServicePhone"].ToString() != ""))
            {
                enterprise.ServicePhone = set.Tables[0].Rows[0]["ServicePhone"].ToString();
            }
            if ((set.Tables[0].Rows[0]["QQ"] != null) && (set.Tables[0].Rows[0]["QQ"].ToString() != ""))
            {
                enterprise.QQ = set.Tables[0].Rows[0]["QQ"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MSN"] != null) && (set.Tables[0].Rows[0]["MSN"].ToString() != ""))
            {
                enterprise.MSN = set.Tables[0].Rows[0]["MSN"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Status"] != null) && (set.Tables[0].Rows[0]["Status"].ToString() != ""))
            {
                enterprise.Status = new int?(int.Parse(set.Tables[0].Rows[0]["Status"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                enterprise.CreatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedUserID"] != null) && (set.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                enterprise.CreatedUserID = new int?(int.Parse(set.Tables[0].Rows[0]["CreatedUserID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["UpdatedDate"] != null) && (set.Tables[0].Rows[0]["UpdatedDate"].ToString() != ""))
            {
                enterprise.UpdatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["UpdatedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["UpdatedUserID"] != null) && (set.Tables[0].Rows[0]["UpdatedUserID"].ToString() != ""))
            {
                enterprise.UpdatedUserID = new int?(int.Parse(set.Tables[0].Rows[0]["UpdatedUserID"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["AgentID"] != null) && (set.Tables[0].Rows[0]["AgentID"].ToString() != ""))
            {
                enterprise.AgentID = int.Parse(set.Tables[0].Rows[0]["AgentID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CreatedUserName"] != null) && (set.Tables[0].Rows[0]["CreatedUserName"].ToString() != ""))
            {
                enterprise.CreatedUserName = set.Tables[0].Rows[0]["CreatedUserName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["UpdatedUserName"] != null) && (set.Tables[0].Rows[0]["UpdatedUserName"].ToString() != ""))
            {
                enterprise.UpdatedUserName = set.Tables[0].Rows[0]["UpdatedUserName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Balance"] != null) && (set.Tables[0].Rows[0]["Balance"].ToString() != ""))
            {
                enterprise.Balance = decimal.Parse(set.Tables[0].Rows[0]["Balance"].ToString());
            }
            return enterprise;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Ms_Enterprise ");
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

        public bool Update(Maticsoft.Model.Ms.Enterprise model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Ms_Enterprise SET ");
            builder.Append("Name=@Name,");
            builder.Append("Introduction=@Introduction,");
            builder.Append("RegisteredCapital=@RegisteredCapital,");
            builder.Append("TelPhone=@TelPhone,");
            builder.Append("CellPhone=@CellPhone,");
            builder.Append("ContactMail=@ContactMail,");
            builder.Append("RegionID=@RegionID,");
            builder.Append("Address=@Address,");
            builder.Append("Remark=@Remark,");
            builder.Append("Contact=@Contact,");
            builder.Append("UserName=@UserName,");
            builder.Append("EstablishedDate=@EstablishedDate,");
            builder.Append("EstablishedCity=@EstablishedCity,");
            builder.Append("LOGO=@LOGO,");
            builder.Append("Fax=@Fax,");
            builder.Append("PostCode=@PostCode,");
            builder.Append("HomePage=@HomePage,");
            builder.Append("ArtiPerson=@ArtiPerson,");
            builder.Append("EnteRank=@EnteRank,");
            builder.Append("EnteClassID=@EnteClassID,");
            builder.Append("CompanyType=@CompanyType,");
            builder.Append("BusinessLicense=@BusinessLicense,");
            builder.Append("TaxNumber=@TaxNumber,");
            builder.Append("AccountBank=@AccountBank,");
            builder.Append("AccountInfo=@AccountInfo,");
            builder.Append("ServicePhone=@ServicePhone,");
            builder.Append("QQ=@QQ,");
            builder.Append("MSN=@MSN,");
            builder.Append("Status=@Status,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("UpdatedDate=@UpdatedDate,");
            builder.Append("UpdatedUserID=@UpdatedUserID,");
            builder.Append("Balance=@Balance,");
            builder.Append("AgentID=@AgentID");
            builder.Append(" WHERE EnterpriseID=@EnterpriseID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Introduction", SqlDbType.NVarChar), new SqlParameter("@RegisteredCapital", SqlDbType.Int, 4), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ContactMail", SqlDbType.NVarChar, 50), new SqlParameter("@RegionID", SqlDbType.Int, 4), new SqlParameter("@Address", SqlDbType.NVarChar, 500), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Contact", SqlDbType.NVarChar, 50), new SqlParameter("@UserName", SqlDbType.NVarChar, 30), new SqlParameter("@EstablishedDate", SqlDbType.DateTime), new SqlParameter("@EstablishedCity", SqlDbType.Int, 4), new SqlParameter("@LOGO", SqlDbType.NVarChar, 300), new SqlParameter("@Fax", SqlDbType.NVarChar, 30), new SqlParameter("@PostCode", SqlDbType.NVarChar, 10), 
                new SqlParameter("@HomePage", SqlDbType.NVarChar, 50), new SqlParameter("@ArtiPerson", SqlDbType.NVarChar, 50), new SqlParameter("@EnteRank", SqlDbType.Int, 4), new SqlParameter("@EnteClassID", SqlDbType.Int, 4), new SqlParameter("@CompanyType", SqlDbType.SmallInt, 2), new SqlParameter("@BusinessLicense", SqlDbType.NVarChar, 300), new SqlParameter("@TaxNumber", SqlDbType.NVarChar, 300), new SqlParameter("@AccountBank", SqlDbType.NVarChar, 300), new SqlParameter("@AccountInfo", SqlDbType.NVarChar, 300), new SqlParameter("@ServicePhone", SqlDbType.NVarChar, 300), new SqlParameter("@QQ", SqlDbType.NVarChar, 30), new SqlParameter("@MSN", SqlDbType.NVarChar, 30), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), 
                new SqlParameter("@UpdatedUserID", SqlDbType.Int, 4), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@AgentID", SqlDbType.Int, 4), new SqlParameter("@EnterpriseID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Introduction;
            cmdParms[2].Value = model.RegisteredCapital;
            cmdParms[3].Value = model.TelPhone;
            cmdParms[4].Value = model.CellPhone;
            cmdParms[5].Value = model.ContactMail;
            cmdParms[6].Value = model.RegionID;
            cmdParms[7].Value = model.Address;
            cmdParms[8].Value = model.Remark;
            cmdParms[9].Value = model.Contact;
            cmdParms[10].Value = model.UserName;
            cmdParms[11].Value = model.EstablishedDate;
            cmdParms[12].Value = model.EstablishedCity;
            cmdParms[13].Value = model.LOGO;
            cmdParms[14].Value = model.Fax;
            cmdParms[15].Value = model.PostCode;
            cmdParms[0x10].Value = model.HomePage;
            cmdParms[0x11].Value = model.ArtiPerson;
            cmdParms[0x12].Value = model.EnteRank;
            cmdParms[0x13].Value = model.EnteClassID;
            cmdParms[20].Value = model.CompanyType;
            cmdParms[0x15].Value = model.BusinessLicense;
            cmdParms[0x16].Value = model.TaxNumber;
            cmdParms[0x17].Value = model.AccountBank;
            cmdParms[0x18].Value = model.AccountInfo;
            cmdParms[0x19].Value = model.ServicePhone;
            cmdParms[0x1a].Value = model.QQ;
            cmdParms[0x1b].Value = model.MSN;
            cmdParms[0x1c].Value = model.Status;
            cmdParms[0x1d].Value = model.CreatedDate;
            cmdParms[30].Value = model.CreatedUserID;
            cmdParms[0x1f].Value = model.UpdatedDate;
            cmdParms[0x20].Value = model.UpdatedUserID;
            cmdParms[0x21].Value = model.Balance;
            cmdParms[0x22].Value = model.AgentID;
            cmdParms[0x23].Value = model.EnterpriseID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_Enterprise set " + strWhere);
            builder.Append(" where EnterpriseID in(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

