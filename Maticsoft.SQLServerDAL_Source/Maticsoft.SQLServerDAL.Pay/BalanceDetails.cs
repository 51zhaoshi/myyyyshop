namespace Maticsoft.SQLServerDAL.Pay
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Pay;
    using Maticsoft.Model.Pay;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BalanceDetails : IBalanceDetails
    {
        public long Add(Maticsoft.Model.Pay.BalanceDetails model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Pay_BalanceDetails(");
            builder.Append("UserId,TradeDate,TradeType,Income,Expenses,Balance,Payer,Payee,Remark)");
            builder.Append(" values (");
            builder.Append("@UserId,@TradeDate,@TradeType,@Income,@Expenses,@Balance,@Payer,@Payee,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@TradeDate", SqlDbType.DateTime), new SqlParameter("@TradeType", SqlDbType.Int, 4), new SqlParameter("@Income", SqlDbType.Money, 8), new SqlParameter("@Expenses", SqlDbType.Money, 8), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@Payer", SqlDbType.Int, 4), new SqlParameter("@Payee", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.TradeDate;
            cmdParms[2].Value = model.TradeType;
            cmdParms[3].Value = model.Income;
            cmdParms[4].Value = model.Expenses;
            cmdParms[5].Value = model.Balance;
            cmdParms[6].Value = model.Payer;
            cmdParms[7].Value = model.Payee;
            cmdParms[8].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public Maticsoft.Model.Pay.BalanceDetails DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Pay.BalanceDetails details = new Maticsoft.Model.Pay.BalanceDetails();
            if (row != null)
            {
                if ((row["JournalNumber"] != null) && (row["JournalNumber"].ToString() != ""))
                {
                    details.JournalNumber = long.Parse(row["JournalNumber"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    details.UserId = int.Parse(row["UserId"].ToString());
                }
                if ((row["TradeDate"] != null) && (row["TradeDate"].ToString() != ""))
                {
                    details.TradeDate = DateTime.Parse(row["TradeDate"].ToString());
                }
                if ((row["TradeType"] != null) && (row["TradeType"].ToString() != ""))
                {
                    details.TradeType = int.Parse(row["TradeType"].ToString());
                }
                if ((row["Income"] != null) && (row["Income"].ToString() != ""))
                {
                    details.Income = new decimal?(decimal.Parse(row["Income"].ToString()));
                }
                if ((row["Expenses"] != null) && (row["Expenses"].ToString() != ""))
                {
                    details.Expenses = new decimal?(decimal.Parse(row["Expenses"].ToString()));
                }
                if ((row["Balance"] != null) && (row["Balance"].ToString() != ""))
                {
                    details.Balance = decimal.Parse(row["Balance"].ToString());
                }
                if ((row["Payer"] != null) && (row["Payer"].ToString() != ""))
                {
                    details.Payer = new int?(int.Parse(row["Payer"].ToString()));
                }
                if ((row["Payee"] != null) && (row["Payee"].ToString() != ""))
                {
                    details.Payee = new int?(int.Parse(row["Payee"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    details.Remark = row["Remark"].ToString();
                }
            }
            return details;
        }

        public bool Delete(long JournalNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Pay_BalanceDetails ");
            builder.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@JournalNumber", SqlDbType.BigInt) };
            cmdParms[0].Value = JournalNumber;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string JournalNumberlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Pay_BalanceDetails ");
            builder.Append(" where JournalNumber in (" + JournalNumberlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long JournalNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Pay_BalanceDetails");
            builder.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@JournalNumber", SqlDbType.BigInt) };
            cmdParms[0].Value = JournalNumber;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select JournalNumber,UserId,TradeDate,TradeType,Income,Expenses,Balance,Payer,Payee,Remark ");
            builder.Append(" FROM Pay_BalanceDetails ");
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
            builder.Append(" JournalNumber,UserId,TradeDate,TradeType,Income,Expenses,Balance,Payer,Payee,Remark ");
            builder.Append(" FROM Pay_BalanceDetails ");
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
            builder.Append(")AS Row, T.*  from Pay_BalanceDetails T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Pay.BalanceDetails GetModel(long JournalNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 JournalNumber,UserId,TradeDate,TradeType,Income,Expenses,Balance,Payer,Payee,Remark from Pay_BalanceDetails ");
            builder.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@JournalNumber", SqlDbType.BigInt) };
            cmdParms[0].Value = JournalNumber;
            new Maticsoft.Model.Pay.BalanceDetails();
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
            builder.Append("select count(1) FROM Pay_BalanceDetails ");
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

        public bool Update(Maticsoft.Model.Pay.BalanceDetails model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Pay_BalanceDetails set ");
            builder.Append("UserId=@UserId,");
            builder.Append("TradeDate=@TradeDate,");
            builder.Append("TradeType=@TradeType,");
            builder.Append("Income=@Income,");
            builder.Append("Expenses=@Expenses,");
            builder.Append("Balance=@Balance,");
            builder.Append("Payer=@Payer,");
            builder.Append("Payee=@Payee,");
            builder.Append("Remark=@Remark");
            builder.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@TradeDate", SqlDbType.DateTime), new SqlParameter("@TradeType", SqlDbType.Int, 4), new SqlParameter("@Income", SqlDbType.Money, 8), new SqlParameter("@Expenses", SqlDbType.Money, 8), new SqlParameter("@Balance", SqlDbType.Money, 8), new SqlParameter("@Payer", SqlDbType.Int, 4), new SqlParameter("@Payee", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@JournalNumber", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.TradeDate;
            cmdParms[2].Value = model.TradeType;
            cmdParms[3].Value = model.Income;
            cmdParms[4].Value = model.Expenses;
            cmdParms[5].Value = model.Balance;
            cmdParms[6].Value = model.Payer;
            cmdParms[7].Value = model.Payee;
            cmdParms[8].Value = model.Remark;
            cmdParms[9].Value = model.JournalNumber;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

