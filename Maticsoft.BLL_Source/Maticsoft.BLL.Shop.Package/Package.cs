namespace Maticsoft.BLL.Shop.Package
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Package;
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Package
    {
        private readonly IPackage dal = DAShopPackage.CreatePackage();

        public int Add(Maticsoft.Model.Shop.Package.Package model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Package.Package> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Package.Package> list = new List<Maticsoft.Model.Shop.Package.Package>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Package.Package item = this.dal.DataRowToModel(dt.Rows[i]);
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int PackageId)
        {
            return this.dal.Delete(PackageId);
        }

        public bool DeleteList(string PackageIdlist)
        {
            return this.dal.DeleteList(PackageIdlist);
        }

        public bool Exists(int PackageId)
        {
            return this.dal.Exists(PackageId);
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

        public DataSet GetListEx(string strWhere)
        {
            return this.dal.GetListEx(strWhere);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Package.Package GetModel(int PackageId)
        {
            return this.dal.GetModel(PackageId);
        }

        public Maticsoft.Model.Shop.Package.Package GetModelByCache(int PackageId)
        {
            string cacheKey = "PackageModel-" + PackageId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(PackageId);
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
            return (Maticsoft.Model.Shop.Package.Package) cache;
        }

        public List<Maticsoft.Model.Shop.Package.Package> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Package.Package model)
        {
            return this.dal.Update(model);
        }
    }
}

