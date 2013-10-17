namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PostsTopics
    {
        private readonly IPostsTopics dal = DASNS.CreatePostsTopics();

        public bool Add(Maticsoft.Model.SNS.PostsTopics model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.PostsTopics> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.PostsTopics> list = new List<Maticsoft.Model.SNS.PostsTopics>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.PostsTopics item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string Title)
        {
            return this.dal.Delete(Title);
        }

        public bool DeleteList(string Titlelist)
        {
            return this.dal.DeleteList(Titlelist);
        }

        public bool Exists(string Title)
        {
            return this.dal.Exists(Title);
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

        public Maticsoft.Model.SNS.PostsTopics GetModel(string Title)
        {
            return this.dal.GetModel(Title);
        }

        public Maticsoft.Model.SNS.PostsTopics GetModelByCache(string Title)
        {
            string cacheKey = "PostsTopicsModel-" + Title;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(Title);
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
            return (Maticsoft.Model.SNS.PostsTopics) cache;
        }

        public List<Maticsoft.Model.SNS.PostsTopics> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.PostsTopics model)
        {
            return this.dal.Update(model);
        }
    }
}

