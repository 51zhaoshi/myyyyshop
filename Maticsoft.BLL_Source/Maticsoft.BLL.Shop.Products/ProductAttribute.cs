namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductAttribute
    {
        private readonly IProductAttribute dal = DAShopProducts.CreateProductAttribute();

        public bool Add(Maticsoft.Model.Shop.Products.ProductAttribute model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductAttribute> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductAttribute> list = new List<Maticsoft.Model.Shop.Products.ProductAttribute>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductAttribute item = new Maticsoft.Model.Shop.Products.ProductAttribute();
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["AttributeId"] != null) && (dt.Rows[i]["AttributeId"].ToString() != ""))
                    {
                        item.AttributeId = long.Parse(dt.Rows[i]["AttributeId"].ToString());
                    }
                    if ((dt.Rows[i]["ValueId"] != null) && (dt.Rows[i]["ValueId"].ToString() != ""))
                    {
                        item.ValueId = int.Parse(dt.Rows[i]["ValueId"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long ProductId, long AttributeId, int ValueId)
        {
            return this.dal.Delete(ProductId, AttributeId, ValueId);
        }

        public bool Exists(long ProductId, long AttributeId, int ValueId)
        {
            return this.dal.Exists(ProductId, AttributeId, ValueId);
        }

        public bool Exists(long? ProductId, long? AttributeId, long? ValueId)
        {
            return this.dal.Exists(ProductId, AttributeId, ValueId);
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

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Products.ProductAttribute GetModel(long ProductId, long AttributeId, int ValueId)
        {
            return this.dal.GetModel(ProductId, AttributeId, ValueId);
        }

        public Maticsoft.Model.Shop.Products.ProductAttribute GetModelByCache(long ProductId, long AttributeId, int ValueId)
        {
            string cacheKey = string.Concat(new object[] { "ProductAttributesModel-", ProductId, AttributeId, ValueId });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ProductId, AttributeId, ValueId);
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
            return (Maticsoft.Model.Shop.Products.ProductAttribute) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductAttribute> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductAttribute model)
        {
            return this.dal.Update(model);
        }
    }
}

