namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductAccessorie
    {
        private readonly IProductAccessorie dal = DAShopProducts.CreateProductAccessorie();

        public bool Add(Maticsoft.Model.Shop.Products.ProductAccessorie model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductAccessorie> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductAccessorie> list = new List<Maticsoft.Model.Shop.Products.ProductAccessorie>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductAccessorie item = new Maticsoft.Model.Shop.Products.ProductAccessorie();
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["AccessoriesValueId"] != null) && (dt.Rows[i]["AccessoriesValueId"].ToString() != ""))
                    {
                        item.AccessoriesValueId = int.Parse(dt.Rows[i]["AccessoriesValueId"].ToString());
                    }
                    if ((dt.Rows[i]["AccessoriesName"] != null) && (dt.Rows[i]["AccessoriesName"].ToString() != ""))
                    {
                        item.AccessoriesName = dt.Rows[i]["AccessoriesName"].ToString();
                    }
                    if ((dt.Rows[i]["MaxQuantity"] != null) && (dt.Rows[i]["MaxQuantity"].ToString() != ""))
                    {
                        item.MaxQuantity = new int?(int.Parse(dt.Rows[i]["MaxQuantity"].ToString()));
                    }
                    if ((dt.Rows[i]["MinQuantity"] != null) && (dt.Rows[i]["MinQuantity"].ToString() != ""))
                    {
                        item.MinQuantity = new int?(int.Parse(dt.Rows[i]["MinQuantity"].ToString()));
                    }
                    if ((dt.Rows[i]["DiscountType"] != null) && (dt.Rows[i]["DiscountType"].ToString() != ""))
                    {
                        item.DiscountType = new int?(int.Parse(dt.Rows[i]["DiscountType"].ToString()));
                    }
                    if ((dt.Rows[i]["DiscountAmount"] != null) && (dt.Rows[i]["DiscountAmount"].ToString() != ""))
                    {
                        item.DiscountAmount = new decimal?(decimal.Parse(dt.Rows[i]["DiscountAmount"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long ProductId, int AccessoriesValueId)
        {
            return this.dal.Delete(ProductId, AccessoriesValueId);
        }

        public bool Exists(long ProductId, int AccessoriesValueId)
        {
            return this.dal.Exists(ProductId, AccessoriesValueId);
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

        public Maticsoft.Model.Shop.Products.ProductAccessorie GetModel(long ProductId, int AccessoriesValueId)
        {
            return this.dal.GetModel(ProductId, AccessoriesValueId);
        }

        public Maticsoft.Model.Shop.Products.ProductAccessorie GetModelByCache(long ProductId, int AccessoriesValueId)
        {
            string cacheKey = "ProductAccessoriesModel-" + ProductId + AccessoriesValueId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ProductId, AccessoriesValueId);
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
            return (Maticsoft.Model.Shop.Products.ProductAccessorie) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductAccessorie> GetModelList(long productId)
        {
            DataSet list = this.dal.GetList(string.Format(" ProductId={0}", productId));
            if ((list != null) && (list.Tables.Count > 0))
            {
                return this.DataTableToList(list.Tables[0]);
            }
            return null;
        }

        public List<Maticsoft.Model.Shop.Products.ProductAccessorie> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductAccessorie model)
        {
            return this.dal.Update(model);
        }
    }
}

