namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductConsultsType : IProductConsultsType
    {
        public int Add(Maticsoft.Model.Shop.Products.ProductConsultsType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductConsultationsType(");
            builder.Append("TypeName,CreatedDate,IsActive)");
            builder.Append(" VALUES (");
            builder.Append("@TypeName,@CreatedDate,@IsActive)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsActive", SqlDbType.Bit, 1) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.IsActive;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductConsultationsType ");
            builder.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TypeIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductConsultationsType ");
            builder.Append(" WHERE TypeId in (" + TypeIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductConsultationsType");
            builder.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TypeId,TypeName,CreatedDate,IsActive ");
            builder.Append(" FROM Shop_ProductConsultationsType ");
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
            builder.Append(" TypeId,TypeName,CreatedDate,IsActive ");
            builder.Append(" FROM Shop_ProductConsultationsType ");
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
                builder.Append("ORDER BY T.TypeId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_ProductConsultationsType T ");
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
            return DbHelperSQL.GetMaxID("TypeId", "Shop_ProductConsultationsType");
        }

        public Maticsoft.Model.Shop.Products.ProductConsultsType GetModel(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 TypeId,TypeName,CreatedDate,IsActive FROM Shop_ProductConsultationsType ");
            builder.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            Maticsoft.Model.Shop.Products.ProductConsultsType type = new Maticsoft.Model.Shop.Products.ProductConsultsType();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["TypeId"] != null) && (set.Tables[0].Rows[0]["TypeId"].ToString() != ""))
            {
                type.TypeId = int.Parse(set.Tables[0].Rows[0]["TypeId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["TypeName"] != null) && (set.Tables[0].Rows[0]["TypeName"].ToString() != ""))
            {
                type.TypeName = set.Tables[0].Rows[0]["TypeName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                type.CreatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["IsActive"] != null) && (set.Tables[0].Rows[0]["IsActive"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["IsActive"].ToString() == "1") || (set.Tables[0].Rows[0]["IsActive"].ToString().ToLower() == "true"))
                {
                    type.IsActive = true;
                    return type;
                }
                type.IsActive = false;
            }
            return type;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductConsultationsType ");
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

        public bool Update(Maticsoft.Model.Shop.Products.ProductConsultsType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductConsultationsType SET ");
            builder.Append("TypeName=@TypeName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("IsActive=@IsActive");
            builder.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsActive", SqlDbType.Bit, 1), new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.CreatedDate;
            cmdParms[2].Value = model.IsActive;
            cmdParms[3].Value = model.TypeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

