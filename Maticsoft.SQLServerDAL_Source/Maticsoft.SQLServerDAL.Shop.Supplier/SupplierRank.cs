namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SupplierRank : ISupplierRank
    {
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierRank model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SupplierRank(");
            builder.Append("Name,ProductCount,ImageCount,Price,IsDefault,IsApproval,Description,RankMin,RankMax)");
            builder.Append(" values (");
            builder.Append("@Name,@ProductCount,@ImageCount,@Price,@IsDefault,@IsApproval,@Description,@RankMin,@RankMax)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@ProductCount", SqlDbType.Int, 4), new SqlParameter("@ImageCount", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Money, 8), new SqlParameter("@IsDefault", SqlDbType.Bit, 1), new SqlParameter("@IsApproval", SqlDbType.Bit, 1), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@RankMin", SqlDbType.Money, 8), new SqlParameter("@RankMax", SqlDbType.Money, 8) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.ProductCount;
            cmdParms[2].Value = model.ImageCount;
            cmdParms[3].Value = model.Price;
            cmdParms[4].Value = model.IsDefault;
            cmdParms[5].Value = model.IsApproval;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.RankMin;
            cmdParms[8].Value = model.RankMax;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierRank DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierRank rank = new Maticsoft.Model.Shop.Supplier.SupplierRank();
            if (row != null)
            {
                if ((row["RankId"] != null) && (row["RankId"].ToString() != ""))
                {
                    rank.RankId = int.Parse(row["RankId"].ToString());
                }
                if (row["Name"] != null)
                {
                    rank.Name = row["Name"].ToString();
                }
                if ((row["ProductCount"] != null) && (row["ProductCount"].ToString() != ""))
                {
                    rank.ProductCount = int.Parse(row["ProductCount"].ToString());
                }
                if ((row["ImageCount"] != null) && (row["ImageCount"].ToString() != ""))
                {
                    rank.ImageCount = int.Parse(row["ImageCount"].ToString());
                }
                if ((row["Price"] != null) && (row["Price"].ToString() != ""))
                {
                    rank.Price = decimal.Parse(row["Price"].ToString());
                }
                if ((row["IsDefault"] != null) && (row["IsDefault"].ToString() != ""))
                {
                    if ((row["IsDefault"].ToString() == "1") || (row["IsDefault"].ToString().ToLower() == "true"))
                    {
                        rank.IsDefault = true;
                    }
                    else
                    {
                        rank.IsDefault = false;
                    }
                }
                if ((row["IsApproval"] != null) && (row["IsApproval"].ToString() != ""))
                {
                    if ((row["IsApproval"].ToString() == "1") || (row["IsApproval"].ToString().ToLower() == "true"))
                    {
                        rank.IsApproval = true;
                    }
                    else
                    {
                        rank.IsApproval = false;
                    }
                }
                if (row["Description"] != null)
                {
                    rank.Description = row["Description"].ToString();
                }
                if ((row["RankMin"] != null) && (row["RankMin"].ToString() != ""))
                {
                    rank.RankMin = decimal.Parse(row["RankMin"].ToString());
                }
                if ((row["RankMax"] != null) && (row["RankMax"].ToString() != ""))
                {
                    rank.RankMax = decimal.Parse(row["RankMax"].ToString());
                }
            }
            return rank;
        }

        public bool Delete(int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierRank ");
            builder.Append(" where RankId=@RankId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string RankIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierRank ");
            builder.Append(" where RankId in (" + RankIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SupplierRank");
            builder.Append(" where RankId=@RankId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RankId,Name,ProductCount,ImageCount,Price,IsDefault,IsApproval,Description,RankMin,RankMax ");
            builder.Append(" FROM Shop_SupplierRank ");
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
            builder.Append(" RankId,Name,ProductCount,ImageCount,Price,IsDefault,IsApproval,Description,RankMin,RankMax ");
            builder.Append(" FROM Shop_SupplierRank ");
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
                builder.Append("order by T.RankId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_SupplierRank T ");
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
            return DbHelperSQL.GetMaxID("RankId", "Shop_SupplierRank");
        }

        public Maticsoft.Model.Shop.Supplier.SupplierRank GetModel(int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RankId,Name,ProductCount,ImageCount,Price,IsDefault,IsApproval,Description,RankMin,RankMax from Shop_SupplierRank ");
            builder.Append(" where RankId=@RankId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            new Maticsoft.Model.Shop.Supplier.SupplierRank();
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
            builder.Append("select count(1) FROM Shop_SupplierRank ");
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

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierRank model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SupplierRank set ");
            builder.Append("Name=@Name,");
            builder.Append("ProductCount=@ProductCount,");
            builder.Append("ImageCount=@ImageCount,");
            builder.Append("Price=@Price,");
            builder.Append("IsDefault=@IsDefault,");
            builder.Append("IsApproval=@IsApproval,");
            builder.Append("Description=@Description,");
            builder.Append("RankMin=@RankMin,");
            builder.Append("RankMax=@RankMax");
            builder.Append(" where RankId=@RankId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@ProductCount", SqlDbType.Int, 4), new SqlParameter("@ImageCount", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Money, 8), new SqlParameter("@IsDefault", SqlDbType.Bit, 1), new SqlParameter("@IsApproval", SqlDbType.Bit, 1), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@RankMin", SqlDbType.Money, 8), new SqlParameter("@RankMax", SqlDbType.Money, 8), new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.ProductCount;
            cmdParms[2].Value = model.ImageCount;
            cmdParms[3].Value = model.Price;
            cmdParms[4].Value = model.IsDefault;
            cmdParms[5].Value = model.IsApproval;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.RankMin;
            cmdParms[8].Value = model.RankMax;
            cmdParms[9].Value = model.RankId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

