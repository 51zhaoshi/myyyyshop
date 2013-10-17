namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductTypeBrand
    {
        private readonly IProductTypeBrand dal = DAShopProducts.CreateProductTypeBrand();

        public bool Add(Maticsoft.Model.Shop.Products.ProductTypeBrand model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductTypeBrand> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductTypeBrand> list = new List<Maticsoft.Model.Shop.Products.ProductTypeBrand>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductTypeBrand item = new Maticsoft.Model.Shop.Products.ProductTypeBrand();
                    if ((dt.Rows[i]["ProductTypeId"] != null) && (dt.Rows[i]["ProductTypeId"].ToString() != ""))
                    {
                        item.ProductTypeId = int.Parse(dt.Rows[i]["ProductTypeId"].ToString());
                    }
                    if ((dt.Rows[i]["BrandId"] != null) && (dt.Rows[i]["BrandId"].ToString() != ""))
                    {
                        item.BrandId = int.Parse(dt.Rows[i]["BrandId"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int? ProductTypeId, int? BrandId)
        {
            return this.dal.Delete(ProductTypeId, BrandId);
        }

        public bool Delete(int ProductTypeId, int BrandId)
        {
            return this.dal.Delete(new int?(ProductTypeId), new int?(BrandId));
        }

        public bool Exists(int ProductTypeId, int BrandId)
        {
            return this.dal.Exists(ProductTypeId, BrandId);
        }

        public bool ExistsBrands(int BrandId)
        {
            return this.dal.ExistsBrands(BrandId);
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

        public Maticsoft.Model.Shop.Products.ProductTypeBrand GetModel(int ProductTypeId, int BrandId)
        {
            return this.dal.GetModel(ProductTypeId, BrandId);
        }

        public Maticsoft.Model.Shop.Products.ProductTypeBrand GetModelByCache(int ProductTypeId, int BrandId)
        {
            string cacheKey = "ProductTypeBrandsModel-" + ProductTypeId + BrandId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ProductTypeId, BrandId);
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
            return (Maticsoft.Model.Shop.Products.ProductTypeBrand) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductTypeBrand> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductTypeBrand model)
        {
            return this.dal.Update(model);
        }
    }
}

