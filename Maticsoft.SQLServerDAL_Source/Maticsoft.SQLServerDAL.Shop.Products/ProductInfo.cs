namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ProductInfo : IProductInfo
    {
        public long Add(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Products(");
            builder.Append("CategoryId,TypeId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle)");
            builder.Append(" values (");
            builder.Append("@CategoryId,@TypeId,@BrandId,@ProductName,@ProductCode,@SupplierId,@RegionId,@ShortDescription,@Unit,@Description,@Meta_Title,@Meta_Description,@Meta_Keywords,@SaleStatus,@AddedDate,@VistiCounts,@SaleCounts,@DisplaySequence,@LineId,@MarketPrice,@LowestSalePrice,@PenetrationStatus,@MainCategoryPath,@ExtendCategoryPath,@HasSKU,@Points,@ImageUrl,@ThumbnailUrl1,@ThumbnailUrl2,@ThumbnailUrl3,@ThumbnailUrl4,@ThumbnailUrl5,@ThumbnailUrl6,@ThumbnailUrl7,@ThumbnailUrl8,@MaxQuantity,@MinQuantity,@Tags,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShortDescription", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Unit", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SaleStatus", SqlDbType.Int, 4), new SqlParameter("@AddedDate", SqlDbType.DateTime), new SqlParameter("@VistiCounts", SqlDbType.Int, 4), 
                new SqlParameter("@SaleCounts", SqlDbType.Int, 4), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@MarketPrice", SqlDbType.Money, 8), new SqlParameter("@LowestSalePrice", SqlDbType.Money, 8), new SqlParameter("@PenetrationStatus", SqlDbType.SmallInt, 2), new SqlParameter("@MainCategoryPath", SqlDbType.NVarChar, 0x100), new SqlParameter("@ExtendCategoryPath", SqlDbType.NVarChar, 0x100), new SqlParameter("@HasSKU", SqlDbType.Bit, 1), new SqlParameter("@Points", SqlDbType.Decimal, 9), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar, 0xff), 
                new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar, 0xff), new SqlParameter("@MaxQuantity", SqlDbType.Int, 4), new SqlParameter("@MinQuantity", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 50), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300)
             };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.TypeId;
            cmdParms[2].Value = model.BrandId;
            cmdParms[3].Value = model.ProductName;
            cmdParms[4].Value = model.ProductCode;
            cmdParms[5].Value = model.SupplierId;
            cmdParms[6].Value = model.RegionId;
            cmdParms[7].Value = model.ShortDescription;
            cmdParms[8].Value = model.Unit;
            cmdParms[9].Value = model.Description;
            cmdParms[10].Value = model.Meta_Title;
            cmdParms[11].Value = model.Meta_Description;
            cmdParms[12].Value = model.Meta_Keywords;
            cmdParms[13].Value = model.SaleStatus;
            cmdParms[14].Value = model.AddedDate;
            cmdParms[15].Value = model.VistiCounts;
            cmdParms[0x10].Value = model.SaleCounts;
            cmdParms[0x11].Value = model.DisplaySequence;
            cmdParms[0x12].Value = model.LineId;
            cmdParms[0x13].Value = model.MarketPrice;
            cmdParms[20].Value = model.LowestSalePrice;
            cmdParms[0x15].Value = model.PenetrationStatus;
            cmdParms[0x16].Value = model.MainCategoryPath;
            cmdParms[0x17].Value = model.ExtendCategoryPath;
            cmdParms[0x18].Value = model.HasSKU;
            cmdParms[0x19].Value = model.Points;
            cmdParms[0x1a].Value = model.ImageUrl;
            cmdParms[0x1b].Value = model.ThumbnailUrl1;
            cmdParms[0x1c].Value = model.ThumbnailUrl2;
            cmdParms[0x1d].Value = model.ThumbnailUrl3;
            cmdParms[30].Value = model.ThumbnailUrl4;
            cmdParms[0x1f].Value = model.ThumbnailUrl5;
            cmdParms[0x20].Value = model.ThumbnailUrl6;
            cmdParms[0x21].Value = model.ThumbnailUrl7;
            cmdParms[0x22].Value = model.ThumbnailUrl8;
            cmdParms[0x23].Value = model.MaxQuantity;
            cmdParms[0x24].Value = model.MinQuantity;
            cmdParms[0x25].Value = model.Tags;
            cmdParms[0x26].Value = model.SeoUrl;
            cmdParms[0x27].Value = model.SeoImageAlt;
            cmdParms[40].Value = model.SeoImageTitle;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public bool ChangeProductsCategory(string productIds, int categoryId)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ProductIds ", SqlDbType.NVarChar), new SqlParameter("@CategoryId ", SqlDbType.Int), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = productIds;
            parameters[1].Value = categoryId;
            int returnValue = 0;
            DbHelperSQL.RunProcedure("sp_Shop_ChangeProductsCategory", parameters, "ds", out returnValue);
            return (returnValue > 0);
        }

        public Maticsoft.Model.Shop.Products.ProductInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Products.ProductInfo info = new Maticsoft.Model.Shop.Products.ProductInfo();
            if (row != null)
            {
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    info.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["TypeId"] != null) && (row["TypeId"].ToString() != ""))
                {
                    info.TypeId = new int?(int.Parse(row["TypeId"].ToString()));
                }
                if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                {
                    info.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if ((row["BrandId"] != null) && (row["BrandId"].ToString() != ""))
                {
                    info.BrandId = int.Parse(row["BrandId"].ToString());
                }
                if (row["ProductName"] != null)
                {
                    info.ProductName = row["ProductName"].ToString();
                }
                if (row["ProductCode"] != null)
                {
                    info.ProductCode = row["ProductCode"].ToString();
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    info.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    info.RegionId = new int?(int.Parse(row["RegionId"].ToString()));
                }
                if (row["ShortDescription"] != null)
                {
                    info.ShortDescription = row["ShortDescription"].ToString();
                }
                if (row["Unit"] != null)
                {
                    info.Unit = row["Unit"].ToString();
                }
                if (row["Description"] != null)
                {
                    info.Description = row["Description"].ToString();
                }
                if (row["Meta_Title"] != null)
                {
                    info.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    info.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    info.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if ((row["SaleStatus"] != null) && (row["SaleStatus"].ToString() != ""))
                {
                    info.SaleStatus = int.Parse(row["SaleStatus"].ToString());
                }
                if ((row["AddedDate"] != null) && (row["AddedDate"].ToString() != ""))
                {
                    info.AddedDate = DateTime.Parse(row["AddedDate"].ToString());
                }
                if ((row["VistiCounts"] != null) && (row["VistiCounts"].ToString() != ""))
                {
                    info.VistiCounts = int.Parse(row["VistiCounts"].ToString());
                }
                if ((row["SaleCounts"] != null) && (row["SaleCounts"].ToString() != ""))
                {
                    info.SaleCounts = int.Parse(row["SaleCounts"].ToString());
                }
                if ((row["DisplaySequence"] != null) && (row["DisplaySequence"].ToString() != ""))
                {
                    info.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if ((row["LineId"] != null) && (row["LineId"].ToString() != ""))
                {
                    info.LineId = int.Parse(row["LineId"].ToString());
                }
                if ((row["MarketPrice"] != null) && (row["MarketPrice"].ToString() != ""))
                {
                    info.MarketPrice = new decimal?(decimal.Parse(row["MarketPrice"].ToString()));
                }
                if ((row["LowestSalePrice"] != null) && (row["LowestSalePrice"].ToString() != ""))
                {
                    info.LowestSalePrice = decimal.Parse(row["LowestSalePrice"].ToString());
                }
                if ((row["PenetrationStatus"] != null) && (row["PenetrationStatus"].ToString() != ""))
                {
                    info.PenetrationStatus = int.Parse(row["PenetrationStatus"].ToString());
                }
                if (row["MainCategoryPath"] != null)
                {
                    info.MainCategoryPath = row["MainCategoryPath"].ToString();
                }
                if (row["ExtendCategoryPath"] != null)
                {
                    info.ExtendCategoryPath = row["ExtendCategoryPath"].ToString();
                }
                if ((row["HasSKU"] != null) && (row["HasSKU"].ToString() != ""))
                {
                    if ((row["HasSKU"].ToString() == "1") || (row["HasSKU"].ToString().ToLower() == "true"))
                    {
                        info.HasSKU = true;
                    }
                    else
                    {
                        info.HasSKU = false;
                    }
                }
                if ((row["Points"] != null) && (row["Points"].ToString() != ""))
                {
                    info.Points = new decimal?(decimal.Parse(row["Points"].ToString()));
                }
                if (row["ImageUrl"] != null)
                {
                    info.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["ThumbnailUrl1"] != null)
                {
                    info.ThumbnailUrl1 = row["ThumbnailUrl1"].ToString();
                }
                if (row["ThumbnailUrl2"] != null)
                {
                    info.ThumbnailUrl2 = row["ThumbnailUrl2"].ToString();
                }
                if (row["ThumbnailUrl3"] != null)
                {
                    info.ThumbnailUrl3 = row["ThumbnailUrl3"].ToString();
                }
                if (row["ThumbnailUrl4"] != null)
                {
                    info.ThumbnailUrl4 = row["ThumbnailUrl4"].ToString();
                }
                if (row["ThumbnailUrl5"] != null)
                {
                    info.ThumbnailUrl5 = row["ThumbnailUrl5"].ToString();
                }
                if (row["ThumbnailUrl6"] != null)
                {
                    info.ThumbnailUrl6 = row["ThumbnailUrl6"].ToString();
                }
                if (row["ThumbnailUrl7"] != null)
                {
                    info.ThumbnailUrl7 = row["ThumbnailUrl7"].ToString();
                }
                if (row["ThumbnailUrl8"] != null)
                {
                    info.ThumbnailUrl8 = row["ThumbnailUrl8"].ToString();
                }
                if ((row["MaxQuantity"] != null) && (row["MaxQuantity"].ToString() != ""))
                {
                    info.MaxQuantity = new int?(int.Parse(row["MaxQuantity"].ToString()));
                }
                if ((row["MinQuantity"] != null) && (row["MinQuantity"].ToString() != ""))
                {
                    info.MinQuantity = new int?(int.Parse(row["MinQuantity"].ToString()));
                }
                if (row["Tags"] != null)
                {
                    info.Tags = row["Tags"].ToString();
                }
                if (row["SeoUrl"] != null)
                {
                    info.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["SeoImageAlt"] != null)
                {
                    info.SeoImageAlt = row["SeoImageAlt"].ToString();
                }
                if (row["SeoImageTitle"] != null)
                {
                    info.SeoImageTitle = row["SeoImageTitle"].ToString();
                }
            }
            return info;
        }

        public bool Delete(long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Products ");
            builder.Append(" where ProductId=@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ProductIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Products ");
            builder.Append(" where ProductId in (" + ProductIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet DeleteProducts(string Ids, out int Result)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ProductIds ", SqlDbType.NVarChar), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = Ids;
            DataSet set = DbHelperSQL.RunProcedure("sp_Shop_DeleteProducts", parameters, "tb", out Result);
            if (Result == 1)
            {
                return set;
            }
            return null;
        }

        public bool Exists(long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Products");
            builder.Append(" where ProductId=@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = ProductId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string productCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Products");
            builder.Append(" where ProductCode=@ProductCode");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = productCode;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExistsBrands(int BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM Shop_Products");
            builder.Append(" WHERE BrandId=@BrandId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BrandId", SqlDbType.BigInt) };
            cmdParms[0].Value = BrandId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CategoryId,TypeId,ProductId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle ");
            builder.Append(" FROM Shop_Products ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string Ids, string DataField)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT " + DataField + " ");
            builder.Append(" FROM Shop_Products ");
            if (!string.IsNullOrWhiteSpace(Ids.Trim()))
            {
                builder.Append(" WHERE ProductId in(" + Ids + ")");
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
            builder.Append(" CategoryId,TypeId,ProductId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle ");
            builder.Append(" FROM Shop_Products ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByCategoryIdSaleStatus(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT CategoryId,TypeId,ProductId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity ");
            builder.Append(" FROM Shop_Products ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.Append(strWhere);
            }
            builder.Append(" ORDER BY AddedDate DESC  ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByExport(int SaleStatus, string ProductName, int CategoryId, string SKU, int BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT P.*,S.SKU FROM Shop_SKUs S LEFT JOIN Shop_Products P on P.ProductId=S.ProductId  ");
            builder.Append(" WHERE ");
            builder.Append(" SaleStatus =" + SaleStatus);
            if (!string.IsNullOrWhiteSpace(ProductName.Trim()))
            {
                builder.AppendFormat(" and ProductName like '%{0}%' ", ProductName);
            }
            builder.Append(" and CategoryId =" + CategoryId);
            if (!string.IsNullOrWhiteSpace(SKU.Trim()))
            {
                builder.AppendFormat(" and SKU like '%{0}%' ", SKU);
            }
            builder.Append((BrandId == -1) ? string.Empty : (" and BrandId =" + BrandId));
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
            builder.Append(")AS Row, T.*  from Shop_Products T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Products.ProductInfo GetModel(long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CategoryId,TypeId,ProductId,BrandId,ProductName,ProductCode,SupplierId,RegionId,ShortDescription,Unit,Description,Meta_Title,Meta_Description,Meta_Keywords,SaleStatus,AddedDate,VistiCounts,SaleCounts,DisplaySequence,LineId,MarketPrice,LowestSalePrice,PenetrationStatus,MainCategoryPath,ExtendCategoryPath,HasSKU,Points,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8,MaxQuantity,MinQuantity,Tags,SeoUrl,SeoImageAlt,SeoImageTitle from Shop_Products ");
            builder.Append(" where ProductId=@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = ProductId;
            new Maticsoft.Model.Shop.Products.ProductInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetProductCommendListInfo(string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount, long productId, int modeType)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@SqlWhere", SqlDbType.NVarChar), new SqlParameter("@OrderBy", SqlDbType.NVarChar), new SqlParameter("@StartIndex", SqlDbType.Int), new SqlParameter("@EndIndex", SqlDbType.Int), new SqlParameter("@ProductId", SqlDbType.BigInt), new SqlParameter("@ModeType", SqlDbType.Int), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = strWhere;
            parameters[1].Value = orderBy;
            parameters[2].Value = startIndex;
            parameters[3].Value = endIndex;
            parameters[4].Value = productId;
            parameters[5].Value = modeType;
            return DbHelperSQL.RunProcedure("sp_Shop_ProductStationModesInfo", parameters, "ds", out dataCount);
        }

        public DataSet GetProductInfo(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT DISTINCT P.CategoryId, TypeId, P.ProductId, BrandId, ProductName, ProductCode, SupplierId, RegionId, ShortDescription, Unit,  Meta_Title, Meta_Description, Meta_Keywords, SaleStatus, AddedDate, VistiCounts, SaleCounts, P.DisplaySequence, LineId, MarketPrice, LowestSalePrice, PenetrationStatus, MainCategoryPath, ExtendCategoryPath, HasSKU, Points, ImageUrl, ThumbnailUrl1, ThumbnailUrl2, ThumbnailUrl3, ThumbnailUrl4, ThumbnailUrl5, ThumbnailUrl6, ThumbnailUrl7, ThumbnailUrl8, MaxQuantity, MinQuantity, Tags, SeoUrl, SeoImageAlt, SeoImageTitle ");
            builder.Append("FROM Shop_Products P ");
            builder.Append("LEFT JOIN (SELECT * FROM Shop_ProductCategories )PC ON P.ProductId = PC.ProductId ");
            builder.Append("LEFT JOIN Shop_SKUs SKU ON PC.ProductId = SKU.ProductId ");
            builder.Append("LEFT JOIN Shop_ProductStationModes PSM ON SKU.ProductId = PSM.ProductId ");
            builder.Append("WHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.Append(strWhere);
            }
            builder.Append("ORDER BY AddedDate DESC ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetProductListByCategoryId(int? categoryId, string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount)
        {
            StringBuilder builder = new StringBuilder(" from Shop_Products P ");
            builder.Append(" WHERE EXISTS ( SELECT 1 FROM Shop_ProductCategories PC ");
            builder.Append(" WHERE P.ProductId = PC.ProductId ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.Append(strWhere);
            }
            builder.Append(" ) ");
            object single = DbHelperSQL.GetSingle("select count(1) " + builder);
            dataCount = (single == null) ? 0 : Convert.ToInt32(single);
            if (dataCount == 0)
            {
                return null;
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("SELECT * FROM ( ");
            builder2.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                builder2.Append("order by " + orderBy);
            }
            else
            {
                builder2.Append("order by P.ProductId desc");
            }
            builder2.Append(")AS Row, P.* ");
            builder2.Append(builder);
            builder2.Append(" ) TT");
            builder2.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder2.ToString());
        }

        public DataSet GetProductListByCategoryIdEx(int? categoryId, string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount)
        {
            StringBuilder builder = new StringBuilder(" from Shop_Products P JOIN Shop_SKUs SKU  ON  P.ProductId=SKU.ProductId");
            builder.Append(" WHERE EXISTS ( SELECT 1 FROM Shop_ProductCategories PC ");
            builder.Append(" WHERE P.ProductId = PC.ProductId ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.Append(strWhere);
            }
            builder.Append(" ) ");
            object single = DbHelperSQL.GetSingle("select count(1) " + builder);
            dataCount = (single == null) ? 0 : Convert.ToInt32(single);
            if (dataCount == 0)
            {
                return null;
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("SELECT * FROM ( ");
            builder2.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                builder2.Append("order by " + orderBy);
            }
            else
            {
                builder2.Append("order by P.ProductId desc");
            }
            builder2.Append(")AS Row, P.* ,SKU.SalePrice");
            builder2.Append(builder);
            builder2.Append(" ) TT");
            builder2.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder2.ToString());
        }

        public DataSet GetProductListInfo(string strProductIds)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  * FROM     ");
            builder.Append("Shop_Products  ");
            builder.Append("WHERE   SaleStatus = 1  ");
            builder.AppendFormat("AND ProductId  IN ({0}) ", strProductIds);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetProductListInfo(string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount, long productId)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@SqlWhere", SqlDbType.NVarChar), new SqlParameter("@OrderBy", SqlDbType.NVarChar), new SqlParameter("@StartIndex", SqlDbType.Int), new SqlParameter("@EndIndex", SqlDbType.Int), new SqlParameter("@ProductId", SqlDbType.BigInt), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = strWhere;
            parameters[1].Value = orderBy;
            parameters[2].Value = startIndex;
            parameters[3].Value = endIndex;
            parameters[4].Value = productId;
            return DbHelperSQL.RunProcedure("sp_Shop_ProductInfo_Get", parameters, "ds", out dataCount);
        }

        public string GetProductName(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProductName  ");
            builder.Append("FROM Shop_Products ");
            builder.AppendFormat("WHERE ProductId={0} ", productId);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return single.ToString();
            }
            return null;
        }

        public int GetProductNoRecCount(int categoryId, string pName, int modeType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_Products ");
            builder.Append(" WHERE  SaleStatus = 1  ");
            if (categoryId > 0)
            {
                builder.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                builder.AppendFormat("  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = Shop_Products.ProductId ) ", categoryId);
            }
            builder.Append(" AND NOT EXISTS ( SELECT *  FROM   Shop_ProductStationModes ");
            builder.AppendFormat("   WHERE  Type = {0} AND ProductId = Shop_Products.ProductId ) ", modeType);
            if (!string.IsNullOrWhiteSpace(pName))
            {
                builder.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetProductNoRecList(int categoryId, string pName, int modeType, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            builder.Append("order by T.ProductId desc");
            builder.Append(")AS Row, T.*  from Shop_Products T ");
            builder.Append(" WHERE  SaleStatus = 1  ");
            if (categoryId > 0)
            {
                builder.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_ProductCategories  ");
                builder.AppendFormat("  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId ) ", categoryId);
            }
            builder.Append(" AND NOT EXISTS ( SELECT *  FROM   Shop_ProductStationModes ");
            builder.AppendFormat("   WHERE  Type = {0} AND ProductId = T.ProductId ) ", modeType);
            if (!string.IsNullOrWhiteSpace(pName))
            {
                builder.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetProductRanList(int top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  ");
            if (top > 0)
            {
                builder.AppendFormat(" TOP {0} ", top);
            }
            builder.Append(" P.*,sku.SalePrice From Shop_Products P JOIN Shop_SKUs sku  ON p.ProductId=sku.ProductId  where SaleStatus=1 order By NewID()  ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetProductRecList(ProductRecType type, int categoryId, int top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  ");
            if (top > 0)
            {
                builder.AppendFormat(" TOP {0} ", top);
            }
            builder.Append(" P.ProductId,P.ShortDescription,P.ProductName,p.ThumbnailUrl1,p.ThumbnailUrl2,P.ProductCode ,P.LowestSalePrice FROM    Shop_Products P ");
            if (categoryId > 0)
            {
                builder.Append(" INNER JOIN  ");
                builder.Append("(SELECT DISTINCT ProductId FROM Shop_ProductCategories ");
                builder.Append("WHERE CategoryPath LIKE (SELECT Path FROM Shop_Categories ");
                builder.AppendFormat("WHERE CategoryId={0})+'%') C ON P.ProductId = C.ProductId ", categoryId);
            }
            builder.Append(" INNER JOIN Shop_ProductStationModes PSM ON P.ProductId = PSM.ProductId ");
            builder.Append(" WHERE PSM.Type=@Type And P.SaleStatus=1 ");
            builder.Append(" ORDER BY PSM.StationId DESC,P.DisplaySequence ASC ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = (int) type;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetProductsByCid(int Cid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            builder.Append("order by T.ProductId desc");
            builder.Append(")AS Row, T.*  from Shop_Products T ");
            builder.AppendFormat(" WHERE   SaleStatus = 1 ", new object[0]);
            if (Cid > 0)
            {
                builder.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ", new object[0]);
                builder.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ", Cid);
                builder.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            builder.Append(" ) TT");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetProductsCountEx(int Cid, int BrandId, string attrValues, string priceRange)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  count(1) from Shop_Products T ");
            builder.AppendFormat(" WHERE   SaleStatus = 1 ", new object[0]);
            if (BrandId > 0)
            {
                builder.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            if (Cid > 0)
            {
                builder.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ", new object[0]);
                builder.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ", Cid);
                builder.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            if (!string.IsNullOrWhiteSpace(attrValues))
            {
                foreach (string str in attrValues.Split(new char[] { '-' }))
                {
                    int num = Globals.SafeInt(str, 0);
                    if (num > 0)
                    {
                        builder.AppendFormat("  AND EXISTS ( SELECT * FROM   Shop_ProductAttributes WHERE  ProductId = T.ProductId AND ValueId = {0} )", num);
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(priceRange))
            {
                string[] strArray2 = priceRange.Split(new char[] { '-' });
                decimal num2 = Globals.SafeInt(strArray2[0], 0);
                builder.AppendFormat("   AND LowestSalePrice >= {0} ", num2);
                if ((strArray2.Length > 1) && (Globals.SafeInt(strArray2[1], 0) > 0))
                {
                    builder.AppendFormat("   AND LowestSalePrice <= {0} ", strArray2[1]);
                }
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetProductsListEx(int Cid, int BrandId, string attrValues, string priceRange, string mod, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                builder.Append("order by T." + mod);
            }
            else
            {
                builder.Append("order by T.ProductId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Products T ");
            builder.AppendFormat(" WHERE   SaleStatus = 1 ", new object[0]);
            if (BrandId > 0)
            {
                builder.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            if (Cid > 0)
            {
                builder.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ", new object[0]);
                builder.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ", Cid);
                builder.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            if (!string.IsNullOrWhiteSpace(attrValues))
            {
                foreach (string str in attrValues.Split(new char[] { '-' }))
                {
                    int num = Globals.SafeInt(str, 0);
                    if (num > 0)
                    {
                        builder.AppendFormat("  AND EXISTS ( SELECT * FROM   Shop_ProductAttributes WHERE  ProductId = T.ProductId AND ValueId = {0} )", num);
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(priceRange))
            {
                string[] strArray2 = priceRange.Split(new char[] { '-' });
                decimal num2 = Globals.SafeInt(strArray2[0], 0);
                builder.AppendFormat("   AND LowestSalePrice >= {0} ", num2);
                if ((strArray2.Length > 1) && (Globals.SafeInt(strArray2[1], 0) > 0))
                {
                    builder.AppendFormat("   AND LowestSalePrice <= {0} ", strArray2[1]);
                }
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetProSaleModel(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 D.CountDownId,D.Price AS ProSalesPrice,D.EndDate AS ProSalesEndDate ,P.* FROM Shop_CountDown D ,Shop_Products P  ");
            builder.Append("  WHERE   Status = 1 and CountDownId=@CountDownId ");
            builder.Append(" AND P.ProductId=d.ProductId AND SaleStatus=1  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CountDownId", SqlDbType.Int) };
            cmdParms[0].Value = id;
            new Maticsoft.Model.Shop.Products.ProductInfo();
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public int GetProSalesCount()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT  COUNT(1) FROM    Shop_CountDown D  ");
            builder.AppendFormat("  WHERE   Status = 1 AND EndDate>=GETDATE() ", new object[0]);
            builder.AppendFormat("   AND EXISTS ( SELECT ProductId FROM   Shop_Products P WHERE  SaleStatus = 1 AND D.ProductId = P.ProductId ) ", new object[0]);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetProSalesList(int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            builder.Append("order by D.Sequence Desc ");
            builder.Append(")AS Row, D.CountDownId,D.Price AS ProSalesPrice,D.EndDate AS ProSalesEndDate ,P.* FROM  Shop_CountDown D ,Shop_Products P ");
            builder.Append("  WHERE   Status = 1 AND EndDate>=GETDATE()  ");
            builder.Append(" AND P.ProductId=d.ProductId AND SaleStatus=1  ");
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_Products ");
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

        public DataSet GetRecycleList(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  * FROM     ");
            builder.Append("Shop_Products  ");
            builder.Append("WHERE   SaleStatus =2  ");
            if (!string.IsNullOrWhiteSpace(where))
            {
                builder.Append(" and " + where);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetSearchCountEx(int Cid, int BrandId, string keyword, string priceRange)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  count(1) from Shop_Products T ");
            builder.AppendFormat(" WHERE   SaleStatus = 1 ", new object[0]);
            if (BrandId > 0)
            {
                builder.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            if (Cid > 0)
            {
                builder.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ", new object[0]);
                builder.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ", Cid);
                builder.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                builder.AppendFormat(" AND (ProductName like '%{0}%' or ShortDescription like '%{0}%' )", keyword);
            }
            if (!string.IsNullOrWhiteSpace(priceRange))
            {
                string[] strArray = priceRange.Split(new char[] { '-' });
                decimal num = Globals.SafeInt(strArray[0], 0);
                builder.AppendFormat("   AND LowestSalePrice >= {0} ", num);
                if ((strArray.Length > 1) && (Globals.SafeInt(strArray[1], 0) > 0))
                {
                    builder.AppendFormat("   AND LowestSalePrice <= {0} ", strArray[1]);
                }
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetSearchListEx(int Cid, int BrandId, string keyword, string priceRange, string mod, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(mod.Trim()))
            {
                builder.Append("order by T." + mod);
            }
            else
            {
                builder.Append("order by T.ProductId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Products T ");
            builder.AppendFormat(" WHERE   SaleStatus = 1 ", new object[0]);
            if (BrandId > 0)
            {
                builder.AppendFormat("   AND BrandId = {0}", BrandId);
            }
            if (Cid > 0)
            {
                builder.AppendFormat(" AND EXISTS ( SELECT *  FROM   Shop_ProductCategories WHERE  ProductId =T.ProductId  ", new object[0]);
                builder.AppendFormat("   AND ( CategoryPath LIKE ( SELECT Path FROM Shop_Categories WHERE CategoryId = {0}  ) + '|%' ", Cid);
                builder.AppendFormat(" OR Shop_ProductCategories.CategoryId = {0}))", Cid);
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                builder.AppendFormat(" AND (ProductName like '%{0}%' or ShortDescription like '%{0}%') ", keyword);
            }
            if (!string.IsNullOrWhiteSpace(priceRange))
            {
                string[] strArray = priceRange.Split(new char[] { '-' });
                decimal num = Globals.SafeInt(strArray[0], 0);
                builder.AppendFormat("   AND LowestSalePrice >= {0} ", num);
                if ((strArray.Length > 1) && (Globals.SafeInt(strArray[1], 0) > 0))
                {
                    builder.AppendFormat("   AND LowestSalePrice <= {0} ", strArray[1]);
                }
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetTableSchema()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from( ");
            builder.Append("SELECT  * FROM     ");
            builder.Append("INFORMATION_SCHEMA.COLUMNS ");
            builder.Append("WHERE   TABLE_Name ='Shop_Products' ");
            builder.Append("  ) as t");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetTableSchemaEx()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  \tu.name + '.' + t.name AS [table], ");
            builder.Append("            td.value AS [table_desc], ");
            builder.Append("    \t\tc.name AS [column], ");
            builder.Append("    \t\tcd.value AS [column_desc] ");
            builder.Append("FROM    \tsysobjects t ");
            builder.Append("INNER JOIN  sysusers u ");
            builder.Append("    ON\t\tu.uid = t.uid AND t.name='Shop_Products' ");
            builder.Append("LEFT OUTER JOIN sys.extended_properties td ");
            builder.Append("    ON\t\ttd.major_id = t.id ");
            builder.Append("    AND \ttd.minor_id = 0 ");
            builder.Append("    AND\t\ttd.name = 'MS_Description'  ");
            builder.Append("INNER JOIN  syscolumns c ");
            builder.Append("    ON\t\tc.id = t.id ");
            builder.Append("LEFT OUTER JOIN sys.extended_properties cd ");
            builder.Append("    ON\t\tcd.major_id = c.id ");
            builder.Append("    AND\t\tcd.minor_id = c.colid ");
            builder.Append("    AND\t\tcd.name = 'MS_Description'  ");
            builder.Append("WHERE t.type = 'u' ");
            builder.Append("ORDER BY    t.name, c.colorder     ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int MaxSequence()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT MAX(DisplaySequence) AS DisplaySequence FROM Shop_Products");
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int MaxSequence(string CategoryPath)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  MAX(DisplaySequence)  AS DisplaySequenceDisplaySequence FROM Shop_ProductCategories  AS prodcate  ");
            builder.Append(" LEFT JOIN   Shop_Products  AS  prod ON prodcate.ProductId=prod.ProductId  ");
            if (!string.IsNullOrWhiteSpace(CategoryPath))
            {
                builder.Append(" WHERE  prodcate.CategoryPath in ( @CategoryPath )");
            }
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryPath", SqlDbType.NVarChar) };
            cmdParms[0].Value = CategoryPath;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet RelatedProductSource(long productId, int top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (top > 0)
            {
                builder.AppendFormat(" TOP {0} ", top);
            }
            builder.Append("P.* FROM Shop_Products P ");
            builder.Append("INNER JOIN (SELECT RelatedId FROM Shop_RelatedProducts ");
            builder.Append("WHERE ProductId=@ProductId)RP ON P.ProductId = RP.RelatedId AND p.SaleStatus=1  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.Int, 8) };
            cmdParms[0].Value = productId;
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                return set;
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("SELECT  P.* ");
            builder2.Append("FROM    Shop_Products P ");
            builder2.Append("WHERE   ProductId IN ( SELECT  ");
            if (top > 0)
            {
                builder2.Append(" TOP " + top);
            }
            else
            {
                builder2.Append(" TOP 3 ");
            }
            builder2.Append("  ProductId ");
            builder2.Append("  FROM     Shop_ProductCategories ");
            builder2.Append("  WHERE    CategoryId IN ( SELECT  CategoryId ");
            builder2.Append("  FROM    Shop_ProductCategories ");
            builder2.Append(string.Concat(new object[] { "  WHERE   ProductId = ", productId, " )  AND ProductId NOT IN ( ", productId, ") ) " }));
            return DbHelperSQL.Query(builder2.ToString());
        }

        public bool RevertAll()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Products    ");
            builder.Append("set SaleStatus=0 ");
            builder.Append("WHERE   SaleStatus =2  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet SearchProducts(int cateId, ProductSearch model)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int), new SqlParameter("@BrandId", SqlDbType.Int), new SqlParameter("@ValueStr1", SqlDbType.Int), new SqlParameter("@ValueStr2", SqlDbType.Int), new SqlParameter("@ValueStr3", SqlDbType.Int), new SqlParameter("@ValueStr4", SqlDbType.Int), new SqlParameter("@ValueStr5", SqlDbType.Int), new SqlParameter("@ValueStr6", SqlDbType.Int) };
            parameters[0].Value = cateId;
            parameters[1].Value = model.Parameter1;
            parameters[2].Value = model.Parameter2;
            parameters[3].Value = model.Parameter3;
            parameters[4].Value = model.Parameter4;
            parameters[5].Value = model.Parameter5;
            parameters[6].Value = model.Parameter6;
            parameters[7].Value = model.Parameter7;
            return DbHelperSQL.RunProcedure("sp_SearchProducts", parameters, "ds");
        }

        public int StockNum(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT SUM(Stock)Stock FROM Shop_SKUs ");
            builder.Append("WHERE ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = productId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Products set ");
            builder.Append("CategoryId=@CategoryId,");
            builder.Append("TypeId=@TypeId,");
            builder.Append("BrandId=@BrandId,");
            builder.Append("ProductName=@ProductName,");
            builder.Append("ProductCode=@ProductCode,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("RegionId=@RegionId,");
            builder.Append("ShortDescription=@ShortDescription,");
            builder.Append("Unit=@Unit,");
            builder.Append("Description=@Description,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("SaleStatus=@SaleStatus,");
            builder.Append("AddedDate=@AddedDate,");
            builder.Append("VistiCounts=@VistiCounts,");
            builder.Append("SaleCounts=@SaleCounts,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("LineId=@LineId,");
            builder.Append("MarketPrice=@MarketPrice,");
            builder.Append("LowestSalePrice=@LowestSalePrice,");
            builder.Append("PenetrationStatus=@PenetrationStatus,");
            builder.Append("MainCategoryPath=@MainCategoryPath,");
            builder.Append("ExtendCategoryPath=@ExtendCategoryPath,");
            builder.Append("HasSKU=@HasSKU,");
            builder.Append("Points=@Points,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("ThumbnailUrl1=@ThumbnailUrl1,");
            builder.Append("ThumbnailUrl2=@ThumbnailUrl2,");
            builder.Append("ThumbnailUrl3=@ThumbnailUrl3,");
            builder.Append("ThumbnailUrl4=@ThumbnailUrl4,");
            builder.Append("ThumbnailUrl5=@ThumbnailUrl5,");
            builder.Append("ThumbnailUrl6=@ThumbnailUrl6,");
            builder.Append("ThumbnailUrl7=@ThumbnailUrl7,");
            builder.Append("ThumbnailUrl8=@ThumbnailUrl8,");
            builder.Append("MaxQuantity=@MaxQuantity,");
            builder.Append("MinQuantity=@MinQuantity,");
            builder.Append("Tags=@Tags,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("SeoImageAlt=@SeoImageAlt,");
            builder.Append("SeoImageTitle=@SeoImageTitle");
            builder.Append(" where ProductId=@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@TypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShortDescription", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@Unit", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 100), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SaleStatus", SqlDbType.Int, 4), new SqlParameter("@AddedDate", SqlDbType.DateTime), new SqlParameter("@VistiCounts", SqlDbType.Int, 4), 
                new SqlParameter("@SaleCounts", SqlDbType.Int, 4), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@MarketPrice", SqlDbType.Money, 8), new SqlParameter("@LowestSalePrice", SqlDbType.Money, 8), new SqlParameter("@PenetrationStatus", SqlDbType.SmallInt, 2), new SqlParameter("@MainCategoryPath", SqlDbType.NVarChar, 0x100), new SqlParameter("@ExtendCategoryPath", SqlDbType.NVarChar, 0x100), new SqlParameter("@HasSKU", SqlDbType.Bit, 1), new SqlParameter("@Points", SqlDbType.Decimal, 9), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar, 0xff), 
                new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar, 0xff), new SqlParameter("@MaxQuantity", SqlDbType.Int, 4), new SqlParameter("@MinQuantity", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 50), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@ProductId", SqlDbType.BigInt, 8)
             };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.TypeId;
            cmdParms[2].Value = model.BrandId;
            cmdParms[3].Value = model.ProductName;
            cmdParms[4].Value = model.ProductCode;
            cmdParms[5].Value = model.SupplierId;
            cmdParms[6].Value = model.RegionId;
            cmdParms[7].Value = model.ShortDescription;
            cmdParms[8].Value = model.Unit;
            cmdParms[9].Value = model.Description;
            cmdParms[10].Value = model.Meta_Title;
            cmdParms[11].Value = model.Meta_Description;
            cmdParms[12].Value = model.Meta_Keywords;
            cmdParms[13].Value = model.SaleStatus;
            cmdParms[14].Value = model.AddedDate;
            cmdParms[15].Value = model.VistiCounts;
            cmdParms[0x10].Value = model.SaleCounts;
            cmdParms[0x11].Value = model.DisplaySequence;
            cmdParms[0x12].Value = model.LineId;
            cmdParms[0x13].Value = model.MarketPrice;
            cmdParms[20].Value = model.LowestSalePrice;
            cmdParms[0x15].Value = model.PenetrationStatus;
            cmdParms[0x16].Value = model.MainCategoryPath;
            cmdParms[0x17].Value = model.ExtendCategoryPath;
            cmdParms[0x18].Value = model.HasSKU;
            cmdParms[0x19].Value = model.Points;
            cmdParms[0x1a].Value = model.ImageUrl;
            cmdParms[0x1b].Value = model.ThumbnailUrl1;
            cmdParms[0x1c].Value = model.ThumbnailUrl2;
            cmdParms[0x1d].Value = model.ThumbnailUrl3;
            cmdParms[30].Value = model.ThumbnailUrl4;
            cmdParms[0x1f].Value = model.ThumbnailUrl5;
            cmdParms[0x20].Value = model.ThumbnailUrl6;
            cmdParms[0x21].Value = model.ThumbnailUrl7;
            cmdParms[0x22].Value = model.ThumbnailUrl8;
            cmdParms[0x23].Value = model.MaxQuantity;
            cmdParms[0x24].Value = model.MinQuantity;
            cmdParms[0x25].Value = model.Tags;
            cmdParms[0x26].Value = model.SeoUrl;
            cmdParms[0x27].Value = model.SeoImageAlt;
            cmdParms[40].Value = model.SeoImageTitle;
            cmdParms[0x29].Value = model.ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strSetValue)
        {
            if (string.IsNullOrWhiteSpace(IDlist))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Products set " + strSetValue);
            builder.Append(" where ProductId in(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) == IDlist.Length);
        }

        public bool UpdateLowestSalePrice(long productId, decimal price)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Products set  LowestSalePrice=@LowestSalePrice");
            builder.Append(" where ProductId =@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt), new SqlParameter("@LowestSalePrice", SqlDbType.Money, 8) };
            cmdParms[0].Value = productId;
            cmdParms[1].Value = price;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateMarketPrice(long productId, decimal price)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Products set  MarketPrice=@MarketPrice");
            builder.Append(" where ProductId =@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt), new SqlParameter("@MarketPrice", SqlDbType.Money, 8) };
            cmdParms[0].Value = productId;
            cmdParms[1].Value = price;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateProductName(long productId, string strSetValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Products set  ProductName=@ProductName");
            builder.Append(" where ProductId =@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt), new SqlParameter("@ProductName", SqlDbType.NVarChar) };
            cmdParms[0].Value = productId;
            cmdParms[1].Value = strSetValue;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatus(long productId, int SaleStatus)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Products set  SaleStatus=@SaleStatus");
            builder.Append(" where ProductId =@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt), new SqlParameter("@SaleStatus", SqlDbType.Int, 4) };
            cmdParms[0].Value = productId;
            cmdParms[1].Value = SaleStatus;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

