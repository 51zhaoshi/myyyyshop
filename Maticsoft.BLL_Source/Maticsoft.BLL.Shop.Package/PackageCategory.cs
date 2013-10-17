namespace Maticsoft.BLL.Shop.Package
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Package;
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PackageCategory
    {
        private readonly IPackageCategory dal = DAShopPackage.CreatePackageCategory();

        public int Add(Maticsoft.Model.Shop.Package.PackageCategory model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Package.PackageCategory> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Package.PackageCategory> list = new List<Maticsoft.Model.Shop.Package.PackageCategory>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Package.PackageCategory item = this.dal.DataRowToModel(dt.Rows[i]);
                    list.Add(item);
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

        public Maticsoft.Model.Shop.Package.PackageCategory GetModel(int CategoryId)
        {
            return this.dal.GetModel(CategoryId);
        }

        public Maticsoft.Model.Shop.Package.PackageCategory GetModelByCache(int CategoryId)
        {
            string cacheKey = "PackageCategoryModel-" + CategoryId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CategoryId);
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
            return (Maticsoft.Model.Shop.Package.PackageCategory) cache;
        }

        public List<Maticsoft.Model.Shop.Package.PackageCategory> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Package.PackageCategory model)
        {
            return this.dal.Update(model);
        }
    }
}

