namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ProductType : IProductType
    {
        public int Add(Maticsoft.Model.Shop.Products.ProductType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductTypes(");
            builder.Append("TypeName,Remark)");
            builder.Append(" VALUES (");
            builder.Append("@TypeName,@Remark)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.Remark;
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
            builder.Append("DELETE FROM Shop_ProductTypes ");
            builder.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string TypeIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductTypes ");
            builder.Append(" WHERE TypeId in (" + TypeIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteManage(int? TypeId, long? AttributeId, long? ValueId)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int), new SqlParameter("@AttributeId", SqlDbType.BigInt), new SqlParameter("@ValueId", SqlDbType.BigInt) };
            parameters[0].Value = TypeId;
            parameters[1].Value = AttributeId;
            parameters[2].Value = ValueId;
            DbHelperSQL.RunProcedure("sp_Shop_DeleteManage", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Exists(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductTypes");
            builder.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TypeId,TypeName,Remark ");
            builder.Append(" FROM Shop_ProductTypes ");
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
            builder.Append(" TypeId,TypeName,Remark ");
            builder.Append(" FROM Shop_ProductTypes ");
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
            builder.Append(")AS Row, T.*  FROM Shop_ProductTypes T ");
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
            return DbHelperSQL.GetMaxID("TypeId", "Shop_ProductTypes");
        }

        public Maticsoft.Model.Shop.Products.ProductType GetModel(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 TypeId,TypeName,Remark FROM Shop_ProductTypes ");
            builder.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = TypeId;
            Maticsoft.Model.Shop.Products.ProductType type = new Maticsoft.Model.Shop.Products.ProductType();
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
            if ((set.Tables[0].Rows[0]["Remark"] != null) && (set.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                type.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            }
            return type;
        }

        public List<Maticsoft.Model.Shop.Products.ProductType> GetProductTypes()
        {
            List<Maticsoft.Model.Shop.Products.ProductType> list = new List<Maticsoft.Model.Shop.Products.ProductType>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Shop_ProductTypes");
            DataSet set = DbHelperSQL.Query(builder.ToString());
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    Maticsoft.Model.Shop.Products.ProductType model = new Maticsoft.Model.Shop.Products.ProductType();
                    this.LoadEntityData(ref model, row);
                    list.Add(model);
                }
            }
            return list;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductTypes ");
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

        private void LoadEntityData(ref Maticsoft.Model.Shop.Products.ProductType model, DataRow dr)
        {
            if ((dr["TypeId"] != null) && (dr["TypeId"].ToString() != ""))
            {
                model.TypeId = int.Parse(dr["TypeId"].ToString());
            }
            if ((dr["TypeName"] != null) && (dr["TypeName"].ToString() != ""))
            {
                model.TypeName = dr["TypeName"].ToString();
            }
            if ((dr["Remark"] != null) && (dr["Remark"].ToString() != ""))
            {
                model.Remark = dr["Remark"].ToString();
            }
        }

        public bool ProductTypeManage(Maticsoft.Model.Shop.Products.ProductType model, DataProviderAction Action, out int Typeid)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int), new SqlParameter("@TypeName", SqlDbType.NVarChar), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@Action", SqlDbType.Int), new SqlParameter("@TypeIdOut", SqlDbType.Int) };
            parameters[0].Value = model.TypeId;
            parameters[1].Value = model.TypeName;
            parameters[2].Value = model.Remark;
            parameters[3].Value = (int) Action;
            parameters[4].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure("sp_Show_Shop_ProductTypesCreateUpdateDelete", parameters, out rowsAffected);
            int typeId = 0;
            if (Action == DataProviderAction.Create)
            {
                typeId = Convert.ToInt32(parameters[4].Value);
            }
            else
            {
                typeId = model.TypeId;
            }
            if ((rowsAffected > 0) && (typeId > 0))
            {
                Maticsoft.SQLServerDAL.Shop.Products.ProductTypeBrand brand = new Maticsoft.SQLServerDAL.Shop.Products.ProductTypeBrand();
                if (Action == DataProviderAction.Update)
                {
                    brand.Delete(new int?(typeId), null);
                }
                foreach (int num3 in model.BrandsTypes)
                {
                    brand.Add(typeId, num3);
                }
                Typeid = typeId;
                return true;
            }
            Typeid = 0;
            return false;
        }

        public bool SwapSeqManage(int? TypeId, long? AttributeId, long? ValueId, SwapSequenceIndex zIndex, bool UsageMode)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int), new SqlParameter("@AttributeId", SqlDbType.BigInt), new SqlParameter("@ValueId", SqlDbType.BigInt), new SqlParameter("@ZIndex", SqlDbType.Int), new SqlParameter("@UsageMode", SqlDbType.Bit) };
            parameters[0].Value = TypeId;
            parameters[1].Value = AttributeId;
            parameters[2].Value = ValueId;
            parameters[3].Value = (int) zIndex;
            parameters[4].Value = UsageMode;
            DbHelperSQL.RunProcedure("sp_Shop_SwapManage", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductTypes SET ");
            builder.Append("TypeName=@TypeName,");
            builder.Append("Remark=@Remark");
            builder.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TypeName", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TypeName;
            cmdParms[1].Value = model.Remark;
            cmdParms[2].Value = model.TypeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

