namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BrandInfo
    {
        private readonly IBrandInfo dal = DAShopProducts.CreateBrandInfo();

        public int Add(Maticsoft.Model.Shop.Products.BrandInfo model)
        {
            return this.dal.Add(model);
        }

        public bool CreateBrandsAndTypes(Maticsoft.Model.Shop.Products.BrandInfo model, DataProviderAction action)
        {
            return this.dal.CreateBrandsAndTypes(model, action);
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> list = new List<Maticsoft.Model.Shop.Products.BrandInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.BrandInfo item = new Maticsoft.Model.Shop.Products.BrandInfo();
                    if ((dt.Rows[i]["BrandId"] != null) && (dt.Rows[i]["BrandId"].ToString() != ""))
                    {
                        item.BrandId = int.Parse(dt.Rows[i]["BrandId"].ToString());
                    }
                    if ((dt.Rows[i]["BrandName"] != null) && (dt.Rows[i]["BrandName"].ToString() != ""))
                    {
                        item.BrandName = dt.Rows[i]["BrandName"].ToString();
                    }
                    if ((dt.Rows[i]["BrandSpell"] != null) && (dt.Rows[i]["BrandSpell"].ToString() != ""))
                    {
                        item.BrandSpell = dt.Rows[i]["BrandSpell"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Description"] != null) && (dt.Rows[i]["Meta_Description"].ToString() != ""))
                    {
                        item.Meta_Description = dt.Rows[i]["Meta_Description"].ToString();
                    }
                    if ((dt.Rows[i]["Meta_Keywords"] != null) && (dt.Rows[i]["Meta_Keywords"].ToString() != ""))
                    {
                        item.Meta_Keywords = dt.Rows[i]["Meta_Keywords"].ToString();
                    }
                    if ((dt.Rows[i]["Logo"] != null) && (dt.Rows[i]["Logo"].ToString() != ""))
                    {
                        item.Logo = dt.Rows[i]["Logo"].ToString();
                    }
                    if ((dt.Rows[i]["CompanyUrl"] != null) && (dt.Rows[i]["CompanyUrl"].ToString() != ""))
                    {
                        item.CompanyUrl = dt.Rows[i]["CompanyUrl"].ToString();
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["Theme"] != null) && (dt.Rows[i]["Theme"].ToString() != ""))
                    {
                        item.Theme = dt.Rows[i]["Theme"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int BrandId)
        {
            return this.dal.Delete(BrandId);
        }

        public bool DeleteList(string BrandIdlist)
        {
            return this.dal.DeleteList(BrandIdlist);
        }

        public bool Exists(int BrandId)
        {
            return this.dal.Exists(BrandId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetBrandList(string strWhere, int Top = -1)
        {
            DataSet set = this.dal.GetList(Top, strWhere, " DisplaySequence");
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetBrands()
        {
            DataSet list = this.dal.GetList("");
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetBrandsByCateId(int cateId, bool IsChild = false, int Top = -1)
        {
            DataSet set = this.dal.GetBrandsByCateId(cateId, IsChild, Top);
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetBrandsModelListByCateId(int? cateId)
        {
            DataSet brandsListByCateId = this.dal.GetBrandsListByCateId(cateId);
            return this.DataTableToList(brandsListByCateId.Tables[0]);
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

        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetListByProductTypeId(out int rowCount, out int pageCount, int ProductTypeId, int PageIndex, int PageSize, int action)
        {
            DataSet set = this.dal.GetListByProductTypeId(out rowCount, out pageCount, ProductTypeId, PageIndex, PageSize, action);
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetMaxDisplaySequence()
        {
            return this.dal.GetMaxDisplaySequence();
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Products.BrandInfo GetModel(int BrandId)
        {
            return this.dal.GetModel(BrandId);
        }

        public Maticsoft.Model.Shop.Products.BrandInfo GetModelByCache(int BrandId)
        {
            string cacheKey = "BrandsModel-" + BrandId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(BrandId);
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
            return (Maticsoft.Model.Shop.Products.BrandInfo) cache;
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetModelListByProductTypeId(int ProductTypeId, int top = -1)
        {
            DataSet listByProductTypeId = this.dal.GetListByProductTypeId(ProductTypeId, top);
            return this.DataTableToList(listByProductTypeId.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public Maticsoft.Model.Shop.Products.BrandInfo GetRelatedProduct(int brandsId)
        {
            return this.dal.GetRelatedProduct(brandsId);
        }

        public Maticsoft.Model.Shop.Products.BrandInfo GetRelatedProduct(int? brandsId, int? ProductTypeId)
        {
            return this.dal.GetRelatedProduct(brandsId, ProductTypeId);
        }

        public bool Update(Maticsoft.Model.Shop.Products.BrandInfo model)
        {
            return this.dal.Update(model);
        }
    }
}

