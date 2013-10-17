namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductCategories
    {
        private readonly IProductCategories dal = DAShopProducts.CreateProductCategories();

        public bool Add(Maticsoft.Model.Shop.Products.ProductCategories model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductCategories> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductCategories> list = new List<Maticsoft.Model.Shop.Products.ProductCategories>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductCategories item = new Maticsoft.Model.Shop.Products.ProductCategories();
                    if ((dt.Rows[i]["CategoryId"] != null) && (dt.Rows[i]["CategoryId"].ToString() != ""))
                    {
                        item.CategoryId = int.Parse(dt.Rows[i]["CategoryId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["CategoryPath"] != null) && (dt.Rows[i]["CategoryPath"].ToString() != ""))
                    {
                        item.CategoryPath = dt.Rows[i]["CategoryPath"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long produtId)
        {
            return this.dal.Delete(produtId);
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

        public DataSet GetListByProductId(long productId)
        {
            return this.dal.GetList(string.Format(" ProductId ={0}", productId));
        }

        public Maticsoft.Model.Shop.Products.ProductCategories GetModel(long produtId)
        {
            return this.dal.GetModel(produtId);
        }

        public Maticsoft.Model.Shop.Products.ProductCategories GetModelByCache(long produtId)
        {
            string cacheKey = "ProductCategoriesModel-";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(produtId);
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
            return (Maticsoft.Model.Shop.Products.ProductCategories) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductCategories> GetModelList(long productId)
        {
            DataSet list = this.dal.GetList(string.Format(" ProductId ={0}", productId));
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductCategories> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }
    }
}

