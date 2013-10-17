namespace Maticsoft.SQLServerDAL.Shop.Sales
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Sales;
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SalesRuleProduct : ISalesRuleProduct
    {
        public bool Add(Maticsoft.Model.Shop.Sales.SalesRuleProduct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SalesRuleProduct(");
            builder.Append("RuleId,ProductId,ProductName)");
            builder.Append(" values (");
            builder.Append("@RuleId,@ProductId,@ProductName)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.RuleId;
            cmdParms[1].Value = model.ProductId;
            cmdParms[2].Value = model.ProductName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Sales.SalesRuleProduct DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Sales.SalesRuleProduct product = new Maticsoft.Model.Shop.Sales.SalesRuleProduct();
            if (row != null)
            {
                if ((row["RuleId"] != null) && (row["RuleId"].ToString() != ""))
                {
                    product.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                {
                    product.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["ProductName"] != null)
                {
                    product.ProductName = row["ProductName"].ToString();
                }
            }
            return product;
        }

        public bool Delete(int RuleId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesRuleProduct ");
            builder.Append(" where RuleId=@RuleId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = RuleId;
            cmdParms[1].Value = ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteByRule(int RuleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesRuleProduct ");
            builder.Append(" where RuleId=@RuleId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int RuleId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SalesRuleProduct");
            builder.Append(" where RuleId=@RuleId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = RuleId;
            cmdParms[1].Value = ProductId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RuleId,ProductId,ProductName ");
            builder.Append(" FROM Shop_SalesRuleProduct ");
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
            builder.Append(" RuleId,ProductId,ProductName ");
            builder.Append(" FROM Shop_SalesRuleProduct ");
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
            builder.Append(")AS Row, T.*  from Shop_SalesRuleProduct T ");
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
            return DbHelperSQL.GetMaxID("RuleId", "Shop_SalesRuleProduct");
        }

        public Maticsoft.Model.Shop.Sales.SalesRuleProduct GetModel(int RuleId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RuleId,ProductId,ProductName from Shop_SalesRuleProduct ");
            builder.Append(" where RuleId=@RuleId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = RuleId;
            cmdParms[1].Value = ProductId;
            new Maticsoft.Model.Shop.Sales.SalesRuleProduct();
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
            builder.Append("select count(1) FROM Shop_SalesRuleProduct ");
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

        public DataSet GetRuleProducts(int ruleId, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT PSM.ProductId ");
            builder.Append("FROM Shop_SalesRuleProduct PSM ");
            builder.Append("INNER JOIN (SELECT ProductId FROM  Shop_Products P ");
            builder.Append("WHERE P.SaleStatus=1 ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.Append(strWhere);
            }
            builder.Append(")A ON PSM.ProductId = A.ProductId ");
            builder.Append(" WHERE RuleId=@RuleId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ruleId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public bool Update(Maticsoft.Model.Shop.Sales.SalesRuleProduct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SalesRuleProduct set ");
            builder.Append("ProductName=@ProductName");
            builder.Append(" where RuleId=@RuleId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.ProductName;
            cmdParms[1].Value = model.RuleId;
            cmdParms[2].Value = model.ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

