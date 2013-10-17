namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class RelatedProduct
    {
        private readonly IRelatedProduct dal = DAShopProducts.CreateRelatedProduct();

        public bool Add(Maticsoft.Model.Shop.Products.RelatedProduct model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.RelatedProduct> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.RelatedProduct> list = new List<Maticsoft.Model.Shop.Products.RelatedProduct>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.RelatedProduct item = new Maticsoft.Model.Shop.Products.RelatedProduct();
                    if ((dt.Rows[i]["RelatedId"] != null) && (dt.Rows[i]["RelatedId"].ToString() != ""))
                    {
                        item.RelatedId = long.Parse(dt.Rows[i]["RelatedId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int RelatedId, long ProductId)
        {
            return this.dal.Delete(RelatedId, ProductId);
        }

        public bool Exists(int RelatedId, long ProductId)
        {
            return this.dal.Exists(RelatedId, ProductId);
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

        public Maticsoft.Model.Shop.Products.RelatedProduct GetModel(int RelatedId, long ProductId)
        {
            return this.dal.GetModel(RelatedId, ProductId);
        }

        public Maticsoft.Model.Shop.Products.RelatedProduct GetModelByCache(int RelatedId, long ProductId)
        {
            string cacheKey = "RelatedProductsModel-" + RelatedId + ProductId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RelatedId, ProductId);
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
            return (Maticsoft.Model.Shop.Products.RelatedProduct) cache;
        }

        public List<Maticsoft.Model.Shop.Products.RelatedProduct> GetModelList(long productId)
        {
            DataSet list = this.dal.GetList(string.Format(" ProductId={0}", productId));
            if ((list != null) && (list.Tables.Count > 0))
            {
                return this.DataTableToList(list.Tables[0]);
            }
            return null;
        }

        public List<Maticsoft.Model.Shop.Products.RelatedProduct> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet IsDoubleRelated(long productId)
        {
            return this.dal.IsDoubleRelated(productId);
        }

        public bool Update(Maticsoft.Model.Shop.Products.RelatedProduct model)
        {
            return this.dal.Update(model);
        }
    }
}

