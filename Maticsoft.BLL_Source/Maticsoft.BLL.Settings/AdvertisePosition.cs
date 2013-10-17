namespace Maticsoft.BLL.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class AdvertisePosition
    {
        private readonly IAdvertisePosition dal = DASettings.CreateAdvertisePosition();

        public int Add(Maticsoft.Model.Settings.AdvertisePosition model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Settings.AdvertisePosition> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.AdvertisePosition> list = new List<Maticsoft.Model.Settings.AdvertisePosition>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Settings.AdvertisePosition item = new Maticsoft.Model.Settings.AdvertisePosition();
                    if ((dt.Rows[i]["AdvPositionId"] != null) && (dt.Rows[i]["AdvPositionId"].ToString() != ""))
                    {
                        item.AdvPositionId = int.Parse(dt.Rows[i]["AdvPositionId"].ToString());
                    }
                    if ((dt.Rows[i]["AdvPositionName"] != null) && (dt.Rows[i]["AdvPositionName"].ToString() != ""))
                    {
                        item.AdvPositionName = dt.Rows[i]["AdvPositionName"].ToString();
                    }
                    if ((dt.Rows[i]["ShowType"] != null) && (dt.Rows[i]["ShowType"].ToString() != ""))
                    {
                        item.ShowType = new int?(int.Parse(dt.Rows[i]["ShowType"].ToString()));
                    }
                    if ((dt.Rows[i]["RepeatColumns"] != null) && (dt.Rows[i]["RepeatColumns"].ToString() != ""))
                    {
                        item.RepeatColumns = new int?(int.Parse(dt.Rows[i]["RepeatColumns"].ToString()));
                    }
                    if ((dt.Rows[i]["Width"] != null) && (dt.Rows[i]["Width"].ToString() != ""))
                    {
                        item.Width = new int?(int.Parse(dt.Rows[i]["Width"].ToString()));
                    }
                    if ((dt.Rows[i]["Height"] != null) && (dt.Rows[i]["Height"].ToString() != ""))
                    {
                        item.Height = new int?(int.Parse(dt.Rows[i]["Height"].ToString()));
                    }
                    if ((dt.Rows[i]["AdvHtml"] != null) && (dt.Rows[i]["AdvHtml"].ToString() != ""))
                    {
                        item.AdvHtml = dt.Rows[i]["AdvHtml"].ToString();
                    }
                    if ((dt.Rows[i]["IsOne"] != null) && (dt.Rows[i]["IsOne"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsOne"].ToString() == "1") || (dt.Rows[i]["IsOne"].ToString().ToLower() == "true"))
                        {
                            item.IsOne = true;
                        }
                        else
                        {
                            item.IsOne = false;
                        }
                    }
                    if ((dt.Rows[i]["TimeInterval"] != null) && (dt.Rows[i]["TimeInterval"].ToString() != ""))
                    {
                        item.TimeInterval = new int?(int.Parse(dt.Rows[i]["TimeInterval"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedUserID"] != null) && (dt.Rows[i]["CreatedUserID"].ToString() != ""))
                    {
                        item.CreatedUserID = new int?(int.Parse(dt.Rows[i]["CreatedUserID"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int AdvPositionId)
        {
            return this.dal.Delete(AdvPositionId);
        }

        public bool DeleteList(string AdvPositionIdlist)
        {
            return this.dal.DeleteList(AdvPositionIdlist);
        }

        public bool Exists(int AdvPositionId)
        {
            return this.dal.Exists(AdvPositionId);
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

        public Maticsoft.Model.Settings.AdvertisePosition GetModel(int AdvPositionId)
        {
            return this.dal.GetModel(AdvPositionId);
        }

        public Maticsoft.Model.Settings.AdvertisePosition GetModelByCache(int AdvPositionId)
        {
            string cacheKey = "AdvertisePositionModel-" + AdvPositionId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AdvPositionId);
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
            return (Maticsoft.Model.Settings.AdvertisePosition) cache;
        }

        public List<Maticsoft.Model.Settings.AdvertisePosition> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Settings.AdvertisePosition model)
        {
            return this.dal.Update(model);
        }
    }
}

