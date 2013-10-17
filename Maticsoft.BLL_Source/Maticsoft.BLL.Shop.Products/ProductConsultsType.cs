namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ProductConsultsType
    {
        private readonly IProductConsultsType dal = DAShopProducts.CreateProductConsultsType();

        public int Add(Maticsoft.Model.Shop.Products.ProductConsultsType model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductConsultsType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductConsultsType> list = new List<Maticsoft.Model.Shop.Products.ProductConsultsType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductConsultsType item = new Maticsoft.Model.Shop.Products.ProductConsultsType();
                    if ((dt.Rows[i]["TypeId"] != null) && (dt.Rows[i]["TypeId"].ToString() != ""))
                    {
                        item.TypeId = int.Parse(dt.Rows[i]["TypeId"].ToString());
                    }
                    if ((dt.Rows[i]["TypeName"] != null) && (dt.Rows[i]["TypeName"].ToString() != ""))
                    {
                        item.TypeName = dt.Rows[i]["TypeName"].ToString();
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["IsActive"] != null) && (dt.Rows[i]["IsActive"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsActive"].ToString() == "1") || (dt.Rows[i]["IsActive"].ToString().ToLower() == "true"))
                        {
                            item.IsActive = true;
                        }
                        else
                        {
                            item.IsActive = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int TypeId)
        {
            return this.dal.Delete(TypeId);
        }

        public bool DeleteList(string TypeIdlist)
        {
            return this.dal.DeleteList(TypeIdlist);
        }

        public bool Exists(int TypeId)
        {
            return this.dal.Exists(TypeId);
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

        public Maticsoft.Model.Shop.Products.ProductConsultsType GetModel(int TypeId)
        {
            return this.dal.GetModel(TypeId);
        }

        public Maticsoft.Model.Shop.Products.ProductConsultsType GetModelByCache(int TypeId)
        {
            string cacheKey = "ProductConsultationsTypeModel-" + TypeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TypeId);
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
            return (Maticsoft.Model.Shop.Products.ProductConsultsType) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductConsultsType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductConsultsType model)
        {
            return this.dal.Update(model);
        }
    }
}

