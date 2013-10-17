namespace Maticsoft.BLL.Shop.Package
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Package;
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductPackage
    {
        private readonly IProductPackage dal = DAShopPackage.CreateProductPackage();

        public bool Add(Maticsoft.Model.Shop.Package.ProductPackage model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Package.ProductPackage> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Package.ProductPackage> list = new List<Maticsoft.Model.Shop.Package.ProductPackage>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Package.ProductPackage item = this.dal.DataRowToModel(dt.Rows[i]);
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long ProductId, int PackageId)
        {
            return this.dal.Delete(ProductId, PackageId);
        }

        public bool Exists(long ProductId, int PackageId)
        {
            return this.dal.Exists(ProductId, PackageId);
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

        public Maticsoft.Model.Shop.Package.ProductPackage GetModel(long ProductId, int PackageId)
        {
            return this.dal.GetModel(ProductId, PackageId);
        }

        public Maticsoft.Model.Shop.Package.ProductPackage GetModelByCache(long ProductId, int PackageId)
        {
            string cacheKey = "ProductPackageModel-" + ProductId + PackageId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ProductId, PackageId);
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
            return (Maticsoft.Model.Shop.Package.ProductPackage) cache;
        }

        public List<Maticsoft.Model.Shop.Package.ProductPackage> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Package.ProductPackage model)
        {
            return this.dal.Update(model);
        }
    }
}

