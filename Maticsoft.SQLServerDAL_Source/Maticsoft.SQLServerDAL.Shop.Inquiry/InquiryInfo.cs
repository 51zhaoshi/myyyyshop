namespace Maticsoft.SQLServerDAL.Shop.Inquiry
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Inquiry;
    using Maticsoft.Model.Shop.Inquiry;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class InquiryInfo : IInquiryInfo
    {
        public long Add(Maticsoft.Model.Shop.Inquiry.InquiryInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Inquiry(");
            builder.Append("ParentId,UserId,UserName,Email,CellPhone,Telephone,RegionId,Company,Address,QQ,Status,LeaveMsg,ReplyMsg,MarketPrice,Amount,CreatedDate,UpdatedDate,UpdatedUserId,Remark)");
            builder.Append(" values (");
            builder.Append("@ParentId,@UserId,@UserName,@Email,@CellPhone,@Telephone,@RegionId,@Company,@Address,@QQ,@Status,@LeaveMsg,@ReplyMsg,@MarketPrice,@Amount,@CreatedDate,@UpdatedDate,@UpdatedUserId,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ParentId", SqlDbType.BigInt, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@CellPhone", SqlDbType.NVarChar, 100), new SqlParameter("@Telephone", SqlDbType.NVarChar, 100), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Company", SqlDbType.NVarChar, 200), new SqlParameter("@Address", SqlDbType.NVarChar, 200), new SqlParameter("@QQ", SqlDbType.NVarChar, 100), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@LeaveMsg", SqlDbType.Text), new SqlParameter("@ReplyMsg", SqlDbType.Text), new SqlParameter("@MarketPrice", SqlDbType.Money, 8), new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), 
                new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedUserId", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 500)
             };
            cmdParms[0].Value = model.ParentId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.UserName;
            cmdParms[3].Value = model.Email;
            cmdParms[4].Value = model.CellPhone;
            cmdParms[5].Value = model.Telephone;
            cmdParms[6].Value = model.RegionId;
            cmdParms[7].Value = model.Company;
            cmdParms[8].Value = model.Address;
            cmdParms[9].Value = model.QQ;
            cmdParms[10].Value = model.Status;
            cmdParms[11].Value = model.LeaveMsg;
            cmdParms[12].Value = model.ReplyMsg;
            cmdParms[13].Value = model.MarketPrice;
            cmdParms[14].Value = model.Amount;
            cmdParms[15].Value = model.CreatedDate;
            cmdParms[0x10].Value = model.UpdatedDate;
            cmdParms[0x11].Value = model.UpdatedUserId;
            cmdParms[0x12].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public Maticsoft.Model.Shop.Inquiry.InquiryInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Inquiry.InquiryInfo info = new Maticsoft.Model.Shop.Inquiry.InquiryInfo();
            if (row != null)
            {
                if ((row["InquiryId"] != null) && (row["InquiryId"].ToString() != ""))
                {
                    info.InquiryId = long.Parse(row["InquiryId"].ToString());
                }
                if ((row["ParentId"] != null) && (row["ParentId"].ToString() != ""))
                {
                    info.ParentId = long.Parse(row["ParentId"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    info.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserName"] != null)
                {
                    info.UserName = row["UserName"].ToString();
                }
                if (row["Email"] != null)
                {
                    info.Email = row["Email"].ToString();
                }
                if (row["CellPhone"] != null)
                {
                    info.CellPhone = row["CellPhone"].ToString();
                }
                if (row["Telephone"] != null)
                {
                    info.Telephone = row["Telephone"].ToString();
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    info.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row["Company"] != null)
                {
                    info.Company = row["Company"].ToString();
                }
                if (row["Address"] != null)
                {
                    info.Address = row["Address"].ToString();
                }
                if (row["QQ"] != null)
                {
                    info.QQ = row["QQ"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    info.Status = int.Parse(row["Status"].ToString());
                }
                if (row["LeaveMsg"] != null)
                {
                    info.LeaveMsg = row["LeaveMsg"].ToString();
                }
                if (row["ReplyMsg"] != null)
                {
                    info.ReplyMsg = row["ReplyMsg"].ToString();
                }
                if ((row["MarketPrice"] != null) && (row["MarketPrice"].ToString() != ""))
                {
                    info.MarketPrice = new decimal?(decimal.Parse(row["MarketPrice"].ToString()));
                }
                if ((row["Amount"] != null) && (row["Amount"].ToString() != ""))
                {
                    info.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    info.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["UpdatedDate"] != null) && (row["UpdatedDate"].ToString() != ""))
                {
                    info.UpdatedDate = new DateTime?(DateTime.Parse(row["UpdatedDate"].ToString()));
                }
                if ((row["UpdatedUserId"] != null) && (row["UpdatedUserId"].ToString() != ""))
                {
                    info.UpdatedUserId = int.Parse(row["UpdatedUserId"].ToString());
                }
                if (row["Remark"] != null)
                {
                    info.Remark = row["Remark"].ToString();
                }
            }
            return info;
        }

        public bool Delete(long InquiryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Inquiry ");
            builder.Append(" where InquiryId=@InquiryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@InquiryId", SqlDbType.BigInt) };
            cmdParms[0].Value = InquiryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(long InquiryId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Inquiry ");
            builder.Append(" where InquiryId=@InquiryId");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@InquiryId", SqlDbType.BigInt) };
            para[0].Value = InquiryId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from Shop_InquiryItem ");
            builder2.Append(" where InquiryId=@InquiryId  ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@InquiryId", SqlDbType.BigInt) };
            parameterArray2[0].Value = InquiryId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteList(string InquiryIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Inquiry ");
            builder.Append(" where InquiryId in (" + InquiryIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long InquiryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Inquiry");
            builder.Append(" where InquiryId=@InquiryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@InquiryId", SqlDbType.BigInt) };
            cmdParms[0].Value = InquiryId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select InquiryId,ParentId,UserId,UserName,Email,CellPhone,Telephone,RegionId,Company,Address,QQ,Status,LeaveMsg,ReplyMsg,MarketPrice,Amount,CreatedDate,UpdatedDate,UpdatedUserId,Remark ");
            builder.Append(" FROM Shop_Inquiry ");
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
            builder.Append(" InquiryId,ParentId,UserId,UserName,Email,CellPhone,Telephone,RegionId,Company,Address,QQ,Status,LeaveMsg,ReplyMsg,MarketPrice,Amount,CreatedDate,UpdatedDate,UpdatedUserId,Remark ");
            builder.Append(" FROM Shop_Inquiry ");
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
                builder.Append("order by T.InquiryId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Inquiry T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Inquiry.InquiryInfo GetModel(long InquiryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 InquiryId,ParentId,UserId,UserName,Email,CellPhone,Telephone,RegionId,Company,Address,QQ,Status,LeaveMsg,ReplyMsg,MarketPrice,Amount,CreatedDate,UpdatedDate,UpdatedUserId,Remark from Shop_Inquiry ");
            builder.Append(" where InquiryId=@InquiryId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@InquiryId", SqlDbType.BigInt) };
            cmdParms[0].Value = InquiryId;
            new Maticsoft.Model.Shop.Inquiry.InquiryInfo();
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
            builder.Append("select count(1) FROM Shop_Inquiry ");
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

        public bool Update(Maticsoft.Model.Shop.Inquiry.InquiryInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Inquiry set ");
            builder.Append("ParentId=@ParentId,");
            builder.Append("UserId=@UserId,");
            builder.Append("UserName=@UserName,");
            builder.Append("Email=@Email,");
            builder.Append("CellPhone=@CellPhone,");
            builder.Append("Telephone=@Telephone,");
            builder.Append("RegionId=@RegionId,");
            builder.Append("Company=@Company,");
            builder.Append("Address=@Address,");
            builder.Append("QQ=@QQ,");
            builder.Append("Status=@Status,");
            builder.Append("LeaveMsg=@LeaveMsg,");
            builder.Append("ReplyMsg=@ReplyMsg,");
            builder.Append("MarketPrice=@MarketPrice,");
            builder.Append("Amount=@Amount,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("UpdatedDate=@UpdatedDate,");
            builder.Append("UpdatedUserId=@UpdatedUserId,");
            builder.Append("Remark=@Remark");
            builder.Append(" where InquiryId=@InquiryId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ParentId", SqlDbType.BigInt, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@Email", SqlDbType.NVarChar, 100), new SqlParameter("@CellPhone", SqlDbType.NVarChar, 100), new SqlParameter("@Telephone", SqlDbType.NVarChar, 100), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@Company", SqlDbType.NVarChar, 200), new SqlParameter("@Address", SqlDbType.NVarChar, 200), new SqlParameter("@QQ", SqlDbType.NVarChar, 100), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@LeaveMsg", SqlDbType.Text), new SqlParameter("@ReplyMsg", SqlDbType.Text), new SqlParameter("@MarketPrice", SqlDbType.Money, 8), new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), 
                new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedUserId", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@InquiryId", SqlDbType.BigInt, 8)
             };
            cmdParms[0].Value = model.ParentId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.UserName;
            cmdParms[3].Value = model.Email;
            cmdParms[4].Value = model.CellPhone;
            cmdParms[5].Value = model.Telephone;
            cmdParms[6].Value = model.RegionId;
            cmdParms[7].Value = model.Company;
            cmdParms[8].Value = model.Address;
            cmdParms[9].Value = model.QQ;
            cmdParms[10].Value = model.Status;
            cmdParms[11].Value = model.LeaveMsg;
            cmdParms[12].Value = model.ReplyMsg;
            cmdParms[13].Value = model.MarketPrice;
            cmdParms[14].Value = model.Amount;
            cmdParms[15].Value = model.CreatedDate;
            cmdParms[0x10].Value = model.UpdatedDate;
            cmdParms[0x11].Value = model.UpdatedUserId;
            cmdParms[0x12].Value = model.Remark;
            cmdParms[0x13].Value = model.InquiryId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

