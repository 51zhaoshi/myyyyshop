namespace Maticsoft.SQLServerDAL.Shop.PromoteSales
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.PromoteSales;
    using Maticsoft.Model.Shop.PromoteSales;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class CountDown : ICountDown
    {
        public int Add(Maticsoft.Model.Shop.PromoteSales.CountDown model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_CountDown(");
            builder.Append("ProductId,EndDate,Description,Sequence,Price,Status)");
            builder.Append(" values (");
            builder.Append("@ProductId,@EndDate,@Description,@Sequence,@Price,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Money, 8), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.EndDate;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.Sequence;
            cmdParms[4].Value = model.Price;
            cmdParms[5].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.PromoteSales.CountDown DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.PromoteSales.CountDown down = new Maticsoft.Model.Shop.PromoteSales.CountDown();
            if (row != null)
            {
                if ((row["CountDownId"] != null) && (row["CountDownId"].ToString() != ""))
                {
                    down.CountDownId = int.Parse(row["CountDownId"].ToString());
                }
                if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                {
                    down.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if ((row["EndDate"] != null) && (row["EndDate"].ToString() != ""))
                {
                    down.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if (row["Description"] != null)
                {
                    down.Description = row["Description"].ToString();
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    down.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["Price"] != null) && (row["Price"].ToString() != ""))
                {
                    down.Price = decimal.Parse(row["Price"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    down.Status = int.Parse(row["Status"].ToString());
                }
            }
            return down;
        }

        public bool Delete(int CountDownId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CountDown ");
            builder.Append(" where CountDownId=@CountDownId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CountDownId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CountDownId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string CountDownIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_CountDown ");
            builder.Append(" where CountDownId in (" + CountDownIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int CountDownId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_CountDown");
            builder.Append(" where CountDownId=@CountDownId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CountDownId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CountDownId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CountDownId,ProductId,EndDate,Description,Sequence,Price,Status ");
            builder.Append(" FROM Shop_CountDown ");
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
            builder.Append(" CountDownId,ProductId,EndDate,Description,Sequence,Price,Status ");
            builder.Append(" FROM Shop_CountDown ");
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
                builder.Append("order by T.CountDownId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_CountDown T ");
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
            return DbHelperSQL.GetMaxID("CountDownId", "Shop_CountDown");
        }

        public Maticsoft.Model.Shop.PromoteSales.CountDown GetModel(int CountDownId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CountDownId,ProductId,EndDate,Description,Sequence,Price,Status from Shop_CountDown ");
            builder.Append(" where CountDownId=@CountDownId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CountDownId", SqlDbType.Int, 4) };
            cmdParms[0].Value = CountDownId;
            new Maticsoft.Model.Shop.PromoteSales.CountDown();
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
            builder.Append("select count(1) FROM Shop_CountDown ");
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

        public bool IsExists(long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_CountDown");
            builder.Append(" where ProductId=@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = ProductId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public int MaxSequence()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT MAX(Sequence) AS Sequence FROM Shop_CountDown");
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Shop.PromoteSales.CountDown model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CountDown set ");
            builder.Append("ProductId=@ProductId,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("Description=@Description,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Price=@Price,");
            builder.Append("Status=@Status");
            builder.Append(" where CountDownId=@CountDownId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Money, 8), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CountDownId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.EndDate;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.Sequence;
            cmdParms[4].Value = model.Price;
            cmdParms[5].Value = model.Status;
            cmdParms[6].Value = model.CountDownId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatus(string ids, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_CountDown set ");
            builder.Append("Status=@Status");
            builder.Append(" where CountDownId in (" + ids + ")  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

