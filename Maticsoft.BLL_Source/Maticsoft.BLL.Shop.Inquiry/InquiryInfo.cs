namespace Maticsoft.BLL.Shop.Inquiry
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Inquiry;
    using Maticsoft.Model.Shop.Inquiry;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class InquiryInfo
    {
        private readonly IInquiryInfo dal = DAShopInquiry.CreateInquiryInfo();

        public long Add(Maticsoft.Model.Shop.Inquiry.InquiryInfo model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Inquiry.InquiryInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Inquiry.InquiryInfo> list = new List<Maticsoft.Model.Shop.Inquiry.InquiryInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Inquiry.InquiryInfo item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long InquiryId)
        {
            return this.dal.Delete(InquiryId);
        }

        public bool DeleteEx(long InquiryId)
        {
            return this.dal.DeleteEx(InquiryId);
        }

        public bool DeleteList(string InquiryIdlist)
        {
            return this.dal.DeleteList(InquiryIdlist);
        }

        public bool Exists(long InquiryId)
        {
            return this.dal.Exists(InquiryId);
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

        public Maticsoft.Model.Shop.Inquiry.InquiryInfo GetModel(long InquiryId)
        {
            return this.dal.GetModel(InquiryId);
        }

        public Maticsoft.Model.Shop.Inquiry.InquiryInfo GetModelByCache(long InquiryId)
        {
            string cacheKey = "InquiryModel-" + InquiryId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(InquiryId);
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
            return (Maticsoft.Model.Shop.Inquiry.InquiryInfo) cache;
        }

        public List<Maticsoft.Model.Shop.Inquiry.InquiryInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Inquiry.InquiryInfo model)
        {
            return this.dal.Update(model);
        }
    }
}

