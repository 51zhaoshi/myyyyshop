namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class AttributeValue
    {
        private readonly IAttributeValue dal = DAShopProducts.CreateAttributeValue();

        public long Add(Maticsoft.Model.Shop.Products.AttributeValue model)
        {
            return this.dal.Add(model);
        }

        public bool AttributeValueManage(Maticsoft.Model.Shop.Products.AttributeValue model, DataProviderAction Action)
        {
            return this.dal.AttributeValueManage(model, Action);
        }

        public List<Maticsoft.Model.Shop.Products.AttributeValue> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.AttributeValue> list = new List<Maticsoft.Model.Shop.Products.AttributeValue>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Products.AttributeValue item = new Maticsoft.Model.Shop.Products.AttributeValue();
                    if ((dt.Rows[i]["ValueId"] != null) && (dt.Rows[i]["ValueId"].ToString() != ""))
                    {
                        item.ValueId = long.Parse(dt.Rows[i]["ValueId"].ToString());
                    }
                    if ((dt.Rows[i]["AttributeId"] != null) && (dt.Rows[i]["AttributeId"].ToString() != ""))
                    {
                        item.AttributeId = long.Parse(dt.Rows[i]["AttributeId"].ToString());
                    }
                    if ((dt.Rows[i]["DisplaySequence"] != null) && (dt.Rows[i]["DisplaySequence"].ToString() != ""))
                    {
                        item.DisplaySequence = int.Parse(dt.Rows[i]["DisplaySequence"].ToString());
                    }
                    if ((dt.Rows[i]["ValueStr"] != null) && (dt.Rows[i]["ValueStr"].ToString() != ""))
                    {
                        item.ValueStr = dt.Rows[i]["ValueStr"].ToString();
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(long ValueId)
        {
            return this.dal.Delete(ValueId);
        }

        public bool DeleteImage(long valueId)
        {
            return this.dal.DeleteImage(valueId);
        }

        public bool DeleteList(string ValueIdlist)
        {
            return this.dal.DeleteList(ValueIdlist);
        }

        public bool Exists(long ValueId)
        {
            return this.dal.Exists(ValueId);
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

        public DataSet GetListByAttribute(long AttributeId)
        {
            return this.dal.GetListByAttribute(AttributeId);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.Shop.Products.AttributeValue GetModel(long ValueId)
        {
            return this.dal.GetModel(ValueId);
        }

        public Maticsoft.Model.Shop.Products.AttributeValue GetModelByCache(long ValueId)
        {
            string cacheKey = "AttributeValuesModel-" + ValueId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ValueId);
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
            return (Maticsoft.Model.Shop.Products.AttributeValue) cache;
        }

        public List<Maticsoft.Model.Shop.Products.AttributeValue> GetModelList(long? AttributeId)
        {
            DataSet list = this.dal.GetList(AttributeId);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.AttributeValue> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.Products.AttributeValue> GetModelListByCateID(int? cateId)
        {
            DataSet attributeValue = this.dal.GetAttributeValue(cateId);
            if ((attributeValue != null) && (attributeValue.Tables.Count > 0))
            {
                return this.DataTableToList(attributeValue.Tables[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Products.AttributeValue model)
        {
            return this.dal.Update(model);
        }
    }
}

