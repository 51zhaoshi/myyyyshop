namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class GroupTags
    {
        private readonly IGroupTags dal = DASNS.CreateGroupTags();

        public int Add(Maticsoft.Model.SNS.GroupTags model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.GroupTags> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GroupTags> list = new List<Maticsoft.Model.SNS.GroupTags>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.GroupTags item = new Maticsoft.Model.SNS.GroupTags();
                    if ((dt.Rows[i]["TagID"] != null) && (dt.Rows[i]["TagID"].ToString() != ""))
                    {
                        item.TagID = int.Parse(dt.Rows[i]["TagID"].ToString());
                    }
                    if ((dt.Rows[i]["TagName"] != null) && (dt.Rows[i]["TagName"].ToString() != ""))
                    {
                        item.TagName = dt.Rows[i]["TagName"].ToString();
                    }
                    if ((dt.Rows[i]["IsRecommand"] != null) && (dt.Rows[i]["IsRecommand"].ToString() != ""))
                    {
                        item.IsRecommand = int.Parse(dt.Rows[i]["IsRecommand"].ToString());
                    }
                    if ((dt.Rows[i]["Status"] != null) && (dt.Rows[i]["Status"].ToString() != ""))
                    {
                        item.Status = int.Parse(dt.Rows[i]["Status"].ToString());
                    }
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

        public Maticsoft.Model.SNS.GroupTags GetModel(int TagID)
        {
            return this.dal.GetModel(TagID);
        }

        public Maticsoft.Model.SNS.GroupTags GetModelByCache(int TagID)
        {
            string cacheKey = "GroupTagsModel-" + TagID;
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
            return (Maticsoft.Model.SNS.GroupTags) cache;
        }

        public List<Maticsoft.Model.SNS.GroupTags> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string Keywords)
        {
            return this.dal.GetList(0, string.Format("TagName like '%{0}%'", Keywords), "");
        }

        public bool Update(Maticsoft.Model.SNS.GroupTags model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            return this.dal.UpdateIsRecommand(IsRecommand, IdList);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            return this.dal.UpdateStatus(Status, IdList);
        }
    }
}

