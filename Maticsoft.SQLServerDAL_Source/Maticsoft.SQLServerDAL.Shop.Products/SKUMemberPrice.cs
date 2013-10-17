namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SKUMemberPrice : ISKUMemberPrice
    {
        public bool Add(Maticsoft.Model.Shop.Products.SKUMemberPrice model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_SKUMemberPrice(");
            builder.Append("SkuId,GradeId,MemberSalePrice)");
            builder.Append(" VALUES (");
            builder.Append("@SkuId,@GradeId,@MemberSalePrice)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@GradeId", SqlDbType.Int, 4), new SqlParameter("@MemberSalePrice", SqlDbType.Money, 8) };
            cmdParms[0].Value = model.SkuId;
            cmdParms[1].Value = model.GradeId;
            cmdParms[2].Value = model.MemberSalePrice;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(long SkuId, int GradeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_SKUMemberPrice ");
            builder.Append(" WHERE SkuId=@SkuId and GradeId=@GradeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@GradeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SkuId;
            cmdParms[1].Value = GradeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(long SkuId, int GradeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_SKUMemberPrice");
            builder.Append(" WHERE SkuId=@SkuId and GradeId=@GradeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@GradeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SkuId;
            cmdParms[1].Value = GradeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT SkuId,GradeId,MemberSalePrice ");
            builder.Append(" FROM Shop_SKUMemberPrice ");
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
            builder.Append(" SkuId,GradeId,MemberSalePrice ");
            builder.Append(" FROM Shop_SKUMemberPrice ");
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
                builder.Append("ORDER BY T.GradeId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_SKUMemberPrice T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("GradeId", "Shop_SKUMemberPrice");
        }

        public Maticsoft.Model.Shop.Products.SKUMemberPrice GetModel(long SkuId, int GradeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 SkuId,GradeId,MemberSalePrice FROM Shop_SKUMemberPrice ");
            builder.Append(" WHERE SkuId=@SkuId and GradeId=@GradeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@GradeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SkuId;
            cmdParms[1].Value = GradeId;
            Maticsoft.Model.Shop.Products.SKUMemberPrice price = new Maticsoft.Model.Shop.Products.SKUMemberPrice();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["SkuId"] != null) && (set.Tables[0].Rows[0]["SkuId"].ToString() != ""))
            {
                price.SkuId = long.Parse(set.Tables[0].Rows[0]["SkuId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["GradeId"] != null) && (set.Tables[0].Rows[0]["GradeId"].ToString() != ""))
            {
                price.GradeId = int.Parse(set.Tables[0].Rows[0]["GradeId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["MemberSalePrice"] != null) && (set.Tables[0].Rows[0]["MemberSalePrice"].ToString() != ""))
            {
                price.MemberSalePrice = decimal.Parse(set.Tables[0].Rows[0]["MemberSalePrice"].ToString());
            }
            return price;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_SKUMemberPrice ");
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

        public bool Update(Maticsoft.Model.Shop.Products.SKUMemberPrice model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_SKUMemberPrice SET ");
            builder.Append("MemberSalePrice=@MemberSalePrice");
            builder.Append(" WHERE SkuId=@SkuId and GradeId=@GradeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MemberSalePrice", SqlDbType.Money, 8), new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@GradeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.MemberSalePrice;
            cmdParms[1].Value = model.SkuId;
            cmdParms[2].Value = model.GradeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

