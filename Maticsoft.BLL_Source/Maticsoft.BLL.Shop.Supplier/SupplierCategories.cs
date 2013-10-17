namespace Maticsoft.BLL.Shop.Supplier
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SupplierCategories
    {
        private readonly ISupplierCategories dal = DAShopSupplier.CreateSupplierCategories();

        public int Add(Maticsoft.Model.Shop.Supplier.SupplierCategories model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> list = new List<Maticsoft.Model.Shop.Supplier.SupplierCategories>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Supplier.SupplierCategories item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int CategoryId)
        {
            return this.dal.Delete(CategoryId);
        }

        public bool DeleteList(string CategoryIdlist)
        {
            return this.dal.DeleteList(CategoryIdlist);
        }

        public bool Exists(int CategoryId)
        {
            return this.dal.Exists(CategoryId);
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

        public Maticsoft.Model.Shop.Supplier.SupplierCategories GetModel(int CategoryId)
        {
            return this.dal.GetModel(CategoryId);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierCategories GetModelByCache(int CategoryId)
        {
            string cacheKey = "SupplierCategoriesModel-" + CategoryId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CategoryId);
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
            return (Maticsoft.Model.Shop.Supplier.SupplierCategories) cache;
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierCategories> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierCategories model)
        {
            return this.dal.Update(model);
        }
    }
}

