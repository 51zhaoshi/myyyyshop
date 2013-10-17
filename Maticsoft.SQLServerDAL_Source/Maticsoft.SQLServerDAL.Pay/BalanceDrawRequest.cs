namespace Maticsoft.SQLServerDAL.Pay
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Pay;
    using Maticsoft.Model.Pay;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BalanceDrawRequest : IBalanceDrawRequest
    {
        public long Add(Maticsoft.Model.Pay.BalanceDrawRequest model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Pay_BalanceDrawRequest(");
            builder.Append("RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,Remark)");
            builder.Append(" values (");
            builder.Append("@RequestTime,@Amount,@UserID,@TrueName,@BankName,@BankCard,@CardTypeID,@RequestStatus,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RequestTime", SqlDbType.DateTime), new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@BankName", SqlDbType.NVarChar, 200), new SqlParameter("@BankCard", SqlDbType.NVarChar, 50), new SqlParameter("@CardTypeID", SqlDbType.Int, 4), new SqlParameter("@RequestStatus", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0) };
            cmdParms[0].Value = model.RequestTime;
            cmdParms[1].Value = model.Amount;
            cmdParms[2].Value = model.UserID;
            cmdParms[3].Value = model.TrueName;
            cmdParms[4].Value = model.BankName;
            cmdParms[5].Value = model.BankCard;
            cmdParms[6].Value = model.CardTypeID;
            cmdParms[7].Value = model.RequestStatus;
            cmdParms[8].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public bool AddEx(Maticsoft.Model.Pay.BalanceDrawRequest model, decimal balance)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Pay_BalanceDrawRequest(");
            builder.Append("RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,Remark)");
            builder.Append(" values (");
            builder.Append("@RequestTime,@Amount,@UserID,@TrueName,@BankName,@BankCard,@CardTypeID,@RequestStatus,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@RequestTime", SqlDbType.DateTime), new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@BankName", SqlDbType.NVarChar, 200), new SqlParameter("@BankCard", SqlDbType.NVarChar, 50), new SqlParameter("@CardTypeID", SqlDbType.Int, 4), new SqlParameter("@RequestStatus", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0) };
            para[0].Value = model.RequestTime;
            para[1].Value = model.Amount;
            para[2].Value = model.UserID;
            para[3].Value = model.TrueName;
            para[4].Value = model.BankName;
            para[5].Value = model.BankCard;
            para[6].Value = model.CardTypeID;
            para[7].Value = model.RequestStatus;
            para[8].Value = model.Remark;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" update Accounts_UsersExp set ");
            builder2.Append(" Balance=Balance- @Amount");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Amount", SqlDbType.Money, 8) };
            parameterArray2[0].Value = model.Amount;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("insert into Pay_BalanceDetails(");
            builder3.Append("UserId,TradeDate,TradeType,Expenses,Balance)");
            builder3.Append(" values (");
            builder3.Append("@UserId,@TradeDate,@TradeType,@Expenses,@Balance)");
            builder3.Append(";select @@IDENTITY");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@TradeDate", SqlDbType.DateTime), new SqlParameter("@TradeType", SqlDbType.Int, 4), new SqlParameter("@Expenses", SqlDbType.Money, 8), new SqlParameter("@Balance", SqlDbType.Money, 8) };
            parameterArray3[0].Value = model.UserID;
            parameterArray3[1].Value = DateTime.Now;
            parameterArray3[2].Value = 2;
            parameterArray3[3].Value = model.Amount;
            parameterArray3[4].Value = balance - model.Amount;
            item = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public Maticsoft.Model.Pay.BalanceDrawRequest DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Pay.BalanceDrawRequest request = new Maticsoft.Model.Pay.BalanceDrawRequest();
            if (row != null)
            {
                if ((row["JournalNumber"] != null) && (row["JournalNumber"].ToString() != ""))
                {
                    request.JournalNumber = long.Parse(row["JournalNumber"].ToString());
                }
                if ((row["RequestTime"] != null) && (row["RequestTime"].ToString() != ""))
                {
                    request.RequestTime = DateTime.Parse(row["RequestTime"].ToString());
                }
                if ((row["Amount"] != null) && (row["Amount"].ToString() != ""))
                {
                    request.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if ((row["UserID"] != null) && (row["UserID"].ToString() != ""))
                {
                    request.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["TrueName"] != null)
                {
                    request.TrueName = row["TrueName"].ToString();
                }
                if (row["BankName"] != null)
                {
                    request.BankName = row["BankName"].ToString();
                }
                if (row["BankCard"] != null)
                {
                    request.BankCard = row["BankCard"].ToString();
                }
                if ((row["CardTypeID"] != null) && (row["CardTypeID"].ToString() != ""))
                {
                    request.CardTypeID = int.Parse(row["CardTypeID"].ToString());
                }
                if ((row["RequestStatus"] != null) && (row["RequestStatus"].ToString() != ""))
                {
                    request.RequestStatus = int.Parse(row["RequestStatus"].ToString());
                }
                if (row["Remark"] != null)
                {
                    request.Remark = row["Remark"].ToString();
                }
            }
            return request;
        }

        public bool Delete(long JournalNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Pay_BalanceDrawRequest ");
            builder.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@JournalNumber", SqlDbType.BigInt) };
            cmdParms[0].Value = JournalNumber;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string JournalNumberlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Pay_BalanceDrawRequest ");
            builder.Append(" where JournalNumber in (" + JournalNumberlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long JournalNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Pay_BalanceDrawRequest");
            builder.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@JournalNumber", SqlDbType.BigInt) };
            cmdParms[0].Value = JournalNumber;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public decimal GetBalanceDraw(int userid, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT SUM( Amount) FROM Pay_BalanceDrawRequest where UserID=@UserID and RequestStatus=@RequestStatus");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RequestStatus", SqlDbType.Int, 4) };
            cmdParms[0].Value = userid;
            cmdParms[1].Value = Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0M;
            }
            return Convert.ToDecimal(single);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select JournalNumber,RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,Remark ");
            builder.Append(" FROM Pay_BalanceDrawRequest ");
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
            builder.Append(" JournalNumber,RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,Remark ");
            builder.Append(" FROM Pay_BalanceDrawRequest ");
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
                builder.Append("order by T.JournalNumber desc");
            }
            builder.Append(")AS Row, T.*  from Pay_BalanceDrawRequest T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT  baldraw.*,users.UserName  ");
            builder.Append(" FROM  Pay_BalanceDrawRequest AS baldraw ");
            builder.Append(" INNER JOIN  Accounts_Users AS users ");
            builder.Append(" ON baldraw.UserID= users.UserID  ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Pay.BalanceDrawRequest GetModel(long JournalNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 JournalNumber,RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,Remark from Pay_BalanceDrawRequest ");
            builder.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@JournalNumber", SqlDbType.BigInt) };
            cmdParms[0].Value = JournalNumber;
            new Maticsoft.Model.Pay.BalanceDrawRequest();
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
            builder.Append("select count(1) FROM Pay_BalanceDrawRequest ");
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

        public bool Update(Maticsoft.Model.Pay.BalanceDrawRequest model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Pay_BalanceDrawRequest set ");
            builder.Append("RequestTime=@RequestTime,");
            builder.Append("Amount=@Amount,");
            builder.Append("UserID=@UserID,");
            builder.Append("TrueName=@TrueName,");
            builder.Append("BankName=@BankName,");
            builder.Append("BankCard=@BankCard,");
            builder.Append("CardTypeID=@CardTypeID,");
            builder.Append("RequestStatus=@RequestStatus,");
            builder.Append("Remark=@Remark");
            builder.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RequestTime", SqlDbType.DateTime), new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@BankName", SqlDbType.NVarChar, 200), new SqlParameter("@BankCard", SqlDbType.NVarChar, 50), new SqlParameter("@CardTypeID", SqlDbType.Int, 4), new SqlParameter("@RequestStatus", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@JournalNumber", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.RequestTime;
            cmdParms[1].Value = model.Amount;
            cmdParms[2].Value = model.UserID;
            cmdParms[3].Value = model.TrueName;
            cmdParms[4].Value = model.BankName;
            cmdParms[5].Value = model.BankCard;
            cmdParms[6].Value = model.CardTypeID;
            cmdParms[7].Value = model.RequestStatus;
            cmdParms[8].Value = model.Remark;
            cmdParms[9].Value = model.JournalNumber;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatus(string JournalNumberlist, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Update  Pay_BalanceDrawRequest  Set ");
            builder.Append(" RequestStatus=@RequestStatus ");
            builder.Append(" where JournalNumber in (" + JournalNumberlist + ")  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RequestStatus", SqlDbType.Int, 4) };
            cmdParms[0].Value = Status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

