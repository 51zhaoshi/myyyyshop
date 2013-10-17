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

    public class RechargeRequest : IRechargeRequest
    {
        public long Add(Maticsoft.Model.Pay.RechargeRequest model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Pay_RechargeRequest(");
            builder.Append("TradeDate,RechargeBlance,UserId,SellerId,Status,Tradetype,PaymentTypeId,PaymentGateway)");
            builder.Append(" values (");
            builder.Append("@TradeDate,@RechargeBlance,@UserId,@SellerId,@Status,@Tradetype,@PaymentTypeId,@PaymentGateway)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TradeDate", SqlDbType.DateTime), new SqlParameter("@RechargeBlance", SqlDbType.Money, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@SellerId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Tradetype", SqlDbType.Int, 4), new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4), new SqlParameter("@PaymentGateway", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = model.TradeDate;
            cmdParms[1].Value = model.RechargeBlance;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.SellerId;
            cmdParms[4].Value = model.Status;
            cmdParms[5].Value = model.Tradetype;
            cmdParms[6].Value = model.PaymentTypeId;
            cmdParms[7].Value = model.PaymentGateway;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public Maticsoft.Model.Pay.RechargeRequest DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Pay.RechargeRequest request = new Maticsoft.Model.Pay.RechargeRequest();
            if (row != null)
            {
                if ((row["RechargeId"] != null) && (row["RechargeId"].ToString() != ""))
                {
                    request.RechargeId = long.Parse(row["RechargeId"].ToString());
                }
                if ((row["TradeDate"] != null) && (row["TradeDate"].ToString() != ""))
                {
                    request.TradeDate = DateTime.Parse(row["TradeDate"].ToString());
                }
                if ((row["RechargeBlance"] != null) && (row["RechargeBlance"].ToString() != ""))
                {
                    request.RechargeBlance = decimal.Parse(row["RechargeBlance"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    request.UserId = int.Parse(row["UserId"].ToString());
                }
                if ((row["SellerId"] != null) && (row["SellerId"].ToString() != ""))
                {
                    request.SellerId = new int?(int.Parse(row["SellerId"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    request.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["Tradetype"] != null) && (row["Tradetype"].ToString() != ""))
                {
                    request.Tradetype = new int?(int.Parse(row["Tradetype"].ToString()));
                }
                if ((row["PaymentTypeId"] != null) && (row["PaymentTypeId"].ToString() != ""))
                {
                    request.PaymentTypeId = int.Parse(row["PaymentTypeId"].ToString());
                }
                if (row["PaymentGateway"] != null)
                {
                    request.PaymentGateway = row["PaymentGateway"].ToString();
                }
            }
            return request;
        }

        public bool Delete(long RechargeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Pay_RechargeRequest ");
            builder.Append(" where RechargeId=@RechargeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RechargeId", SqlDbType.BigInt) };
            cmdParms[0].Value = RechargeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string RechargeIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Pay_RechargeRequest ");
            builder.Append(" where RechargeId in (" + RechargeIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RechargeId,TradeDate,RechargeBlance,UserId,SellerId,Status,Tradetype,PaymentTypeId,PaymentGateway ");
            builder.Append(" FROM Pay_RechargeRequest ");
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
            builder.Append(" RechargeId,TradeDate,RechargeBlance,UserId,SellerId,Status,Tradetype,PaymentTypeId,PaymentGateway ");
            builder.Append(" FROM Pay_RechargeRequest ");
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
                builder.Append("order by T.RechargeId desc");
            }
            builder.Append(")AS Row, T.*  from Pay_RechargeRequest T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Pay.RechargeRequest GetModel(long RechargeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RechargeId,TradeDate,RechargeBlance,UserId,SellerId,Status,Tradetype,PaymentTypeId,PaymentGateway from Pay_RechargeRequest ");
            builder.Append(" where RechargeId=@RechargeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RechargeId", SqlDbType.BigInt) };
            cmdParms[0].Value = RechargeId;
            new Maticsoft.Model.Pay.RechargeRequest();
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
            builder.Append("select count(1) FROM Pay_RechargeRequest ");
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

        public bool Update(Maticsoft.Model.Pay.RechargeRequest model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Pay_RechargeRequest set ");
            builder.Append("TradeDate=@TradeDate,");
            builder.Append("RechargeBlance=@RechargeBlance,");
            builder.Append("UserId=@UserId,");
            builder.Append("SellerId=@SellerId,");
            builder.Append("Status=@Status,");
            builder.Append("Tradetype=@Tradetype,");
            builder.Append("PaymentTypeId=@PaymentTypeId,");
            builder.Append("PaymentGateway=@PaymentGateway");
            builder.Append(" where RechargeId=@RechargeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TradeDate", SqlDbType.DateTime), new SqlParameter("@RechargeBlance", SqlDbType.Money, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@SellerId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Tradetype", SqlDbType.Int, 4), new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4), new SqlParameter("@PaymentGateway", SqlDbType.NVarChar, 50), new SqlParameter("@RechargeId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.TradeDate;
            cmdParms[1].Value = model.RechargeBlance;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.SellerId;
            cmdParms[4].Value = model.Status;
            cmdParms[5].Value = model.Tradetype;
            cmdParms[6].Value = model.PaymentTypeId;
            cmdParms[7].Value = model.PaymentGateway;
            cmdParms[8].Value = model.RechargeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatus(Maticsoft.Model.Pay.RechargeRequest model, decimal balance)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update Pay_RechargeRequest set ");
            builder.Append("Status=@Status");
            builder.Append(" where RechargeId=@RechargeId");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@RechargeId", SqlDbType.BigInt, 8) };
            para[0].Value = model.Status;
            para[1].Value = model.RechargeId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" update Accounts_UsersExp set ");
            builder2.Append(" Balance=Balance+ @Balance");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Balance", SqlDbType.Money, 8) };
            parameterArray2[0].Value = model.RechargeBlance;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("insert into Pay_BalanceDetails(");
            builder3.Append("UserId,TradeDate,TradeType,Income,Balance)");
            builder3.Append(" values (");
            builder3.Append("@UserId,@TradeDate,@TradeType,@Income,@Balance)");
            builder3.Append(";select @@IDENTITY");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@TradeDate", SqlDbType.DateTime), new SqlParameter("@TradeType", SqlDbType.Int, 4), new SqlParameter("@Income", SqlDbType.Money, 8), new SqlParameter("@Balance", SqlDbType.Money, 8) };
            parameterArray3[0].Value = model.UserId;
            parameterArray3[1].Value = DateTime.Now;
            parameterArray3[2].Value = 1;
            parameterArray3[3].Value = model.RechargeBlance;
            parameterArray3[4].Value = balance + model.RechargeBlance;
            item = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }
    }
}

