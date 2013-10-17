namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class AccessoriesValue
    {
        private readonly IAccessoriesValue dal = DAShopProducts.CreateAccessoriesValue();

        public List<Maticsoft.Model.Shop.Products.AccessoriesValue> AccessoriesByProductId(long productId)
        {
            DataSet set = this.dal.AccessoriesByProductId(productId);
            if ((set != null) && (set.Tables.Count > 0))
            {
                return this.DataTableToList(set.Tables[0]);
            }
            return null;
        }

        public int Add(Maticsoft.Model.Shop.Products.AccessoriesValue model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.AccessoriesValue> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.AccessoriesValue> list = new List<Maticsoft.Model.Shop.Products.AccessoriesValue>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.AccessoriesValue item = new Maticsoft.Model.Shop.Products.AccessoriesValue();
                    if ((dt.Rows[i]["AccessoriesValueId"] != null) && (dt.Rows[i]["AccessoriesValueId"].ToString() != ""))
                    {
                        item.AccessoriesValueId = int.Parse(dt.Rows[i]["AccessoriesValueId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductAccessoriesId"] != null) && (dt.Rows[i]["ProductAccessoriesId"].ToString() != ""))
                    {
                        item.ProductAccessoriesId = int.Parse(dt.Rows[i]["ProductAccessoriesId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductAccessoriesSKU"] != null) && (dt.Rows[i]["ProductAccessoriesSKU"].ToString() != ""))
                    {
                        item.ProductAccessoriesSKU = dt.Rows[i]["ProductAccessoriesSKU"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int AccessoriesValueId)
        {
            return this.dal.Delete(AccessoriesValueId);
        }

        public bool DeleteList(string AccessoriesValueIdlist)
        {
            return this.dal.DeleteList(AccessoriesValueIdlist);
        }

        public bool Exists(int AccessoriesValueId)
        {
            return this.dal.Exists(AccessoriesValueId);
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

        public Maticsoft.Model.Shop.Products.AccessoriesValue GetModel(int AccessoriesValueId)
        {
            return this.dal.GetModel(AccessoriesValueId);
        }

        public Maticsoft.Model.Shop.Products.AccessoriesValue GetModelByCache(int AccessoriesValueId)
        {
            string cacheKey = "AccessoriesValuesModel-" + AccessoriesValueId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AccessoriesValueId);
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
            return (Maticsoft.Model.Shop.Products.AccessoriesValue) cache;
        }

        public List<Maticsoft.Model.Shop.Products.AccessoriesValue> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.AccessoriesValue model)
        {
            return this.dal.Update(model);
        }
    }
}

