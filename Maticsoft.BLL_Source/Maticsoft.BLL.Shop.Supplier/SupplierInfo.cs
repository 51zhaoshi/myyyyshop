namespace Maticsoft.BLL.Shop.Supplier
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SupplierInfo
    {
        private readonly ISupplierInfo dal = DAShopSupplier.CreateSupplierInfo();

        public int Add(Maticsoft.Model.Shop.Supplier.SupplierInfo model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Supplier.SupplierInfo> list = new List<Maticsoft.Model.Shop.Supplier.SupplierInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Supplier.SupplierInfo item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int SupplierId)
        {
            return this.dal.Delete(SupplierId);
        }

        public bool DeleteList(string SupplierIdlist)
        {
            return this.dal.DeleteList(SupplierIdlist);
        }

        public bool Exists(int SupplierId)
        {
            return this.dal.Exists(SupplierId);
        }

        public bool Exists(string Name)
        {
            return this.dal.Exists(Name);
        }

        public bool Exists(string Name, int id)
        {
            return this.dal.Exists(Name, id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetEnteName(string name, int iCount)
        {
            string strWhere = "Name like '" + name + "%' AND Status=1 ";
            return this.dal.GetList(iCount, strWhere, "Name");
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

        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModel(int SupplierId)
        {
            return this.dal.GetModel(SupplierId);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModelByCache(int SupplierId)
        {
            string cacheKey = "SuppliersModel-" + SupplierId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SupplierId);
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
            return (Maticsoft.Model.Shop.Supplier.SupplierInfo) cache;
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> GetModelBySupplierName(string name)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                strWhere = "Name = '" + name + "'";
            }
            return this.GetModelList(strWhere);
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetStatisticsSales(int supplierId, int year)
        {
            return this.dal.GetStatisticsSales(supplierId, year);
        }

        public DataSet GetStatisticsSupply(int supplierId)
        {
            return this.dal.GetStatisticsSupply(supplierId);
        }

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierInfo model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            return this.dal.UpdateList(IDlist, strWhere);
        }
    }
}

