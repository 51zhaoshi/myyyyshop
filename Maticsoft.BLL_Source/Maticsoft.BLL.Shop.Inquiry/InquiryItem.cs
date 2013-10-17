namespace Maticsoft.BLL.Shop.Inquiry
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Inquiry;
    using Maticsoft.Model.Shop.Inquiry;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class InquiryItem
    {
        private readonly IInquiryItem dal = DAShopInquiry.CreateInquiryItem();

        public long Add(Maticsoft.Model.Shop.Inquiry.InquiryItem model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Inquiry.InquiryItem> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Inquiry.InquiryItem> list = new List<Maticsoft.Model.Shop.Inquiry.InquiryItem>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Inquiry.InquiryItem item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long ItemId)
        {
            return this.dal.Delete(ItemId);
        }

        public bool DeleteList(string ItemIdlist)
        {
            return this.dal.DeleteList(ItemIdlist);
        }

        public bool Exists(long ItemId)
        {
            return this.dal.Exists(ItemId);
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

        public Maticsoft.Model.Shop.Inquiry.InquiryItem GetModel(long ItemId)
        {
            return this.dal.GetModel(ItemId);
        }

        public Maticsoft.Model.Shop.Inquiry.InquiryItem GetModelByCache(long ItemId)
        {
            string cacheKey = "InquiryItemModel-" + ItemId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ItemId);
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Shop.Inquiry.InquiryItem) cache;
        }

        public List<Maticsoft.Model.Shop.Inquiry.InquiryItem> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Inquiry.InquiryItem model)
        {
            return this.dal.Update(model);
        }
    }
}

