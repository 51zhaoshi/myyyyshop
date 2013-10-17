namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.SQLServerDAL.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PicAd
    {
        private readonly Maticsoft.SQLServerDAL.SNS.PicAd dal = new Maticsoft.SQLServerDAL.SNS.PicAd();

        public int Add(Maticsoft.Model.SNS.PicAd model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.PicAd> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.PicAd> list = new List<Maticsoft.Model.SNS.PicAd>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.PicAd item = new Maticsoft.Model.SNS.PicAd();
                    if ((dt.Rows[i]["Id"] != null) && (dt.Rows[i]["Id"].ToString() != ""))
                    {
                        item.Id = int.Parse(dt.Rows[i]["Id"].ToString());
                    }
                    if ((dt.Rows[i]["Name"] != null) && (dt.Rows[i]["Name"].ToString() != ""))
                    {
                        item.Name = dt.Rows[i]["Name"].ToString();
                    }
                    if ((dt.Rows[i]["Src"] != null) && (dt.Rows[i]["Src"].ToString() != ""))
                    {
                        item.Src = dt.Rows[i]["Src"].ToString();
                    }
                    if ((dt.Rows[i]["Href"] != null) && (dt.Rows[i]["Href"].ToString() != ""))
                    {
                        item.Href = dt.Rows[i]["Href"].ToString();
                    }
                    if ((dt.Rows[i]["Title"] != null) && (dt.Rows[i]["Title"].ToString() != ""))
                    {
                        item.Title = dt.Rows[i]["Title"].ToString();
                    }
                    if ((dt.Rows[i]["Alt"] != null) && (dt.Rows[i]["Alt"].ToString() != ""))
                    {
                        item.Alt = dt.Rows[i]["Alt"].ToString();
                    }
                    if ((dt.Rows[i]["IsShow"] != null) && (dt.Rows[i]["IsShow"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsShow"].ToString() == "1") || (dt.Rows[i]["IsShow"].ToString().ToLower() == "true"))
                        {
                            item.IsShow = true;
                        }
                        else
                        {
                            item.IsShow = false;
                        }
                    }
                    if ((dt.Rows[i]["Orders"] != null) && (dt.Rows[i]["Orders"].ToString() != ""))
                    {
                        item.Orders = int.Parse(dt.Rows[i]["Orders"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int Id)
        {
            return this.dal.Delete(Id);
        }

        public bool DeleteList(string Idlist)
        {
            return this.dal.DeleteList(Idlist);
        }

        public bool Exists(int Id)
        {
            return this.dal.Exists(Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList()
        {
            return this.dal.GetList();
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

        public Maticsoft.Model.SNS.PicAd GetModel(int Id)
        {
            return this.dal.GetModel(Id);
        }

        public Maticsoft.Model.SNS.PicAd GetModelByCache(int Id)
        {
            string cacheKey = "PicAdModel-" + Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Id);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.PicAd) cache;
        }

        public List<Maticsoft.Model.SNS.PicAd> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.PicAd model)
        {
            return this.dal.Update(model);
        }
    }
}

