namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OrderRemark : IOrderRemark
    {
        public long Add(Maticsoft.Model.Shop.Order.OrderRemark model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_OrderRemark(");
            builder.Append("OrderId,OrderCode,UserId,UserName,Remark,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@OrderId,@OrderCode,@UserId,@UserName,@Remark,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.OrderId;
            cmdParms[1].Value = model.OrderCode;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public Maticsoft.Model.Shop.Order.OrderRemark DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderRemark remark = new Maticsoft.Model.Shop.Order.OrderRemark();
            if (row != null)
            {
                if ((row["RemarkId"] != null) && (row["RemarkId"].ToString() != ""))
                {
                    remark.RemarkId = long.Parse(row["RemarkId"].ToString());
                }
                if ((row["OrderId"] != null) && (row["OrderId"].ToString() != ""))
                {
                    remark.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    remark.OrderCode = row["OrderCode"].ToString();
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    remark.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserName"] != null)
                {
                    remark.UserName = row["UserName"].ToString();
                }
                if (row["Remark"] != null)
                {
                    remark.Remark = row["Remark"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    remark.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
            }
            return remark;
        }

        public bool Delete(long RemarkId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderRemark ");
            builder.Append(" where RemarkId=@RemarkId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RemarkId", SqlDbType.BigInt) };
            cmdParms[0].Value = RemarkId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string RemarkIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderRemark ");
            builder.Append(" where RemarkId in (" + RemarkIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long RemarkId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_OrderRemark");
            builder.Append(" where RemarkId=@RemarkId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RemarkId", SqlDbType.BigInt) };
            cmdParms[0].Value = RemarkId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RemarkId,OrderId,OrderCode,UserId,UserName,Remark,CreatedDate ");
            builder.Append(" FROM Shop_OrderRemark ");
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
            builder.Append(" RemarkId,OrderId,OrderCode,UserId,UserName,Remark,CreatedDate ");
            builder.Append(" FROM Shop_OrderRemark ");
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
                builder.Append("order by T.RemarkId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_OrderRemark T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Order.OrderRemark GetModel(long RemarkId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RemarkId,OrderId,OrderCode,UserId,UserName,Remark,CreatedDate from Shop_OrderRemark ");
            builder.Append(" where RemarkId=@RemarkId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RemarkId", SqlDbType.BigInt) };
            cmdParms[0].Value = RemarkId;
            new Maticsoft.Model.Shop.Order.OrderRemark();
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
            builder.Append("select count(1) FROM Shop_OrderRemark ");
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

        public bool Update(Maticsoft.Model.Shop.Order.OrderRemark model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_OrderRemark set ");
            builder.Append("OrderId=@OrderId,");
            builder.Append("OrderCode=@OrderCode,");
            builder.Append("UserId=@UserId,");
            builder.Append("UserName=@UserName,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" where RemarkId=@RemarkId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@RemarkId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.OrderId;
            cmdParms[1].Value = model.OrderCode;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.RemarkId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

