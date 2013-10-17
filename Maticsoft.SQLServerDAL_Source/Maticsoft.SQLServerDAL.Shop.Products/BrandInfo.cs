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

    public class BrandInfo : IBrandInfo
    {
        public int Add(Maticsoft.Model.Shop.Products.BrandInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_Brands(");
            builder.Append("BrandName,BrandSpell,Meta_Description,Meta_Keywords,Logo,CompanyUrl,Description,DisplaySequence,Theme)");
            builder.Append(" VALUES (");
            builder.Append("@BrandName,@BrandSpell,@Meta_Description,@Meta_Keywords,@Logo,@CompanyUrl,@Description,@DisplaySequence,@Theme)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BrandName", SqlDbType.NVarChar, 50), new SqlParameter("@BrandSpell", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Logo", SqlDbType.NVarChar, 0xff), new SqlParameter("@CompanyUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Theme", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.BrandName;
            cmdParms[1].Value = model.BrandSpell;
            cmdParms[2].Value = model.Meta_Description;
            cmdParms[3].Value = model.Meta_Keywords;
            cmdParms[4].Value = model.Logo;
            cmdParms[5].Value = model.CompanyUrl;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.DisplaySequence;
            cmdParms[8].Value = model.Theme;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool CreateBrandsAndTypes(Maticsoft.Model.Shop.Products.BrandInfo model, DataProviderAction action)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@BrandName", SqlDbType.NVarChar, 50), new SqlParameter("@BrandSpell", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Logo", SqlDbType.NVarChar, 0xff), new SqlParameter("@CompanyUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Theme", SqlDbType.NVarChar, 100), new SqlParameter("@BrandId", SqlDbType.Int, 4), new SqlParameter("@Action", SqlDbType.Int, 4), new SqlParameter("@BrandIdOutPut", SqlDbType.Int, 4) };
            parameters[0].Value = model.BrandName;
            parameters[1].Value = model.BrandSpell;
            parameters[2].Value = model.Meta_Description;
            parameters[3].Value = model.Meta_Keywords;
            parameters[4].Value = model.Logo;
            parameters[5].Value = model.CompanyUrl;
            parameters[6].Value = model.Description;
            parameters[7].Value = model.DisplaySequence;
            parameters[8].Value = model.Theme;
            parameters[9].Value = model.BrandId;
            parameters[10].Value = (int) action;
            parameters[11].Direction = ParameterDirection.Output;
            DbHelperSQL.RunProcedure("sp_Shop_BrandsCreateUpdateDelete", parameters, out rowsAffected);
            int brandId = 0;
            if (action == DataProviderAction.Create)
            {
                brandId = Convert.ToInt32(parameters[11].Value);
            }
            else
            {
                brandId = model.BrandId;
            }
            if ((rowsAffected <= 0) || (brandId <= 0))
            {
                return false;
            }
            Maticsoft.SQLServerDAL.Shop.Products.ProductTypeBrand brand = new Maticsoft.SQLServerDAL.Shop.Products.ProductTypeBrand();
            if (action == DataProviderAction.Update)
            {
                brand.Delete(null, new int?(brandId));
            }
            foreach (int num3 in model.ProductTypes)
            {
                brand.Add(num3, brandId);
            }
            return true;
        }

        public bool Delete(int BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_Brands ");
            builder.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = BrandId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string BrandIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_Brands ");
            builder.Append(" WHERE BrandId in (" + BrandIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Brands");
            builder.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = BrandId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetBrandsByCateId(int cateId, bool IsChild, int Top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT   ");
            if (Top > 0)
            {
                builder.Append(" top " + Top);
            }
            builder.Append("   * FROM    Shop_Brands ");
            if (cateId > 0)
            {
                builder.Append(" WHERE   EXISTS ( SELECT * FROM   Shop_Products ");
                builder.Append("  WHERE  SaleStatus=1 and  BrandId = Shop_Brands.BrandId ");
                builder.Append(" AND EXISTS ( SELECT * FROM   Shop_ProductCategories  ");
                builder.Append(" WHERE  ProductId = Shop_Products.ProductId  ");
                if (IsChild)
                {
                    builder.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ", cateId);
                    builder.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0})", cateId);
                }
                else
                {
                    builder.AppendFormat("  Shop_ProductCategories.CategoryId = {0}", cateId);
                }
                builder.Append(")) ");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetBrandsListByCateId(int? cateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Shop_Brands ");
            builder.Append("WHERE BrandId IN(SELECT DISTINCT BrandId FROM Shop_Products ");
            if (cateId.HasValue)
            {
                builder.AppendFormat("WHERE CategoryId={0} ", cateId.Value);
            }
            builder.Append(")");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT BrandId,BrandName,BrandSpell,Meta_Description,Meta_Keywords,Logo,CompanyUrl,Description,DisplaySequence,Theme ");
            builder.Append(" FROM Shop_Brands ");
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
            builder.Append(" BrandId,BrandName,BrandSpell,Meta_Description,Meta_Keywords,Logo,CompanyUrl,Description,DisplaySequence,Theme ");
            builder.Append(" FROM Shop_Brands ");
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
                builder.Append("ORDER BY T.BrandId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_Brands T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByProductTypeId(int ProductTypeId, int Top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT   ");
            if (Top > 0)
            {
                builder.Append(" top " + Top);
            }
            builder.Append("  A.ProductTypeId,B.*  ");
            builder.Append("FROM Shop_ProductTypeBrands A ");
            builder.Append("LEFT JOIN Shop_Brands B ON A.BrandId=B.BrandId ");
            if (ProductTypeId != 0)
            {
                builder.AppendFormat("WHERE A.ProductTypeId={0} ", ProductTypeId);
            }
            builder.Append(" ORDER BY DisplaySequence ASC ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByProductTypeId(out int rowCount, out int pageCount, int ProductTypeId, int PageIndex, int PageSize, int action)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ProductTypeId", SqlDbType.Int), new SqlParameter("@PageIndex", SqlDbType.Int), new SqlParameter("@PageSize", SqlDbType.Int), new SqlParameter("@RowsCount", SqlDbType.Float), new SqlParameter("@PageCount", SqlDbType.Float), new SqlParameter("@Action", SqlDbType.Int) };
            parameters[0].Value = ProductTypeId;
            parameters[1].Value = PageIndex;
            parameters[2].Value = PageSize;
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4].Direction = ParameterDirection.Output;
            parameters[5].Value = action;
            DataSet set = DbHelperSQL.RunProcedure("sp_Shop_BrandsPageInfo", parameters, "ds");
            rowCount = Convert.ToInt32(parameters[3].Value);
            pageCount = Convert.ToInt32(parameters[4].Value);
            return set;
        }

        public int GetMaxDisplaySequence()
        {
            return DbHelperSQL.GetMaxID("DisplaySequence", "Shop_Brands");
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("BrandId", "Shop_Brands");
        }

        public Maticsoft.Model.Shop.Products.BrandInfo GetModel(int BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 BrandId,BrandName,BrandSpell,Meta_Description,Meta_Keywords,Logo,CompanyUrl,Description,DisplaySequence,Theme FROM Shop_Brands ");
            builder.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = BrandId;
            Maticsoft.Model.Shop.Products.BrandInfo info = new Maticsoft.Model.Shop.Products.BrandInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["BrandId"] != null) && (set.Tables[0].Rows[0]["BrandId"].ToString() != ""))
            {
                info.BrandId = int.Parse(set.Tables[0].Rows[0]["BrandId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["BrandName"] != null) && (set.Tables[0].Rows[0]["BrandName"].ToString() != ""))
            {
                info.BrandName = set.Tables[0].Rows[0]["BrandName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["BrandSpell"] != null) && (set.Tables[0].Rows[0]["BrandSpell"].ToString() != ""))
            {
                info.BrandSpell = set.Tables[0].Rows[0]["BrandSpell"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Description"] != null) && (set.Tables[0].Rows[0]["Meta_Description"].ToString() != ""))
            {
                info.Meta_Description = set.Tables[0].Rows[0]["Meta_Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Meta_Keywords"] != null) && (set.Tables[0].Rows[0]["Meta_Keywords"].ToString() != ""))
            {
                info.Meta_Keywords = set.Tables[0].Rows[0]["Meta_Keywords"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Logo"] != null) && (set.Tables[0].Rows[0]["Logo"].ToString() != ""))
            {
                info.Logo = set.Tables[0].Rows[0]["Logo"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CompanyUrl"] != null) && (set.Tables[0].Rows[0]["CompanyUrl"].ToString() != ""))
            {
                info.CompanyUrl = set.Tables[0].Rows[0]["CompanyUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Description"] != null) && (set.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                info.Description = set.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["DisplaySequence"] != null) && (set.Tables[0].Rows[0]["DisplaySequence"].ToString() != ""))
            {
                info.DisplaySequence = int.Parse(set.Tables[0].Rows[0]["DisplaySequence"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Theme"] != null) && (set.Tables[0].Rows[0]["Theme"].ToString() != ""))
            {
                info.Theme = set.Tables[0].Rows[0]["Theme"].ToString();
            }
            return info;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Brands ");
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

        public Maticsoft.Model.Shop.Products.BrandInfo GetRelatedProduct(int brandsId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Shop_ProductTypeBrands ");
            builder.AppendFormat(" WHERE BrandId={0}", brandsId);
            DataSet set = DbHelperSQL.Query(builder.ToString());
            IList<int> list = new List<int>();
            Maticsoft.Model.Shop.Products.BrandInfo info = new Maticsoft.Model.Shop.Products.BrandInfo();
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if ((row["ProductTypeId"] != null) && (row["ProductTypeId"].ToString() != ""))
                    {
                        list.Add((int) row["ProductTypeId"]);
                    }
                }
            }
            info.ProductTypes = list;
            return info;
        }

        public Maticsoft.Model.Shop.Products.BrandInfo GetRelatedProduct(int? brandsId, int? ProductTypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Shop_ProductTypeBrands ");
            builder.Append(" WHERE 1=1 ");
            if (brandsId.HasValue)
            {
                builder.AppendFormat(" AND BrandId={0}", brandsId);
            }
            if (ProductTypeId.HasValue)
            {
                builder.AppendFormat(" AND ProductTypeId={0}", ProductTypeId);
            }
            DataSet set = DbHelperSQL.Query(builder.ToString());
            IList<int> list = new List<int>();
            Maticsoft.Model.Shop.Products.BrandInfo info = new Maticsoft.Model.Shop.Products.BrandInfo();
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    if ((brandsId.HasValue && (row["ProductTypeId"] != null)) && (row["ProductTypeId"].ToString() != ""))
                    {
                        list.Add((int) row["ProductTypeId"]);
                    }
                    if ((ProductTypeId.HasValue && (row["BrandId"] != null)) && (row["BrandId"].ToString() != ""))
                    {
                        list.Add((int) row["BrandId"]);
                    }
                }
            }
            info.ProductTypeIdOrBrandsId = list;
            return info;
        }

        public bool Update(Maticsoft.Model.Shop.Products.BrandInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_Brands SET ");
            builder.Append("BrandName=@BrandName,");
            builder.Append("BrandSpell=@BrandSpell,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("Logo=@Logo,");
            builder.Append("CompanyUrl=@CompanyUrl,");
            builder.Append("Description=@Description,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("Theme=@Theme");
            builder.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BrandName", SqlDbType.NVarChar, 50), new SqlParameter("@BrandSpell", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Logo", SqlDbType.NVarChar, 0xff), new SqlParameter("@CompanyUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@Theme", SqlDbType.NVarChar, 100), new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.BrandName;
            cmdParms[1].Value = model.BrandSpell;
            cmdParms[2].Value = model.Meta_Description;
            cmdParms[3].Value = model.Meta_Keywords;
            cmdParms[4].Value = model.Logo;
            cmdParms[5].Value = model.CompanyUrl;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.DisplaySequence;
            cmdParms[8].Value = model.Theme;
            cmdParms[9].Value = model.BrandId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

