namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductLine
    {
        private readonly IProductLine dal = DAShopProducts.CreateProductLine();

        public int Add(Maticsoft.Model.Shop.Products.ProductLine model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductLine> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductLine> list = new List<Maticsoft.Model.Shop.Products.ProductLine>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductLine item = new Maticsoft.Model.Shop.Products.ProductLine();
                    if ((dt.Rows[i]["LineId"] != null) && (dt.Rows[i]["LineId"].ToString() != ""))
                    {
                        item.LineId = int.Parse(dt.Rows[i]["LineId"].ToString());
                    }
                    if ((dt.Rows[i]["LineName"] != null) && (dt.Rows[i]["LineName"].ToString() != ""))
                    {
                        item.LineName = dt.Rows[i]["LineName"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int LineId)
        {
            return this.dal.Delete(LineId);
        }

        public bool DeleteList(string LineIdlist)
        {
            return this.dal.DeleteList(LineIdlist);
        }

        public bool Exists(int LineId)
        {
            return this.dal.Exists(LineId);
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

        public Maticsoft.Model.Shop.Products.ProductLine GetModel(int LineId)
        {
            return this.dal.GetModel(LineId);
        }

        public Maticsoft.Model.Shop.Products.ProductLine GetModelByCache(int LineId)
        {
            string cacheKey = "ProductLinesModel-" + LineId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(LineId);
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
            return (Maticsoft.Model.Shop.Products.ProductLine) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductLine> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductLine model)
        {
            return this.dal.Update(model);
        }
    }
}

