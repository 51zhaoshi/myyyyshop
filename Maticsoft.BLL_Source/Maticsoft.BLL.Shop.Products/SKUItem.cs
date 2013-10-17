namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SKUItem
    {
        private readonly ISKUItem dal = DAShopProducts.CreateSKUItem();

        public bool Add(Maticsoft.Model.Shop.Products.SKUItem model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.SKUItem> AttributeVakueDataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.SKUItem> list = new List<Maticsoft.Model.Shop.Products.SKUItem>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.SKUItem item = new Maticsoft.Model.Shop.Products.SKUItem();
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["AttributeId"] != null) && (dt.Rows[i]["AttributeId"].ToString() != ""))
                    {
                        item.AttributeId = long.Parse(dt.Rows[i]["AttributeId"].ToString());
                    }
                    if ((dt.Rows[i]["ValueId"] != null) && (dt.Rows[i]["ValueId"].ToString() != ""))
                    {
                        item.ValueId = long.Parse(dt.Rows[i]["ValueId"].ToString());
                    }
                    if ((dt.Rows[i]["ValueStr"] != null) && (dt.Rows[i]["ValueStr"].ToString() != ""))
                    {
                        item.ValueStr = dt.Rows[i]["ValueStr"].ToString();
                    }
                    if ((dt.Rows[i]["UserDefinedPic"] != null) && (dt.Rows[i]["UserDefinedPic"].ToString() != ""))
                    {
                        item.UserDefinedPic = bool.Parse(dt.Rows[i]["UserDefinedPic"].ToString());
                    }
                    if ((dt.Rows[i]["SpecId"] != null) && (dt.Rows[i]["SpecId"].ToString() != ""))
                    {
                        item.SpecId = long.Parse(dt.Rows[i]["SpecId"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Maticsoft.Model.Shop.Products.SKUItem> AttributeValueInfo(long productId)
        {
            DataSet set = this.dal.AttributeValuesInfo(productId);
            if ((set != null) && (set.Tables.Count > 0))
            {
                return this.AttributeVakueDataTableToList(set.Tables[0]);
            }
            return null;
        }

        public List<Maticsoft.Model.Shop.Products.SKUItem> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.SKUItem> list = new List<Maticsoft.Model.Shop.Products.SKUItem>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.SKUItem item = new Maticsoft.Model.Shop.Products.SKUItem();
                    if ((dt.Rows[i]["SkuId"] != null) && (dt.Rows[i]["SkuId"].ToString() != ""))
                    {
                        item.SkuId = long.Parse(dt.Rows[i]["SkuId"].ToString());
                    }
                    if ((dt.Rows[i]["AttributeId"] != null) && (dt.Rows[i]["AttributeId"].ToString() != ""))
                    {
                        item.AttributeId = long.Parse(dt.Rows[i]["AttributeId"].ToString());
                    }
                    if ((dt.Rows[i]["ValueId"] != null) && (dt.Rows[i]["ValueId"].ToString() != ""))
                    {
                        item.ValueId = long.Parse(dt.Rows[i]["ValueId"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long SkuId, long AttributeId, long ValueId)
        {
            return this.dal.Delete(SkuId, AttributeId, ValueId);
        }

        public bool Exists(long SkuId, long AttributeId, long ValueId)
        {
            return this.dal.Exists(SkuId, AttributeId, ValueId);
        }

        public bool Exists(long? SkuId, long? AttributeId, long? ValueId)
        {
            return this.dal.Exists(SkuId, AttributeId, ValueId);
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

        public Maticsoft.Model.Shop.Products.SKUItem GetModel(long SkuId, long AttributeId, long ValueId)
        {
            return this.dal.GetModel(SkuId, AttributeId, ValueId);
        }

        public Maticsoft.Model.Shop.Products.SKUItem GetModelByCache(long SkuId, long AttributeId, long ValueId)
        {
            string cacheKey = string.Concat(new object[] { "SKUItemsModel-", SkuId, AttributeId, ValueId });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SkuId, AttributeId, ValueId);
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
            return (Maticsoft.Model.Shop.Products.SKUItem) cache;
        }

        public List<Maticsoft.Model.Shop.Products.SKUItem> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.Shop.Products.SKUItem> GetSKUItemsByProductId(long productId)
        {
            DataSet set = this.dal.GetSKUItem4AttrValByProductId(productId);
            return this.SKUItem4AVDataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.SKUItem> GetSKUItemsBySkuId(long skuId)
        {
            DataSet set = this.dal.GetSKUItem4AttrValBySkuId(skuId);
            return this.SKUItem4AVDataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.SKUItem> SKUItem4AVDataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.SKUItem> list = new List<Maticsoft.Model.Shop.Products.SKUItem>();
            if (dt.Rows.Count >= 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Maticsoft.Model.Shop.Products.SKUItem item = new Maticsoft.Model.Shop.Products.SKUItem {
                        SkuId = row.Field<long>("SkuId"),
                        SpecId = row.Field<long>("SpecId"),
                        AttributeId = row.Field<long>("AttributeId"),
                        ValueId = row.Field<long>("ValueId"),
                        ImageUrl = row.Field<string>("ImageUrl"),
                        ValueStr = row.Field<string>("ValueStr"),
                        ProductId = row.Field<long>("ProductId"),
                        AttributeName = row.Field<string>("AttributeName"),
                        AB_DisplaySequence = row.Field<int>("AB_DisplaySequence"),
                        UsageMode = row.Field<int>("UsageMode"),
                        UseAttributeImage = row.Field<bool>("UseAttributeImage"),
                        UserDefinedPic = row.Field<bool>("UserDefinedPic"),
                        AV_DisplaySequence = row.Field<int>("AV_DisplaySequence"),
                        AV_ValueStr = row.Field<string>("AV_ValueStr"),
                        AV_ImageUrl = row.Field<string>("AV_ImageUrl")
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Update(Maticsoft.Model.Shop.Products.SKUItem model)
        {
            return this.dal.Update(model);
        }
    }
}

