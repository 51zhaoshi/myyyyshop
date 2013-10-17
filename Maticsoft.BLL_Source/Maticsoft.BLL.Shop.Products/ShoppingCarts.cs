namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ShoppingCarts
    {
        private readonly IShoppingCarts dal = DAShopProducts.CreateShoppingCarts();

        public bool Add(ShoppingCartItem model)
        {
            return this.dal.Add(model);
        }

        public List<ShoppingCartItem> DataTableToList(DataTable dt)
        {
            List<ShoppingCartItem> list = new List<ShoppingCartItem>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    ShoppingCartItem item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ItemId, int UserId)
        {
            return this.dal.Delete(ItemId, UserId);
        }

        public bool Exists(int ItemId, int UserId)
        {
            return this.dal.Exists(ItemId, UserId);
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

        public ShoppingCartItem GetModel(int ItemId, int UserId)
        {
            return this.dal.GetModel(ItemId, UserId);
        }

        public ShoppingCartItem GetModelByCache(int ItemId, int UserId)
        {
            string cacheKey = "ShoppingCartsModel-" + ItemId + UserId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ItemId, UserId);
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
            return (ShoppingCartItem) cache;
        }

        public List<ShoppingCartItem> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(ShoppingCartItem model)
        {
            return this.dal.Update(model);
        }
    }
}

