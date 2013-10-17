namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductConsults
    {
        private readonly IProductConsults dal = DAShopProducts.CreateProductConsults();

        public int Add(Maticsoft.Model.Shop.Products.ProductConsults model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductConsults> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductConsults> list = new List<Maticsoft.Model.Shop.Products.ProductConsults>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductConsults item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ConsultationId)
        {
            return this.dal.Delete(ConsultationId);
        }

        public bool DeleteList(string ConsultationIdlist)
        {
            return this.dal.DeleteList(ConsultationIdlist);
        }

        public bool Exists(int ConsultationId)
        {
            return this.dal.Exists(ConsultationId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.Shop.Products.ProductConsults> GetConsultationsByPage(long productId, string orderBy, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetListByPage("Status=1 and ProductId=" + productId, orderBy, startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
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

        public Maticsoft.Model.Shop.Products.ProductConsults GetModel(int ConsultationId)
        {
            return this.dal.GetModel(ConsultationId);
        }

        public Maticsoft.Model.Shop.Products.ProductConsults GetModelByCache(int ConsultationId)
        {
            string cacheKey = "ProductConsultsModel-" + ConsultationId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ConsultationId);
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
            return (Maticsoft.Model.Shop.Products.ProductConsults) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductConsults> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductConsults model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStatusList(string ids, int status)
        {
            return this.dal.UpdateStatusList(ids, status);
        }
    }
}

