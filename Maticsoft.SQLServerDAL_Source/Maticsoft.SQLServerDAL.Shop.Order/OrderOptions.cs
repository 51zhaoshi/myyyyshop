namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OrderOptions : IOrderOptions
    {
        public bool Add(Maticsoft.Model.Shop.Order.OrderOptions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_OrderOptions(");
            builder.Append("LookupListId,LookupItemId,OrderId,OrderCode,ListDescription,ItemDescription,AdjustedPrice,CustomerTitle,CustomerDescription)");
            builder.Append(" values (");
            builder.Append("@LookupListId,@LookupItemId,@OrderId,@OrderCode,@ListDescription,@ItemDescription,@AdjustedPrice,@CustomerTitle,@CustomerDescription)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4), new SqlParameter("@LookupItemId", SqlDbType.Int, 4), new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ListDescription", SqlDbType.NVarChar, 500), new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 500), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@CustomerTitle", SqlDbType.NVarChar, 500), new SqlParameter("@CustomerDescription", SqlDbType.NVarChar, 500) };
            cmdParms[0].Value = model.LookupListId;
            cmdParms[1].Value = model.LookupItemId;
            cmdParms[2].Value = model.OrderId;
            cmdParms[3].Value = model.OrderCode;
            cmdParms[4].Value = model.ListDescription;
            cmdParms[5].Value = model.ItemDescription;
            cmdParms[6].Value = model.AdjustedPrice;
            cmdParms[7].Value = model.CustomerTitle;
            cmdParms[8].Value = model.CustomerDescription;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Order.OrderOptions DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderOptions options = new Maticsoft.Model.Shop.Order.OrderOptions();
            if (row != null)
            {
                if ((row["LookupListId"] != null) && (row["LookupListId"].ToString() != ""))
                {
                    options.LookupListId = int.Parse(row["LookupListId"].ToString());
                }
                if ((row["LookupItemId"] != null) && (row["LookupItemId"].ToString() != ""))
                {
                    options.LookupItemId = int.Parse(row["LookupItemId"].ToString());
                }
                if ((row["OrderId"] != null) && (row["OrderId"].ToString() != ""))
                {
                    options.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    options.OrderCode = row["OrderCode"].ToString();
                }
                if (row["ListDescription"] != null)
                {
                    options.ListDescription = row["ListDescription"].ToString();
                }
                if (row["ItemDescription"] != null)
                {
                    options.ItemDescription = row["ItemDescription"].ToString();
                }
                if ((row["AdjustedPrice"] != null) && (row["AdjustedPrice"].ToString() != ""))
                {
                    options.AdjustedPrice = decimal.Parse(row["AdjustedPrice"].ToString());
                }
                if (row["CustomerTitle"] != null)
                {
                    options.CustomerTitle = row["CustomerTitle"].ToString();
                }
                if (row["CustomerDescription"] != null)
                {
                    options.CustomerDescription = row["CustomerDescription"].ToString();
                }
            }
            return options;
        }

        public bool Delete(int LookupListId, int LookupItemId, long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderOptions ");
            builder.Append(" where LookupListId=@LookupListId and LookupItemId=@LookupItemId and OrderId=@OrderId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4), new SqlParameter("@LookupItemId", SqlDbType.Int, 4), new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = LookupListId;
            cmdParms[1].Value = LookupItemId;
            cmdParms[2].Value = OrderId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int LookupListId, int LookupItemId, long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_OrderOptions");
            builder.Append(" where LookupListId=@LookupListId and LookupItemId=@LookupItemId and OrderId=@OrderId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4), new SqlParameter("@LookupItemId", SqlDbType.Int, 4), new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = LookupListId;
            cmdParms[1].Value = LookupItemId;
            cmdParms[2].Value = OrderId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select LookupListId,LookupItemId,OrderId,OrderCode,ListDescription,ItemDescription,AdjustedPrice,CustomerTitle,CustomerDescription ");
            builder.Append(" FROM Shop_OrderOptions ");
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
            builder.Append(" LookupListId,LookupItemId,OrderId,OrderCode,ListDescription,ItemDescription,AdjustedPrice,CustomerTitle,CustomerDescription ");
            builder.Append(" FROM Shop_OrderOptions ");
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
                builder.Append("order by T.OrderId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_OrderOptions T ");
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
            return DbHelperSQL.GetMaxID("LookupListId", "Shop_OrderOptions");
        }

        public Maticsoft.Model.Shop.Order.OrderOptions GetModel(int LookupListId, int LookupItemId, long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 LookupListId,LookupItemId,OrderId,OrderCode,ListDescription,ItemDescription,AdjustedPrice,CustomerTitle,CustomerDescription from Shop_OrderOptions ");
            builder.Append(" where LookupListId=@LookupListId and LookupItemId=@LookupItemId and OrderId=@OrderId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4), new SqlParameter("@LookupItemId", SqlDbType.Int, 4), new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = LookupListId;
            cmdParms[1].Value = LookupItemId;
            cmdParms[2].Value = OrderId;
            new Maticsoft.Model.Shop.Order.OrderOptions();
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
            builder.Append("select count(1) FROM Shop_OrderOptions ");
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

        public bool Update(Maticsoft.Model.Shop.Order.OrderOptions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_OrderOptions set ");
            builder.Append("OrderCode=@OrderCode,");
            builder.Append("ListDescription=@ListDescription,");
            builder.Append("ItemDescription=@ItemDescription,");
            builder.Append("AdjustedPrice=@AdjustedPrice,");
            builder.Append("CustomerTitle=@CustomerTitle,");
            builder.Append("CustomerDescription=@CustomerDescription");
            builder.Append(" where LookupListId=@LookupListId and LookupItemId=@LookupItemId and OrderId=@OrderId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ListDescription", SqlDbType.NVarChar, 500), new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 500), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@CustomerTitle", SqlDbType.NVarChar, 500), new SqlParameter("@CustomerDescription", SqlDbType.NVarChar, 500), new SqlParameter("@LookupListId", SqlDbType.Int, 4), new SqlParameter("@LookupItemId", SqlDbType.Int, 4), new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.OrderCode;
            cmdParms[1].Value = model.ListDescription;
            cmdParms[2].Value = model.ItemDescription;
            cmdParms[3].Value = model.AdjustedPrice;
            cmdParms[4].Value = model.CustomerTitle;
            cmdParms[5].Value = model.CustomerDescription;
            cmdParms[6].Value = model.LookupListId;
            cmdParms[7].Value = model.LookupItemId;
            cmdParms[8].Value = model.OrderId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

