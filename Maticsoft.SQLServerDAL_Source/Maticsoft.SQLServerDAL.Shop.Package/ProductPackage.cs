namespace Maticsoft.SQLServerDAL.Shop.Package
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Package;
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductPackage : IProductPackage
    {
        public bool Add(Maticsoft.Model.Shop.Package.ProductPackage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ProductPackage(");
            builder.Append("ProductId,PackageId)");
            builder.Append(" values (");
            builder.Append("@ProductId,@PackageId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.PackageId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Package.ProductPackage DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Package.ProductPackage package = new Maticsoft.Model.Shop.Package.ProductPackage();
            if (row != null)
            {
                if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                {
                    package.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if ((row["PackageId"] != null) && (row["PackageId"].ToString() != ""))
                {
                    package.PackageId = int.Parse(row["PackageId"].ToString());
                }
            }
            return package;
        }

        public bool Delete(long ProductId, int PackageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ProductPackage ");
            builder.Append(" where ProductId=@ProductId and PackageId=@PackageId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = PackageId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(long ProductId, int PackageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ProductPackage");
            builder.Append(" where ProductId=@ProductId and PackageId=@PackageId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = PackageId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ProductId,PackageId ");
            builder.Append(" FROM Shop_ProductPackage ");
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
            builder.Append(" ProductId,PackageId ");
            builder.Append(" FROM Shop_ProductPackage ");
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
                builder.Append("order by T.PackageId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ProductPackage T ");
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
            return DbHelperSQL.GetMaxID("PackageId", "Shop_ProductPackage");
        }

        public Maticsoft.Model.Shop.Package.ProductPackage GetModel(long ProductId, int PackageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ProductId,PackageId from Shop_ProductPackage ");
            builder.Append(" where ProductId=@ProductId and PackageId=@PackageId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = PackageId;
            new Maticsoft.Model.Shop.Package.ProductPackage();
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
            builder.Append("select count(1) FROM Shop_ProductPackage ");
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

        public bool Update(Maticsoft.Model.Shop.Package.ProductPackage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ProductPackage set ");
            builder.Append("ProductId=@ProductId,");
            builder.Append("PackageId=@PackageId");
            builder.Append(" where ProductId=@ProductId and PackageId=@PackageId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@PackageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.PackageId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

