namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class LineDistributor
    {
        private readonly ILineDistributor dal = DAShopProducts.CreateLineDistributor();

        public bool Add(Maticsoft.Model.Shop.Products.LineDistributor model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.LineDistributor> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.LineDistributor> list = new List<Maticsoft.Model.Shop.Products.LineDistributor>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.LineDistributor item = new Maticsoft.Model.Shop.Products.LineDistributor();
                    if ((dt.Rows[i]["LineId"] != null) && (dt.Rows[i]["LineId"].ToString() != ""))
                    {
                        item.LineId = int.Parse(dt.Rows[i]["LineId"].ToString());
                    }
                    if ((dt.Rows[i]["DistributorId"] != null) && (dt.Rows[i]["DistributorId"].ToString() != ""))
                    {
                        item.DistributorId = int.Parse(dt.Rows[i]["DistributorId"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int LineId, int DistributorId)
        {
            return this.dal.Delete(LineId, DistributorId);
        }

        public bool Exists(int LineId, int DistributorId)
        {
            return this.dal.Exists(LineId, DistributorId);
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

        public Maticsoft.Model.Shop.Products.LineDistributor GetModel(int LineId, int DistributorId)
        {
            return this.dal.GetModel(LineId, DistributorId);
        }

        public Maticsoft.Model.Shop.Products.LineDistributor GetModelByCache(int LineId, int DistributorId)
        {
            string cacheKey = "LineDistributorsModel-" + LineId + DistributorId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(LineId, DistributorId);
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
            return (Maticsoft.Model.Shop.Products.LineDistributor) cache;
        }

        public List<Maticsoft.Model.Shop.Products.LineDistributor> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.LineDistributor model)
        {
            return this.dal.Update(model);
        }
    }
}

