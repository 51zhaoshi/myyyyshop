namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Feedback
    {
        private readonly IFeedback dal = DAMembers.CreateFeedback();

        public int Add(Maticsoft.Model.Members.Feedback model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.Feedback> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.Feedback> list = new List<Maticsoft.Model.Members.Feedback>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.Feedback item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int FeedbackId)
        {
            return this.dal.Delete(FeedbackId);
        }

        public bool DeleteList(string FeedbackIdlist)
        {
            return this.dal.DeleteList(FeedbackIdlist);
        }

        public bool Exists(int FeedbackId)
        {
            return this.dal.Exists(FeedbackId);
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

        public Maticsoft.Model.Members.Feedback GetModel(int FeedbackId)
        {
            return this.dal.GetModel(FeedbackId);
        }

        public Maticsoft.Model.Members.Feedback GetModelByCache(int FeedbackId)
        {
            string cacheKey = "FeedbackModel-" + FeedbackId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FeedbackId);
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
            return (Maticsoft.Model.Members.Feedback) cache;
        }

        public List<Maticsoft.Model.Members.Feedback> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Members.Feedback model)
        {
            return this.dal.Update(model);
        }
    }
}

