namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class VideoClass
    {
        private readonly IVideoClass dal = DACMS.CreateVideoClass();

        public int Add(Maticsoft.Model.CMS.VideoClass model)
        {
            return this.dal.Add(model);
        }

        public int AddEx(Maticsoft.Model.CMS.VideoClass model)
        {
            return this.dal.AddEx(model);
        }

        public List<Maticsoft.Model.CMS.VideoClass> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.VideoClass> list = new List<Maticsoft.Model.CMS.VideoClass>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.VideoClass item = new Maticsoft.Model.CMS.VideoClass();
                    if ((dt.Rows[i]["VideoClassID"] != null) && (dt.Rows[i]["VideoClassID"].ToString() != ""))
                    {
                        item.VideoClassID = int.Parse(dt.Rows[i]["VideoClassID"].ToString());
                    }
                    if ((dt.Rows[i]["VideoClassName"] != null) && (dt.Rows[i]["VideoClassName"].ToString() != ""))
                    {
                        item.VideoClassName = dt.Rows[i]["VideoClassName"].ToString();
                    }
                    if ((dt.Rows[i]["ParentID"] != null) && (dt.Rows[i]["ParentID"].ToString() != ""))
                    {
                        item.ParentID = new int?(int.Parse(dt.Rows[i]["ParentID"].ToString()));
                    }
                    if ((dt.Rows[i]["Sequence"] != null) && (dt.Rows[i]["Sequence"].ToString() != ""))
                    {
                        item.Sequence = int.Parse(dt.Rows[i]["Sequence"].ToString());
                    }
                    if ((dt.Rows[i]["Path"] != null) && (dt.Rows[i]["Path"].ToString() != ""))
                    {
                        item.Path = dt.Rows[i]["Path"].ToString();
                    }
                    if ((dt.Rows[i]["Depth"] != null) && (dt.Rows[i]["Depth"].ToString() != ""))
                    {
                        item.Depth = int.Parse(dt.Rows[i]["Depth"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int VideoClassID)
        {
            return this.dal.Delete(VideoClassID);
        }

        public int DeleteEx(int VideoClassID)
        {
            return this.dal.DeleteEx(VideoClassID);
        }

        public bool DeleteList(string VideoClassIDlist)
        {
            return this.dal.DeleteList(VideoClassIDlist);
        }

        public bool Exists(int VideoClassID)
        {
            return this.dal.Exists(VideoClassID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.CMS.VideoClass> GetCategorysByDepth(int depth)
        {
            return this.GetModelList("Depth = " + depth);
        }

        public DataSet GetCategorysByParentIdDs(int parentCategoryId)
        {
            return this.GetModelDs("ParentID = " + parentCategoryId);
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

        public DataSet GetListEx(string strWhere, string orderby)
        {
            return this.dal.GetListEx(strWhere, orderby);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public int GetMaxSequence()
        {
            return this.dal.GetMaxSequence();
        }

        public Maticsoft.Model.CMS.VideoClass GetModel(int VideoClassID)
        {
            return this.dal.GetModel(VideoClassID);
        }

        public Maticsoft.Model.CMS.VideoClass GetModelByCache(int VideoClassID)
        {
            string cacheKey = "VideoClassModel-" + VideoClassID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(VideoClassID);
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
            return (Maticsoft.Model.CMS.VideoClass) cache;
        }

        public Maticsoft.Model.CMS.VideoClass GetModelByParentID(int ParentID)
        {
            return this.dal.GetModelByParentID(ParentID);
        }

        public DataSet GetModelDs(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public List<Maticsoft.Model.CMS.VideoClass> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int SwapCategorySequence(int VideoClassId, SwapSequenceIndex zIndex)
        {
            return this.dal.SwapCategorySequence(VideoClassId, zIndex);
        }

        public bool Update(Maticsoft.Model.CMS.VideoClass model)
        {
            return this.dal.Update(model);
        }
    }
}

