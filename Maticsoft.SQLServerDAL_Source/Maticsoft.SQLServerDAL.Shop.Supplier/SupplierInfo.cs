namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SupplierInfo : ISupplierInfo
    {
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Suppliers(");
            builder.Append("Name,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId)");
            builder.Append(" values (");
            builder.Append("@Name,@CategoryId,@Rank,@UserId,@UserName,@TelPhone,@CellPhone,@ContactMail,@Introduction,@RegisteredCapital,@RegionId,@Address,@Contact,@EstablishedDate,@EstablishedCity,@LOGO,@Fax,@PostCode,@HomePage,@ArtiPerson,@CompanyType,@BusinessLicense,@TaxNumber,@AccountBank,@AccountInfo,@ServicePhone,@QQ,@MSN,@Status,@CreatedDate,@CreatedUserId,@UpdatedDate,@UpdatedUserId,@ExpirationDate,@Balance,@IsUserApprove,@IsSuppApprove,@ScoreDesc,@ScoreService,@ScoreSpeed,@Recomend,@Sequence,@ProductCount,@PhotoCount,@ThemeId,@Remark,@AgentId)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@Rank", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ContactMail", SqlDbType.NVarChar, 50), new SqlParameter("@Introduction", SqlDbType.NVarChar, -1), new SqlParameter("@RegisteredCapital", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Address", SqlDbType.NVarChar, 500), new SqlParameter("@Contact", SqlDbType.NVarChar, 50), new SqlParameter("@EstablishedDate", SqlDbType.DateTime), new SqlParameter("@EstablishedCity", SqlDbType.Int, 4), new SqlParameter("@LOGO", SqlDbType.NVarChar, 300), 
                new SqlParameter("@Fax", SqlDbType.NVarChar, 30), new SqlParameter("@PostCode", SqlDbType.NVarChar, 10), new SqlParameter("@HomePage", SqlDbType.NVarChar, 50), new SqlParameter("@ArtiPerson", SqlDbType.NVarChar, 50), new SqlParameter("@CompanyType", SqlDbType.SmallInt, 2), new SqlParameter("@BusinessLicense", SqlDbType.NVarChar, 300), new SqlParameter("@TaxNumber", SqlDbType.NVarChar, 300), new SqlParameter("@AccountBank", SqlDbType.NVarChar, 300), new SqlParameter("@AccountInfo", SqlDbType.NVarChar, 300), new SqlParameter("@ServicePhone", SqlDbType.NVarChar, 300), new SqlParameter("@QQ", SqlDbType.NVarChar, 30), new SqlParameter("@MSN", SqlDbType.NVarChar, 30), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), 
                new SqlParameter("@UpdatedUserId", SqlDbType.Int, 4), new SqlParameter("@ExpirationDate", SqlDbType.DateTime), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@IsUserApprove", SqlDbType.Bit, 1), new SqlParameter("@IsSuppApprove", SqlDbType.Bit, 1), new SqlParameter("@ScoreDesc", SqlDbType.Money, 8), new SqlParameter("@ScoreService", SqlDbType.Money, 8), new SqlParameter("@ScoreSpeed", SqlDbType.Money, 8), new SqlParameter("@Recomend", SqlDbType.SmallInt, 2), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ProductCount", SqlDbType.Int, 4), new SqlParameter("@PhotoCount", SqlDbType.Int, 4), new SqlParameter("@ThemeId", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AgentId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.CategoryId;
            cmdParms[2].Value = model.Rank;
            cmdParms[3].Value = model.UserId;
            cmdParms[4].Value = model.UserName;
            cmdParms[5].Value = model.TelPhone;
            cmdParms[6].Value = model.CellPhone;
            cmdParms[7].Value = model.ContactMail;
            cmdParms[8].Value = model.Introduction;
            cmdParms[9].Value = model.RegisteredCapital;
            cmdParms[10].Value = model.RegionId;
            cmdParms[11].Value = model.Address;
            cmdParms[12].Value = model.Contact;
            cmdParms[13].Value = model.EstablishedDate;
            cmdParms[14].Value = model.EstablishedCity;
            cmdParms[15].Value = model.LOGO;
            cmdParms[0x10].Value = model.Fax;
            cmdParms[0x11].Value = model.PostCode;
            cmdParms[0x12].Value = model.HomePage;
            cmdParms[0x13].Value = model.ArtiPerson;
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
            cmdParms[30].Value = model.CreatedUserId;
            cmdParms[0x1f].Value = model.UpdatedDate;
            cmdParms[0x20].Value = model.UpdatedUserId;
            cmdParms[0x21].Value = model.ExpirationDate;
            cmdParms[0x22].Value = model.Balance;
            cmdParms[0x23].Value = model.IsUserApprove;
            cmdParms[0x24].Value = model.IsSuppApprove;
            cmdParms[0x25].Value = model.ScoreDesc;
            cmdParms[0x26].Value = model.ScoreService;
            cmdParms[0x27].Value = model.ScoreSpeed;
            cmdParms[40].Value = model.Recomend;
            cmdParms[0x29].Value = model.Sequence;
            cmdParms[0x2a].Value = model.ProductCount;
            cmdParms[0x2b].Value = model.PhotoCount;
            cmdParms[0x2c].Value = model.ThemeId;
            cmdParms[0x2d].Value = model.Remark;
            cmdParms[0x2e].Value = model.AgentId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierInfo info = new Maticsoft.Model.Shop.Supplier.SupplierInfo();
            if (row != null)
            {
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    info.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["Name"] != null)
                {
                    info.Name = row["Name"].ToString();
                }
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    info.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["Rank"] != null) && (row["Rank"].ToString() != ""))
                {
                    info.Rank = int.Parse(row["Rank"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    info.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserName"] != null)
                {
                    info.UserName = row["UserName"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    info.TelPhone = row["TelPhone"].ToString();
                }
                if (row["CellPhone"] != null)
                {
                    info.CellPhone = row["CellPhone"].ToString();
                }
                if (row["ContactMail"] != null)
                {
                    info.ContactMail = row["ContactMail"].ToString();
                }
                if (row["Introduction"] != null)
                {
                    info.Introduction = row["Introduction"].ToString();
                }
                if ((row["RegisteredCapital"] != null) && (row["RegisteredCapital"].ToString() != ""))
                {
                    info.RegisteredCapital = new int?(int.Parse(row["RegisteredCapital"].ToString()));
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    info.RegionId = new int?(int.Parse(row["RegionId"].ToString()));
                }
                if (row["Address"] != null)
                {
                    info.Address = row["Address"].ToString();
                }
                if (row["Contact"] != null)
                {
                    info.Contact = row["Contact"].ToString();
                }
                if ((row["EstablishedDate"] != null) && (row["EstablishedDate"].ToString() != ""))
                {
                    info.EstablishedDate = new DateTime?(DateTime.Parse(row["EstablishedDate"].ToString()));
                }
                if ((row["EstablishedCity"] != null) && (row["EstablishedCity"].ToString() != ""))
                {
                    info.EstablishedCity = new int?(int.Parse(row["EstablishedCity"].ToString()));
                }
                if (row["LOGO"] != null)
                {
                    info.LOGO = row["LOGO"].ToString();
                }
                if (row["Fax"] != null)
                {
                    info.Fax = row["Fax"].ToString();
                }
                if (row["PostCode"] != null)
                {
                    info.PostCode = row["PostCode"].ToString();
                }
                if (row["HomePage"] != null)
                {
                    info.HomePage = row["HomePage"].ToString();
                }
                if (row["ArtiPerson"] != null)
                {
                    info.ArtiPerson = row["ArtiPerson"].ToString();
                }
                if ((row["CompanyType"] != null) && (row["CompanyType"].ToString() != ""))
                {
                    info.CompanyType = new int?(int.Parse(row["CompanyType"].ToString()));
                }
                if (row["BusinessLicense"] != null)
                {
                    info.BusinessLicense = row["BusinessLicense"].ToString();
                }
                if (row["TaxNumber"] != null)
                {
                    info.TaxNumber = row["TaxNumber"].ToString();
                }
                if (row["AccountBank"] != null)
                {
                    info.AccountBank = row["AccountBank"].ToString();
                }
                if (row["AccountInfo"] != null)
                {
                    info.AccountInfo = row["AccountInfo"].ToString();
                }
                if (row["ServicePhone"] != null)
                {
                    info.ServicePhone = row["ServicePhone"].ToString();
                }
                if (row["QQ"] != null)
                {
                    info.QQ = row["QQ"].ToString();
                }
                if (row["MSN"] != null)
                {
                    info.MSN = row["MSN"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    info.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    info.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CreatedUserId"] != null) && (row["CreatedUserId"].ToString() != ""))
                {
                    info.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if ((row["UpdatedDate"] != null) && (row["UpdatedDate"].ToString() != ""))
                {
                    info.UpdatedDate = new DateTime?(DateTime.Parse(row["UpdatedDate"].ToString()));
                }
                if ((row["UpdatedUserId"] != null) && (row["UpdatedUserId"].ToString() != ""))
                {
                    info.UpdatedUserId = new int?(int.Parse(row["UpdatedUserId"].ToString()));
                }
                if ((row["ExpirationDate"] != null) && (row["ExpirationDate"].ToString() != ""))
                {
                    info.ExpirationDate = new DateTime?(DateTime.Parse(row["ExpirationDate"].ToString()));
                }
                if ((row["Balance"] != null) && (row["Balance"].ToString() != ""))
                {
                    info.Balance = decimal.Parse(row["Balance"].ToString());
                }
                if ((row["IsUserApprove"] != null) && (row["IsUserApprove"].ToString() != ""))
                {
                    if ((row["IsUserApprove"].ToString() == "1") || (row["IsUserApprove"].ToString().ToLower() == "true"))
                    {
                        info.IsUserApprove = true;
                    }
                    else
                    {
                        info.IsUserApprove = false;
                    }
                }
                if ((row["IsSuppApprove"] != null) && (row["IsSuppApprove"].ToString() != ""))
                {
                    if ((row["IsSuppApprove"].ToString() == "1") || (row["IsSuppApprove"].ToString().ToLower() == "true"))
                    {
                        info.IsSuppApprove = true;
                    }
                    else
                    {
                        info.IsSuppApprove = false;
                    }
                }
                if ((row["ScoreDesc"] != null) && (row["ScoreDesc"].ToString() != ""))
                {
                    info.ScoreDesc = decimal.Parse(row["ScoreDesc"].ToString());
                }
                if ((row["ScoreService"] != null) && (row["ScoreService"].ToString() != ""))
                {
                    info.ScoreService = decimal.Parse(row["ScoreService"].ToString());
                }
                if ((row["ScoreSpeed"] != null) && (row["ScoreSpeed"].ToString() != ""))
                {
                    info.ScoreSpeed = decimal.Parse(row["ScoreSpeed"].ToString());
                }
                if ((row["Recomend"] != null) && (row["Recomend"].ToString() != ""))
                {
                    info.Recomend = int.Parse(row["Recomend"].ToString());
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    info.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["ProductCount"] != null) && (row["ProductCount"].ToString() != ""))
                {
                    info.ProductCount = int.Parse(row["ProductCount"].ToString());
                }
                if ((row["PhotoCount"] != null) && (row["PhotoCount"].ToString() != ""))
                {
                    info.PhotoCount = int.Parse(row["PhotoCount"].ToString());
                }
                if ((row["ThemeId"] != null) && (row["ThemeId"].ToString() != ""))
                {
                    info.ThemeId = int.Parse(row["ThemeId"].ToString());
                }
                if (row["Remark"] != null)
                {
                    info.Remark = row["Remark"].ToString();
                }
                if ((row["AgentId"] != null) && (row["AgentId"].ToString() != ""))
                {
                    info.AgentId = int.Parse(row["AgentId"].ToString());
                }
            }
            return info;
        }

        public bool Delete(int SupplierId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Suppliers ");
            builder.Append(" where SupplierId=@SupplierId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SupplierId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SupplierId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SupplierIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Suppliers ");
            builder.Append(" where SupplierId in (" + SupplierIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int SupplierId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Suppliers");
            builder.Append(" where SupplierId=@SupplierId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SupplierId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SupplierId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string Name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Suppliers");
            builder.Append(" where Name=@Name");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = Name;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string Name, int SupplierID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Suppliers");
            builder.Append(" where Name=@Name AND SupplierID<>@SupplierID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@SupplierID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Name;
            cmdParms[1].Value = SupplierID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SupplierId,Name,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId ");
            builder.Append(" FROM Shop_Suppliers ");
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
            builder.Append(" SupplierId,Name,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId ");
            builder.Append(" FROM Shop_Suppliers ");
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
                builder.Append("order by T.SupplierId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Suppliers T ");
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
            return DbHelperSQL.GetMaxID("SupplierId", "Shop_Suppliers");
        }

        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModel(int SupplierId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SupplierId,Name,CategoryId,Rank,UserId,UserName,TelPhone,CellPhone,ContactMail,Introduction,RegisteredCapital,RegionId,Address,Contact,EstablishedDate,EstablishedCity,LOGO,Fax,PostCode,HomePage,ArtiPerson,CompanyType,BusinessLicense,TaxNumber,AccountBank,AccountInfo,ServicePhone,QQ,MSN,Status,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,ExpirationDate,Balance,IsUserApprove,IsSuppApprove,ScoreDesc,ScoreService,ScoreSpeed,Recomend,Sequence,ProductCount,PhotoCount,ThemeId,Remark,AgentId from Shop_Suppliers ");
            builder.Append(" where SupplierId=@SupplierId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SupplierId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SupplierId;
            new Maticsoft.Model.Shop.Supplier.SupplierInfo();
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
            builder.Append("select count(1) FROM Shop_Suppliers ");
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

        public DataSet GetStatisticsSales(int supplierId, int year)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n--销量/业绩走势图\r\nSELECT  A.[Month] AS Mon\r\n      , CASE WHEN B.ToalQuantity IS NULL THEN 0\r\n             ELSE B.ToalQuantity\r\n        END AS ToalQuantity\r\n      , CASE WHEN B.ToalPrice IS NULL THEN 0.00\r\n             ELSE B.ToalPrice\r\n        END AS ToalPrice\r\nFROM    ( SELECT    *\r\n          FROM      GET_GeneratedMonthEx()\r\n        ) A\r\n        LEFT JOIN ( SELECT  MONTH(O.CreatedDate) Mon\r\n                          , SUM(I.Quantity) ToalQuantity\r\n                          , SUM(I.SellPrice) ToalPrice\r\n                    FROM    Shop_OrderItems I\r\n                          , Shop_Orders O\r\n                    WHERE   I.OrderId = O.OrderId\r\n                            AND O.SupplierId = {0}\r\n                            AND O.OrderStatus = 2 AND O.OrderType = 1\r\n                            AND YEAR(O.CreatedDate) = '{1}'\r\n                    GROUP BY MONTH(O.CreatedDate)\r\n                  ) B ON A.[Month] = B.Mon \r\n", supplierId, year);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetStatisticsSupply(int supplierId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n--剩余总量/额\r\nSELECT 1 AS [Type], SUM(S.Stock) ToalQuantity\r\n      , SUM(S.SalePrice * S.Stock) ToalPrice\r\nFROM    Shop_SKUs S\r\nWHERE   EXISTS ( SELECT *, P.MarketPrice\r\n                 FROM   Shop_Products P\r\n                 WHERE  S.ProductId = P.ProductId\r\n                        AND P.SupplierId = {0} )\r\nUNION ALL      \r\n--已售量/额         \r\nSELECT  2 AS [Type], SUM(I.Quantity) ToalQuantity\r\n      , SUM(I.SellPrice * I.Quantity) ToalPrice\r\nFROM    Shop_OrderItems I, Shop_Orders O\r\nWHERE I.OrderId = O.OrderId AND O.SupplierId = {0} AND O.OrderStatus = 2 AND O.OrderType = 1\r\n", supplierId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Suppliers set ");
            builder.Append("Name=@Name,");
            builder.Append("CategoryId=@CategoryId,");
            builder.Append("Rank=@Rank,");
            builder.Append("UserId=@UserId,");
            builder.Append("UserName=@UserName,");
            builder.Append("TelPhone=@TelPhone,");
            builder.Append("CellPhone=@CellPhone,");
            builder.Append("ContactMail=@ContactMail,");
            builder.Append("Introduction=@Introduction,");
            builder.Append("RegisteredCapital=@RegisteredCapital,");
            builder.Append("RegionId=@RegionId,");
            builder.Append("Address=@Address,");
            builder.Append("Contact=@Contact,");
            builder.Append("EstablishedDate=@EstablishedDate,");
            builder.Append("EstablishedCity=@EstablishedCity,");
            builder.Append("LOGO=@LOGO,");
            builder.Append("Fax=@Fax,");
            builder.Append("PostCode=@PostCode,");
            builder.Append("HomePage=@HomePage,");
            builder.Append("ArtiPerson=@ArtiPerson,");
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
            builder.Append("CreatedUserId=@CreatedUserId,");
            builder.Append("UpdatedDate=@UpdatedDate,");
            builder.Append("UpdatedUserId=@UpdatedUserId,");
            builder.Append("ExpirationDate=@ExpirationDate,");
            builder.Append("Balance=@Balance,");
            builder.Append("IsUserApprove=@IsUserApprove,");
            builder.Append("IsSuppApprove=@IsSuppApprove,");
            builder.Append("ScoreDesc=@ScoreDesc,");
            builder.Append("ScoreService=@ScoreService,");
            builder.Append("ScoreSpeed=@ScoreSpeed,");
            builder.Append("Recomend=@Recomend,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("ProductCount=@ProductCount,");
            builder.Append("PhotoCount=@PhotoCount,");
            builder.Append("ThemeId=@ThemeId,");
            builder.Append("Remark=@Remark,");
            builder.Append("AgentId=@AgentId");
            builder.Append(" where SupplierId=@SupplierId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@Rank", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ContactMail", SqlDbType.NVarChar, 50), new SqlParameter("@Introduction", SqlDbType.NVarChar, -1), new SqlParameter("@RegisteredCapital", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Address", SqlDbType.NVarChar, 500), new SqlParameter("@Contact", SqlDbType.NVarChar, 50), new SqlParameter("@EstablishedDate", SqlDbType.DateTime), new SqlParameter("@EstablishedCity", SqlDbType.Int, 4), new SqlParameter("@LOGO", SqlDbType.NVarChar, 300), 
                new SqlParameter("@Fax", SqlDbType.NVarChar, 30), new SqlParameter("@PostCode", SqlDbType.NVarChar, 10), new SqlParameter("@HomePage", SqlDbType.NVarChar, 50), new SqlParameter("@ArtiPerson", SqlDbType.NVarChar, 50), new SqlParameter("@CompanyType", SqlDbType.SmallInt, 2), new SqlParameter("@BusinessLicense", SqlDbType.NVarChar, 300), new SqlParameter("@TaxNumber", SqlDbType.NVarChar, 300), new SqlParameter("@AccountBank", SqlDbType.NVarChar, 300), new SqlParameter("@AccountInfo", SqlDbType.NVarChar, 300), new SqlParameter("@ServicePhone", SqlDbType.NVarChar, 300), new SqlParameter("@QQ", SqlDbType.NVarChar, 30), new SqlParameter("@MSN", SqlDbType.NVarChar, 30), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), 
                new SqlParameter("@UpdatedUserId", SqlDbType.Int, 4), new SqlParameter("@ExpirationDate", SqlDbType.DateTime), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@IsUserApprove", SqlDbType.Bit, 1), new SqlParameter("@IsSuppApprove", SqlDbType.Bit, 1), new SqlParameter("@ScoreDesc", SqlDbType.Money, 8), new SqlParameter("@ScoreService", SqlDbType.Money, 8), new SqlParameter("@ScoreSpeed", SqlDbType.Money, 8), new SqlParameter("@Recomend", SqlDbType.SmallInt, 2), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ProductCount", SqlDbType.Int, 4), new SqlParameter("@PhotoCount", SqlDbType.Int, 4), new SqlParameter("@ThemeId", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AgentId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.CategoryId;
            cmdParms[2].Value = model.Rank;
            cmdParms[3].Value = model.UserId;
            cmdParms[4].Value = model.UserName;
            cmdParms[5].Value = model.TelPhone;
            cmdParms[6].Value = model.CellPhone;
            cmdParms[7].Value = model.ContactMail;
            cmdParms[8].Value = model.Introduction;
            cmdParms[9].Value = model.RegisteredCapital;
            cmdParms[10].Value = model.RegionId;
            cmdParms[11].Value = model.Address;
            cmdParms[12].Value = model.Contact;
            cmdParms[13].Value = model.EstablishedDate;
            cmdParms[14].Value = model.EstablishedCity;
            cmdParms[15].Value = model.LOGO;
            cmdParms[0x10].Value = model.Fax;
            cmdParms[0x11].Value = model.PostCode;
            cmdParms[0x12].Value = model.HomePage;
            cmdParms[0x13].Value = model.ArtiPerson;
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
            cmdParms[30].Value = model.CreatedUserId;
            cmdParms[0x1f].Value = model.UpdatedDate;
            cmdParms[0x20].Value = model.UpdatedUserId;
            cmdParms[0x21].Value = model.ExpirationDate;
            cmdParms[0x22].Value = model.Balance;
            cmdParms[0x23].Value = model.IsUserApprove;
            cmdParms[0x24].Value = model.IsSuppApprove;
            cmdParms[0x25].Value = model.ScoreDesc;
            cmdParms[0x26].Value = model.ScoreService;
            cmdParms[0x27].Value = model.ScoreSpeed;
            cmdParms[40].Value = model.Recomend;
            cmdParms[0x29].Value = model.Sequence;
            cmdParms[0x2a].Value = model.ProductCount;
            cmdParms[0x2b].Value = model.PhotoCount;
            cmdParms[0x2c].Value = model.ThemeId;
            cmdParms[0x2d].Value = model.Remark;
            cmdParms[0x2e].Value = model.AgentId;
            cmdParms[0x2f].Value = model.SupplierId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Suppliers set " + strWhere);
            builder.Append(" where SupplierId in(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

