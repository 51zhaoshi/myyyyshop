namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PhotoTags
    {
        private readonly IPhotoTags dal = DASNS.CreatePhotoTags();

        public int Add(Maticsoft.Model.SNS.PhotoTags model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.PhotoTags> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.PhotoTags> list = new List<Maticsoft.Model.SNS.PhotoTags>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.PhotoTags item = this.dal.DataRowToModel(dt.Rows[i]);
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int TagID)
        {
            return this.dal.Delete(TagID);
        }

        public bool DeleteList(string TagIDlist)
        {
            return this.dal.DeleteList(TagIDlist);
        }

        public bool Exists(int TagID)
        {
            return this.dal.Exists(TagID);
        }

        public bool Exists(string TagName)
        {
            return this.dal.Exists(TagName);
        }

        public bool Exists(int TagID, string TagName)
        {
            return this.dal.Exists(TagID, TagName);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.PhotoTags> GetHotTags(int top)
        {
            return this.DataTableToList(this.dal.GetHotTags(top).Tables[0]);
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

        public Maticsoft.Model.SNS.PhotoTags GetModel(int TagID)
        {
            return this.dal.GetModel(TagID);
        }

        public Maticsoft.Model.SNS.PhotoTags GetModelByCache(int TagID)
        {
            string cacheKey = "PhotoTagsModel-" + TagID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TagID);
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
            return (Maticsoft.Model.SNS.PhotoTags) cache;
        }

        public List<Maticsoft.Model.SNS.PhotoTags> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.PhotoTags model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            return this.dal.UpdateStatus(Status, IdList);
        }
    }
}

