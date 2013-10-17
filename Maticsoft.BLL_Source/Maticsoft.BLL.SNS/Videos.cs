namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Videos
    {
        private readonly IVideos dal = DASNS.CreateVideos();

        public int Add(Maticsoft.Model.SNS.Videos model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.Videos> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Videos> list = new List<Maticsoft.Model.SNS.Videos>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Videos item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int VideoID)
        {
            return this.dal.Delete(VideoID);
        }

        public bool DeleteList(string VideoIDlist)
        {
            return this.dal.DeleteList(VideoIDlist);
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

        public Maticsoft.Model.SNS.Videos GetModel(int VideoID)
        {
            return this.dal.GetModel(VideoID);
        }

        public Maticsoft.Model.SNS.Videos GetModelByCache(int VideoID)
        {
            string cacheKey = "VideosModel-" + VideoID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(VideoID);
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
            return (Maticsoft.Model.SNS.Videos) cache;
        }

        public List<Maticsoft.Model.SNS.Videos> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.Videos model)
        {
            return this.dal.Update(model);
        }
    }
}

