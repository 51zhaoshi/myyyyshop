namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class ProductType
    {
        private readonly IProductType dal = DAShopProducts.CreateProductType();

        public int Add(Maticsoft.Model.Shop.Products.ProductType model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Products.ProductType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductType> list = new List<Maticsoft.Model.Shop.Products.ProductType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.ProductType item = new Maticsoft.Model.Shop.Products.ProductType();
                    if ((dt.Rows[i]["TypeId"] != null) && (dt.Rows[i]["TypeId"].ToString() != ""))
                    {
                        item.TypeId = int.Parse(dt.Rows[i]["TypeId"].ToString());
                    }
                    if ((dt.Rows[i]["TypeName"] != null) && (dt.Rows[i]["TypeName"].ToString() != ""))
                    {
                        item.TypeName = dt.Rows[i]["TypeName"].ToString();
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
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

        public bool DeleteManage(int? TypeId, long? AttributeId, long? ValueId)
        {
            return this.dal.DeleteManage(TypeId, AttributeId, ValueId);
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

        public Maticsoft.Model.Shop.Products.ProductType GetModel(int TypeId)
        {
            return this.dal.GetModel(TypeId);
        }

        public Maticsoft.Model.Shop.Products.ProductType GetModelByCache(int TypeId)
        {
            string cacheKey = "ProductTypesModel-" + TypeId;
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
            return (Maticsoft.Model.Shop.Products.ProductType) cache;
        }

        public List<Maticsoft.Model.Shop.Products.ProductType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.ProductType> GetProductTypes()
        {
            return this.dal.GetProductTypes();
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool ProductTypeManage(Maticsoft.Model.Shop.Products.ProductType model, DataProviderAction Action, out int Typeid)
        {
            return this.dal.ProductTypeManage(model, Action, out Typeid);
        }

        public bool SwapSeqManage(int? TypeId, long? AttributeId, long? ValueId, SwapSequenceIndex zIndex, bool UsageMode)
        {
            return this.dal.SwapSeqManage(TypeId, AttributeId, ValueId, zIndex, UsageMode);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductType model)
        {
            return this.dal.Update(model);
        }
    }
}

