namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductSources
    {
        private readonly IProductSources dal = DASNS.CreateProductSources();

        public int Add(Maticsoft.Model.SNS.ProductSources model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.ProductSources> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.ProductSources> list = new List<Maticsoft.Model.SNS.ProductSources>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.ProductSources item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(string WebSiteName, string WebSiteUrl)
        {
            return this.dal.Exists(WebSiteName, WebSiteUrl);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetIdByWebSiteName(string WebSiteName)
        {
            List<Maticsoft.Model.SNS.ProductSources> modelList = this.GetModelList("WebSiteName='" + WebSiteName + "'");
            if (modelList.Count > 0)
            {
                return modelList[0].ID;
            }
            return 0;
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

        public Maticsoft.Model.SNS.ProductSources GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.ProductSources GetModelByCache(int ID)
        {
            string cacheKey = "ProductSourcesModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.ProductSources) cache;
        }

        public List<Maticsoft.Model.SNS.ProductSources> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.ProductSources model)
        {
            return this.dal.Update(model);
        }
    }
}

