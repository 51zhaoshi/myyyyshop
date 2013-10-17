namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.ViewModel.Shop;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SKUInfo
    {
        private readonly ISKUInfo dal = DAShopProducts.CreateSKUInfo();

        public long Add(Maticsoft.Model.Shop.Products.SKUInfo model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.SKUInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.SKUInfo> list = new List<Maticsoft.Model.Shop.Products.SKUInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.SKUInfo item = new Maticsoft.Model.Shop.Products.SKUInfo();
                    if ((dt.Rows[i]["SkuId"] != null) && (dt.Rows[i]["SkuId"].ToString() != ""))
                    {
                        item.SkuId = long.Parse(dt.Rows[i]["SkuId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["SKU"] != null) && (dt.Rows[i]["SKU"].ToString() != ""))
                    {
                        item.SKU = dt.Rows[i]["SKU"].ToString();
                    }
                    if ((dt.Rows[i]["Weight"] != null) && (dt.Rows[i]["Weight"].ToString() != ""))
                    {
                        item.Weight = new int?(int.Parse(dt.Rows[i]["Weight"].ToString()));
                    }
                    if ((dt.Rows[i]["Stock"] != null) && (dt.Rows[i]["Stock"].ToString() != ""))
                    {
                        item.Stock = int.Parse(dt.Rows[i]["Stock"].ToString());
                    }
                    if ((dt.Rows[i]["AlertStock"] != null) && (dt.Rows[i]["AlertStock"].ToString() != ""))
                    {
                        item.AlertStock = int.Parse(dt.Rows[i]["AlertStock"].ToString());
                    }
                    if ((dt.Rows[i]["CostPrice"] != null) && (dt.Rows[i]["CostPrice"].ToString() != ""))
                    {
                        item.CostPrice = new decimal?(decimal.Parse(dt.Rows[i]["CostPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["SalePrice"] != null) && (dt.Rows[i]["SalePrice"].ToString() != ""))
                    {
                        item.SalePrice = decimal.Parse(dt.Rows[i]["SalePrice"].ToString());
                    }
                    if ((dt.Rows[i]["Upselling"] != null) && (dt.Rows[i]["Upselling"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["Upselling"].ToString() == "1") || (dt.Rows[i]["Upselling"].ToString().ToLower() == "true"))
                        {
                            item.Upselling = true;
                        }
                        else
                        {
                            item.Upselling = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long SkuId)
        {
            return this.dal.Delete(SkuId);
        }

        public bool DeleteList(string SkuIdlist)
        {
            return this.dal.DeleteList(SkuIdlist);
        }

        public bool Exists(long SkuId)
        {
            return this.dal.Exists(SkuId);
        }

        public bool Exists(string SkuCode)
        {
            return this.dal.Exists(SkuCode);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.Shop.Products.SKUInfo GetModel(long SkuId)
        {
            return this.dal.GetModel(SkuId);
        }

        public Maticsoft.Model.Shop.Products.SKUInfo GetModelByCache(long SkuId)
        {
            string cacheKey = "SKUsModel-" + SkuId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SkuId);
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
            return (Maticsoft.Model.Shop.Products.SKUInfo) cache;
        }

        public Maticsoft.Model.Shop.Products.SKUInfo GetModelBySKU(string sku)
        {
            return this.dal.GetModelBySKU(sku);
        }

        public List<Maticsoft.Model.Shop.Products.SKUInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.SKUInfo> GetProductSkuInfo(long productId)
        {
            DataSet set = this.dal.PrductsSkuInfo(productId);
            if ((set != null) && (set.Tables.Count > 0))
            {
                return this.ProductSkuDataTableToList(set.Tables[0]);
            }
            return null;
        }

        public ProductSKUModel GetProductSKUInfoByProductId(long productId)
        {
            ProductSKUModel productSKUModel = new ProductSKUModel {
                ListSKUInfos = this.GetProductSkuInfo(productId)
            };
            if ((productSKUModel.ListSKUInfos != null) && (productSKUModel.ListSKUInfos.Count >= 1))
            {
                productSKUModel.ListSKUItems = this.GetSKUItemsByProductId(productId);
                if ((productSKUModel.ListSKUItems == null) || (productSKUModel.ListSKUItems.Count < 1))
                {
                    return productSKUModel;
                }
                productSKUModel.ListSKUInfos.ForEach(delegate (Maticsoft.Model.Shop.Products.SKUInfo skuInfo) {
                    if (skuInfo.Upselling)
                    {
                        foreach (Maticsoft.Model.Shop.Products.SKUItem item in productSKUModel.ListSKUItems)
                        {
                            if (item.SkuId == skuInfo.SkuId)
                            {
                                if (skuInfo.SkuItems == null)
                                {
                                    skuInfo.SkuItems = new List<Maticsoft.Model.Shop.Products.SKUItem>();
                                }
                                this.MergeSKUItem4AV(item);
                                Maticsoft.Model.Shop.Products.AttributeInfo key = new Maticsoft.Model.Shop.Products.AttributeInfo {
                                    AttributeId = item.AttributeId,
                                    AttributeName = item.AttributeName,
                                    DisplaySequence = item.AB_DisplaySequence,
                                    UsageMode = item.UsageMode,
                                    UseAttributeImage = item.UseAttributeImage,
                                    UserDefinedPic = item.UserDefinedPic
                                };
                                productSKUModel.ListAttrSKUItems.Add(key, item);
                                skuInfo.SkuItems.Add(item);
                            }
                        }
                    }
                });
            }
            return productSKUModel;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.Shop.Products.SKUInfo> GetSKU4AttrVal(string[] selectedSkus, int startIndex, int endIndex, out int dataCount, long productId)
        {
            if ((selectedSkus == null) || (selectedSkus.Length < 1))
            {
                dataCount = 0;
                return null;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" SkuId IN (");
            builder.Append(string.Join(",", selectedSkus));
            builder.Append(") ");
            return this.GetSKUInfosData(builder.ToString(), null, startIndex, endIndex, out dataCount, productId);
        }

        public List<Maticsoft.Model.Shop.Products.SKUInfo> GetSKU4AttrVal(string productName, string categoryId, string[] selectedSkus, int startIndex, int endIndex, out int dataCount, long productId)
        {
            return this.GetSKU4AttrVal(null, selectedSkus, productName, categoryId, startIndex, endIndex, out dataCount, productId);
        }

        public List<Maticsoft.Model.Shop.Products.SKUInfo> GetSKU4AttrVal(string sku, string[] selectedSkus, string productName, string categoryId, int startIndex, int endIndex, out int dataCount, long productId)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sku))
            {
                builder.Append("SKU = '");
                builder.Append(InjectionFilter.SqlFilter(sku));
                builder.Append("'");
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.Append(" P.ProductId IN(  SELECT DISTINCT ProductId FROM Shop_ProductCategories WHERE CategoryPath  LIKE '");
                builder.Append(InjectionFilter.SqlFilter(categoryId.Trim()));
                builder.Append("%')");
            }
            if (!string.IsNullOrWhiteSpace(productName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.Append(" ProductName LIKE '");
                builder.Append(InjectionFilter.SqlFilter(productName.Trim()));
                builder.Append("%'");
            }
            if ((selectedSkus != null) && (selectedSkus.Length > 0))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.Append(" SkuId NOT IN (");
                builder.Append(string.Join(",", selectedSkus));
                builder.Append(") ");
            }
            return this.GetSKUInfosData(builder.ToString(), null, startIndex, endIndex, out dataCount, productId);
        }

        private List<Maticsoft.Model.Shop.Products.SKUInfo> GetSKUInfosData(string where, string orderby, int startIndex, int endIndex, out int dataCount, long productId)
        {
            Predicate<Maticsoft.Model.Shop.Products.SKUInfo> match = null;
            long tmpSkuId;
            List<Maticsoft.Model.Shop.Products.SKUInfo> list = new List<Maticsoft.Model.Shop.Products.SKUInfo>();
            DataSet ds = this.dal.GetSKUListByPage(where, orderby, startIndex, endIndex, out dataCount, productId);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tmpSkuId = row.Field<long>("SkuId");
                if (match == null)
                {
                    match = info => info.SkuId == tmpSkuId;
                }
                Maticsoft.Model.Shop.Products.SKUInfo info = list.Find(match);
                if (info == null)
                {
                    info = new Maticsoft.Model.Shop.Products.SKUInfo {
                        SkuId = tmpSkuId,
                        ProductId = row.Field<long>("ProductId"),
                        SKU = row.Field<string>("SKU"),
                        Weight = row.Field<int?>("Weight"),
                        Stock = row.Field<int>("Stock"),
                        AlertStock = row.Field<int>("AlertStock"),
                        CostPrice = row.Field<decimal?>("CostPrice"),
                        SalePrice = row.Field<decimal>("SalePrice"),
                        Upselling = row.Field<bool>("Upselling"),
                        ProductName = row.Field<string>("ProductName"),
                        ProductImageUrl = row.Field<string>("ImageUrl"),
                        ProductThumbnailUrl = row.Field<string>("ThumbnailUrl1")
                    };
                    list.Add(info);
                }
                Maticsoft.Model.Shop.Products.SKUItem item = new Maticsoft.Model.Shop.Products.SKUItem {
                    AttributeId = row.Field<long>("AttributeId"),
                    ValueId = row.Field<long>("ValueId"),
                    ValueStr = row.Field<string>("ValueStr"),
                    ImageUrl = row.Field<string>("ImageUrl")
                };
                info.SkuItems.Add(item);
            }
            List<Maticsoft.Model.Shop.Products.SKUInfo> list2 = new List<Maticsoft.Model.Shop.Products.SKUInfo>();
            dataCount = list.Count;
            if (dataCount <= (startIndex - 1))
            {
                return list;
            }
            for (int i = startIndex - 1; (i < endIndex) && (i < list.Count); i++)
            {
                list2.Add(list[i]);
            }
            return list2;
        }

        private List<Maticsoft.Model.Shop.Products.SKUItem> GetSKUItemsByProductId(long productId)
        {
            Maticsoft.BLL.Shop.Products.SKUItem item = new Maticsoft.BLL.Shop.Products.SKUItem();
            return item.GetSKUItemsByProductId(productId);
        }

        public List<Maticsoft.Model.Shop.Products.SKUItem> GetSKUItemsBySkuId(long skuId)
        {
            List<Maticsoft.Model.Shop.Products.SKUItem> sKUItemsBySkuId = new Maticsoft.BLL.Shop.Products.SKUItem().GetSKUItemsBySkuId(skuId);
            sKUItemsBySkuId.ForEach(new Action<Maticsoft.Model.Shop.Products.SKUItem>(this.MergeSKUItem4AV));
            return sKUItemsBySkuId;
        }

        public int GetStockById(long productId)
        {
            return this.dal.GetStockById(productId);
        }

        public int GetStockBySKU(string SKU)
        {
            return this.dal.GetStockBySKU(SKU);
        }

        private void MergeSKUItem4AV(Maticsoft.Model.Shop.Products.SKUItem skuItem)
        {
            if (skuItem != null)
            {
                if (string.IsNullOrWhiteSpace(skuItem.ValueStr) && !string.IsNullOrWhiteSpace(skuItem.AV_ValueStr))
                {
                    skuItem.ValueStr = skuItem.AV_ValueStr;
                }
                if (string.IsNullOrWhiteSpace(skuItem.ImageUrl) && !string.IsNullOrWhiteSpace(skuItem.AV_ImageUrl))
                {
                    skuItem.ImageUrl = skuItem.AV_ImageUrl;
                }
            }
        }

        public List<Maticsoft.Model.Shop.Products.SKUInfo> ProductSkuDataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.SKUInfo> list = new List<Maticsoft.Model.Shop.Products.SKUInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.SKUInfo item = new Maticsoft.Model.Shop.Products.SKUInfo();
                    if ((dt.Rows[i]["SkuId"] != null) && (dt.Rows[i]["SkuId"].ToString() != ""))
                    {
                        item.SkuId = long.Parse(dt.Rows[i]["SkuId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["SKU"] != null) && (dt.Rows[i]["SKU"].ToString() != ""))
                    {
                        item.SKU = dt.Rows[i]["SKU"].ToString();
                    }
                    if ((dt.Rows[i]["Weight"] != null) && (dt.Rows[i]["Weight"].ToString() != ""))
                    {
                        item.Weight = new int?(int.Parse(dt.Rows[i]["Weight"].ToString()));
                    }
                    if ((dt.Rows[i]["Stock"] != null) && (dt.Rows[i]["Stock"].ToString() != ""))
                    {
                        item.Stock = int.Parse(dt.Rows[i]["Stock"].ToString());
                    }
                    if ((dt.Rows[i]["AlertStock"] != null) && (dt.Rows[i]["AlertStock"].ToString() != ""))
                    {
                        item.AlertStock = int.Parse(dt.Rows[i]["AlertStock"].ToString());
                    }
                    if ((dt.Rows[i]["CostPrice"] != null) && (dt.Rows[i]["CostPrice"].ToString() != ""))
                    {
                        item.CostPrice = new decimal?(decimal.Parse(dt.Rows[i]["CostPrice"].ToString()));
                    }
                    if ((dt.Rows[i]["SalePrice"] != null) && (dt.Rows[i]["SalePrice"].ToString() != ""))
                    {
                        item.SalePrice = decimal.Parse(dt.Rows[i]["SalePrice"].ToString());
                    }
                    if ((dt.Rows[i]["Upselling"] != null) && (dt.Rows[i]["Upselling"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["Upselling"].ToString() == "1") || (dt.Rows[i]["Upselling"].ToString().ToLower() == "true"))
                        {
                            item.Upselling = true;
                        }
                        else
                        {
                            item.Upselling = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Update(Maticsoft.Model.Shop.Products.SKUInfo model)
        {
            return this.dal.Update(model);
        }
    }
}

