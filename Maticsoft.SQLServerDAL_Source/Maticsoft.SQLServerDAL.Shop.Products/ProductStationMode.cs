namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductStationMode : IProductStationMode
    {
        public int Add(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductStationModes(");
            builder.Append("ProductId,DisplaySequence,Type)");
            builder.Append(" VALUES (");
            builder.Append("@ProductId,@DisplaySequence,@Type)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.SmallInt, 2) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.DisplaySequence;
            cmdParms[2].Value = model.Type;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int StationId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductStationModes ");
            builder.Append(" WHERE StationId=@StationId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StationId", SqlDbType.Int, 4) };
            cmdParms[0].Value = StationId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int productId, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductStationModes ");
            builder.Append(" WHERE ProductId=@ProductId and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = productId;
            cmdParms[1].Value = type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteByType(int type, int categoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductStationModes ");
            builder.Append(" WHERE Type=@Type ");
            if (categoryId > 0)
            {
                builder.Append(" AND EXISTS ( SELECT DISTINCT *   FROM   Shop_ProductCategories ");
                builder.AppendFormat(" WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0} ) AND ProductId = Shop_ProductStationModes.ProductId ) ", categoryId);
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StationIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductStationModes ");
            builder.Append(" WHERE StationId in (" + StationIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int StationId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductStationModes");
            builder.Append(" WHERE StationId=@StationId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StationId", SqlDbType.Int, 4) };
            cmdParms[0].Value = StationId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(int productId, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductStationModes");
            builder.Append(" WHERE ProductId=@ProductId and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = productId;
            cmdParms[1].Value = type;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT StationId,ProductId,DisplaySequence,Type ");
            builder.Append(" FROM Shop_ProductStationModes ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" StationId,ProductId,DisplaySequence,Type ");
            builder.Append(" FROM Shop_ProductStationModes ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.StationId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_ProductStationModes T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByType(string strType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select P.* From Shop_ProductStationModes S, Shop_Products P ");
            builder.Append(" WHERE S.ProductId = P.ProductId ");
            if (!string.IsNullOrWhiteSpace(strType.Trim()))
            {
                builder.Append(" and S.Type =" + strType);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("StationId", "Shop_ProductStationModes");
        }

        public Maticsoft.Model.Shop.Products.ProductStationMode GetModel(int StationId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 StationId,ProductId,DisplaySequence,Type FROM Shop_ProductStationModes ");
            builder.Append(" WHERE StationId=@StationId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StationId", SqlDbType.Int, 4) };
            cmdParms[0].Value = StationId;
            Maticsoft.Model.Shop.Products.ProductStationMode mode = new Maticsoft.Model.Shop.Products.ProductStationMode();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["StationId"] != null) && (set.Tables[0].Rows[0]["StationId"].ToString() != ""))
            {
                mode.StationId = int.Parse(set.Tables[0].Rows[0]["StationId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                mode.ProductId = long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["DisplaySequence"] != null) && (set.Tables[0].Rows[0]["DisplaySequence"].ToString() != ""))
            {
                mode.DisplaySequence = int.Parse(set.Tables[0].Rows[0]["DisplaySequence"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Type"] != null) && (set.Tables[0].Rows[0]["Type"].ToString() != ""))
            {
                mode.Type = int.Parse(set.Tables[0].Rows[0]["Type"].ToString());
            }
            return mode;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductStationModes ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetStationMode(int modeType, int categoryId, string pName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT PSM.ProductId ");
            builder.Append("FROM Shop_ProductStationModes PSM ");
            builder.Append(" WHERE Type=@Type ");
            if (categoryId > 0)
            {
                builder.Append(" AND EXISTS ( SELECT DISTINCT *   FROM   Shop_ProductCategories ");
                builder.AppendFormat(" WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0} ) AND ProductId = PSM.ProductId ) ", categoryId);
            }
            if (!string.IsNullOrWhiteSpace(pName))
            {
                builder.Append(" AND EXISTS ( SELECT *  FROM   dbo.Shop_Products WHERE  SaleStatus = 1   ");
                builder.AppendFormat("  AND ProductName LIKE '%{0}%'  AND ProductId = PSM.ProductId ) ", pName);
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = modeType;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductStationModes SET ");
            builder.Append("ProductId=@ProductId,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("Type=@Type");
            builder.Append(" WHERE StationId=@StationId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@StationId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.DisplaySequence;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.StationId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

