namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Distributor
    {
        private readonly IDistributor dal = DAShopProducts.CreateDistributor();

        public int Add(Maticsoft.Model.Shop.Products.Distributor model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.Distributor> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.Distributor> list = new List<Maticsoft.Model.Shop.Products.Distributor>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.Distributor item = new Maticsoft.Model.Shop.Products.Distributor();
                    if ((dt.Rows[i]["DistributorId"] != null) && (dt.Rows[i]["DistributorId"].ToString() != ""))
                    {
                        item.DistributorId = int.Parse(dt.Rows[i]["DistributorId"].ToString());
                    }
                    if ((dt.Rows[i]["DistributorName"] != null) && (dt.Rows[i]["DistributorName"].ToString() != ""))
                    {
                        item.DistributorName = dt.Rows[i]["DistributorName"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int DistributorId)
        {
            return this.dal.Delete(DistributorId);
        }

        public bool DeleteList(string DistributorIdlist)
        {
            return this.dal.DeleteList(DistributorIdlist);
        }

        public bool Exists(int DistributorId)
        {
            return this.dal.Exists(DistributorId);
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

        public Maticsoft.Model.Shop.Products.Distributor GetModel(int DistributorId)
        {
            return this.dal.GetModel(DistributorId);
        }

        public Maticsoft.Model.Shop.Products.Distributor GetModelByCache(int DistributorId)
        {
            string cacheKey = "DistributorsModel-" + DistributorId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(DistributorId);
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
            return (Maticsoft.Model.Shop.Products.Distributor) cache;
        }

        public List<Maticsoft.Model.Shop.Products.Distributor> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.Distributor model)
        {
            return this.dal.Update(model);
        }
    }
}

