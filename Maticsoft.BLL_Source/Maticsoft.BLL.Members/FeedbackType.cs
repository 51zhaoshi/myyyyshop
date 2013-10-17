namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class FeedbackType
    {
        private readonly IFeedbackType dal = DAMembers.CreateFeedbackType();

        public int Add(Maticsoft.Model.Members.FeedbackType model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.FeedbackType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.FeedbackType> list = new List<Maticsoft.Model.Members.FeedbackType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.FeedbackType item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int TypeId)
        {
            return this.dal.Delete(TypeId);
        }

        public bool DeleteList(string TypeIdlist)
        {
            return this.dal.DeleteList(TypeIdlist);
        }

        public bool Exists(int TypeId)
        {
            return this.dal.Exists(TypeId);
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

        public Maticsoft.Model.Members.FeedbackType GetModel(int TypeId)
        {
            return this.dal.GetModel(TypeId);
        }

        public Maticsoft.Model.Members.FeedbackType GetModelByCache(int TypeId)
        {
            string cacheKey = "FeedbackTypeModel-" + TypeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TypeId);
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
            return (Maticsoft.Model.Members.FeedbackType) cache;
        }

        public List<Maticsoft.Model.Members.FeedbackType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Members.FeedbackType model)
        {
            return this.dal.Update(model);
        }
    }
}

