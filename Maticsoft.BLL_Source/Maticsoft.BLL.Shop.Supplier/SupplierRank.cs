namespace Maticsoft.BLL.Shop.Supplier
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SupplierRank
    {
        private readonly ISupplierRank dal = DAShopSupplier.CreateSupplierRank();

        public int Add(Maticsoft.Model.Shop.Supplier.SupplierRank model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierRank> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Supplier.SupplierRank> list = new List<Maticsoft.Model.Shop.Supplier.SupplierRank>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Supplier.SupplierRank item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int RankId)
        {
            return this.dal.Delete(RankId);
        }

        public bool DeleteList(string RankIdlist)
        {
            return this.dal.DeleteList(RankIdlist);
        }

        public bool Exists(int RankId)
        {
            return this.dal.Exists(RankId);
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

        public Maticsoft.Model.Shop.Supplier.SupplierRank GetModel(int RankId)
        {
            return this.dal.GetModel(RankId);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierRank GetModelByCache(int RankId)
        {
            string cacheKey = "SupplierRankModel-" + RankId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RankId);
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
            return (Maticsoft.Model.Shop.Supplier.SupplierRank) cache;
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierRank> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierRank model)
        {
            return this.dal.Update(model);
        }
    }
}

