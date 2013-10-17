namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.Shop;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Request;
    using Maticsoft.TaoBao.Response;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ProductInfo
    {
        private readonly IProductInfo dal = DAShopProducts.CreateProductInfo();

        public long Add(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            return this.dal.Add(model);
        }

        public bool ChangeProductsCategory(string productIds, int categoryId)
        {
            return this.dal.ChangeProductsCategory(productIds, categoryId);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo item = new Maticsoft.Model.Shop.Products.ProductInfo();
                    if ((dt.Rows[i]["CategoryId"] != null) && (dt.Rows[i]["CategoryId"].ToString() != ""))
                    {
                        item.CategoryId = int.Parse(dt.Rows[i]["CategoryId"].ToString());
                    }
                    if ((dt.Rows[i]["TypeId"] != null) && (dt.Rows[i]["TypeId"].ToString() != ""))
                    {
                        item.TypeId = new int?(int.Parse(dt.Rows[i]["TypeId"].ToString()));
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["BrandId"] != null) && (dt.Rows[i]["BrandId"].ToString() != ""))
                    {
                        item.BrandId = int.Parse(dt.Rows[i]["BrandId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductName"] != null) && (dt.Rows[i]["ProductName"].ToString() != ""))
                    {
                        item.ProductName = dt.Rows[i]["ProductName"].ToString();
                    }
                    if ((dt.Rows[i]["ProductCode"] != null) && (dt.Rows[i]["ProductCode"].ToString() != ""))
                    {
                        item.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                    }
                    if ((dt.Rows[i]["SupplierId"] != null) && (dt.Rows[i]["SupplierId"].ToString() != ""))
                    {
                        item.SupplierId = int.Parse(dt.Rows[i]["SupplierId"].ToString());
                    }
                    if ((dt.Rows[i]["RegionId"] != null) && (dt.Rows[i]["RegionId"].ToString() != ""))
                    {
                        item.RegionId = new int?(int.Parse(dt.Rows[i]["RegionId"].ToString()));
                    }
                    if ((dt.Rows[i]["ShortDescription"] != null) && (dt.Rows[i]["ShortDescription"].ToString() != ""))
                    {
                        item.ShortDescription = dt.Rows[i]["ShortDescription"].ToString();
                    }
                    if ((dt.Rows[i]["Unit"] != null) && (dt.Rows[i]["Unit"].ToString() != ""))
                    {
                        item.Unit = dt.Rows[i]["Unit"].ToString();
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Title"] != null) && (dt.Rows[i]["Meta_Title"].ToString() != ""))
                    {
                        item.Meta_Title = dt.Rows[i]["Meta_Title"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Description"] != null) && (dt.Rows[i]["Meta_Description"].ToString() != ""))
                    {
                        item.Meta_Description = dt.Rows[i]["Meta_Description"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Keywords"] != null) && (dt.Rows[i]["Meta_Keywords"].ToString() != ""))
                    {
                        item.Meta_Keywords = dt.Rows[i]["Meta_Keywords"].ToString();
                    }
                    if ((dt.Rows[i]["SaleStatus"] != null) && (dt.Rows[i]["SaleStatus"].ToString() != ""))
                    {
                        item.SaleStatus = int.Parse(dt.Rows[i]["SaleStatus"].ToString());
                    }
                    if ((dt.Rows[i]["AddedDate"] != null) && (dt.Rows[i]["AddedDate"].ToString() != ""))
                    {
                        item.AddedDate = DateTime.Parse(dt.Rows[i]["AddedDate"].ToString());
                    }
                    if ((dt.Rows[i]["VistiCounts"] != null) && (dt.Rows[i]["VistiCounts"].ToString() != ""))
                    {
                        item.VistiCounts = int.Parse(dt.Rows[i]["VistiCounts"].ToString());
                    }
                    if ((dt.Rows[i]["SaleCounts"] != null) && (dt.Rows[i]["SaleCounts"].ToString() != ""))
                    {
                        item.SaleCounts = int.Parse(dt.Rows[i]["SaleCounts"].ToString());
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["LineId"] != null) && (dt.Rows[i]["LineId"].ToString() != ""))
                    {
                        item.LineId = int.Parse(dt.Rows[i]["LineId"].ToString());
                    }
                    if ((dt.Rows[i]["MarketPrice"] != null) && (dt.Rows[i]["MarketPrice"].ToString() != ""))
                    {
                        item.MarketPrice = new decimal?(decimal.Parse(dt.Rows[i]["MarketPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["LowestSalePrice"] != null) && (dt.Rows[i]["LowestSalePrice"].ToString() != ""))
                    {
                        item.LowestSalePrice = decimal.Parse(dt.Rows[i]["LowestSalePrice"].ToString());
                    }
                    if ((dt.Rows[i]["PenetrationStatus"] != null) && (dt.Rows[i]["PenetrationStatus"].ToString() != ""))
                    {
                        item.PenetrationStatus = int.Parse(dt.Rows[i]["PenetrationStatus"].ToString());
                    }
                    if ((dt.Rows[i]["MainCategoryPath"] != null) && (dt.Rows[i]["MainCategoryPath"].ToString() != ""))
                    {
                        item.MainCategoryPath = dt.Rows[i]["MainCategoryPath"].ToString();
                    }
                    if ((dt.Rows[i]["ExtendCategoryPath"] != null) && (dt.Rows[i]["ExtendCategoryPath"].ToString() != ""))
                    {
                        item.ExtendCategoryPath = dt.Rows[i]["ExtendCategoryPath"].ToString();
                    }
                    if ((dt.Rows[i]["HasSKU"] != null) && (dt.Rows[i]["HasSKU"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["HasSKU"].ToString() == "1") || (dt.Rows[i]["HasSKU"].ToString().ToLower() == "true"))
                        {
                            item.HasSKU = true;
                        }
                        else
                        {
                            item.HasSKU = false;
                        }
                    }
                    if ((dt.Rows[i]["Points"] != null) && (dt.Rows[i]["Points"].ToString() != ""))
                    {
                        item.Points = new decimal?(decimal.Parse(dt.Rows[i]["Points"].ToString()));
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl1"] != null) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != ""))
                    {
                        item.ThumbnailUrl1 = dt.Rows[i]["ThumbnailUrl1"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl2"] != null) && (dt.Rows[i]["ThumbnailUrl2"].ToString() != ""))
                    {
                        item.ThumbnailUrl2 = dt.Rows[i]["ThumbnailUrl2"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl3"] != null) && (dt.Rows[i]["ThumbnailUrl3"].ToString() != ""))
                    {
                        item.ThumbnailUrl3 = dt.Rows[i]["ThumbnailUrl3"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl4"] != null) && (dt.Rows[i]["ThumbnailUrl4"].ToString() != ""))
                    {
                        item.ThumbnailUrl4 = dt.Rows[i]["ThumbnailUrl4"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl5"] != null) && (dt.Rows[i]["ThumbnailUrl5"].ToString() != ""))
                    {
                        item.ThumbnailUrl5 = dt.Rows[i]["ThumbnailUrl5"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl6"] != null) && (dt.Rows[i]["ThumbnailUrl6"].ToString() != ""))
                    {
                        item.ThumbnailUrl6 = dt.Rows[i]["ThumbnailUrl6"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl7"] != null) && (dt.Rows[i]["ThumbnailUrl7"].ToString() != ""))
                    {
                        item.ThumbnailUrl7 = dt.Rows[i]["ThumbnailUrl7"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl8"] != null) && (dt.Rows[i]["ThumbnailUrl8"].ToString() != ""))
                    {
                        item.ThumbnailUrl8 = dt.Rows[i]["ThumbnailUrl8"].ToString();
                    }
                    if ((dt.Rows[i]["MaxQuantity"] != null) && (dt.Rows[i]["MaxQuantity"].ToString() != ""))
                    {
                        item.MaxQuantity = new int?(int.Parse(dt.Rows[i]["MaxQuantity"].ToString()));
                    }
                    if ((dt.Rows[i]["MinQuantity"] != null) && (dt.Rows[i]["MinQuantity"].ToString() != ""))
                    {
                        item.MinQuantity = new int?(int.Parse(dt.Rows[i]["MinQuantity"].ToString()));
                    }
                    if ((dt.Rows[i]["Tags"] != null) && (dt.Rows[i]["Tags"].ToString() != ""))
                    {
                        item.Tags = dt.Rows[i]["Tags"].ToString();
                    }
                    if ((dt.Rows[i]["SeoUrl"] != null) && (dt.Rows[i]["SeoUrl"].ToString() != ""))
                    {
                        item.SeoUrl = dt.Rows[i]["SeoUrl"].ToString();
                    }
                    if ((dt.Rows[i]["SeoImageAlt"] != null) && (dt.Rows[i]["SeoImageAlt"].ToString() != ""))
                    {
                        item.SeoImageAlt = dt.Rows[i]["SeoImageAlt"].ToString();
                    }
                    if ((dt.Rows[i]["SeoImageTitle"] != null) && (dt.Rows[i]["SeoImageTitle"].ToString() != ""))
                    {
                        item.SeoImageTitle = dt.Rows[i]["SeoImageTitle"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long ProductId)
        {
            return this.dal.Delete(ProductId);
        }

        public bool DeleteList(string ProductIdlist)
        {
            return this.dal.DeleteList(ProductIdlist);
        }

        public DataSet DeleteProducts(string Ids, out int Result)
        {
            return this.dal.DeleteProducts(Ids, out Result);
        }

        public bool Exists(long ProductId)
        {
            return this.dal.Exists(ProductId);
        }

        public bool Exists(string productCode)
        {
            return this.dal.Exists(productCode);
        }

        public bool ExistsBrands(int BrandId)
        {
            return this.dal.ExistsBrands(BrandId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetCommendProductsList(string[] selectedPids, string pName, string categoryId, int startIdex, int endIndex, out int dataCount, long productId, int? commendType)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pName))
            {
                builder.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                builder.AppendFormat(" AND CategoryId ={0}", categoryId);
            }
            if ((selectedPids != null) && (selectedPids.Length > 0))
            {
                builder.AppendFormat(" AND ProductId NOT IN ({0})", (object[]) selectedPids);
            }
            if (commendType.HasValue && (commendType.Value == 0))
            {
                builder.AppendFormat(" AND ProductId NOT IN ({0})", (object[]) selectedPids);
            }
            DataSet ds = this.dal.GetProductListInfo(builder.ToString(), " ORDER BY SaleCounts DESC ", startIdex, endIndex, out dataCount, productId);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetKeyWordList(int top, string keyWord)
        {
            string cacheKey = "GetKeyWordList-" + top + keyWord;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("  SaleStatus = 1  ");
                    builder.AppendFormat("  and  ProductName like '%{0}%' or ShortDescription like '%{0}%'  ", keyWord);
                    DataSet set = this.dal.GetList(top, builder.ToString(), "  NewID()");
                    cache = this.DataTableToList(set.Tables[0]);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.Shop.Products.ProductInfo>) cache;
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(string strWhere, string DataField)
        {
            return this.dal.GetList(strWhere, DataField);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByCategoryIdSaleStatus(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            if (model == null)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" WHERE CategoryId={0}", model.CategoryId);
            builder.AppendFormat(" AND SaleStatus <>{0}", model.SaleStatus);
            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                builder.AppendFormat(" AND ProductName LIKE '%{0}%'", model.ProductName);
            }
            return this.dal.GetListByCategoryIdSaleStatus(builder.ToString());
        }

        public DataSet GetListByExport(int SaleStatus, string ProductName, int CategoryId, string SKU, int BrandId)
        {
            return this.dal.GetListByExport(SaleStatus, ProductName, CategoryId, SKU, BrandId);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListEx(string InIds, string productName, string OutIds, int CategoryId = 0)
        {
            return this.dal.GetList(this.GetListExSql(InIds, productName, OutIds, CategoryId));
        }

        public string GetListExSql(string InIds, string productName, string OutIds, int CategoryId = 0)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(InIds))
            {
                builder.Append(" ProductId in (" + InIds.TrimStart(new char[] { ',' }).TrimEnd(new char[] { ',' }) + ")");
            }
            if (!string.IsNullOrEmpty(OutIds))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" ProductId not in (" + OutIds.TrimStart(new char[] { ',' }).TrimEnd(new char[] { ',' }) + ")");
            }
            if (!string.IsNullOrEmpty(productName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" ProductName like '%" + productName + "%'");
            }
            if (CategoryId > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" CategoryId =" + CategoryId);
            }
            return builder.ToString();
        }

        public Maticsoft.Model.Shop.Products.ProductInfo GetModel(long ProductId)
        {
            return this.dal.GetModel(ProductId);
        }

        public Maticsoft.Model.Shop.Products.ProductInfo GetModelByCache(long ProductId)
        {
            string cacheKey = "ProductsModel-" + ProductId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ProductId);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Shop.Products.ProductInfo) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetModelList(long productId)
        {
            DataSet list = this.dal.GetList(string.Format(" ProductId={0}", productId));
            if ((list != null) && (list.Tables.Count > 0))
            {
                return this.DataTableToList(list.Tables[0]);
            }
            return null;
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetNoRuleProductCount(string pName, string categoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SaleStatus=1");
            if (!string.IsNullOrWhiteSpace(pName))
            {
                builder.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                builder.AppendFormat(" AND ProductId IN( SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}) ) ", categoryId);
            }
            builder.Append(" AND  NOT EXISTS(SELECT *  FROM Shop_SalesRuleProduct WHERE ProductId=Shop_Products.ProductId) ");
            return this.GetRecordCount(builder.ToString());
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetNoRuleProductList(string pName, string categoryId, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SaleStatus=1");
            if (!string.IsNullOrWhiteSpace(pName))
            {
                builder.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                builder.AppendFormat(" AND ProductId IN( SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}) ) ", categoryId);
            }
            builder.Append(" AND  NOT EXISTS(SELECT *  FROM Shop_SalesRuleProduct WHERE ProductId=T.ProductId) ");
            DataSet ds = this.GetListByPage(builder.ToString(), "  SaleCounts DESC ", startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.DataTableToList(ds.Tables[0]);
        }

        public DataSet GetProductInfo(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            StringBuilder builder = new StringBuilder();
            if (model == null)
            {
                return this.dal.GetProductInfo(null);
            }
            if (!string.IsNullOrWhiteSpace(model.ProductName))
            {
                builder.AppendFormat(" AND ProductName LIKE '%{0}%' ", model.ProductName);
            }
            if (model.CategoryId > 0)
            {
                builder.AppendFormat("AND PC.CategoryPath LIKE '{0}%' ", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.SearchProductCategories))
            {
                builder.AppendFormat(" AND PC.CategoryId IN ( {0} ) ", model.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(model.ProductCode))
            {
                builder.AppendFormat("AND SKU.SKU LIKE '%{0}%' ", model.ProductCode);
            }
            if (model.SupplierId > 0)
            {
                builder.AppendFormat("AND P.SupplierId = {0} ", model.SupplierId);
            }
            builder.AppendFormat(" AND P.SaleStatus= {0}  ", model.SaleStatus);
            return this.dal.GetProductInfo(builder.ToString());
        }

        public string GetProductName(long productId)
        {
            return this.dal.GetProductName(productId);
        }

        public int GetProductNoRecCount(int categoryId, string pName, int modeType)
        {
            return this.dal.GetProductNoRecCount(categoryId, pName, modeType);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductNoRecList(int categoryId, string pName, int modeType, int startIndex, int endIndex)
        {
            DataSet ds = this.dal.GetProductNoRecList(categoryId, pName, modeType, startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductRanList(int top = -1)
        {
            DataSet productRanList = this.dal.GetProductRanList(top);
            return this.ProductAndSKUToList(productRanList.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductRecList(ProductRecType type, int categoryId = 0, int top = -1)
        {
            DataSet set = this.dal.GetProductRecList(type, categoryId, top);
            if ((set == null) || (set.Tables[0].Rows.Count <= 0))
            {
                return null;
            }
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = this.ProductRecTableToList(set.Tables[0]);
            Maticsoft.BLL.Shop.Products.CategoryInfo info = new Maticsoft.BLL.Shop.Products.CategoryInfo();
            foreach (Maticsoft.Model.Shop.Products.ProductInfo info2 in list)
            {
                info2.CategoryName = info.GetNameByPid(info2.ProductId);
            }
            return list;
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductRecListByPage(string[] selectedSkus, int startIndex, int endIndex)
        {
            if ((selectedSkus == null) || (selectedSkus.Length < 1))
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" SaleStatus=1");
            if (selectedSkus.Length > 0)
            {
                builder.Append("  AND  ProductId IN (");
                builder.Append(string.Join(",", selectedSkus));
                builder.Append(") ");
            }
            DataSet set = this.GetListByPage(builder.ToString(), " SaleCounts DESC", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetProductRecListCount(string[] selectedSkus)
        {
            if ((selectedSkus == null) || (selectedSkus.Length < 1))
            {
                return 0;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" SaleStatus=1");
            if (selectedSkus.Length > 0)
            {
                builder.Append("  AND  ProductId IN (");
                builder.Append(string.Join(",", selectedSkus));
                builder.Append(") ");
            }
            return this.GetRecordCount(builder.ToString());
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductsByCid(int cid)
        {
            DataSet productsByCid = this.dal.GetProductsByCid(cid);
            return this.DataTableToList(productsByCid.Tables[0]);
        }

        public int GetProductsCountEx(int Cid, int BrandId, string attrValues, string priceRange)
        {
            return this.dal.GetProductsCountEx(Cid, BrandId, attrValues, priceRange);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductsList(string[] selectedSkus, int startIndex, int endIndex, out int dataCount, long productId)
        {
            if ((selectedSkus == null) || (selectedSkus.Length < 1))
            {
                dataCount = 0;
                return null;
            }
            StringBuilder builder = new StringBuilder();
            if (selectedSkus.Length > 0)
            {
                builder.Append("  AND  ProductId IN (");
                builder.Append(string.Join(",", selectedSkus));
                builder.Append(") ");
            }
            DataSet ds = this.dal.GetProductListInfo(builder.ToString(), " ORDER BY SaleCounts DESC ", startIndex, endIndex, out dataCount, productId);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductsList(int? categoryId, string mod, int startIndex, int endIndex, out int dataCount)
        {
            DataSet set;
            StringBuilder builder = new StringBuilder();
            builder.Append(" AND P.SaleStatus=1");
            if (categoryId.HasValue && (categoryId.Value > 0))
            {
                builder.AppendFormat(" AND PC.CategoryPath LIKE '{0}%' ", categoryId);
            }
            string str = mod;
            if (str != null)
            {
                if (!(str == "rec"))
                {
                    if (str == "hot")
                    {
                        mod = " P.SaleCounts DESC ";
                        goto Label_007A;
                    }
                    if (str == "new")
                    {
                    }
                }
                else
                {
                    mod = " P.DisplaySequence DESC ";
                    goto Label_007A;
                }
            }
            mod = null;
        Label_007A:
            set = this.dal.GetProductListByCategoryId(categoryId, builder.ToString(), mod, startIndex, endIndex, out dataCount);
            if (DataSetTools.DataSetIsNull(set))
            {
                return null;
            }
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductsList(string selectedPids, string pName, string categoryId, int startIdex, int endIndex, out int dataCount, long productId)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pName))
            {
                builder.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                builder.AppendFormat(" AND ProductId IN( SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE CategoryPath LIKE '{0}%' ) ", categoryId);
            }
            if (!string.IsNullOrWhiteSpace(selectedPids))
            {
                builder.AppendFormat(" AND ProductId NOT IN ({0})", selectedPids);
            }
            DataSet ds = this.dal.GetProductListInfo(builder.ToString(), " ORDER BY SaleCounts DESC ", startIdex, endIndex, out dataCount, productId);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductsListEx(int? parentCategoryId, int? subCategoryId, string mod, int startIndex, int endIndex, out int dataCount)
        {
            DataSet set;
            StringBuilder builder = new StringBuilder();
            builder.Append(" AND P.SaleStatus=1");
            if (parentCategoryId.HasValue && (parentCategoryId.Value > 0))
            {
                builder.AppendFormat(" AND PC.CategoryPath LIKE '{0}%' ", parentCategoryId);
            }
            else if (subCategoryId.HasValue && (subCategoryId.Value > 0))
            {
                builder.AppendFormat(" AND PC.CategoryId = {0} ", subCategoryId);
            }
            string str = mod;
            if (str != null)
            {
                if (!(str == "rec"))
                {
                    if (str == "hot")
                    {
                        mod = " P.SaleCounts DESC ";
                        goto Label_00A1;
                    }
                    if (str == "new")
                    {
                    }
                }
                else
                {
                    mod = " P.DisplaySequence DESC ";
                    goto Label_00A1;
                }
            }
            mod = null;
        Label_00A1:
            set = this.dal.GetProductListByCategoryIdEx(null, builder.ToString(), mod, startIndex, endIndex, out dataCount);
            if (DataSetTools.DataSetIsNull(set))
            {
                return null;
            }
            return this.ProductAndSKUToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProductsListEx(int Cid, int BrandId, string attrValues, string priceRange, string mod, int startIndex, int endIndex)
        {
            DataSet set;
            string str = mod;
            if (str != null)
            {
                if (!(str == "default"))
                {
                    if (str == "hot")
                    {
                        mod = " SaleCounts DESC ";
                        goto Label_0079;
                    }
                    if (str == "new")
                    {
                        mod = "AddedDate desc ";
                        goto Label_0079;
                    }
                    if (str == "price")
                    {
                        mod = "LowestSalePrice ";
                        goto Label_0079;
                    }
                    if (str == "pricedesc")
                    {
                        mod = "LowestSalePrice  desc";
                        goto Label_0079;
                    }
                }
                else
                {
                    mod = " DisplaySequence DESC ";
                    goto Label_0079;
                }
            }
            mod = null;
        Label_0079:
            set = this.dal.GetProductsListEx(Cid, BrandId, attrValues, priceRange, mod, startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(set))
            {
                return null;
            }
            return this.DataTableToList(set.Tables[0]);
        }

        public Maticsoft.Model.Shop.Products.ProductInfo GetProSaleModel(int id)
        {
            DataSet proSaleModel = this.dal.GetProSaleModel(id);
            if (this.ProSalesToList(proSaleModel.Tables[0]).Count <= 0)
            {
                return null;
            }
            return this.ProSalesToList(proSaleModel.Tables[0])[0];
        }

        public int GetProSalesCount()
        {
            return this.dal.GetProSalesCount();
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetProSalesList(int startIndex, int endIndex)
        {
            DataSet proSalesList = this.dal.GetProSalesList(startIndex, endIndex);
            return this.ProSalesToList(proSalesList.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountEx(string InIds, string productName, string OutIds, int CategoryId = 0)
        {
            return this.dal.GetRecordCount(this.GetListExSql(InIds, productName, OutIds, CategoryId));
        }

        public DataSet GetRecycleList(string strWhere)
        {
            return this.dal.GetRecycleList(strWhere);
        }

        public int GetRuleProductCount(string[] selectedSkus)
        {
            if ((selectedSkus == null) || (selectedSkus.Length < 1))
            {
                return 0;
            }
            StringBuilder builder = new StringBuilder();
            if (selectedSkus.Length > 0)
            {
                builder.Append("   ProductId IN (");
                builder.Append(string.Join(",", selectedSkus));
                builder.Append(") ");
            }
            return this.GetRecordCount(builder.ToString());
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetRuleProductList(string[] selectedSkus, int startIndex, int endIndex)
        {
            if ((selectedSkus == null) || (selectedSkus.Length < 1))
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            if (selectedSkus.Length > 0)
            {
                builder.Append("   ProductId IN (");
                builder.Append(string.Join(",", selectedSkus));
                builder.Append(") ");
            }
            DataSet ds = this.GetListByPage(builder.ToString(), " SaleCounts DESC", startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.DataTableToList(ds.Tables[0]);
        }

        public int GetSearchCountEx(int Cid, int BrandId, string keyWord, string priceRange)
        {
            return this.dal.GetSearchCountEx(Cid, BrandId, keyWord, priceRange);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> GetSearchListEx(int Cid, int BrandId, string keyWord, string priceRange, string mod, int startIndex, int endIndex)
        {
            DataSet set;
            string str = mod;
            if (str != null)
            {
                if (!(str == "default"))
                {
                    if (str == "hot")
                    {
                        mod = " SaleCounts DESC ";
                        goto Label_0079;
                    }
                    if (str == "new")
                    {
                        mod = "AddedDate desc ";
                        goto Label_0079;
                    }
                    if (str == "price")
                    {
                        mod = "LowestSalePrice ";
                        goto Label_0079;
                    }
                    if (str == "pricedesc")
                    {
                        mod = "LowestSalePrice  desc";
                        goto Label_0079;
                    }
                }
                else
                {
                    mod = " DisplaySequence DESC ";
                    goto Label_0079;
                }
            }
            mod = null;
        Label_0079:
            set = this.dal.GetSearchListEx(Cid, BrandId, keyWord, priceRange, mod, startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(set))
            {
                return null;
            }
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetTableSchema()
        {
            return this.dal.GetTableSchema();
        }

        public DataSet GetTableSchemaEx()
        {
            return this.dal.GetTableSchemaEx();
        }

        public List<Item> GetTaoListByUser(string sessionKey, int cid, string keyword, int page_no = 1, int page_size = 40, string hasDiscount = "", string hasShowcase = "")
        {
            List<Item> list = new List<Item>();
            ITopClient topClient = TaoBaoConfig.GetTopClient();
            ItemsOnsaleGetRequest request = new ItemsOnsaleGetRequest();
            if (cid > 0)
            {
                request.Cid = new long?((long) cid);
            }
            request.PageSize = new long?((long) page_size);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                request.Q = keyword;
            }
            if (!string.IsNullOrWhiteSpace(hasDiscount))
            {
                request.HasDiscount = new bool?(Globals.SafeBool(hasDiscount, false));
            }
            if (!string.IsNullOrWhiteSpace(hasShowcase))
            {
                request.HasShowcase = new bool?(Globals.SafeBool(hasShowcase, false));
            }
            request.Fields = "approve_status,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_invoice,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id";
            for (int i = 1; i <= page_no; i++)
            {
                request.PageNo = new long?((long) i);
                ItemsOnsaleGetResponse response = topClient.Execute<ItemsOnsaleGetResponse>(request, sessionKey);
                if (response.Items.Count > 0)
                {
                    list.AddRange(response.Items);
                }
            }
            return list;
        }

        public int MaxSequence()
        {
            return this.dal.MaxSequence();
        }

        public int MaxSequence(string CategoryPath)
        {
            return this.dal.MaxSequence(CategoryPath);
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProductAndSKUToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo item = new Maticsoft.Model.Shop.Products.ProductInfo();
                    if ((dt.Rows[i]["CategoryId"] != null) && (dt.Rows[i]["CategoryId"].ToString() != ""))
                    {
                        item.CategoryId = int.Parse(dt.Rows[i]["CategoryId"].ToString());
                    }
                    if ((dt.Rows[i]["TypeId"] != null) && (dt.Rows[i]["TypeId"].ToString() != ""))
                    {
                        item.TypeId = new int?(int.Parse(dt.Rows[i]["TypeId"].ToString()));
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["BrandId"] != null) && (dt.Rows[i]["BrandId"].ToString() != ""))
                    {
                        item.BrandId = int.Parse(dt.Rows[i]["BrandId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductName"] != null) && (dt.Rows[i]["ProductName"].ToString() != ""))
                    {
                        item.ProductName = dt.Rows[i]["ProductName"].ToString();
                    }
                    if ((dt.Rows[i]["ProductCode"] != null) && (dt.Rows[i]["ProductCode"].ToString() != ""))
                    {
                        item.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                    }
                    if ((dt.Rows[i]["SupplierId"] != null) && (dt.Rows[i]["SupplierId"].ToString() != ""))
                    {
                        item.SupplierId = int.Parse(dt.Rows[i]["SupplierId"].ToString());
                    }
                    if ((dt.Rows[i]["RegionId"] != null) && (dt.Rows[i]["RegionId"].ToString() != ""))
                    {
                        item.RegionId = new int?(int.Parse(dt.Rows[i]["RegionId"].ToString()));
                    }
                    if ((dt.Rows[i]["ShortDescription"] != null) && (dt.Rows[i]["ShortDescription"].ToString() != ""))
                    {
                        item.ShortDescription = dt.Rows[i]["ShortDescription"].ToString();
                    }
                    if ((dt.Rows[i]["Unit"] != null) && (dt.Rows[i]["Unit"].ToString() != ""))
                    {
                        item.Unit = dt.Rows[i]["Unit"].ToString();
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Title"] != null) && (dt.Rows[i]["Meta_Title"].ToString() != ""))
                    {
                        item.Meta_Title = dt.Rows[i]["Meta_Title"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Description"] != null) && (dt.Rows[i]["Meta_Description"].ToString() != ""))
                    {
                        item.Meta_Description = dt.Rows[i]["Meta_Description"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Keywords"] != null) && (dt.Rows[i]["Meta_Keywords"].ToString() != ""))
                    {
                        item.Meta_Keywords = dt.Rows[i]["Meta_Keywords"].ToString();
                    }
                    if ((dt.Rows[i]["SaleStatus"] != null) && (dt.Rows[i]["SaleStatus"].ToString() != ""))
                    {
                        item.SaleStatus = int.Parse(dt.Rows[i]["SaleStatus"].ToString());
                    }
                    if ((dt.Rows[i]["AddedDate"] != null) && (dt.Rows[i]["AddedDate"].ToString() != ""))
                    {
                        item.AddedDate = DateTime.Parse(dt.Rows[i]["AddedDate"].ToString());
                    }
                    if ((dt.Rows[i]["VistiCounts"] != null) && (dt.Rows[i]["VistiCounts"].ToString() != ""))
                    {
                        item.VistiCounts = int.Parse(dt.Rows[i]["VistiCounts"].ToString());
                    }
                    if ((dt.Rows[i]["SaleCounts"] != null) && (dt.Rows[i]["SaleCounts"].ToString() != ""))
                    {
                        item.SaleCounts = int.Parse(dt.Rows[i]["SaleCounts"].ToString());
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["LineId"] != null) && (dt.Rows[i]["LineId"].ToString() != ""))
                    {
                        item.LineId = int.Parse(dt.Rows[i]["LineId"].ToString());
                    }
                    if ((dt.Rows[i]["MarketPrice"] != null) && (dt.Rows[i]["MarketPrice"].ToString() != ""))
                    {
                        item.MarketPrice = new decimal?(decimal.Parse(dt.Rows[i]["MarketPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["LowestSalePrice"] != null) && (dt.Rows[i]["LowestSalePrice"].ToString() != ""))
                    {
                        item.LowestSalePrice = decimal.Parse(dt.Rows[i]["LowestSalePrice"].ToString());
                    }
                    if ((dt.Rows[i]["PenetrationStatus"] != null) && (dt.Rows[i]["PenetrationStatus"].ToString() != ""))
                    {
                        item.PenetrationStatus = int.Parse(dt.Rows[i]["PenetrationStatus"].ToString());
                    }
                    if ((dt.Rows[i]["MainCategoryPath"] != null) && (dt.Rows[i]["MainCategoryPath"].ToString() != ""))
                    {
                        item.MainCategoryPath = dt.Rows[i]["MainCategoryPath"].ToString();
                    }
                    if ((dt.Rows[i]["ExtendCategoryPath"] != null) && (dt.Rows[i]["ExtendCategoryPath"].ToString() != ""))
                    {
                        item.ExtendCategoryPath = dt.Rows[i]["ExtendCategoryPath"].ToString();
                    }
                    if ((dt.Rows[i]["HasSKU"] != null) && (dt.Rows[i]["HasSKU"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["HasSKU"].ToString() == "1") || (dt.Rows[i]["HasSKU"].ToString().ToLower() == "true"))
                        {
                            item.HasSKU = true;
                        }
                        else
                        {
                            item.HasSKU = false;
                        }
                    }
                    if ((dt.Rows[i]["Points"] != null) && (dt.Rows[i]["Points"].ToString() != ""))
                    {
                        item.Points = new decimal?(decimal.Parse(dt.Rows[i]["Points"].ToString()));
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl1"] != null) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != ""))
                    {
                        item.ThumbnailUrl1 = dt.Rows[i]["ThumbnailUrl1"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl2"] != null) && (dt.Rows[i]["ThumbnailUrl2"].ToString() != ""))
                    {
                        item.ThumbnailUrl2 = dt.Rows[i]["ThumbnailUrl2"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl3"] != null) && (dt.Rows[i]["ThumbnailUrl3"].ToString() != ""))
                    {
                        item.ThumbnailUrl3 = dt.Rows[i]["ThumbnailUrl3"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl4"] != null) && (dt.Rows[i]["ThumbnailUrl4"].ToString() != ""))
                    {
                        item.ThumbnailUrl4 = dt.Rows[i]["ThumbnailUrl4"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl5"] != null) && (dt.Rows[i]["ThumbnailUrl5"].ToString() != ""))
                    {
                        item.ThumbnailUrl5 = dt.Rows[i]["ThumbnailUrl5"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl6"] != null) && (dt.Rows[i]["ThumbnailUrl6"].ToString() != ""))
                    {
                        item.ThumbnailUrl6 = dt.Rows[i]["ThumbnailUrl6"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl7"] != null) && (dt.Rows[i]["ThumbnailUrl7"].ToString() != ""))
                    {
                        item.ThumbnailUrl7 = dt.Rows[i]["ThumbnailUrl7"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl8"] != null) && (dt.Rows[i]["ThumbnailUrl8"].ToString() != ""))
                    {
                        item.ThumbnailUrl8 = dt.Rows[i]["ThumbnailUrl8"].ToString();
                    }
                    if ((dt.Rows[i]["MaxQuantity"] != null) && (dt.Rows[i]["MaxQuantity"].ToString() != ""))
                    {
                        item.MaxQuantity = new int?(int.Parse(dt.Rows[i]["MaxQuantity"].ToString()));
                    }
                    if ((dt.Rows[i]["MinQuantity"] != null) && (dt.Rows[i]["MinQuantity"].ToString() != ""))
                    {
                        item.MinQuantity = new int?(int.Parse(dt.Rows[i]["MinQuantity"].ToString()));
                    }
                    if ((dt.Rows[i]["Tags"] != null) && (dt.Rows[i]["Tags"].ToString() != ""))
                    {
                        item.Tags = dt.Rows[i]["Tags"].ToString();
                    }
                    if ((dt.Rows[i]["SeoUrl"] != null) && (dt.Rows[i]["SeoUrl"].ToString() != ""))
                    {
                        item.SeoUrl = dt.Rows[i]["SeoUrl"].ToString();
                    }
                    if ((dt.Rows[i]["SeoImageAlt"] != null) && (dt.Rows[i]["SeoImageAlt"].ToString() != ""))
                    {
                        item.SeoImageAlt = dt.Rows[i]["SeoImageAlt"].ToString();
                    }
                    if ((dt.Rows[i]["SeoImageTitle"] != null) && (dt.Rows[i]["SeoImageTitle"].ToString() != ""))
                    {
                        item.SeoImageTitle = dt.Rows[i]["SeoImageTitle"].ToString();
                    }
                    if ((dt.Rows[i]["SalePrice"] != null) && (dt.Rows[i]["SalePrice"].ToString() != ""))
                    {
                        item.SalePrice = decimal.Parse(dt.Rows[i]["SalePrice"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProductDataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo item = new Maticsoft.Model.Shop.Products.ProductInfo();
                    if ((dt.Rows[i]["CategoryId"] != null) && (dt.Rows[i]["CategoryId"].ToString() != ""))
                    {
                        item.CategoryId = int.Parse(dt.Rows[i]["CategoryId"].ToString());
                    }
                    if ((dt.Rows[i]["TypeId"] != null) && (dt.Rows[i]["TypeId"].ToString() != ""))
                    {
                        item.TypeId = new int?(int.Parse(dt.Rows[i]["TypeId"].ToString()));
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["BrandId"] != null) && (dt.Rows[i]["BrandId"].ToString() != ""))
                    {
                        item.BrandId = int.Parse(dt.Rows[i]["BrandId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductName"] != null) && (dt.Rows[i]["ProductName"].ToString() != ""))
                    {
                        item.ProductName = dt.Rows[i]["ProductName"].ToString();
                    }
                    if ((dt.Rows[i]["ProductCode"] != null) && (dt.Rows[i]["ProductCode"].ToString() != ""))
                    {
                        item.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                    }
                    if ((dt.Rows[i]["SupplierId"] != null) && (dt.Rows[i]["SupplierId"].ToString() != ""))
                    {
                        item.SupplierId = int.Parse(dt.Rows[i]["SupplierId"].ToString());
                    }
                    if ((dt.Rows[i]["RegionId"] != null) && (dt.Rows[i]["RegionId"].ToString() != ""))
                    {
                        item.RegionId = new int?(int.Parse(dt.Rows[i]["RegionId"].ToString()));
                    }
                    if ((dt.Rows[i]["ShortDescription"] != null) && (dt.Rows[i]["ShortDescription"].ToString() != ""))
                    {
                        item.ShortDescription = dt.Rows[i]["ShortDescription"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Title"] != null) && (dt.Rows[i]["Meta_Title"].ToString() != ""))
                    {
                        item.Meta_Title = dt.Rows[i]["Meta_Title"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Description"] != null) && (dt.Rows[i]["Meta_Description"].ToString() != ""))
                    {
                        item.Meta_Description = dt.Rows[i]["Meta_Description"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Keywords"] != null) && (dt.Rows[i]["Meta_Keywords"].ToString() != ""))
                    {
                        item.Meta_Keywords = dt.Rows[i]["Meta_Keywords"].ToString();
                    }
                    if ((dt.Rows[i]["SaleStatus"] != null) && (dt.Rows[i]["SaleStatus"].ToString() != ""))
                    {
                        item.SaleStatus = int.Parse(dt.Rows[i]["SaleStatus"].ToString());
                    }
                    if ((dt.Rows[i]["VistiCounts"] != null) && (dt.Rows[i]["VistiCounts"].ToString() != ""))
                    {
                        item.VistiCounts = int.Parse(dt.Rows[i]["VistiCounts"].ToString());
                    }
                    if ((dt.Rows[i]["SaleCounts"] != null) && (dt.Rows[i]["SaleCounts"].ToString() != ""))
                    {
                        item.SaleCounts = int.Parse(dt.Rows[i]["SaleCounts"].ToString());
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["MarketPrice"] != null) && (dt.Rows[i]["MarketPrice"].ToString() != ""))
                    {
                        item.MarketPrice = new decimal?(decimal.Parse(dt.Rows[i]["MarketPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["LowestSalePrice"] != null) && (dt.Rows[i]["LowestSalePrice"].ToString() != ""))
                    {
                        item.LowestSalePrice = decimal.Parse(dt.Rows[i]["LowestSalePrice"].ToString());
                    }
                    if ((dt.Rows[i]["PenetrationStatus"] != null) && (dt.Rows[i]["PenetrationStatus"].ToString() != ""))
                    {
                        item.PenetrationStatus = int.Parse(dt.Rows[i]["PenetrationStatus"].ToString());
                    }
                    if ((dt.Rows[i]["MainCategoryPath"] != null) && (dt.Rows[i]["MainCategoryPath"].ToString() != ""))
                    {
                        item.MainCategoryPath = dt.Rows[i]["MainCategoryPath"].ToString();
                    }
                    if ((dt.Rows[i]["ExtendCategoryPath"] != null) && (dt.Rows[i]["ExtendCategoryPath"].ToString() != ""))
                    {
                        item.ExtendCategoryPath = dt.Rows[i]["ExtendCategoryPath"].ToString();
                    }
                    if ((dt.Rows[i]["Points"] != null) && (dt.Rows[i]["Points"].ToString() != ""))
                    {
                        item.Points = new decimal?(decimal.Parse(dt.Rows[i]["Points"].ToString()));
                    }
                    if ((dt.Rows[i]["ThumbnailUrl1"] != null) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != ""))
                    {
                        item.ThumbnailUrl1 = dt.Rows[i]["ThumbnailUrl1"].ToString();
                    }
                    if ((dt.Rows[i]["MaxQuantity"] != null) && (dt.Rows[i]["MaxQuantity"].ToString() != ""))
                    {
                        item.MaxQuantity = new int?(int.Parse(dt.Rows[i]["MaxQuantity"].ToString()));
                    }
                    if ((dt.Rows[i]["MinQuantity"] != null) && (dt.Rows[i]["MinQuantity"].ToString() != ""))
                    {
                        item.MinQuantity = new int?(int.Parse(dt.Rows[i]["MinQuantity"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProductRecTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo item = new Maticsoft.Model.Shop.Products.ProductInfo();
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductName"] != null) && (dt.Rows[i]["ProductName"].ToString() != ""))
                    {
                        item.ProductName = dt.Rows[i]["ProductName"].ToString();
                    }
                    if ((dt.Rows[i]["ProductCode"] != null) && (dt.Rows[i]["ProductCode"].ToString() != ""))
                    {
                        item.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl1"] != null) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != ""))
                    {
                        item.ThumbnailUrl1 = dt.Rows[i]["ThumbnailUrl1"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl2"] != null) && (dt.Rows[i]["ThumbnailUrl2"].ToString() != ""))
                    {
                        item.ThumbnailUrl2 = dt.Rows[i]["ThumbnailUrl2"].ToString();
                    }
                    if ((dt.Rows[i]["ShortDescription"] != null) && (dt.Rows[i]["ShortDescription"].ToString() != ""))
                    {
                        item.ShortDescription = dt.Rows[i]["ShortDescription"].ToString();
                    }
                    if ((dt.Rows[i]["LowestSalePrice"] != null) && (dt.Rows[i]["LowestSalePrice"].ToString() != ""))
                    {
                        item.LowestSalePrice = Globals.SafeDecimal(dt.Rows[i]["LowestSalePrice"].ToString(), (decimal) 0M);
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> ProSalesToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo item = new Maticsoft.Model.Shop.Products.ProductInfo();
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["BrandId"] != null) && (dt.Rows[i]["BrandId"].ToString() != ""))
                    {
                        item.BrandId = int.Parse(dt.Rows[i]["BrandId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductName"] != null) && (dt.Rows[i]["ProductName"].ToString() != ""))
                    {
                        item.ProductName = dt.Rows[i]["ProductName"].ToString();
                    }
                    if ((dt.Rows[i]["ProductCode"] != null) && (dt.Rows[i]["ProductCode"].ToString() != ""))
                    {
                        item.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                    }
                    if ((dt.Rows[i]["RegionId"] != null) && (dt.Rows[i]["RegionId"].ToString() != ""))
                    {
                        item.RegionId = new int?(int.Parse(dt.Rows[i]["RegionId"].ToString()));
                    }
                    if ((dt.Rows[i]["SaleStatus"] != null) && (dt.Rows[i]["SaleStatus"].ToString() != ""))
                    {
                        item.SaleStatus = int.Parse(dt.Rows[i]["SaleStatus"].ToString());
                    }
                    if ((dt.Rows[i]["AddedDate"] != null) && (dt.Rows[i]["AddedDate"].ToString() != ""))
                    {
                        item.AddedDate = DateTime.Parse(dt.Rows[i]["AddedDate"].ToString());
                    }
                    if ((dt.Rows[i]["SaleCounts"] != null) && (dt.Rows[i]["SaleCounts"].ToString() != ""))
                    {
                        item.SaleCounts = int.Parse(dt.Rows[i]["SaleCounts"].ToString());
                    }
                    if ((dt.Rows[i]["MarketPrice"] != null) && (dt.Rows[i]["MarketPrice"].ToString() != ""))
                    {
                        item.MarketPrice = new decimal?(decimal.Parse(dt.Rows[i]["MarketPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["LowestSalePrice"] != null) && (dt.Rows[i]["LowestSalePrice"].ToString() != ""))
                    {
                        item.LowestSalePrice = decimal.Parse(dt.Rows[i]["LowestSalePrice"].ToString());
                    }
                    if ((dt.Rows[i]["Points"] != null) && (dt.Rows[i]["Points"].ToString() != ""))
                    {
                        item.Points = new decimal?(decimal.Parse(dt.Rows[i]["Points"].ToString()));
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbnailUrl1"] != null) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != ""))
                    {
                        item.ThumbnailUrl1 = dt.Rows[i]["ThumbnailUrl1"].ToString();
                    }
                    if ((dt.Rows[i]["Tags"] != null) && (dt.Rows[i]["Tags"].ToString() != ""))
                    {
                        item.Tags = dt.Rows[i]["Tags"].ToString();
                    }
                    if ((dt.Rows[i]["SeoUrl"] != null) && (dt.Rows[i]["SeoUrl"].ToString() != ""))
                    {
                        item.SeoUrl = dt.Rows[i]["SeoUrl"].ToString();
                    }
                    if ((dt.Rows[i]["ProSalesPrice"] != null) && (dt.Rows[i]["ProSalesPrice"].ToString() != ""))
                    {
                        item.ProSalesPrice = decimal.Parse(dt.Rows[i]["ProSalesPrice"].ToString());
                    }
                    if ((dt.Rows[i]["ProSalesEndDate"] != null) && (dt.Rows[i]["ProSalesEndDate"].ToString() != ""))
                    {
                        item.ProSalesEndDate = DateTime.Parse(dt.Rows[i]["ProSalesEndDate"].ToString());
                    }
                    if ((dt.Rows[i]["CountDownId"] != null) && (dt.Rows[i]["CountDownId"].ToString() != ""))
                    {
                        item.CountDownId = int.Parse(dt.Rows[i]["CountDownId"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> RelatedProductsList(long productId, int top = -1)
        {
            DataSet set = this.dal.RelatedProductSource(productId, top);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                return this.DataTableToList(set.Tables[0]);
            }
            return null;
        }

        public bool RevertAll()
        {
            return this.dal.RevertAll();
        }

        public List<Maticsoft.Model.Shop.Products.ProductInfo> SearchProducts(int cateId, ProductSearch model)
        {
            DataSet set = this.dal.SearchProducts(cateId, model);
            if ((set != null) && (set.Tables.Count > 0))
            {
                return this.ProductDataTableToList(set.Tables[0]);
            }
            return null;
        }

        public int StockNum(long productId)
        {
            return this.dal.StockNum(productId);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductInfo model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, ProductSaleStatus saleStatus)
        {
            string strSetValue = string.Format(" SaleStatus ={0}", (int) saleStatus);
            return this.dal.UpdateList(IDlist, strSetValue);
        }

        public bool UpdateLowestSalePrice(long productId, decimal price)
        {
            return this.dal.UpdateLowestSalePrice(productId, price);
        }

        public bool UpdateMarketPrice(long productId, decimal price)
        {
            return this.dal.UpdateMarketPrice(productId, price);
        }

        public bool UpdateProductName(long productId, string productName)
        {
            return this.dal.UpdateProductName(productId, productName);
        }

        public bool UpdateStatus(long productId, int SaleStatus)
        {
            return this.dal.UpdateStatus(productId, SaleStatus);
        }
    }
}

