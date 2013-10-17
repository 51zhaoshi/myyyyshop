namespace Maticsoft.BLL.Shop.Supplier
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SupplierRankThemes
    {
        private readonly ISupplierRankThemes dal = DAShopSupplier.CreateSupplierRankThemes();

        public bool Add(Maticsoft.Model.Shop.Supplier.SupplierRankThemes model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierRankThemes> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Supplier.SupplierRankThemes> list = new List<Maticsoft.Model.Shop.Supplier.SupplierRankThemes>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Supplier.SupplierRankThemes item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int RankId, int ThemeId)
        {
            return this.dal.Delete(RankId, ThemeId);
        }

        public bool Exists(int RankId, int ThemeId)
        {
            return this.dal.Exists(RankId, ThemeId);
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

        public Maticsoft.Model.Shop.Supplier.SupplierRankThemes GetModel(int RankId, int ThemeId)
        {
            return this.dal.GetModel(RankId, ThemeId);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierRankThemes GetModelByCache(int RankId, int ThemeId)
        {
            string cacheKey = "SupplierRankThemesModel-" + RankId + ThemeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RankId, ThemeId);
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
            return (Maticsoft.Model.Shop.Supplier.SupplierRankThemes) cache;
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierRankThemes> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierRankThemes model)
        {
            return this.dal.Update(model);
        }
    }
}

