namespace Maticsoft.BLL.Shop.Supplier
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SuppProductCategories
    {
        private readonly ISuppProductCategories dal = DAShopSupplier.CreateSuppProductCategories();

        public bool Add(Maticsoft.Model.Shop.Supplier.SuppProductCategories model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Supplier.SuppProductCategories> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Supplier.SuppProductCategories> list = new List<Maticsoft.Model.Shop.Supplier.SuppProductCategories>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Supplier.SuppProductCategories item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int CategoryId, long ProductId)
        {
            return this.dal.Delete(CategoryId, ProductId);
        }

        public bool Exists(int CategoryId, long ProductId)
        {
            return this.dal.Exists(CategoryId, ProductId);
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

        public Maticsoft.Model.Shop.Supplier.SuppProductCategories GetModel(int CategoryId, long ProductId)
        {
            return this.dal.GetModel(CategoryId, ProductId);
        }

        public Maticsoft.Model.Shop.Supplier.SuppProductCategories GetModelByCache(int CategoryId, long ProductId)
        {
            string cacheKey = "SuppProductCategoriesModel-" + CategoryId + ProductId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CategoryId, ProductId);
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Shop.Supplier.SuppProductCategories) cache;
        }

        public List<Maticsoft.Model.Shop.Supplier.SuppProductCategories> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Supplier.SuppProductCategories model)
        {
            return this.dal.Update(model);
        }
    }
}

