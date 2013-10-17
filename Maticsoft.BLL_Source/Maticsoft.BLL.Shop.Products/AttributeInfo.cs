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

    public class AttributeInfo
    {
        private readonly IAttributeInfo dal = DAShopProducts.CreateAttributeInfo();

        public long Add(Maticsoft.Model.Shop.Products.AttributeInfo model)
        {
            return this.dal.Add(model);
        }

        public List<AttributeHelper> AttributeDataTableToList(DataTable dt)
        {
            List<AttributeHelper> list = new List<AttributeHelper>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    AttributeHelper item = new AttributeHelper();
                    if ((dt.Rows[i]["ValueId"] != null) && (dt.Rows[i]["ValueId"].ToString() != ""))
                    {
                        item.ValueId = int.Parse(dt.Rows[i]["ValueId"].ToString());
                    }
                    if ((dt.Rows[i]["AttributeId"] != null) && (dt.Rows[i]["AttributeId"].ToString() != ""))
                    {
                        item.AttributeId = long.Parse(dt.Rows[i]["AttributeId"].ToString());
                    }
                    if ((dt.Rows[i]["AttributeName"] != null) && (dt.Rows[i]["AttributeName"].ToString() != ""))
                    {
                        item.AttributeName = dt.Rows[i]["AttributeName"].ToString();
                    }
                    if ((dt.Rows[i]["TypeId"] != null) && (dt.Rows[i]["TypeId"].ToString() != ""))
                    {
                        item.TypeId = int.Parse(dt.Rows[i]["TypeId"].ToString());
                    }
                    if ((dt.Rows[i]["UsageMode"] != null) && (dt.Rows[i]["UsageMode"].ToString() != ""))
                    {
                        item.UsageMode = int.Parse(dt.Rows[i]["UsageMode"].ToString());
                    }
                    if ((dt.Rows[i]["UseAttributeImage"] != null) && (dt.Rows[i]["UseAttributeImage"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["UseAttributeImage"].ToString() == "1") || (dt.Rows[i]["UseAttributeImage"].ToString().ToLower() == "true"))
                        {
                            item.UseAttributeImage = true;
                        }
                        else
                        {
                            item.UseAttributeImage = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool AttributeManage(Maticsoft.Model.Shop.Products.AttributeInfo model, DataProviderAction Action)
        {
            return this.dal.AttributeManage(model, Action);
        }

        public bool ChangeImageStatue(long AttributeId, ProductAttributeModel status)
        {
            return this.dal.ChangeImageStatue(AttributeId, status);
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.AttributeInfo> list = new List<Maticsoft.Model.Shop.Products.AttributeInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.AttributeInfo item = new Maticsoft.Model.Shop.Products.AttributeInfo();
                    if ((dt.Rows[i]["AttributeId"] != null) && (dt.Rows[i]["AttributeId"].ToString() != ""))
                    {
                        item.AttributeId = long.Parse(dt.Rows[i]["AttributeId"].ToString());
                    }
                    if ((dt.Rows[i]["AttributeName"] != null) && (dt.Rows[i]["AttributeName"].ToString() != ""))
                    {
                        item.AttributeName = dt.Rows[i]["AttributeName"].ToString();
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["TypeId"] != null) && (dt.Rows[i]["TypeId"].ToString() != ""))
                    {
                        item.TypeId = int.Parse(dt.Rows[i]["TypeId"].ToString());
                    }
                    if ((dt.Rows[i]["UsageMode"] != null) && (dt.Rows[i]["UsageMode"].ToString() != ""))
                    {
                        item.UsageMode = int.Parse(dt.Rows[i]["UsageMode"].ToString());
                    }
                    if ((dt.Rows[i]["UseAttributeImage"] != null) && (dt.Rows[i]["UseAttributeImage"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["UseAttributeImage"].ToString() == "1") || (dt.Rows[i]["UseAttributeImage"].ToString().ToLower() == "true"))
                        {
                            item.UseAttributeImage = true;
                        }
                        else
                        {
                            item.UseAttributeImage = false;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long AttributeId)
        {
            return this.dal.Delete(AttributeId);
        }

        public bool DeleteList(string AttributeIdlist)
        {
            return this.dal.DeleteList(AttributeIdlist);
        }

        public bool Exists(long AttributeId)
        {
            return this.dal.Exists(AttributeId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetAttributeInfoList(int? typeId, SearchType searchType)
        {
            return this.dal.GetAttributeInfoList(typeId, searchType);
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetAttributeInfoListByProductId(long productId)
        {
            return this.dal.GetAttributeInfoListByProductId(productId);
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetAttributeListByCateID(int cateID, bool isChild = false)
        {
            DataSet attributesByCate = this.dal.GetAttributesByCate(cateID, isChild);
            if ((attributesByCate != null) && (attributesByCate.Tables.Count > 0))
            {
                return this.DataTableToList(attributesByCate.Tables[0]);
            }
            return null;
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(long? Typeid, SearchType searchType)
        {
            return this.dal.GetList(Typeid, searchType);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.Shop.Products.AttributeInfo GetModel(long AttributeId)
        {
            return this.dal.GetModel(AttributeId);
        }

        public Maticsoft.Model.Shop.Products.AttributeInfo GetModelByCache(long AttributeId)
        {
            string cacheKey = "AttributesModel-" + AttributeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AttributeId);
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
            return (Maticsoft.Model.Shop.Products.AttributeInfo) cache;
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetModelList(SearchType searchType)
        {
            switch (searchType)
            {
                case SearchType.ExtAttribute:
                    return this.GetModelList("UsageMode <>3");

                case SearchType.Specification:
                    return this.GetModelList("UsageMode =3");
            }
            return null;
        }

        public List<Maticsoft.Model.Shop.Products.AttributeInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool IsExistDefinedAttribute(int typeId, long? attId)
        {
            return this.dal.IsExistDefinedAttribute(typeId, attId);
        }

        public bool IsExistName(int typeId, string name)
        {
            return this.dal.IsExistName(typeId, name);
        }

        public List<AttributeHelper> ProductAttributeInfo(long productId)
        {
            DataSet productAttributes = this.dal.GetProductAttributes(productId);
            if ((productAttributes != null) && (productAttributes.Tables.Count > 0))
            {
                return this.AttributeDataTableToList(productAttributes.Tables[0]);
            }
            return null;
        }

        public bool Update(Maticsoft.Model.Shop.Products.AttributeInfo model)
        {
            return this.dal.Update(model);
        }
    }
}

