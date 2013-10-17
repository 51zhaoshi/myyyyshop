namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductStationMode
    {
        private readonly IProductStationMode dal = DAShopProducts.CreateProductStationMode();

        public int Add(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductStationMode> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductStationMode> list = new List<Maticsoft.Model.Shop.Products.ProductStationMode>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductStationMode item = new Maticsoft.Model.Shop.Products.ProductStationMode();
                    if ((dt.Rows[i]["StationId"] != null) && (dt.Rows[i]["StationId"].ToString() != ""))
                    {
                        item.StationId = int.Parse(dt.Rows[i]["StationId"].ToString());
                    }
                    if ((dt.Rows[i]["ProductId"] != null) && (dt.Rows[i]["ProductId"].ToString() != ""))
                    {
                        item.ProductId = long.Parse(dt.Rows[i]["ProductId"].ToString());
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["Type"] != null) && (dt.Rows[i]["Type"].ToString() != ""))
                    {
                        item.Type = int.Parse(dt.Rows[i]["Type"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int StationId)
        {
            return this.dal.Delete(StationId);
        }

        public bool Delete(int productId, int type)
        {
            return this.dal.Delete(productId, type);
        }

        public bool DeleteByType(int type, int categoryId)
        {
            return this.dal.DeleteByType(type, categoryId);
        }

        public bool DeleteList(string StationIdlist)
        {
            return this.dal.DeleteList(StationIdlist);
        }

        public bool Exists(int StationId)
        {
            return this.dal.Exists(StationId);
        }

        public bool Exists(int productId, int type)
        {
            return this.dal.Exists(productId, type);
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

        public DataSet GetListByType(string strType)
        {
            return this.dal.GetListByType(strType);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Products.ProductStationMode GetModel(int StationId)
        {
            return this.dal.GetModel(StationId);
        }

        public Maticsoft.Model.Shop.Products.ProductStationMode GetModelByCache(int StationId)
        {
            string cacheKey = "ProductStationModesModel-" + StationId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(StationId);
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
            return (Maticsoft.Model.Shop.Products.ProductStationMode) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductStationMode> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetStationMode(int modeType, int categoryId, string pName)
        {
            return this.dal.GetStationMode(modeType, categoryId, pName);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            return this.dal.Update(model);
        }
    }
}

