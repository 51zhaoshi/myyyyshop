namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ShippingPayment : IShippingPayment
    {
        public bool Add(Maticsoft.Model.Shop.Shipping.ShippingPayment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ShippingPayment(");
            builder.Append("ShippingModeId,PaymentModeId)");
            builder.Append(" values (");
            builder.Append("@ShippingModeId,@PaymentModeId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@PaymentModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ShippingModeId;
            cmdParms[1].Value = model.PaymentModeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingPayment DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Shipping.ShippingPayment payment = new Maticsoft.Model.Shop.Shipping.ShippingPayment();
            if (row != null)
            {
                if ((row["ShippingModeId"] != null) && (row["ShippingModeId"].ToString() != ""))
                {
                    payment.ShippingModeId = int.Parse(row["ShippingModeId"].ToString());
                }
                if ((row["PaymentModeId"] != null) && (row["PaymentModeId"].ToString() != ""))
                {
                    payment.PaymentModeId = int.Parse(row["PaymentModeId"].ToString());
                }
            }
            return payment;
        }

        public bool Delete(int modeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingPayment ");
            builder.Append(" where ShippingModeId=@ShippingModeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = modeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int ShippingModeId, int PaymentModeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingPayment ");
            builder.Append(" where ShippingModeId=@ShippingModeId and PaymentModeId=@PaymentModeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@PaymentModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ShippingModeId;
            cmdParms[1].Value = PaymentModeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int ShippingModeId, int PaymentModeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ShippingPayment");
            builder.Append(" where ShippingModeId=@ShippingModeId and PaymentModeId=@PaymentModeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@PaymentModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ShippingModeId;
            cmdParms[1].Value = PaymentModeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ShippingModeId,PaymentModeId ");
            builder.Append(" FROM Shop_ShippingPayment ");
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
            builder.Append(" ShippingModeId,PaymentModeId ");
            builder.Append(" FROM Shop_ShippingPayment ");
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
                builder.Append("order by T.PaymentModeId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ShippingPayment T ");
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
            return DbHelperSQL.GetMaxID("ShippingModeId", "Shop_ShippingPayment");
        }

        public Maticsoft.Model.Shop.Shipping.ShippingPayment GetModel(int ShippingModeId, int PaymentModeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ShippingModeId,PaymentModeId from Shop_ShippingPayment ");
            builder.Append(" where ShippingModeId=@ShippingModeId and PaymentModeId=@PaymentModeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@PaymentModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ShippingModeId;
            cmdParms[1].Value = PaymentModeId;
            new Maticsoft.Model.Shop.Shipping.ShippingPayment();
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
            builder.Append("select count(1) FROM Shop_ShippingPayment ");
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

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingPayment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ShippingPayment set ");
            builder.Append("ShippingModeId=@ShippingModeId,");
            builder.Append("PaymentModeId=@PaymentModeId");
            builder.Append(" where ShippingModeId=@ShippingModeId and PaymentModeId=@PaymentModeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@PaymentModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ShippingModeId;
            cmdParms[1].Value = model.PaymentModeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

