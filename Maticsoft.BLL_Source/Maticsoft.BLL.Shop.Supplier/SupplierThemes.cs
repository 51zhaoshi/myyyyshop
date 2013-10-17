namespace Maticsoft.BLL.Shop.Supplier
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SupplierThemes
    {
        private readonly ISupplierThemes dal = DAShopSupplier.CreateSupplierThemes();

        public int Add(Maticsoft.Model.Shop.Supplier.SupplierThemes model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierThemes> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Supplier.SupplierThemes> list = new List<Maticsoft.Model.Shop.Supplier.SupplierThemes>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Supplier.SupplierThemes item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ThemeId)
        {
            return this.dal.Delete(ThemeId);
        }

        public bool DeleteList(string ThemeIdlist)
        {
            return this.dal.DeleteList(ThemeIdlist);
        }

        public bool Exists(int ThemeId)
        {
            return this.dal.Exists(ThemeId);
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

        public Maticsoft.Model.Shop.Supplier.SupplierThemes GetModel(int ThemeId)
        {
            return this.dal.GetModel(ThemeId);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierThemes GetModelByCache(int ThemeId)
        {
            string cacheKey = "SupplierThemesModel-" + ThemeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ThemeId);
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
            return (Maticsoft.Model.Shop.Supplier.SupplierThemes) cache;
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierThemes> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierThemes model)
        {
            return this.dal.Update(model);
        }
    }
}

