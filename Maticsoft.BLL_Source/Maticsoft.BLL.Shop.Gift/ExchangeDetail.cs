namespace Maticsoft.BLL.Shop.Gift
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Gift;
    using Maticsoft.Model.Shop.Gift;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class ExchangeDetail
    {
        private readonly IExchangeDetail dal = DAShopGifts.CreateExchangeDetail();

        public int Add(Maticsoft.Model.Shop.Gift.ExchangeDetail model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Gift.ExchangeDetail> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Gift.ExchangeDetail> list = new List<Maticsoft.Model.Shop.Gift.ExchangeDetail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Gift.ExchangeDetail item = new Maticsoft.Model.Shop.Gift.ExchangeDetail();
                    if ((dt.Rows[i]["ExchangeDetailID"] != null) && (dt.Rows[i]["ExchangeDetailID"].ToString() != ""))
                    {
                        item.ExchangeDetailID = int.Parse(dt.Rows[i]["ExchangeDetailID"].ToString());
                    }
                    if ((dt.Rows[i]["GiftID"] != null) && (dt.Rows[i]["GiftID"].ToString() != ""))
                    {
                        item.GiftID = int.Parse(dt.Rows[i]["GiftID"].ToString());
                    }
                    if ((dt.Rows[i]["UserID"] != null) && (dt.Rows[i]["UserID"].ToString() != ""))
                    {
                        item.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
                    }
                    if ((dt.Rows[i]["OrderID"] != null) && (dt.Rows[i]["OrderID"].ToString() != ""))
                    {
                        item.OrderID = new int?(int.Parse(dt.Rows[i]["OrderID"].ToString()));
                    }
                    if ((dt.Rows[i]["GiftName"] != null) && (dt.Rows[i]["GiftName"].ToString() != ""))
                    {
                        item.GiftName = dt.Rows[i]["GiftName"].ToString();
                    }
                    if ((dt.Rows[i]["CostScore"] != null) && (dt.Rows[i]["CostScore"].ToString() != ""))
                    {
                        item.CostScore = int.Parse(dt.Rows[i]["CostScore"].ToString());
                    }
                    if ((dt.Rows[i]["Status"] != null) && (dt.Rows[i]["Status"].ToString() != ""))
                    {
                        item.Status = int.Parse(dt.Rows[i]["Status"].ToString());
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ExchangeDetailID)
        {
            return this.dal.Delete(ExchangeDetailID);
        }

        public bool DeleteList(string ExchangeDetailIDlist)
        {
            return this.dal.DeleteList(ExchangeDetailIDlist);
        }

        public bool Exists(int ExchangeDetailID)
        {
            return this.dal.Exists(ExchangeDetailID);
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

        public DataSet GetListEX(int type, string keyword)
        {
            StringBuilder builder = new StringBuilder();
            if (type != -1)
            {
                builder.AppendFormat("Status={0}", type);
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    builder.AppendFormat("and GiftName like '%{0}%' or Description like '%{0}%'", keyword);
                }
            }
            else if (!string.IsNullOrWhiteSpace(keyword))
            {
                builder.AppendFormat("GiftName like '%{0}%' or Description like '%{0}%'", keyword);
            }
            else
            {
                return this.GetAllList();
            }
            return this.dal.GetList(builder.ToString());
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Gift.ExchangeDetail GetModel(int ExchangeDetailID)
        {
            return this.dal.GetModel(ExchangeDetailID);
        }

        public Maticsoft.Model.Shop.Gift.ExchangeDetail GetModelByCache(int ExchangeDetailID)
        {
            string cacheKey = "ExchangeDetailModel-" + ExchangeDetailID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ExchangeDetailID);
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
            return (Maticsoft.Model.Shop.Gift.ExchangeDetail) cache;
        }

        public List<Maticsoft.Model.Shop.Gift.ExchangeDetail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool SetStatus(int detailId, int status)
        {
            return this.dal.SetStatus(detailId, status);
        }

        public bool SetStatusList(string detailIds, int status)
        {
            return this.dal.SetStatusList(detailIds, status);
        }

        public bool Update(Maticsoft.Model.Shop.Gift.ExchangeDetail model)
        {
            return this.dal.Update(model);
        }
    }
}

