namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SuppProductCategories : ISuppProductCategories
    {
        public bool Add(Maticsoft.Model.Shop.Supplier.SuppProductCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SuppProductCategories(");
            builder.Append("CategoryId,ProductId,CategoryPath)");
            builder.Append(" values (");
            builder.Append("@CategoryId,@ProductId,@CategoryPath)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@CategoryPath", SqlDbType.NVarChar, 0xfa0) };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.ProductId;
            cmdParms[2].Value = model.CategoryPath;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Supplier.SuppProductCategories DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SuppProductCategories categories = new Maticsoft.Model.Shop.Supplier.SuppProductCategories();
            if (row != null)
            {
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    categories.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                {
                    categories.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["CategoryPath"] != null)
                {
                    categories.CategoryPath = row["CategoryPath"].ToString();
                }
            }
            return categories;
        }

        public bool Delete(int CategoryId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SuppProductCategories ");
            builder.Append(" where CategoryId=@CategoryId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = CategoryId;
            cmdParms[1].Value = ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int CategoryId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SuppProductCategories");
            builder.Append(" where CategoryId=@CategoryId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = CategoryId;
            cmdParms[1].Value = ProductId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryId,ProductId,CategoryPath ");
            builder.Append(" FROM Shop_SuppProductCategories ");
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
            builder.Append(" CategoryId,ProductId,CategoryPath ");
            builder.Append(" FROM Shop_SuppProductCategories ");
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
                builder.Append("order by T.ProductId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_SuppProductCategories T ");
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
            return DbHelperSQL.GetMaxID("CategoryId", "Shop_SuppProductCategories");
        }

        public Maticsoft.Model.Shop.Supplier.SuppProductCategories GetModel(int CategoryId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CategoryId,ProductId,CategoryPath from Shop_SuppProductCategories ");
            builder.Append(" where CategoryId=@CategoryId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = CategoryId;
            cmdParms[1].Value = ProductId;
            new Maticsoft.Model.Shop.Supplier.SuppProductCategories();
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
            builder.Append("select count(1) FROM Shop_SuppProductCategories ");
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

        public bool Update(Maticsoft.Model.Shop.Supplier.SuppProductCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SuppProductCategories set ");
            builder.Append("CategoryPath=@CategoryPath");
            builder.Append(" where CategoryId=@CategoryId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryPath", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.CategoryPath;
            cmdParms[1].Value = model.CategoryId;
            cmdParms[2].Value = model.ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

