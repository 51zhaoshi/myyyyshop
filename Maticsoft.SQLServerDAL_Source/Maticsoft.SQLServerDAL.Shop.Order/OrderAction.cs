namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OrderAction : IOrderAction
    {
        public long Add(Maticsoft.Model.Shop.Order.OrderAction model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_OrderAction(");
            builder.Append("OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark)");
            builder.Append(" values (");
            builder.Append("@OrderId,@OrderCode,@UserId,@Username,@ActionCode,@ActionDate,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Username", SqlDbType.NVarChar, 200), new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100), new SqlParameter("@ActionDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8) };
            cmdParms[0].Value = model.OrderId;
            cmdParms[1].Value = model.OrderCode;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.Username;
            cmdParms[4].Value = model.ActionCode;
            cmdParms[5].Value = model.ActionDate;
            cmdParms[6].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public Maticsoft.Model.Shop.Order.OrderAction DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderAction action = new Maticsoft.Model.Shop.Order.OrderAction();
            if (row != null)
            {
                if ((row["ActionId"] != null) && (row["ActionId"].ToString() != ""))
                {
                    action.ActionId = long.Parse(row["ActionId"].ToString());
                }
                if ((row["OrderId"] != null) && (row["OrderId"].ToString() != ""))
                {
                    action.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    action.OrderCode = row["OrderCode"].ToString();
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    action.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["Username"] != null)
                {
                    action.Username = row["Username"].ToString();
                }
                if (row["ActionCode"] != null)
                {
                    action.ActionCode = row["ActionCode"].ToString();
                }
                if ((row["ActionDate"] != null) && (row["ActionDate"].ToString() != ""))
                {
                    action.ActionDate = DateTime.Parse(row["ActionDate"].ToString());
                }
                if (row["Remark"] != null)
                {
                    action.Remark = row["Remark"].ToString();
                }
            }
            return action;
        }

        public bool Delete(long ActionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderAction ");
            builder.Append(" where ActionId=@ActionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionId", SqlDbType.BigInt) };
            cmdParms[0].Value = ActionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ActionIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderAction ");
            builder.Append(" where ActionId in (" + ActionIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long ActionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_OrderAction");
            builder.Append(" where ActionId=@ActionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionId", SqlDbType.BigInt) };
            cmdParms[0].Value = ActionId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ActionId,OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark ");
            builder.Append(" FROM Shop_OrderAction ");
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
            builder.Append(" ActionId,OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark ");
            builder.Append(" FROM Shop_OrderAction ");
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
                builder.Append("order by T.ActionId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_OrderAction T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Order.OrderAction GetModel(long ActionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ActionId,OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark from Shop_OrderAction ");
            builder.Append(" where ActionId=@ActionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ActionId", SqlDbType.BigInt) };
            cmdParms[0].Value = ActionId;
            new Maticsoft.Model.Shop.Order.OrderAction();
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
            builder.Append("select count(1) FROM Shop_OrderAction ");
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

        public bool Update(Maticsoft.Model.Shop.Order.OrderAction model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_OrderAction set ");
            builder.Append("OrderId=@OrderId,");
            builder.Append("OrderCode=@OrderCode,");
            builder.Append("UserId=@UserId,");
            builder.Append("Username=@Username,");
            builder.Append("ActionCode=@ActionCode,");
            builder.Append("ActionDate=@ActionDate,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ActionId=@ActionId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Username", SqlDbType.NVarChar, 200), new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100), new SqlParameter("@ActionDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ActionId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.OrderId;
            cmdParms[1].Value = model.OrderCode;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.Username;
            cmdParms[4].Value = model.ActionCode;
            cmdParms[5].Value = model.ActionDate;
            cmdParms[6].Value = model.Remark;
            cmdParms[7].Value = model.ActionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

